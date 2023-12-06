/*
    Keyed services

    .NET also supports service registrations and lookups based on a key.

 */


// <Sample>
using Microsoft.Extensions.DependencyInjection;

ServiceCollection services = new();

ServiceProvider serviceProvider = services
    .AddKeyedScoped<IPaymentMethod, GooglePayPayment>("GooglePay")
    .AddKeyedScoped<IPaymentMethod, PayPalPayment>("PayPal")
    .AddScoped<ShoppingCart>()
    .AddScoped<Purchase>()
    .BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    ShoppingCart cart = scope.ServiceProvider.GetRequiredService<ShoppingCart>();
    cart.AddItem(new("Producto 1", 30.0));
    cart.AddItem(new("Producto 2", 50.0));

    var purchase = scope.ServiceProvider.GetRequiredService<Purchase>();
    purchase.Checkout();
}
// </Sample>


// <Models>
class Purchase(ShoppingCart cart, 
    [FromKeyedServices("GooglePay")] IPaymentMethod paymentMethod) // or PayPal
{
    private ShoppingCart Cart { get; } = cart;
    private IPaymentMethod PaymentMethod { get; } = paymentMethod;

    public void Checkout()
    {
        double totalAmount = Cart.CalculateTotal();
        PaymentMethod.ProcessPayment(totalAmount);
        Console.WriteLine("Compra realizada con éxito.");
    }
}

class Item(string name, double price)
{
    public string Name { get; set; } = name;
    public double Price { get; set; } = price;
}

class ShoppingCart()
{
    public List<Item> Items { get; } = [];

    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    public double CalculateTotal()
    {
        return Items.Sum(item => item.Price);
    }
}

interface IPaymentMethod
{
    void ProcessPayment(double amount);
}

class GooglePayPayment : IPaymentMethod
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Pago realizado con Google Pay: ${amount}");
    }
}

class PayPalPayment : IPaymentMethod
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Pago realizado con PayPal: ${amount}");
    }
}
// </Models>