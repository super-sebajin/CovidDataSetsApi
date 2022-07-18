
using Microsoft.EntityFrameworkCore;

namespace CovidDataSetsApi.DataAccessLayer
{

    public class CovidDataSetsDbContext : DbContext
    {
        public CovidDataSetsDbContext()  : base()
        {
        }

        public CovidDataSetsDbContext(DbContextOptions<CovidDataSetsDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        public virtual DbSet<CovidDataSets>  CovidDataSets { get; set; }
        public virtual DbSet<CovidCasesOverTimeUsa> CovidCasesOverTimeUsa { get; set; }

    }
}
