using System.ComponentModel.DataAnnotations;

namespace Adventure_Work.Models
{
    public class SubCategory
    {
        [Key]
        [Required]
        public int ProductSubcategoryID { get; set; }
        [Required]
        public int ProductCategoryID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid rowguid { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}
