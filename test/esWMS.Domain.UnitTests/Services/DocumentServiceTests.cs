using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Services;
using FluentAssertions;

namespace esWMS.Domain.UnitTests.Services
{
    public class DocumentServiceTests
    {
        public static IEnumerable<object[]> ValidTestDataForGenerateDocumentId => new List<object[]>
        {
            new object[]
            {
                new PZ()
                {
                    DocumentIssueDate = new DateTime(2023, 7, 1),
                    IssueWarehouseId = "MPT"
                },
                1,
                "PZ/MPT/2023/07/01/001"
            },
            new object[]
            {
                new MMP()
                {
                    DocumentIssueDate = new DateTime(2034, 11, 8),
                    IssueWarehouseId = "W)!"
                },
                999,
                "MMP/W)!/2034/11/08/999"
            },
            new object[]
            {
                new RW()
                {
                    DocumentIssueDate = new DateTime(2024, 12, 30),
                    IssueWarehouseId = "P-1"
                },
                876,
                "RW/P-1/2024/12/30/876"
            },
        };

        [Theory]
        [MemberData(nameof(ValidTestDataForGenerateDocumentId))]
        public void GenerateDocumentId_ForValidData_ReturnsValidId(BaseDocument document, int documentNumber, string result)
        {
            string documentId = document.GenerateDocumentId(documentNumber);
            documentId.Should().Be(result);
        }

        public static IEnumerable<object[]> InvalidTestDataForGenerateDocumentId => new List<object[]>
        {
            new object[]
            {
                new PZ()
                {
                    DocumentIssueDate = new DateTime(2023, 7, 1),
                    IssueWarehouseId = "MPT"
                },
                0
            },
            new object[]
            {
                new MMM()
                {
                    DocumentIssueDate = new DateTime(2034, 11, 8),
                    IssueWarehouseId = "W)!"
                },
                -23
            },
            new object[]
            {
                new MMP()
                {
                    DocumentIssueDate = new DateTime(2034, 11, 8),
                    IssueWarehouseId = "W)!"
                },
                1000
            },
            new object[]
            {
                new MMP()
                {
                    DocumentIssueDate = new DateTime(2034, 11, 8),
                    IssueWarehouseId = "A"
                },
                11100
            },
            new object[]
            {
                new PZ()
                {
                    DocumentIssueDate = new DateTime(2023, 7, 1),
                    IssueWarehouseId = ""
                },
                1
            },
            new object[]
            {
                new MMP()
                {
                    DocumentIssueDate = default,
                    IssueWarehouseId = "MPT"
                },
                1
            },
            new object[]
            {
                new MMP()
                {
                    DocumentIssueDate = default,
                    IssueWarehouseId = ""
                },
                -1
            }
        };

        [Theory]
        [MemberData(nameof(InvalidTestDataForGenerateDocumentId))]
        public void GenerateDocumentId_ForInvalidData_ThrowsArgumentException(BaseDocument document, int documentNumber)
        {
            Action action = () => document.GenerateDocumentId(documentNumber);
            action.Should().Throw<ArgumentException>();
        }
    }
}
