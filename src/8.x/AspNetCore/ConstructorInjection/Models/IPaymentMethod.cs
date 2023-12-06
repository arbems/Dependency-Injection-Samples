namespace RegisterGroups.Models;

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