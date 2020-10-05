namespace Flaschenpost.Shared.Models
{
    public class ArticleModel
    {
        public int Id { get; set; }
        public string ShortDescription { get; set; }
        public double Price { get; set; }
        public string Unit { get; set; }
        public string PricePerUnitText { get; set; }

    }
}
