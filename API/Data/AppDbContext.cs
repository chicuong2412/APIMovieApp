using System.Security;
using API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        public DbSet<Movie> Movies { get; set; }

        public DbSet<Season> Seasons { get; set; }

        public DbSet<Episode> Episodes { get; set; }

        public DbSet<Video> Videos { get; set; }

        public DbSet<RefreshTokens> RefreshTokens { get; set; }

        public DbSet<LogoutJWT> LogoutJWTs { get; set; }

        public DbSet<Genere> Generes { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<ProductionCompany> ProductionCompanies { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<PasswordResetCode> PasswordResetCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Seasons)
                .WithOne(s => s.Movie)
                .HasForeignKey(s => s.MovieId);

            modelBuilder.Entity<Season>()
                .HasMany(s => s.Episodes)
                .WithOne(e => e.Season)
                .HasForeignKey(e => e.SeasonId);

            modelBuilder.Entity<Role>()
                .HasMany(u => u.Permissions)
                .WithMany(p => p.RolePermissions)
                .UsingEntity(j => j.ToTable("RolePermissions"));

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity(j => j.ToTable("UserRoles"));

            modelBuilder.Entity<User>()
                .HasOne(u => u.PasswordResetCode)
                .WithOne(p => p.User)
                .HasForeignKey<PasswordResetCode>(p => p.UserId);

        }

    }
}
