using System.ComponentModel.DataAnnotations;

namespace Adventure_Work.Models
{
    public class ProductCategory
    {
        [Key]
        [Required]
        public int ProductCategoryID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public Guid rowguid { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}
