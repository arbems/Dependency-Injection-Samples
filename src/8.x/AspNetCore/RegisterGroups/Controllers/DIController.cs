using Microsoft.AspNetCore.Mvc;

namespace RegisterGroups.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DIController(IServiceProvider serviceProvider) : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        [HttpGet]
        public ActionResult Get()
        {
            ShoppingCart cart = _serviceProvider.GetRequiredService<ShoppingCart>();
            cart.AddItem(new("Producto 1", 30.0));
            cart.AddItem(new("Producto 2", 50.0));

            // PayPalPayment payPalPayment = ActivatorUtilities.CreateInstance<PayPalPayment>(_serviceProvider);
            GooglePayPayment googlePayPayment = ActivatorUtilities.CreateInstance<GooglePayPayment>(_serviceProvider);

            Purchase purchase = ActivatorUtilities.CreateInstance<Purchase>(_serviceProvider, cart, googlePayPayment);

            return Ok(purchase.Checkout());
        }

    }

}