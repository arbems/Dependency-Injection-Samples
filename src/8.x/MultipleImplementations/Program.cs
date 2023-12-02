/*
    Multiple implementations: register multiple service instances of the same service type
 
 */

// <Sample>
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

ServiceCollection services = new();

services.AddScoped<IPaymentMethod, GooglePayPayment>()
    .AddScoped<IPaymentMethod, PayPalPayment>()
    .AddScoped<ShoppingCart>()
    .AddScoped<Purchase>();

ServiceProvider serviceProvider = services.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    Item item1 = new("Producto 1", 30.0);
    Item item2 = new("Producto 2", 50.0);

    ShoppingCart cart = scope.ServiceProvider.GetRequiredService<ShoppingCart>();
    cart.AddItem(item1);
    cart.AddItem(item2);

    var purchase = scope.ServiceProvider.GetRequiredService<Purchase>();
    purchase.Checkout();
}
// </Sample>


// <Models>
class Purchase(ShoppingCart cart, IPaymentMethod paymentMethod, IEnumerable<IPaymentMethod> paymentMethods)
{
    private ShoppingCart Cart { get; } = cart;
    private IPaymentMethod PaymentMethod { get; } = paymentMethod;
    private IEnumerable<IPaymentMethod> PaymentMethods { get; } = paymentMethods;

    public void Checkout()
    {
        Trace.Assert(paymentMethod is PayPalPayment);

        var dependencyArray = paymentMethods.ToArray();
        Trace.Assert(dependencyArray[0] is GooglePayPayment);
        Trace.Assert(dependencyArray[1] is PayPalPayment);

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