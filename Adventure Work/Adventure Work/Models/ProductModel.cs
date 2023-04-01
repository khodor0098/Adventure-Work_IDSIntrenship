using System.ComponentModel.DataAnnotations;

namespace Adventure_Work.Models
{
    public class ProductModel
    {
        [Key]
        [Required]
        public int ProductModelID { get; set; }
        [Required]
        public string Name { get; set; }
        public string CatalogDescription { get; set; }
        public string Instructions { get; set; }
        [Required]
        public Guid rowguid { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}
