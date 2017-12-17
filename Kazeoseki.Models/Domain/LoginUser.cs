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
        [Required, MaxLength(50, ErrorMessage = "Username is too fucking long")]
        public string Username { get; set; }
        [EmailAddress(ErrorMessage = "Email address is invalid")]
        public string Email { get; set; }
        public string Salt { get; set; }
        public string HashPassword { get; set; }
        public int RoleId { get; set; }
        public bool Confirmed { get; set; }
        public bool Suspended { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        [Required, MinLength(6, ErrorMessage = "Password requires minimum of 6 characters")]
        //[RegularExpression(@"^[a-zA-Z][0-9]$", ErrorMessage = "Does not contain a letter AND a number")]
        public string Password { get; set; }
        public int LoginTypeId { get; set; }
    }
}