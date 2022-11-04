using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pictureAPI.Auth.Model;
using pictureAPI.Data.Entities;

namespace pictureAPI.Data
{
    public class PictureAPIDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSqlLocalDb;database=PictureStore;Trusted_Connection=True;");
        }
    }
}
