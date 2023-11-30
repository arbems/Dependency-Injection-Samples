using DemoDI.Domain;

namespace DemoDI.Infrastructure;

public class RepoB(string key) : IRepoB
{
    private readonly string _key = key;

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