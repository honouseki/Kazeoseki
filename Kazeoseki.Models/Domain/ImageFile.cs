using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kazeoseki.Models.Domain
{
    public class ImageFile
    {
        public int FileId { get; set; }
        public string ImageFileName { get; set; }
        public string SystemFileName { get; set; }
        public int ImageFileType { get; set; }
        public string Location { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] ByteArray { get; set; }
        public string Extension { get; set; }
        public string ImageUrl { get; set; }
    }
}
