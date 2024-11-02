using esWMS.Domain.Entities.Documents;

namespace esWMS.Infrastructure.UnitTests.Utilities.SieveProcessor.SortMethods.DocumentIssueDateSort
{
    internal class DocumentIssueDateTestsSortData
    {
        public static IEnumerable<object[]> TestData =>
        [
            [
                new List<PZ>()
                {
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/011",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "CPL"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/001",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "ZXC"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/23/004",
                        DocumentIssueDate = new DateTime(2023, 10, 23),
                        SupplierContractorId = "ABC"
                    },
                    new()
                    {
                        DocumentId = "PZ/PTD/2024/04/01/022",
                        DocumentIssueDate = new DateTime(2024, 04, 01),
                        SupplierContractorId = "GBC"
                    },
                    new()
                    {
                        DocumentId = "PZ/KPL/2023/10/24/002",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "ABC"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/25/001",
                        DocumentIssueDate = new DateTime(2023, 10, 25),
                        SupplierContractorId = "POL"
                    }
                }.AsQueryable(),
                "false",
                "false",
                new List<PZ>()
                {
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/23/004",
                        DocumentIssueDate = new DateTime(2023, 10, 23),
                        SupplierContractorId = "ABC"
                    },
                    new()
                    {
                        DocumentId = "PZ/KPL/2023/10/24/002",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "ABC"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/001",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "ZXC"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/011",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "CPL"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/25/001",
                        DocumentIssueDate = new DateTime(2023, 10, 25),
                        SupplierContractorId = "POL"
                    },
                    new()
                    {
                        DocumentId = "PZ/PTD/2024/04/01/022",
                        DocumentIssueDate = new DateTime(2024, 04, 01),
                        SupplierContractorId = "GBC"
                    }
                }.AsQueryable()
            ],
            [
                new List<PZ>()
                {
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/011",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "CPL"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/001",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "ZXC"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/23/004",
                        DocumentIssueDate = new DateTime(2023, 10, 23),
                        SupplierContractorId = "ABC"
                    },
                    new()
                    {
                        DocumentId = "PZ/PTD/2024/04/01/022",
                        DocumentIssueDate = new DateTime(2024, 04, 01),
                        SupplierContractorId = "GBC"
                    },
                    new()
                    {
                        DocumentId = "PZ/KPL/2023/10/24/002",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "ABC"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/25/001",
                        DocumentIssueDate = new DateTime(2023, 10, 25),
                        SupplierContractorId = "POL"
                    }
                }.AsQueryable(),
                "false",
                "true",
                new List<PZ>()
                {
                    new()
                    {
                        DocumentId = "PZ/PTD/2024/04/01/022",
                        DocumentIssueDate = new DateTime(2024, 04, 01),
                        SupplierContractorId = "GBC"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/25/001",
                        DocumentIssueDate = new DateTime(2023, 10, 25),
                        SupplierContractorId = "POL"
                    },
                    new()
                    {
                        DocumentId = "PZ/KPL/2023/10/24/002",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "ABC"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/001",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "ZXC"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/011",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "CPL"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/23/004",
                        DocumentIssueDate = new DateTime(2023, 10, 23),
                        SupplierContractorId = "ABC"
                    }
                }.AsQueryable()
            ],
            [
                new List<PZ>()
                {
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/23/004",
                        DocumentIssueDate = new DateTime(2023, 10, 23),
                        SupplierContractorId = "ABC"
                    },
                    new()
                    {
                        DocumentId = "PZ/KPL/2023/10/24/002",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "ABC"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/011",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "CPL"
                    },
                    new()
                    {
                        DocumentId = "PZ/PTD/2024/04/01/022",
                        DocumentIssueDate = new DateTime(2024, 04, 01),
                        SupplierContractorId = "GBC"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/25/001",
                        DocumentIssueDate = new DateTime(2023, 10, 25),
                        SupplierContractorId = "POL"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/001",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "ZXC"
                    }
                }.AsQueryable().OrderBy(x => x.SupplierContractorId),
                "true",
                "false",
                new List<PZ>()
                {
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/23/004",
                        DocumentIssueDate = new DateTime(2023, 10, 23),
                        SupplierContractorId = "ABC"
                    },
                    new()
                    {
                        DocumentId = "PZ/KPL/2023/10/24/002",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "ABC"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/011",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "CPL"
                    },
                    new()
                    {
                        DocumentId = "PZ/PTD/2024/04/01/022",
                        DocumentIssueDate = new DateTime(2024, 04, 01),
                        SupplierContractorId = "GBC"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/25/001",
                        DocumentIssueDate = new DateTime(2023, 10, 25),
                        SupplierContractorId = "POL"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/001",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "ZXC"
                    }
                }.AsQueryable()
            ],
            [
                new List<PZ>()
                {
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/23/004",
                        DocumentIssueDate = new DateTime(2023, 10, 23),
                        SupplierContractorId = "ABC"
                    },
                    new()
                    {
                        DocumentId = "PZ/KPL/2023/10/24/002",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "ABC"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/011",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "CPL"
                    },
                    new()
                    {
                        DocumentId = "PZ/PTD/2024/04/01/022",
                        DocumentIssueDate = new DateTime(2024, 04, 01),
                        SupplierContractorId = "GBC"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/25/001",
                        DocumentIssueDate = new DateTime(2023, 10, 25),
                        SupplierContractorId = "POL"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/001",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "ZXC"
                    }
                }.AsQueryable().OrderBy(x => x.SupplierContractorId),
                "true",
                "true",
                new List<PZ>()
                {
                    new()
                    {
                        DocumentId = "PZ/KPL/2023/10/24/002",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "ABC"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/23/004",
                        DocumentIssueDate = new DateTime(2023, 10, 23),
                        SupplierContractorId = "ABC"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/011",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "CPL"
                    },
                    new()
                    {
                        DocumentId = "PZ/PTD/2024/04/01/022",
                        DocumentIssueDate = new DateTime(2024, 04, 01),
                        SupplierContractorId = "GBC"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/25/001",
                        DocumentIssueDate = new DateTime(2023, 10, 25),
                        SupplierContractorId = "POL"
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/001",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        SupplierContractorId = "ZXC"
                    }
                }.AsQueryable()
            ]
        ];
    }
}
