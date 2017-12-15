using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kazeoseki.Models.Domain
{
    public class NewUser
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string HashPassword { get; set; }
    }
}
