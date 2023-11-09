using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Role : Base
    {
        private Role()
        {
            
        }
        public Role(string roleName)
        {
            RoleName = roleName;
        }
        public string RoleName { get; private set; }

        [JsonIgnore]
        public ICollection<User> Users { get; } = new List<User>();

        public static implicit operator Role(string roleName) => new Role(roleName);

        public static implicit operator string(Role role) => role.ToString();

        public override string ToString() 
            => RoleName;

        public override bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}