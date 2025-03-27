using ProgettoBackend_S7_L5.Models;
using ProgettoBackend_S7_L5.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ProgettoBackend_S7_L5.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUserRole>().HasOne(a => a.ApplicationUser).WithMany(u => u.UserRoles).HasForeignKey(a => a.UserId);

            modelBuilder.Entity<ApplicationUserRole>().HasOne(a => a.ApplicationRole).WithMany(r => r.UserRoles).HasForeignKey(a => a.RoleId);

            modelBuilder.Entity<Biglietto>().HasOne(b => b.User).WithMany(u => u.Biglietti).HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Biglietto>().HasOne(b => b.Evento).WithMany(e => e.Biglietti).HasForeignKey(b => b.EventoId);

            modelBuilder.Entity<Evento>().HasOne(e => e.Artista).WithMany(a => a.Eventi).HasForeignKey(e => e.ArtistaId);
        }
    }
}
