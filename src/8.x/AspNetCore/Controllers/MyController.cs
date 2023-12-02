using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreDISample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyController : ControllerBase
    {
        private readonly ILogger<MyController> _logger;
        private readonly IServiceProvider _serviceProvider;

        public MyController(ILogger<MyController> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        [HttpGet]
        public ActionResult Get()
        {
            ShoppingCart cart = _serviceProvider.GetRequiredService<ShoppingCart>();
            cart.AddItem(new("Producto 1", 30.0));
            cart.AddItem(new("Producto 2", 50.0));

            PayPalPayment payPalPayment = ActivatorUtilities.CreateInstance<PayPalPayment>(_serviceProvider);
            GooglePayPayment googlePayPayment = ActivatorUtilities.CreateInstance<GooglePayPayment>(_serviceProvider);

            Purchase purchase = ActivatorUtilities.CreateInstance<Purchase>(_serviceProvider, cart, googlePayPayment);
            
            return Ok(purchase.Checkout());
        }

    }
    
    class Purchase(ShoppingCart cart, IPaymentMethod paymentMethod)
    {
        private ShoppingCart Cart { get; } = cart;
        private IPaymentMethod PaymentMethod { get; } = paymentMethod;

        public string Checkout()
        {
            double totalAmount = Cart.CalculateTotal();
            string paid = PaymentMethod.ProcessPayment(totalAmount);
            return $"{paid}. Compra realizada con éxito.";
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
        string ProcessPayment(double amount);
    }

    class GooglePayPayment : IPaymentMethod
    {
        public string ProcessPayment(double amount)
        {
            return $"Pago realizado con Google Pay: ${amount}";
        }
    }

    class PayPalPayment : IPaymentMethod
    {
        public string ProcessPayment(double amount)
        {
            return $"Pago realizado con PayPal: ${amount}";
        }
    }

}
