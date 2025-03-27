using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ProgettoBackend_S7_L5.Models.Auth
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }

        public ICollection<ApplicationUserRole> UserRoles { get; set; }
        public ICollection<Biglietto> Biglietti { get; set; }
    }
}
