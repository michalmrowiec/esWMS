using esWMS.Application.Functions.Documents.PzFunctions;
using esWMS.Domain.Entities.Documents;
using esWMS.Domain.Entities.SystemActors;
using esWMS.Domain.Entities.WarehouseEnviroment;
using esWMS.Domain.Models;
using Sieve.Models;

namespace esWMS.API.IntegrationTests.Controllers.Documents.TestData
{
    internal class GetSortedAndFilteredPzTestData
    {
        public static IEnumerable<object[]> ValidaData =>
        [
            [
                new Warehouse[]
                {
                    new() { WarehouseId = "MPT", WarehouseName = "MPT-test", CreatedAt = new DateTime(2020, 01, 01) },
                    new() { WarehouseId = "PTD", WarehouseName = "PTD-test", CreatedAt = new DateTime(2020, 01, 01) },
                    new() { WarehouseId = "KPL", WarehouseName = "KPL-test", CreatedAt = new DateTime(2020, 01, 01) },
                },
                new Contractor[]
                {
                    new() { ContractorId = "CPL", ContractorName = "CPL-test", CreatedAt = new DateTime(2020, 01, 01) },
                    new() { ContractorId = "ZXC", ContractorName = "ZXC-test", CreatedAt = new DateTime(2020, 01, 01) },
                    new() { ContractorId = "ABC", ContractorName = "ABC-test", CreatedAt = new DateTime(2020, 01, 01) },
                    new() { ContractorId = "GBC", ContractorName = "GBC-test", CreatedAt = new DateTime(2020, 01, 01) },
                    new() { ContractorId = "POL", ContractorName = "POL-test", CreatedAt = new DateTime(2020, 01, 01) }
                },
                new PZ[]
                {
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/011",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "CPL",
                        IssueWarehouseId = "MPT",
                        IsApproved = true,
                        CreatedAt = new DateTime(2023, 10, 24)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/001",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "ZXC",
                        IssueWarehouseId = "MPT",
                        IsApproved = false,
                        CreatedAt = new DateTime(2023, 10, 24)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/23/004",
                        DocumentIssueDate = new DateTime(2023, 10, 23),
                        SupplierContractorId = "ABC",
                        IssueWarehouseId = "MPT",
                        IsApproved = false,
                        CreatedAt = new DateTime(2023, 10, 23)
                    },
                    new()
                    {
                        DocumentId = "PZ/PTD/2024/04/01/022",
                        DocumentIssueDate = new DateTime(2024, 04, 01),
                        SupplierContractorId = "GBC",
                        IssueWarehouseId = "PTD",
                        IsApproved = true,
                        CreatedAt = new DateTime(2024, 04, 01)
                    },
                    new()
                    {
                        DocumentId = "PZ/KPL/2023/10/24/002",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "ABC",
                        IssueWarehouseId = "KPL",
                        IsApproved = false,
                        CreatedAt = new DateTime(2023, 10, 24)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/25/001",
                        DocumentIssueDate = new DateTime(2023, 10, 25),
                        SupplierContractorId = "POL",
                        IssueWarehouseId = "MPT",
                        IsApproved = true,
                        CreatedAt = new DateTime(2023, 10, 25)
                    }
                },
                new SieveModel()
                {
                    Filters = "DocumentIssueDate<2024-01-01",
                    Sorts = "-SupplierContractorId,DocumentIssueDate",
                    Page = 1,
                    PageSize = 2
                },
                new PagedResult<PzDto>()
                {
                    TotalPages = 3,
                    TotalItems = 5,
                    ItemsFrom = 1,
                    ItemsTo = 2,
                    Items =
                    [
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/001",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "ZXC",
                        IssueWarehouseId = "MPT",
                        IsApproved = false,
                        IssueWarehouse = new() { WarehouseId = "MPT", WarehouseName = "MPT-test" },
                        SupplierContractor = new() { ContractorId = "ZXC", ContractorName = "ZXC-test" }
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/25/001",
                        DocumentIssueDate = new DateTime(2023, 10, 25),
                        SupplierContractorId = "POL",
                        IssueWarehouseId = "MPT",
                        IsApproved = true,
                        IssueWarehouse = new() { WarehouseId = "MPT", WarehouseName = "MPT-test" },
                        SupplierContractor = new() { ContractorId = "POL", ContractorName = "POL-test" }
                    }
                    ]
                }
            ]
        ];
    }
}
