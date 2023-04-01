using System.ComponentModel.DataAnnotations;

namespace Adventure_Work.Models
{
    public class Product
    {
        [Key]
        [Required]
        public int ProductID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(25)]
        public string ProductNumber { get; set; }
        [Required]
        public bool MakeFlag { get; set; }
        [Required]
        public bool FinishedGoodsFlag { get; set; }
        [StringLength(15)]
        public string Color { get; set; }
        [Required]
        public short SafetyStockLevel { get; set; }
        [Required]
        public short ReorderPoint { get; set; }
        [Required]
        public decimal StandardCost { get; set; }
        [Required]
        public decimal ListPrice { get; set; }
        [StringLength(5)]
        public string Size { get; set; }
        [StringLength(3)]
        public string SizeUnitMeasureCode { get; set; }
        [StringLength(3)]
        public string WeightUnitMeasureCode { get; set; }
        public decimal Weight { get; set; }
        [Required]
        public int DaysToManufacture { get; set; }
        [StringLength(2)]
        public string ProductLine { get; set; }
        [StringLength(2)]
        public string Class { get; set; }
        [StringLength(2)]
        public string Style { get; set; }
        public int ProductSubcategoryID { get; set; }
        public int ProductModelID { get; set; }
        [Required]
        public DateTime SellStartDate { get; set; }
        public DateTime? SellEndDate { get; set; }
        public DateTime? DiscontinuedDate { get; set; }
        [Required]
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
