using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kazeoseki.Models.Domain
{
    public class LoginUser
    {
        public int UserId { get; set; }
        [Required, MaxLength(50)]
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Salt { get; set; }
        public string HashPassword { get; set; }
        public int RoleId { get; set; }
        public bool Confirmed { get; set; }
        public bool Suspended { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        [Required]
        public string Password { get; set; }
        public int LoginTypeId { get; set; }
    }
}
