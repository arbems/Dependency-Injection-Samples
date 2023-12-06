/*
    Multiple constructor discovery rules

 
 */

// <Sample>
using Microsoft.Extensions.DependencyInjection;

ServiceCollection services = new();

services.AddScoped<IPaymentMethod, GooglePayPayment>()
    .AddScoped<ShoppingCart>()
    .AddScoped<Purchase>();

ServiceProvider serviceProvider = services.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    ShoppingCart cart = scope.ServiceProvider.GetRequiredService<ShoppingCart>();
    cart.AddItem(new("Producto 1", 30.0));
    cart.AddItem(new("Producto 2", 50.0));

    GooglePayPayment googlePayPayment = ActivatorUtilities.CreateInstance<GooglePayPayment>(serviceProvider);

    Purchase purchase1 = ActivatorUtilities.CreateInstance<Purchase>(serviceProvider, cart, googlePayPayment);
    purchase1.Checkout();

    Purchase purchase2 = ActivatorUtilities.CreateInstance<Purchase>(serviceProvider, googlePayPayment);
    purchase2.Checkout();

    Purchase purchase3 = ActivatorUtilities.CreateInstance<Purchase>(serviceProvider);
    purchase3.Checkout();
}
// </Sample>


// <Models>
class Purchase
{
    private ShoppingCart Cart { get; }
    private IPaymentMethod PaymentMethod { get; }

    public Purchase()
    {
        Cart = new ShoppingCart();
        PaymentMethod = new GooglePayPayment();
    }

    public Purchase(IPaymentMethod paymentMethod)
    {
        Cart = new ShoppingCart();
        PaymentMethod = paymentMethod;
    }

    public Purchase(ShoppingCart cart)
    {
        Cart = cart;
        PaymentMethod = new GooglePayPayment();
    }

    public Purchase(ShoppingCart cart, IPaymentMethod paymentMethod)
    {
        Cart = cart;
        PaymentMethod = paymentMethod;
    }

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