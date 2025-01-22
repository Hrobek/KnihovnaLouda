using Microsoft.AspNetCore.Identity;

namespace KnihovnaLouda.Models
{
    public class ApplicationsUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
