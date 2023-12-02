/*
    Options using .NET dependency injection    
 
 */

// <Sample>
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

ServiceCollection services = new();    

services.AddScoped<IPaymentMethod>(sp =>
{
    var options = sp.GetRequiredService<IOptions<GooglePayOptions>>().Value;

    return new GooglePayPayment(options);
})
    .AddScoped<ShoppingCart>()
    .AddScoped<Purchase>();

services.Configure<GooglePayOptions>(options =>
{
    options.MerchantId = "1";
    options.CurrencyCode = "123";
    options.Amount = 30.0;
});

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

class GooglePayPayment(GooglePayOptions options) : IPaymentMethod
{
    private readonly GooglePayOptions _options = options;

    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Pago realizado con Google Pay: ${amount}. MerchantId: {_options.MerchantId}. CurrencyCode: {_options.CurrencyCode}. Amount: {_options.Amount}");
    }
}

class PayPalPayment : IPaymentMethod
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Pago realizado con PayPal: ${amount}");
    }
}

class GooglePayOptions
{
    // Properties to store payment details
    public string MerchantId { get; set; }
    public string CurrencyCode { get; set; }
    public double Amount { get; set; }
}
// </Models>