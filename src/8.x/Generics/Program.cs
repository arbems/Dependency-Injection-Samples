/*
    Dependency Injection (DI) in .NET using generics

 */

using Microsoft.Extensions.DependencyInjection;

ServiceCollection services = new();

services
    .AddScoped<ShoppingCart>()
    .AddScoped<GooglePayPayment>()
    .AddScoped<PayPalPayment>()
    .AddScoped(typeof(Purchase<>))
    .AddScoped(typeof(IPaymentMethod<>), typeof(PaymentMethod<>));

ServiceProvider serviceProvider = services.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    Item item1 = new("Producto 1", 30.0);
    Item item2 = new("Producto 2", 50.0);

    ShoppingCart cart = scope.ServiceProvider.GetRequiredService<ShoppingCart>();
    cart.AddItem(item1);
    cart.AddItem(item2);

    var purchase = scope.ServiceProvider.GetRequiredService<Purchase<GooglePayPayment>>();
    purchase.Checkout();
}

class Purchase<TPaymentMethod>(ShoppingCart cart, IPaymentMethod<TPaymentMethod> paymentMethod)
{
    private ShoppingCart Cart { get; } = cart;
    private IPaymentMethod<TPaymentMethod> PaymentMethod { get; } = paymentMethod;

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

interface IPaymentMethod<TPaymentMethod>
{
    void ProcessPayment(double amount);
}

class PaymentMethod<TPaymentMethod>(IPaymentMethod<TPaymentMethod> paymentMethod)
    : IPaymentMethod<TPaymentMethod>
{
    private readonly IPaymentMethod<TPaymentMethod> _paymentMethod = paymentMethod;

    public void ProcessPayment(double amount)
    {
        _paymentMethod.ProcessPayment(amount);
    }
}

class GooglePayPayment : IPaymentMethod<GooglePayPayment>
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Pago realizado con Google Pay: ${amount}");
    }
}

class PayPalPayment : IPaymentMethod<PayPalPayment>
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Pago realizado con PayPal: ${amount}");
    }
}