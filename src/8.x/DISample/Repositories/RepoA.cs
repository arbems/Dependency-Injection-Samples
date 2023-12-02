using DemoDI.Domain;
using DISample.Entities;

namespace DemoDI.Infrastructure;

public class RepoA(RepoAOptions repoAOptions) : IRepoA
{
    private readonly RepoAOptions _repoAOptions = repoAOptions;

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