namespace RegisterGroups;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IPaymentMethod, GooglePayPayment>()  // or PayPalPayment
            .AddScoped<ShoppingCart>()
            .AddScoped<Purchase>();

        return services;
    }
}