using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kazeoseki.Models.Domain
{
    public class Link
    {
        public int Id { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Title's Too Long")]
        public string Title { get; set; }
        [Required, MaxLength(255, ErrorMessage = "Description is TL;DR")]
        public string Description { get; set; }
        [Required, Url(ErrorMessage = "Invalid Url")]
        public string Url { get; set; }
        public int ImageId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ImageUrl { get; set; }
    }
}
