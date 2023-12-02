/*
    Basic dependency injection without using .NET dependency injection
 
 */

// <Sample>
ShoppingCart cart = new();
cart.AddItem(new("Producto 1", 30.0));
cart.AddItem(new("Producto 2", 50.0));

IPaymentMethod paymentMethod = new GooglePayPayment(); // or PayPalPayment

Purchase purchase = new(cart, paymentMethod);
purchase.Checkout();
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