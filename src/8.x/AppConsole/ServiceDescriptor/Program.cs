/*
    ServiceDescriptor

    IServiceCollection is a collection of ServiceDescriptor objects.

 */

// <Sample>
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

ServiceCollection services = [];

var descriptor = new ServiceDescriptor(
    typeof(IPerson),
    _ => new Person("", 12),
    ServiceLifetime.Transient);

services.Add(descriptor);

ServiceProvider serviceProvider = services.BuildServiceProvider();
// </Sample>


// <Models>
interface IPerson
{
    void SetName(string name);
    string GetName();
    void SetAge(int age);
    int GetAge();
}
class Person(string name = "", int age = 0) : IPerson
{
    private string name = name;
    private int age = age;

    public void SetName(string name)
    {
        this.name = name;
    }

    public string GetName()
    {
        return name;
    }

    public void SetAge(int age)
    {
        this.age = age;
    }

    public int GetAge()
    {
        return age;
    }
}
// </Models>