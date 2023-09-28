namespace Request.Aggregator.Moldes
{
    public class ShoppingModel
    {

        public string UserName { get; set; }

        public BasketModel BasketWithProducts { get; set; }
        
        public IEnumerable<OrderResponseModel> orders { get; set; }
    }
}
