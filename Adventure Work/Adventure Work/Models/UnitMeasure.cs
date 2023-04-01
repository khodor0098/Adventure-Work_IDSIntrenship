using System.ComponentModel.DataAnnotations;

namespace Adventure_Work.Models
{
    public class UnitMeasure
    {
        [Key]
        [Required]
        public string UnitMeasureCode { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}
