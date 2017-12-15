using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kazeoseki.Models
{
    public interface IUserAuthData
    {
        int UserId { get; }
        string Username { get; }
        string Email { get; }
        bool Remember { get; }
        int RoleId { get; }
        bool Confirmed { get; }
        bool Suspended { get; }
    }
}
