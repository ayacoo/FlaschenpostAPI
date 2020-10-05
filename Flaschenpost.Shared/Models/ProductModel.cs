using System.Collections.Generic;

namespace Flaschenpost.Shared.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Name { get; set; }
        public string DescriptionText { get; set; }
        public IEnumerable<ArticleModel> Articles { get; set; }
    }
}
