namespace Request.Aggregator.Moldes
{
    public class BasketModel
    {

        public string UserName { get; set; }

        public List<BasketItemExtendedModel> Items { get; set; } = new List<BasketItemExtendedModel>();

        public decimal TotalPriceCal { get; set; }
    }
}
