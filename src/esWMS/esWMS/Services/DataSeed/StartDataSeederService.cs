using esWMS.Domain.Entities.SystemActors;
using esWMS.Domain.Entities.WarehouseEnviroment;
using esWMS.Infrastructure;

namespace esWMS.Services.DataSeed
{
    internal static class StartDataSeederService
    {
        public static async Task SeedStartData(this EsWmsDbContext dbContext)
        {
            if (!dbContext.Categories.Any())
            {
                await dbContext.Categories.AddRangeAsync(Categories);
            }

            if (!dbContext.Products.Any())
            {
                await dbContext.Products.AddRangeAsync(Products);
            }

            if (!dbContext.Warehouses.Any())
            {
                await dbContext.Warehouses.AddRangeAsync(Warehouses);
            }

            if (!dbContext.Contractors.Any())
            {
                await dbContext.Contractors.AddRangeAsync(Contractors);
            }

            await dbContext.SaveChangesAsync();
        }

        public static List<Category> Categories =
        [
            new Category
            {
                CategoryId = "CAT-001",
                CategoryName = "Narzędzia",
                CreatedAt = DateTime.Now
            },
            new Category
            {
                CategoryId = "CAT-002",
                CategoryName = "Narzędzia ręczne",
                ParentCategoryId = "CAT-001",
                CreatedAt = DateTime.Now
            },
            new Category
            {
                CategoryId = "CAT-003",
                CategoryName = "Elektronarzędzia",
                ParentCategoryId = "CAT-001",
                CreatedAt = DateTime.Now
            },
            new Category
            {
                CategoryId = "CAT-004",
                CategoryName = "Materiały",
                CreatedAt = DateTime.Now
            },
            new Category
            {
                CategoryId = "CAT-005",
                CategoryName = "Materiały produkcyjne",
                ParentCategoryId = "CAT-004",
                CreatedAt = DateTime.Now
            },
            new Category
            {
                CategoryId = "CAT-006",
                CategoryName = "Materiały eksploatacyjne",
                ParentCategoryId = "CAT-004",
                CreatedAt = DateTime.Now
            },
            new Category
            {
                CategoryId = "CAT-007",
                CategoryName = "Nośniki",
                CreatedAt = DateTime.Now
            }
        ];

