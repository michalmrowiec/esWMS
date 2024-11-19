using esWMS.Domain.Entities.Documents;
using esWMS.Domain.Entities.SystemActors;
using esWMS.Domain.Entities.WarehouseEnviroment;
using esWMS.Infrastructure.EntitiesConfigurations.Documents;
using Microsoft.EntityFrameworkCore;

namespace esWMS.Infrastructure
{
    public class EsWmsDbContext : DbContext
    {
        public DbSet<BaseDocument> Documents { get; set; }
        public DbSet<PZ> PZ { get; set; }
        public DbSet<PW> PW { get; set; }
        public DbSet<ZW> ZW { get; set; }
        public DbSet<MMP> MMP { get; set; }
        public DbSet<WZ> WZ { get; set; }
        public DbSet<RW> RW { get; set; }
        public DbSet<MMM> MMM { get; set; }
        public DbSet<DocumentItem> DocumentItems { get; set; }
        public DbSet<DocumentWarehouseUnitItem> DocumentWarehouseUnitItems { get; set; }
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

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseDocumentConfiguration).Assembly);
        }
    }
}
