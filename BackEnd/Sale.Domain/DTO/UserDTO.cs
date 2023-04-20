using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Domain.DTO
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? PasswordHash { get; set; }
        public string? Token { get; set; }
        public int? IsActive { get; set; }
    }
}
