using Flaschenpost.Clients;
using Flaschenpost.Core.Contracts;
using Flaschenpost.Core.Entities;
using Flaschenpost.Services;
using Flaschenpost.Shared.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Flaschenpost.Infrastructure
{
    public class ProductStatisticsModule : IModule<IServiceCollection>
    {
        public IServiceCollection RegisterServices(IServiceCollection container)
        {
            container.AddHttpClient<IRequestHandler<string,IEnumerable<ProductModel>>,JsonProductReader>();            
            container.AddScoped<IProductService, PorductService>();

            return container;
        }
    }
}
