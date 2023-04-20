using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySaleApp.Domain.DTOs
{
    public class SessionDTO
    {
        public string? UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? RoleName { get; set; }
        public string? Token { get; set; }
    }
}
