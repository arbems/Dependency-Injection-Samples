# Service lifetimes: Transient, Scoped & Singleton in ASP.NET Core

Service lifetimes define how to create and delete the services stored in the service container. 
You must choose a suitable durability for each service according to its characteristics and requirements.

Services can be registered with one of the following lifetimes:

### Transient
Transient objects are always different, they are created if you request 
them from the service container. <br />Function better for lighter services and without status.

### Scoped
Scoped objects are the same for a given request, but vary between requests.<br />
Scoped is the default life of Entity Framework Core when you add AddDbContext.

### Singleton
Singleton objects are the same for each request, they are created the first time 
they are requested, each subsequent request of the service implementation from the dependency 
injection container uses the same instance. <br />
Because memory is not freed until the application is closed, consider using memory with a singleton service.

  * Important:
Do not resolve a Scoped service from a Singleton and be careful not to do so indirectly. 
Solve as follows:
	- Resolve a singleton service from a Transient or Scoped service.
	- Resolve a Scoped service from another Transient or Scoped service.

## Getting Started
This is an example to show how service lifetimes work using dependency injection in ASP.NET Core
### Installation
1. Install packages
```
Install-Package Microsoft.Extensions.DependencyInjection
```
### Usage
1. Create class and interface "Operation"
```csharp
public interface IOperation
{
    string OperationId { get; }
}

public interface IOperationTransient : IOperation { }
public interface IOperationScoped : IOperation { }
public interface IOperationSingleton : IOperation { }
```

```csharp
public class Operation : IOperationTransient, IOperationScoped, IOperationSingleton
{
    public Operation()
    {
        OperationId = Guid.NewGuid().ToString()[^4..];
    }

    public string OperationId { get; }
}
```

2. Add services to the container
```csharp
builder.Services.AddTransient<IOperationTransient, Operation>();
builder.Services.AddScoped<IOperationScoped, Operation>();
builder.Services.AddSingleton<IOperationSingleton, Operation>();
```

3. Injection and use in controller and middleware to see how values to get OperationId change according to service lifetime
```csharp
_logger.LogInformation("Transient: " + transientOperation.OperationId);
_logger.LogInformation("Scoped: " + scopedOperation.OperationId);
_logger.LogInformation("Singleton: " + _singletonOperation.OperationId);
```
4. When you start the application, you see how "Transient" middleware changes with respect to controller. While Scoped and Singleton do not change. 
```
info: Lifetimes.MyMiddleware[0]
      Transient: cc44
info: Lifetimes.MyMiddleware[0]
      Scoped: 5582
info: Lifetimes.MyMiddleware[0]
      Singleton: d05c
info: DbContext.Controllers.DIController[0]
      Transient: 8af0
info: DbContext.Controllers.DIController[0]
      Scoped: 5582
info: DbContext.Controllers.DIController[0]
      Singleton: d05c
```
5. In a new http request, you can see that "Transient" changes. "Singleton" doesn't change. But "Scoped" changes compared to the first results, because it is a new request.
```
info: Lifetimes.MyMiddleware[0]
      Transient: 42b0
info: Lifetimes.MyMiddleware[0]
      Scoped: 839f
info: Lifetimes.MyMiddleware[0]
      Singleton: d05c
info: DbContext.Controllers.DIController[0]
      Transient: d512
info: DbContext.Controllers.DIController[0]
      Scoped: 839f
info: DbContext.Controllers.DIController[0]
      Singleton: d05c
```

Conclusion, "Transient" objects are always different. "Scoped" objects are the same for a given request, but vary between requests. And "Singleton" objects are the same for each request, they are created the first time they are requested.
