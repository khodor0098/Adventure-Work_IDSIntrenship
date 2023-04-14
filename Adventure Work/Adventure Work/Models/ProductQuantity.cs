using System.ComponentModel.DataAnnotations;

namespace Adventure_Work.Models
{
    public class ProductQuantity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
