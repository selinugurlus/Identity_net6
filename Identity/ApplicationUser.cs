using Microsoft.AspNetCore.Identity;

namespace blog.Identity
{
    public class ApplicationUser:IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
