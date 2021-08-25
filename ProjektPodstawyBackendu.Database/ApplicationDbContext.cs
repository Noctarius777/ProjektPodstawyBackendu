using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjektPodstawyBackendu.Domain;
using System;

namespace ProjektPodstawyBackendu.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<MessageEntity> Message { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)  // przekazujemy do bazowej klasy DbContext
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                Id = Guid.NewGuid().ToString(),
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"

                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Premium",
                    NormalizedName = "PREMIUM"
                }
            );
        }
    }
}
