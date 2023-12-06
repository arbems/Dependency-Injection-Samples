/*
    Constructor injection behavior. Services can be resolved by using:

        * IServiceProvider
        * ActivatorUtilities:
            - Creates objects that aren't registered in the container.
            - Used with some framework features.
 
 */

// <Sample>
using Microsoft.Extensions.DependencyInjection;

ServiceCollection services = new();

services.AddScoped<ShoppingCart>();

ServiceProvider serviceProvider = services.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    ShoppingCart cart = scope.ServiceProvider.GetRequiredService<ShoppingCart>();
    cart.AddItem(new("Producto 1", 30.0));
    cart.AddItem(new("Producto 2", 50.0));

    GooglePayPayment googlePayPayment = ActivatorUtilities.CreateInstance<GooglePayPayment>(serviceProvider);
    Purchase purchase = ActivatorUtilities.CreateInstance<Purchase>(serviceProvider, cart, googlePayPayment);
    purchase.Checkout();

    // or

    PayPalPayment payPalPayment = ActivatorUtilities.CreateInstance<PayPalPayment>(serviceProvider);
    purchase = ActivatorUtilities.CreateInstance<Purchase>(serviceProvider, cart, payPalPayment);
    purchase.Checkout();
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