        public static List<Product> Products =
        [
            new Product
            {
                ProductId = "PROD-001",
                ProductCode = "NRZ-001",
                ProductName = "Młotek ślusarski 500g",
                ShortProductName = "Młotek ślusarski 500g",
                ProductDescription = "Młotek ślusarski 500g z rękojeścią z włókna szklanego",
                CategoryId = "CAT-002",
                Unit = "szt",
                IsWeight = false,
                TotalWeight = 0.5m,
                Price = 50.00m,
                IsActive = true,
                CreatedAt = DateTime.Now
            },
            new Product
            {
                ProductId = "PROD-002",
                ProductCode = "NRZ-002",
                ProductName = "Klucz płaski 13mm chromowany",
                ShortProductName = "Klucz płaski 13mm",
                ProductDescription = "Klucz płaski 13mm chromowany, wytrzymały na korozję",
                CategoryId = "CAT-002",
                Unit = "szt",
                IsWeight = false,
                TotalWeight = 0.2m,
                Price = 20.00m,
                IsActive = true,
                CreatedAt = DateTime.Now
            },
            new Product
            {
                ProductId = "PROD-003",
                ProductCode = "ELEK-001",
                ProductName = "Wiertarka akumulatorowa 18V Li-Ion",
                ShortProductName = "Wiertarka akumulatorowa 18V",
                ProductDescription = "Wiertarka akumulatorowa 18V Li-Ion, z akumulatorem 2.0Ah",
                CategoryId = "CAT-003",
                Unit = "szt",
                IsWeight = false,
                TotalWeight = 2m,
                Price = 350.00m,
                IsActive = true,
                CreatedAt = DateTime.Now
            },
            new Product
            {
                ProductId = "PROD-004",
                ProductCode = "ELEK-002",
                ProductName = "Szlifierka kątowa 125mm 1000W",
                ShortProductName = "Szlifierka kątowa 125mm",
                ProductDescription = "Szlifierka kątowa 125mm, moc 1000W, z systemem antywibracyjnym",
                CategoryId = "CAT-003",
                Unit = "szt",
                IsWeight = false,
                TotalWeight = 2.5m,
                Price = 500.00m,
                IsActive = true,
                CreatedAt = DateTime.Now
            },
            new Product
            {
                ProductId = "PROD-005",
                ProductCode = "MP-001",
                ProductName = "Blacha stalowa ocynkowana 2mm",
                ShortProductName = "Blacha stalowa ocynkowana 2mm",
                ProductDescription = "Blacha stalowa ocynkowana 2mm, wymiary 2000x1000mm, gatunek S235JR",
                CategoryId = "CAT-005",
                Unit = "szt",
                IsWeight = false,
                TotalWeight = 10m,
                Price = 500.00m,
                IsActive = true,
                CreatedAt = DateTime.Now
            },
            new Product
            {
                ProductId = "PROD-006",
                ProductCode = "MP-002",
                ProductName = "Profil aluminiowy 40x40mm",
                ShortProductName = "Profil aluminiowy 40x40mm",
                ProductDescription = "Profil aluminiowy 40x40mm, anodowany, długość 6m",
                CategoryId = "CAT-005",
                Unit = "szt",
                IsWeight = false,
                TotalWeight = 5m,
                Price = 300.00m,
                IsActive = true,
                CreatedAt = DateTime.Now
            },
            new Product
            {
                ProductId = "PROD-007",
                ProductCode = "ME-001",
                ProductName = "Papier ścierny P80 230x280mm",
                ShortProductName = "Papier ścierny P80",
                ProductDescription = "Papier ścierny P80, format 230x280mm, do obróbki drewna",
                CategoryId = "CAT-006",
                Unit = "szt",
                IsWeight = false,
                TotalWeight = 0.01m,
                Price = 1.00m,
                IsActive = true,
                CreatedAt = DateTime.Now
            },
            new Product
            {
                ProductId = "PROD-008",
                ProductCode = "ME-002",
                ProductName = "Olej maszynowy Shell Tellus S2 M 46",
                ShortProductName = "Olej maszynowy Shell Tellus 20L",
                ProductDescription = "Olej maszynowy Shell Tellus S2 M 46, opakowanie 20L",
                CategoryId = "CAT-006",
                Unit = "szt",
                IsWeight = false,
                TotalWeight = 20m,
                Price = 10.00m,
                IsActive = true,
                CreatedAt = DateTime.Now
            },
            new Product
            {
                ProductId = "PROD-009",
                ProductCode = "ME-003",
                ProductName = "Filtr przemysłowy Donaldson P191280",
                ShortProductName = "Filtr Donaldson P191280",
                ProductDescription = "Filtr przemysłowy Donaldson P191280, do sprężarek powietrza",
                CategoryId = "CAT-006",
                Unit = "szt",
                IsWeight = false,
                TotalWeight = 0.3m,
                Price = 50.00m,
                IsActive = true,
                CreatedAt = DateTime.Now
            },
            new Product
            {
                ProductId = "PROD-010",
                ProductCode = "ME-004",
                ProductName = "Smar techniczny Molykote 111",
                ShortProductName = "Smar techniczny Molykote 111",
                ProductDescription = "Smar techniczny Molykote 111, do uszczelek i zaworów, opakowanie 1kg",
                CategoryId = "CAT-006",
                Unit = "szt",
                IsWeight = false,
                TotalWeight = 1m,
                Price = 20.00m,
                IsActive = true,
                CreatedAt = DateTime.Now
            },
            new Product
            {
                ProductId = "PROD-011",
                ProductCode = "NOS-001",
                ProductName = "Europaleta EPAL 1200x800mm",
                ShortProductName = "Europaleta EPAL 1200x800mm",
                ProductDescription = "Europaleta EPAL o wymiarach 1200x800mm, nośność 1500 kg",
                CategoryId = "CAT-007",
                Unit = "szt",
                IsWeight = false,
                TotalWeight = 25m,
                TotalHeight = 0.144m,
                TotalWidth = 0.8m,
                TotalLength = 1.2m,
                IsMedia = true,
                Price = 70.00m,
                IsActive = true,
                CreatedAt = DateTime.Now
            }
        ];


        public static List<Warehouse> Warehouses =
        [
            new Warehouse
            {
                WarehouseId = "MWS",
                WarehouseName = "Magazyn Wysokiego Składowania",
                CreatedAt = DateTime.Now
            },
            new Warehouse
            {
                WarehouseId = "MPT",
                WarehouseName = "Magazyn Przyjęcia Towaru",
                CreatedAt = DateTime.Now
            },
            new Warehouse
            {
                WarehouseId = "MWT",
                WarehouseName = "Magazyn Wydania Towaru",
                CreatedAt = DateTime.Now
            },
            new Warehouse
            {
                WarehouseId = "MPR",
                WarehouseName = "Magazyn Produkcji",
                CreatedAt = DateTime.Now
            }
        ];

        public static List<Contractor> Contractors =
        [
            new Contractor
            {
                ContractorId = "PTK",
                ContractorName = "Pierwszy Testowy Kontrahent",
                IsSupplier = true,
                IsRecipient = true,
                IsActive = true,
                CreatedAt= DateTime.Now
            },
            new Contractor
            {
                ContractorId = "DTK",
                ContractorName = "Drugi Testowy Kontrahent",
                IsSupplier = true,
                IsRecipient = true,
                IsActive = true,
                CreatedAt= DateTime.Now
            }
        ];
    }
}
