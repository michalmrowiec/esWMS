using esWMS.Domain.Entities.Documents;

namespace esWMS.Infrastructure.UnitTests.Utilities.SieveProcessor.Filters.DocumentIssueDate
{
    internal class DocumentIssueDateTestsData
    {
        public static IEnumerable<object[]> TestData =>
        [
            [
                new List<PZ>()
                {
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/23/001",
                        DocumentIssueDate = new DateTime(2023, 10, 23),
                        CreatedAt = new DateTime(2023, 10, 23)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/001",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        CreatedAt = new DateTime(2023, 10, 24)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/002",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        CreatedAt = new DateTime(2023, 10, 24)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/25/001",
                        DocumentIssueDate = new DateTime(2023, 10, 25),
                        CreatedAt = new DateTime(2023, 10, 25)
                    }
                }.AsQueryable(),
                "==",
                new string[] { "2023-10-24" },
                new List<PZ>()
                {
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/001",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        CreatedAt = new DateTime(2023, 10, 24)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/002",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        CreatedAt = new DateTime(2023, 10, 24)
                    }
                }.AsQueryable()
            ],
            [
                new List<PZ>()
                {
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/23/001",
                        DocumentIssueDate = new DateTime(2023, 10, 23),
                        CreatedAt = new DateTime(2023, 10, 23)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/001",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        CreatedAt = new DateTime(2023, 10, 24)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/002",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        CreatedAt = new DateTime(2023, 10, 24)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/11/25/001",
                        DocumentIssueDate = new DateTime(2023, 11, 25),
                        CreatedAt = new DateTime(2023, 10, 25)
                    }
                }.AsQueryable(),
                "!=",
                new string[] { "2023-10-23" },
                new List<PZ>()
                {
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/001",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        CreatedAt = new DateTime(2023, 10, 24)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/002",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        CreatedAt = new DateTime(2023, 10, 24)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/11/25/001",
                        DocumentIssueDate = new DateTime(2023, 11, 25),
                        CreatedAt = new DateTime(2023, 10, 25)
                    }
                }.AsQueryable()
            ],
            [
                new List<PZ>()
                {
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/23/001",
                        DocumentIssueDate = new DateTime(2023, 10, 23),
                        CreatedAt = new DateTime(2023, 10, 23)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/001",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        CreatedAt = new DateTime(2023, 10, 24)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/002",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        CreatedAt = new DateTime(2023, 10, 24)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/11/25/001",
                        DocumentIssueDate = new DateTime(2023, 11, 25),
                        CreatedAt = new DateTime(2023, 10, 25)
                    }
                }.AsQueryable(),
                ">",
                new string[] { "2023-10-24" },
                new List<PZ>()
                {
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/11/25/001",
                        DocumentIssueDate = new DateTime(2023, 11, 25),
                        CreatedAt = new DateTime(2023, 10, 25)
                    }
                }.AsQueryable()
            ],
            [
                new List<PZ>()
                {
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/23/001",
                        DocumentIssueDate = new DateTime(2023, 10, 23),
                        CreatedAt = new DateTime(2023, 10, 23)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/001",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        CreatedAt = new DateTime(2023, 10, 24)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/002",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        CreatedAt = new DateTime(2023, 10, 24)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/11/25/001",
                        DocumentIssueDate = new DateTime(2023, 11, 25),
                        CreatedAt = new DateTime(2023, 10, 25)
                    }
                }.AsQueryable(),
                "<",
                new string[] { "2023-10-25" },
                new List<PZ>()
                {
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/23/001",
                        DocumentIssueDate = new DateTime(2023, 10, 23),
                        CreatedAt = new DateTime(2023, 10, 23)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/001",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        CreatedAt = new DateTime(2023, 10, 24)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/002",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        CreatedAt = new DateTime(2023, 10, 24)
                    }
                }.AsQueryable()
            ],
            [
                new List<PZ>()
                {
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/23/001",
                        DocumentIssueDate = new DateTime(2023, 10, 23),
                        CreatedAt = new DateTime(2023, 10, 23)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/001",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        CreatedAt = new DateTime(2023, 10, 24)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/002",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        CreatedAt = new DateTime(2023, 10, 24)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/11/25/001",
                        DocumentIssueDate = new DateTime(2023, 11, 25),
                        CreatedAt = new DateTime(2023, 10, 25)
                    }
                }.AsQueryable(),
                ">=",
                new string[] { "2023-10-24" },
                new List<PZ>()
                {
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/001",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        CreatedAt = new DateTime(2023, 10, 24)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/002",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        CreatedAt = new DateTime(2023, 10, 24)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/11/25/001",
                        DocumentIssueDate = new DateTime(2023, 11, 25),
                        CreatedAt = new DateTime(2023, 10, 25)
                    }
                }.AsQueryable()
            ],
            [
                new List<PZ>()
                {
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/23/001",
                        DocumentIssueDate = new DateTime(2023, 10, 23),
                        CreatedAt = new DateTime(2023, 10, 23)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/001",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        CreatedAt = new DateTime(2023, 10, 24)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/002",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        CreatedAt = new DateTime(2023, 10, 24)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/11/25/001",
                        DocumentIssueDate = new DateTime(2023, 11, 25),
                        CreatedAt = new DateTime(2023, 10, 25)
                    }
                }.AsQueryable(),
                "<=",
                new string[] { "2023-10-24" },
                new List<PZ>()
                {
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/23/001",
                        DocumentIssueDate = new DateTime(2023, 10, 23),
                        CreatedAt = new DateTime(2023, 10, 23)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/001",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        CreatedAt = new DateTime(2023, 10, 24)
                    },
                    new()
                    {
                        DocumentId = "PZ/MPT/2023/10/24/002",
                        DocumentIssueDate = new DateTime(2023, 10, 24),
                        CreatedAt = new DateTime(2023, 10, 24)
                    }
                }.AsQueryable()
            ]
        ];
    }
}
