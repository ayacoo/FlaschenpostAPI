using Flaschenpost.Core.Contracts;
using Flaschenpost.Core.Entities;
using Flaschenpost.Services.Common;
using Flaschenpost.Shared;
using Flaschenpost.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flaschenpost.Services
{
    public class PorductService : IProductService
    {
        private readonly IRequestHandler<string, IEnumerable<ProductModel>> _dataReader;

        public PorductService(IRequestHandler<string, IEnumerable<ProductModel>> dataReader)
        {
            _dataReader = dataReader.GuardAgainstNull(nameof(dataReader));
        }

        public async Task<ExecutionResult<IEnumerable<Article>>> FetchAticles(string dataSource)
        {
            try
            {
                var articles = await GetAllArticles(dataSource);

                if (articles is null || !articles.Any())
                    return ExecutionResult.Error(Enumerable.Empty<Article>(), "could not find any article!");

                return
                ExecutionResult.Success(articles);

            }
            catch (System.Exception ex)
            {
                return
                ExecutionResult.Error<IEnumerable<Article>>(ex);
            }
        }


        public async Task<ExecutionResult<Product>> FindProductWithMostBottles(string dataSource)
        {
            try
            {
                var products = await GetProducts(dataSource);

                if (products is null || !products.Any())
                    return ExecutionResult.Error(new Product(), "could not find any product!");

                var product = products
                    .Select(p => new
                    {
                        Product = p,
                        Bottles = p.Articles.GroupBy(a => a.Bottle)
                    }).OrderByDescending(g => g.Bottles.Count())
                    .Select(g => g.Product)
                    .FirstOrDefault();

                return
                    ExecutionResult.Success(product);
            }
            catch (System.Exception ex)
            {
                return
                    ExecutionResult.Error(ex, new Product());
            }
        }
        public async Task<ExecutionResult< IEnumerable<Product>>> FindProductByPrice(string dataSource, double price)
        {
            try
            {
                var products = await GetProducts(dataSource);

                if (products is null || !products.Any())
                    return ExecutionResult.Error(Enumerable.Empty<Product>(), "could not find any product!");


                products = products
                    .Where(p => p.ContainsArticleWithPrice(price))
                    .OrderBy(p => p.GetCheapestPericePerUnit());

                return
                    ExecutionResult.Success(products);

            }
            catch (System.Exception ex)
            {

                return
                    ExecutionResult.Error(ex, Enumerable.Empty<Product>());
            }        }



        private async Task<IEnumerable<Product>> GetProducts(string dataSource)
        {
            var list = await _dataReader.HandleRequest(dataSource);

            return
            list.Select(p => CreateProductDto(p));
        }

        private Product CreateProductDto(ProductModel product)
        {
            return new Product
            {
                Id = product.Id,
                BrandName = product.BrandName,
                DescriptionText = product.DescriptionText,
                Name = product.Name
            }.WithArticles(product.Articles.ToArticleList());
        }

        private async Task<IEnumerable<Article>> GetAllArticles(string dataSource)
        {
            var list = await GetProducts(dataSource);

            return
            list.SelectMany(p => p.Articles);
        }

    }
}
