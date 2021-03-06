﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kazeoseki.Models.ViewModels
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string HashPassword { get; set; }
        public int RoleId { get; set; }
        public bool Confirmed { get; set; }
        public bool Suspended { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
