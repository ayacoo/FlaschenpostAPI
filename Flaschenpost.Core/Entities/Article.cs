using Flaschenpost.Core.ValueObjects;

namespace Flaschenpost.Core.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public string ShortDescription { get; set; }
        public double PricePerUnit { get; set; }
        public double TotalPrice { get; set; }
        public string Unit { get; set; }
        public int Amount { get; set; }
        public Bottle Bottle { get; set; }
    }
}
