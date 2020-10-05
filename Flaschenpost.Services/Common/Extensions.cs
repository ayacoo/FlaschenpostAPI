using Flaschenpost.Core.Entities;
using Flaschenpost.Core.ValueObjects;
using Flaschenpost.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace Flaschenpost.Services.Common
{


    public static class Extensions
    {
        public static Article FindCheapestArticlePerLitre(this IEnumerable<Article> articles)
        {
            return
            articles
            .OrderBy(a => a.PricePerUnit)
            .First();
        }

        public static Article FindMostExpansiveArticlePerLitre(this IEnumerable<Article> articles)
        {
            return
            articles
            .OrderByDescending(a => a.PricePerUnit)
            .First();
        }

        internal static double GetCheapestPericePerUnit(this Product product)
        {
            if (product is null || product.Articles is null || !product.Articles.Any())
                return -1;

            return product.Articles
                .Select(a => a.PricePerUnit)
                .Min();
        }

        internal static Article ToArticle(this ArticleModel article)
        {
            var values = article.CreateBottle();

            return new Article
            {
                Id = article.Id,
                TotalPrice = article.Price,
                PricePerUnit = article.GetPricePerUnit(),
                ShortDescription = article.ShortDescription,
                Unit = article.Unit,
                Amount = values.Amount,
                Bottle = values.Bottle
            };
        }

        internal static double GetPricePerUnit(this ArticleModel article)
        {
            //"(2,10 €/Liter)"
            var value = article
                        .PricePerUnitText
                        .Replace("(", "")
                        .Replace(")", "")
                        .Split("/")
                        .FirstOrDefault()
                        ?.Split(" ")
                        ?.FirstOrDefault() ?? string.Empty;

            if (double.TryParse(value, out var price))
                return price;

            return -1;
        }

        private static (int Amount, Bottle Bottle) CreateBottle(this ArticleModel article)
        {
            //"20 x 0,5L (Glas)"
            var attributes = article.ShortDescription
                        ?.Replace("(", string.Empty)
                        ?.Replace(")", string.Empty)
                        ?.Split(" ");

            if (attributes is null)
                return (-1, null);

            if (!int.TryParse(attributes[0], out var amount)
              || !double.TryParse(attributes[2].Replace("L", ""), out var volume)
              || attributes.Length < 4)
                return (-1, null);

            return (amount, new Bottle
            {
                Material = attributes[3],
                Volume = volume
            });
        }

        internal static IEnumerable<Article> ToArticleList(this IEnumerable<ArticleModel> articles)
        {
            return
            articles.Select(a => a.ToArticle())
                .Where(a => a.PricePerUnit != -1
                    && a.Amount != -1
                    && a.PricePerUnit != -1);
        }


    }
}
