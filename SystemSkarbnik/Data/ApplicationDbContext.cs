using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SystemSkarbnik.Models;

namespace SystemSkarbnik.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<SystemSkarbnik.Models.Klasa> Klasa { get; set; } = default!;
        public DbSet<SystemSkarbnik.Models.Skarbnik> Skarbnik { get; set; } = default!;
        public DbSet<SystemSkarbnik.Models.Uczen> Uczen { get; set; } = default!;
        public DbSet<SystemSkarbnik.Models.Zbiorka> Zbiorka { get; set; } = default!;
        public DbSet<SystemSkarbnik.Models.ZbiorkaUczen> ZbiorkaUczen { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Zbiorka>().HasOne(e => e.Klasa).WithMany(e => e.Zbiorkas).Metadata.DeleteBehavior = DeleteBehavior.Restrict;
            builder.Entity<Zbiorka>().HasOne(e => e.Skarbnik).WithMany(e => e.Zbiorkas).Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            builder.Entity<ZbiorkaUczen>().HasOne(e => e.Klasa).WithMany(e => e.ZbiorkaUczens).Metadata.DeleteBehavior = DeleteBehavior.Restrict;
            builder.Entity<ZbiorkaUczen>().HasOne(e => e.Uczen).WithMany(e => e.ZbiorkaUczens).Metadata.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
