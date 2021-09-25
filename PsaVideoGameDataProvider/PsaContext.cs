using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PsaVideoGameCommon;

namespace PsaVideoGameDataProvider
{

    
    public class PsaContext : DbContext
    {
        public PsaContext(DbContextOptions options) : base(options)
        {
          // Database.SetInitializer()
        }
        public virtual DbSet<VideoGame> VideoGames { get; set; }
        
       // public virtual DbSet<CustomerBalanceSummary> CustomerBalanceSummaries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("Psa");
            //modelBuilder.Ignore<CustomerBalanceSummary>();

        }
    }
    public class PsaContextFactory : IDesignTimeDbContextFactory<PsaContext>
    {
      public PsaContext CreateDbContext(string[] args)
      {
        var optionsBuilder = new DbContextOptionsBuilder<PsaContext>();
        optionsBuilder.UseSqlServer($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\AngularAssignment\\PsaDb.mdf;Integrated Security=True;Connect Timeout=30");

        return new PsaContext(optionsBuilder.Options);
      }
    }
}
