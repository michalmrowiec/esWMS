using esWMS.Domain.Entities.Documents;
using esWMS.Domain.Services;
using FluentAssertions;

namespace esWMS.Domain.UnitTests.Services.DocumentService
{
    public class DocumentServiceTests
    {
        [Theory]
        [MemberData(
            nameof(DocumentServiceTestsData.ValidTestDataForGenerateDocumentId),
            MemberType = typeof(DocumentServiceTestsData))]
        public void GenerateDocumentId_ForValidData_ReturnsValidId(
            BaseDocument document,
            string[] documentIds,
            string result)
        {
            string documentId = document.GenerateDocumentId(documentIds);
            documentId.Should().Be(result);
        }

        [Theory]
        [MemberData(
            nameof(DocumentServiceTestsData.InvalidTestDataForGenerateDocumentId),
            MemberType = typeof(DocumentServiceTestsData))]
        public void GenerateDocumentId_ForInvalidData_ThrowsArgumentException(
            BaseDocument document,
            string[] documentIds)
        {
            Action action = () => document.GenerateDocumentId(documentIds);

            action.Should().Throw<ArgumentException>();
        }
    }
}
