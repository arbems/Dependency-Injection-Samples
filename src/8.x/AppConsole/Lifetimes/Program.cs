/*
    Lifetimes
 
    - Transient objects are always different. The transient OperationId value is different.
    - Scoped objects are the same for a given request but differ across each new request.
    - Singleton objects are the same for every request.
 */

// <Sample>
using Lifetimes;
using Microsoft.Extensions.DependencyInjection;

ServiceCollection services = new();

services.AddTransient<IOperationTransient, Operation>();
services.AddScoped<IOperationScoped, Operation>();
services.AddSingleton<IOperationSingleton, Operation>();

ServiceProvider serviceProvider = services.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    IOperationTransient transientOperation = scope.ServiceProvider.GetRequiredService<IOperationTransient>();
    IOperationScoped scopedOperation = scope.ServiceProvider.GetRequiredService<IOperationScoped>();
    IOperationSingleton singletonOperation = scope.ServiceProvider.GetRequiredService<IOperationSingleton>();

    Console.WriteLine("Transient: " + transientOperation.OperationId);
    Console.WriteLine("Scoped: " + scopedOperation.OperationId);
    Console.WriteLine("Singleton: " + singletonOperation.OperationId);
}

Console.WriteLine("...");

using (var scope = serviceProvider.CreateScope())
{
    IOperationTransient transientOperation = scope.ServiceProvider.GetRequiredService<IOperationTransient>();
    IOperationScoped scopedOperation = scope.ServiceProvider.GetRequiredService<IOperationScoped>();
    IOperationSingleton singletonOperation = scope.ServiceProvider.GetRequiredService<IOperationSingleton>();

    Console.WriteLine("Transient: " + transientOperation.OperationId);
    Console.WriteLine("Scoped: " + scopedOperation.OperationId);
    Console.WriteLine("Singleton: " + singletonOperation.OperationId);
}
// </Sample>