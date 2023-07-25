using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace blog.Identity
{
    public class ApplicationRole:IdentityRole<int>
    {
        public int ID { get; set; }
        public string Description { get; set; }

        public ApplicationRole()
        { 
        }

        public ApplicationRole(string rolename, string description)
        {
            this.Description = description;
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}
