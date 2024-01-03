using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Services.DTOs
{
    public class UserCreateDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}