using esWMS.Domain.Entities.Documents;
using esWMS.Domain.Entities.SystemActors;
using esWMS.Domain.Entities.WarehouseEnviroment;
using esWMS.Infrastructure.EntitiesConfigurations.Documents;
using esWMS.Infrastructure.EntitiesConfigurations.SystemActors;
using esWMS.Infrastructure.EntitiesConfigurations.WarehouseEnviroment;
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

            //modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly); in test project it doesn't work

            new BaseDocumentConfiguration().Configure(modelBuilder.Entity<BaseDocument>());
            new DocumentItemConfiguration().Configure(modelBuilder.Entity<DocumentItem>());
            new DocumentWarehouseUnitItemConfiguration().Configure(modelBuilder.Entity<DocumentWarehouseUnitItem>());
            new MmmConfiguration().Configure(modelBuilder.Entity<MMM>());
            new MmpConfiguration().Configure(modelBuilder.Entity<MMP>());
            new PwConfiguration().Configure(modelBuilder.Entity<PW>());
            new PzConfiguration().Configure(modelBuilder.Entity<PZ>());
            new RwConfiguration().Configure(modelBuilder.Entity<RW>());
            new WzConfiguration().Configure(modelBuilder.Entity<WZ>());
            new ZwConfiguration().Configure(modelBuilder.Entity<ZW>());
            new ContractorConfiguration().Configure(modelBuilder.Entity<Contractor>());
            new EmployeeConfiguration().Configure(modelBuilder.Entity<Employee>());
            new CategoryConfiguration().Configure(modelBuilder.Entity<Category>());
            new LocationConfiguration().Configure(modelBuilder.Entity<Location>());
            new ProductConfiguration().Configure(modelBuilder.Entity<Product>());
            new WarehouseConfiguration().Configure(modelBuilder.Entity<Warehouse>());
            new WarehouseUnitConfiguration().Configure(modelBuilder.Entity<WarehouseUnit>());
            new WarehouseUnitItemConfiguration().Configure(modelBuilder.Entity<WarehouseUnitItem>());
            new ZoneConfiguration().Configure(modelBuilder.Entity<Zone>());

        }
    }
}
