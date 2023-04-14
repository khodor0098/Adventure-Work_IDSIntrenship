using System.ComponentModel.DataAnnotations;

namespace Adventure_Work.Models
{
    public class Inventory
    {
        [Key]
        [Required]
        public int ProductID { get; set; }
        [Key]
        [Required]
        public short LocationID { get; set; }
        [Required]
        [StringLength(10)]
        public string Shelf { get; set; }
        [Required]
        public byte Bin { get; set; }
        [Required]
        public short Quantity { get; set; }
        [Required]
        public Guid rowguid { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}
