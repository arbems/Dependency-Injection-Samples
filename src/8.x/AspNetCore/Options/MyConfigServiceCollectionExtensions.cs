namespace Options;

public static class MyConfigServiceCollectionExtensions
{
    public static IServiceCollection AddConfig(
         this IServiceCollection services, IConfiguration config)
    {
        services.Configure<ColorOptions>(
            config.GetSection("ColorOptions"));

        return services;
    }

    public static IServiceCollection AddMyDependencyGroup(
         this IServiceCollection services)
    {
        // services.AddScoped<IMyDependency, MyDependency>();

        return services;
    }
}
