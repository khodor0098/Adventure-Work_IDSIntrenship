using System.ComponentModel.DataAnnotations;

namespace Adventure_Work.Models
{
    public class ProductPhoto
    {
        [Key]
        [Required]
        public int ProductPhotoID { get; set; }
        public byte[] ThumbNailPhoto { get; set; }
        [StringLength(50)]
        public string ThumbnailPhotoFileName { get; set; }
        public byte[] LargePhoto { get; set; }
        [StringLength(50)]
        public string LargePhotoFileName { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}
