namespace Basket.Api.Entities
{
    public class ShoppingCart
    {

        public string UserName { get; set; }
        public List<ShoppingCartItem> items { get; set; } = new List<ShoppingCartItem>();

        public ShoppingCart()
        {
  
        }

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }

        public decimal TotalPriceCal {
        
            get {
                decimal total = 0;

                foreach (var item in items)
                {
                    total += item.Price * item.Quantity;
                }

                return total;

            }
        }
    }
}
