using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kazeoseki.Models.Domain
{
    public class GalleryImage
    {
        public int Id { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Max length is 50 characters")]
        public string ImageTitle { get; set; }
        [Required, MaxLength(2000, ErrorMessage = "Max length is 2000 characters. Was that not enough?")]
        public string Description { get; set; }
        public int FileId { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
