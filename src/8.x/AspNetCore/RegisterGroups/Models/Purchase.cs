namespace RegisterGroups;

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