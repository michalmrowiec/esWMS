using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Entities.SystemActors;
using esMWS.Domain.Entities.WarehouseEnviroment;
using Microsoft.EntityFrameworkCore;

namespace esWMS.Infrastructure
{
    public class EsWmsDbContext : DbContext
    {
        public DbSet<DocumentBase> Documents { get; set; }
        public DbSet<PZ> PZ { get; set; }
        public DbSet<PW> PW { get; set; }
        public DbSet<ZW> ZW { get; set; }
        public DbSet<MMP> MMP { get; set; }
        public DbSet<WZ> WZ { get; set; }
        public DbSet<RW> RW { get; set; }
        public DbSet<MMM> MMM { get; set; }
        public DbSet<DocumentItem> DocumentItems { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<WarehouseUnit> WarehouseUnits { get; set; }
        public DbSet<WarehouseUnitItem> WarehouseUnitItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public EsWmsDbContext(DbContextOptions<EsWmsDbContext> dbContextOptions)
            : base(dbContextOptions)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
