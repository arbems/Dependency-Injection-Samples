using DemoDI.Domain;
using DISample.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDI;

public class MyService(IServiceProvider serviceProvider)
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public IRepository GetRepository(string repositoryType)
    {
        switch (repositoryType)
        {
            case "RepoA":
                return _serviceProvider.GetRequiredService<IRepoA>();
            case "RepoB":
                return _serviceProvider.GetRequiredService<IRepoB>();
            // Agrega más casos según la cantidad de repositorios
            default:
                throw new NotSupportedException("Tipo de repositorio no admitido");
        }
    }
}