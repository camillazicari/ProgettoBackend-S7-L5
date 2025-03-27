using Microsoft.AspNetCore.Identity;

namespace ProgettoBackend_S7_L5.Models.Auth
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }

    }
}
