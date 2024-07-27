﻿using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Infrastructure;

namespace esWMS.Services
{
    internal static class StartDataSeederService
    {
        public static void SeedStartData(this EsWmsDbContext dbContext)
        {
            if (!dbContext.Categories.Any())
            {
                dbContext.Categories.AddRange(Categories);
            }

            if (!dbContext.Products.Any())
            {
                dbContext.Products.AddRange(Products);
            }

            dbContext.SaveChanges();
        }

        public static List<Category> Categories = new List<Category>
        {
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
        };

        public static List<Product> Products = new List<Product>
        {
            new Product
            {
                ProductId = "PROD-001",
                ProductCode = "NRZ-001",
                ProductName = "Młotek ślusarski 500g",
                ProductDescription = "Młotek ślusarski 500g z rękojeścią z włókna szklanego",
                CategoryId = "CAT-002",
                Unit = "szt",
                IsWeight = false,
                WeightPerUnit = 500,
                Price = 50.00m,
                IsActive = true,
                CreatedAt = DateTime.Now
            },
            new Product
            {
                ProductId = "PROD-002",
                ProductCode = "NRZ-002",
                ProductName = "Klucz płaski 13mm chromowany",
                ProductDescription = "Klucz płaski 13mm chromowany, wytrzymały na korozję",
                CategoryId = "CAT-002",
                Unit = "szt",
                IsWeight = false,
                WeightPerUnit = 200,
                Price = 20.00m,
                IsActive = true,
                CreatedAt = DateTime.Now
            },
            new Product
            {
                ProductId = "PROD-003",
                ProductCode = "ELEK-001",
                ProductName = "Wiertarka akumulatorowa 18V Li-Ion",
                ProductDescription = "Wiertarka akumulatorowa 18V Li-Ion, z akumulatorem 2.0Ah",
                CategoryId = "CAT-003",
                Unit = "szt",
                IsWeight = false,
                WeightPerUnit = 2000,
                Price = 350.00m,
                IsActive = true,
                CreatedAt = DateTime.Now
            },
            new Product
            {
                ProductId = "PROD-004",
                ProductCode = "ELEK-002",
                ProductName = "Szlifierka kątowa 125mm 1000W",
                ProductDescription = "Szlifierka kątowa 125mm, moc 1000W, z systemem antywibracyjnym",
                CategoryId = "CAT-003",
                Unit = "szt",
                IsWeight = false,
                WeightPerUnit = 2500,
                Price = 500.00m,
                IsActive = true,
                CreatedAt = DateTime.Now
            },
            new Product
            {
                ProductId = "PROD-005",
                ProductCode = "MP-001",
                ProductName = "Blacha stalowa ocynkowana 2mm",
                ProductDescription = "Blacha stalowa ocynkowana 2mm, wymiary 2000x1000mm, gatunek S235JR",
                CategoryId = "CAT-005",
                Unit = "szt",
                IsWeight = false,
                WeightPerUnit = 10000,
                Price = 500.00m,
                IsActive = true,
                CreatedAt = DateTime.Now
            },
            new Product
            {
                ProductId = "PROD-006",
                ProductCode = "MP-002",
                ProductName = "Profil aluminiowy 40x40mm",
                ProductDescription = "Profil aluminiowy 40x40mm, anodowany, długość 6m",
                CategoryId = "CAT-005",
                Unit = "szt",
                IsWeight = false,
                WeightPerUnit = 5000,
                Price = 300.00m,
                IsActive = true,
                CreatedAt = DateTime.Now
            },
            new Product
            {
                ProductId = "PROD-007",
                ProductCode = "ME-001",
                ProductName = "Papier ścierny P80 230x280mm",
                ProductDescription = "Papier ścierny P80, format 230x280mm, do obróbki drewna",
                CategoryId = "CAT-006",
                Unit = "szt",
                IsWeight = false,
                Price = 1.00m,
                IsActive = true,
                CreatedAt = DateTime.Now
            },
            new Product
            {
                ProductId = "PROD-008",
                ProductCode = "ME-002",
                ProductName = "Olej maszynowy Shell Tellus S2 M 46",
                ProductDescription = "Olej maszynowy Shell Tellus S2 M 46, opakowanie 20L",
                CategoryId = "CAT-006",
                Unit = "szt",
                IsWeight = false,
                WeightPerUnit = 20,
                Price = 10.00m,
                IsActive = true,
                CreatedAt = DateTime.Now
            },
            new Product
            {
                ProductId = "PROD-009",
                ProductCode = "ME-003",
                ProductName = "Filtr przemysłowy Donaldson P191280",
                ProductDescription = "Filtr przemysłowy Donaldson P191280, do sprężarek powietrza",
                CategoryId = "CAT-006",
                Unit = "szt",
                IsWeight = false,
                WeightPerUnit = 300,
                Price = 50.00m,
                IsActive = true,
                CreatedAt = DateTime.Now
            },
            new Product
            {
                ProductId = "PROD-010",
                ProductCode = "ME-004",
                ProductName = "Smar techniczny Molykote 111",
                ProductDescription = "Smar techniczny Molykote 111, do uszczelek i zaworów, opakowanie 1kg",
                CategoryId = "CAT-006",
                Unit = "szt",
                IsWeight = false,
                WeightPerUnit = 1,
                Price = 20.00m,
                IsActive = true,
                CreatedAt = DateTime.Now
            },
            new Product
            {
                ProductId = "PROD-011",
                ProductCode = "NOS-001",
                ProductName = "Europaleta EPAL 1200x800mm",
                ProductDescription = "Europaleta EPAL o wymiarach 1200x800mm, nośność 1500 kg",
                CategoryId = "CAT-007",
                Unit = "szt",
                IsWeight = false,
                WeightPerUnit = 25000,
                IsMedia = true,
                Price = 70.00m,
                IsActive = true,
                CreatedAt = DateTime.Now
            }
        };
    }
}
