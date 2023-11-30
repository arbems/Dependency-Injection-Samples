using DemoDI;
using DemoDI.Domain;
using DemoDI.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

// Configurar el contenedor de servicios
var serviceProvider = ConfigureServices();

// Crear una instancia de MyService y pasar el ServiceProvider
var myService = new MyService(serviceProvider);

// Usar MyService para obtener un repositorio
var repositoryTypeA = "RepoA";
var repositoryA = myService.GetRepository(repositoryTypeA);

var repositoryTypeB = "RepoB";
var repositoryB = myService.GetRepository(repositoryTypeB);

Console.ReadLine();

static IServiceProvider ConfigureServices()
{
    var services = new ServiceCollection();

    // Configurar tus servicios aquí
    services.AddScoped<IRepoA, RepoA>();
    services.AddScoped<IRepoB, RepoB>();

    // Construir el ServiceProvider
    return services.BuildServiceProvider();
}