using Flaschenpost.Core.Contracts;
using Flaschenpost.Services;
using Flaschenpost.Shared.Models;
using FluentAssertions;
using Newtonsoft.Json;
using NSubstitute;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Flasschenpost.Tests
{


    public class ProductServiceTests
    {
        [Fact]
        public async Task FetchAticles_returns_all_articles()
        {
            var dataReader = Substitute
                .For<IRequestHandler<string,IEnumerable<ProductModel>>>();

            dataReader.HandleRequest(Arg.Any<string>())
                .Returns(GetProducts());

            var service = new PorductService(dataReader);

            var actual = await service.FetchAticles("url");

            actual.IsSuccess.Should().BeTrue();
            actual.Value.Count().Should().Be(4);
        }

        private IEnumerable<ProductModel> GetProducts()
        {
            var json = File.ReadAllText("resources/Productsbottles.json");

            return JsonConvert.DeserializeObject<ProductModel[]>(json);
        }


        [Fact]
        public async Task FindProductWithMostBottles_returns_the_product()
        {
            var dataReader = Substitute
                .For<IRequestHandler<string, IEnumerable<ProductModel>>>();

            var products = GetProducts();

            dataReader.HandleRequest(Arg.Any<string>())
                .Returns(products.ToImmutableList());

            var service = new PorductService(dataReader);

            var actual = await service.FindProductWithMostBottles("url");

            actual.IsSuccess.Should().BeTrue();
            actual.Value.Id.Should().Be(1138);
        }


        [Fact]
        public async Task FindProductWithMostBottles_returns_the_product_with_most_bottles_variasions()
        {
            var dataReader = Substitute
                .For<IRequestHandler<string, IEnumerable<ProductModel>>>();

            var products = GetProducts();

            dataReader.HandleRequest(Arg.Any<string>())
                .Returns(products.ToImmutableList());

            var service = new PorductService(dataReader);

            var actual = await service.FindProductByPrice("url",17.99);

            actual.IsSuccess.Should().BeTrue();
            actual.Value.SelectMany(p=> p.Articles)
                .All(a=> a.TotalPrice == 17.99)
                .Should().BeTrue();
        }
    }
}
