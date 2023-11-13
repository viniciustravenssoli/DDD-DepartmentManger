using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.DTOs
{
    public class AddRoleToUserDTO
    {
        public long UserId { get; set; }
        public string Role { get; set; }
    }
}