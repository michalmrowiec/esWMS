﻿using esWMS.Domain.Entities.Documents;

namespace esWMS.Domain.UnitTests.Services.DocumentService
{
    internal static class DocumentServiceTestsData
    {
        public static IEnumerable<object[]> ValidTestDataForGenerateDocumentId =>
        [
            [
                new PZ()
                {
                    DocumentIssueDate = new DateTime(2023, 7, 1),
                    IssueWarehouseId = "MPT"
                },
                new string[] { },
                "PZ/MPT/2023/07/01/001"
            ],
            [
                new MMP()
                {
                    DocumentIssueDate = new DateTime(2034, 11, 8),
                    IssueWarehouseId = "W10"
                },
                new string[] { "MMP/W10/2034/11/08/996", "MMP/W10/2034/11/08/998", "MMP/W10/2034/11/08/100", "MM+/W10/2034/11/08/102" },
                "MM+/W10/2034/11/08/999"
            ],
            [
                new MMM()
                {
                    DocumentIssueDate = new DateTime(2034, 11, 8),
                    IssueWarehouseId = "WPO"
                },
                new string[] { "MM-/WPO/2034/11/08/996", "MMM/WPO/2034/11/08/998", "MM-/WPO/2034/11/08/101", "MMM/WPO/2034/11/08/100" },
                "MM-/WPO/2034/11/08/999"
            ],
            [
                new RW()
                {
                    DocumentIssueDate = new DateTime(2024, 12, 30),
                    IssueWarehouseId = "PA1"
                },
                new string[] { "RW/PA1/2024/12/30/873", "RW/PA1/2024/12/30/874", "RW/PA1/2024/12/30/875" },
                "RW/PA1/2024/12/30/876"
            ],
        ];

        public static IEnumerable<object[]> InvalidTestDataForGenerateDocumentId => new List<object[]>
        {
            new object[]
            {
                new PZ()
                {
                    DocumentIssueDate = new DateTime(2023, 7, 1),
                    IssueWarehouseId = "MPT"
                },
                new string[] { "INVALID", "BADID" }
            },
            new object[]
            {
                new MMM()
                {
                    DocumentIssueDate = new DateTime(2034, 11, 8),
                    IssueWarehouseId = "W)!"
                },
                new string[] { "DOC/1001" }
            },
            new object[]
            {
                new MMP()
                {
                    DocumentIssueDate = new DateTime(2034, 11, 8),
                    IssueWarehouseId = "W)!"
                },
                new string[] { "DOC/0" }
            },
            new object[]
            {
                new MMP()
                {
                    DocumentIssueDate = new DateTime(2034, 11, 8),
                    IssueWarehouseId = "A"
                },
                new string[] { "DOC/001" }
            },
            new object[]
            {
                new PZ()
                {
                    DocumentIssueDate = new DateTime(2023, 7, 1),
                    IssueWarehouseId = ""
                },
                new string[] { "DOC/001" }
            },
            new object[]
            {
                new MMP()
                {
                    DocumentIssueDate = default,
                    IssueWarehouseId = "MPT"
                },
                new string[] { "DOC/001" }
            },
            new object[]
            {
                new MMP()
                {
                    DocumentIssueDate = default,
                    IssueWarehouseId = ""
                },
                new string[] { "DOC/001" }
            },
            new object[]
            {
                new PZ()
                {
                    DocumentIssueDate = new DateTime(2023, 7, 1),
                    IssueWarehouseId = "MPT"
                },
                null
            }
        };
    }
}