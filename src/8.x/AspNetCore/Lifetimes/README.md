# ﻿Service lifetimes: Transient, Scoped & Singleton in ASP.NET Core

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

## ﻿Getting Started
This is an example to show how service lifetimes work using dependency injection in .NET
### Installation
1. Install packages
```
Install-Package Microsoft.Extensions.DependencyInjection
```
### Usage
```csharp
function test() {
  console.log("notice the blank line before this function?");
}
```
