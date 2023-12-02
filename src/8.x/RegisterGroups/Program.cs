/*
    Register groups of services with extension methods

    Microsoft Extensions uses a convention for registering a group of related services. 
    The convention is to use a single Add{GROUP_NAME} extension method to register all 
    of the services required by a framework feature.
 
 */

// <Sample>
using Microsoft.Extensions.DependencyInjection;

ServiceCollection services = new();

ServiceProvider serviceProvider = services.AddCustomServices()
    .BuildServiceProvider();

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

static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IPaymentMethod, GooglePayPayment>()  // or PayPalPayment
            .AddScoped<ShoppingCart>()
            .AddScoped<Purchase>();

        return services;
    }
}
// </Sample>


// <Models>
class Purchase(ShoppingCart cart, IPaymentMethod paymentMethod)
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