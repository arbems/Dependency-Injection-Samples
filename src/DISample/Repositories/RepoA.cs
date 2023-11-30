using DemoDI.Domain;

namespace DemoDI.Infrastructure;

public class RepoA(string connString) : IRepoA
{
    private readonly string _connString = connString;

    // Implementación específica para RepoA
    public void Add(object entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(object entity)
    {
        throw new NotImplementedException();
    }

    public void Update(object entity)
    {
        throw new NotImplementedException();
    }
}