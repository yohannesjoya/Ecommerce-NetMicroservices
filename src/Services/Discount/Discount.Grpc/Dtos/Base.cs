using System.ComponentModel.DataAnnotations;

namespace Discount.Grpc.Dtos
{
    public class Base
    {
        [Required(ErrorMessage = "Product name is required")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Product Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Amountis required")]
        public int Amount { get; set; }
    }
}
