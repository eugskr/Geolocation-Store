using GeolocationStore.Domain.RepositoryModels;
using Microsoft.EntityFrameworkCore;

namespace GeolocationStore.Infrastructure.Database
{
    public class GeolocationStoreContext: DbContext
    {
        public GeolocationStoreContext(DbContextOptions<GeolocationStoreContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<IpAddressDetails>()
                .Property(x => x.Location)
                .HasColumnType("jsonb");
        }

        public DbSet<IpAddressDetails> IpAddressDetails { get; set; }
    }
}