using System.ComponentModel.DataAnnotations;

namespace blog.Models
{
    public class Login
    {
        [Required (ErrorMessage ="!Username!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "!Password!")]
        public string Password { get; set; }
    }
}
