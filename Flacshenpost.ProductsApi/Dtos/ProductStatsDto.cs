using Flaschenpost.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flacshenpost.ProductsApi.Dtos
{
    //api/products/unit/Price/MinMax
    //api/products/mostBottels
    //api/products/sale => price 17.99
    public class ProductStatsDto
    {
        public Product Ceheapest { get; set; }
        public Product MostExpensive { get; set; }
        public Product WithMostBottles { get; set; }
        public Product Sale { get; set; }
    }

    public class CheapesAndMostExpensiveArticle
    {
        public Article Ceheapest { get; set; }
        public Article MostExpensive { get; set; }
    }

    
}
