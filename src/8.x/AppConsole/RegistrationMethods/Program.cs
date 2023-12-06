/*
    Service registration methods

    The framework provides service registration extension methods that are useful in specific scenarios.
 
 */

// <Sample>
using Microsoft.Extensions.DependencyInjection;

ServiceCollection services = new();

// Method: Automatic object disposal |	Multiple implementations
//Add{LIFETIME}<{SERVICE}, {IMPLEMENTATION}>()
services.AddSingleton<IPerson, Person>();

// Method: Automatic object disposal |	Multiple implementations | Pass args
//Add{LIFETIME}<{SERVICE}>(sp => new {IMPLEMENTATION })
services.AddSingleton<IPerson>(sp => new Person());
services.AddSingleton<IPerson>(sp => new Person("Jesus", 14));

// Method: Automatic object disposal
//Add{LIFETIME}<{IMPLEMENTATION}>()
services.AddSingleton<Person>();

// Method: Multiple implementations | Pass args
//AddSingleton<{SERVICE}>(new {IMPLEMENTATION })
services.AddSingleton<IPerson>(new Person());
services.AddSingleton<IPerson>(new Person("Elena", 40));

// Method: Pass args
//AddSingleton(new {IMPLEMENTATION })
services.AddSingleton(new Person());
services.AddSingleton(new Person("Ana", 40));

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