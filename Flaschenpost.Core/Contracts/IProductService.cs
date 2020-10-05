using Flaschenpost.Core.Entities;
using Flaschenpost.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flaschenpost.Core.Contracts
{
    public interface IProductService
    {
        Task<ExecutionResult<IEnumerable<Article>>> FetchAticles(string dataSource);
        Task<ExecutionResult<IEnumerable<Product>>> FindProductByPrice(string dataSource, double price);
        Task<ExecutionResult<Product>> FindProductWithMostBottles(string dataSource);
    }

}