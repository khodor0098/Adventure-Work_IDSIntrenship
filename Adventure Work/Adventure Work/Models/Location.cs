using System.ComponentModel.DataAnnotations;

namespace Adventure_Work.Models
{
    public class Location
    {
        [Key]
        [Required]
        public short LocationID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal CostRate { get; set; }
        [Required]
        public decimal Availability { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}
