using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StiebelEltronDashboard.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<HeatPumpDataPerPeriod> HeatPumpDataPerPeriods { get; set; }
        public DbSet<HeatPumpDatum> HeatPumpData { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            modelBuilder.Entity<HeatPumpDataPerPeriod>(entity =>
            {
                entity.HasIndex(e => e.Id, "Id");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.First).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.Last).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");
            });

            modelBuilder.Entity<HeatPumpDatum>(entity =>
            {
                entity.HasIndex(e => e.Id, "Id1");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");
            });
        }

    }
}
