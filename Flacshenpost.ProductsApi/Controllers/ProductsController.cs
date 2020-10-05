using System.Collections.Generic;
using System.Threading.Tasks;
using Flacshenpost.ProductsApi.Dtos;
using Flaschenpost.Core.Contracts;
using Flaschenpost.Core.Entities;
using Flaschenpost.Shared;
using Microsoft.AspNetCore.Mvc;
using Flaschenpost.Services.Common;
namespace Flacshenpost.ProductsApi.Controllers
{
    [ApiController]
    [Route("api/v1/beers")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [Route("litrepriceminmax")]
        [HttpGet]
        public async Task<ActionResult<CheapesAndMostExpensiveArticle>> GetCheapestAndMostExpensive(string url)
        {
            var articlesResult = await _service.FetchAticles(url);


            if (!articlesResult.IsSuccess )
                return BadRequest(ExecutionResult.Error(new CheapesAndMostExpensiveArticle(),articlesResult.Message));

            var cheapest = articlesResult.Value.FindCheapestArticlePerLitre();

            var mostExpensive = articlesResult.Value.FindMostExpansiveArticlePerLitre();


            var result = new CheapesAndMostExpensiveArticle
            {
                Ceheapest = cheapest,
                MostExpensive = mostExpensive
            };

            return new JsonResult(result);
        }

        [Route("mostbottles")]
        [HttpGet]
        public async Task<ActionResult<Product>> GetProductWithMostBottles(string url)
        {
            var result = await _service.FindProductWithMostBottles(url);

            if (!result.IsSuccess)
                return BadRequest(result);

            return new JsonResult(result.Value);
        }


        [Route("bestseller")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetSaleProducts(string url)
        {
            var result = await _service.FindProductByPrice(url, 17.99);

            if (!result.IsSuccess)
                return BadRequest(result);

            return new JsonResult(result.Value);
        }
    }
}
