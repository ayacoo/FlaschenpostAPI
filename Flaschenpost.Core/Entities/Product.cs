using System.Collections.Generic;
using System.Linq;

namespace Flaschenpost.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Name { get; set; }
        public string DescriptionText { get; set; }
        public IEnumerable<Article> Articles { get; set; } = Enumerable.Empty<Article>();

        public Product WithArticles(IEnumerable<Article> articles)
        {
            var list = Articles?.ToList() ?? new List<Article>();
            list.AddRange(articles);
            Articles = list;

            return this;
        }

        public bool ContainsArticleWithPrice(double price)
        {
            if (Articles is null || !Articles.Any())
                return false;

            return Articles
                  .Any(a => a.TotalPrice == price);
        }
    }
}
