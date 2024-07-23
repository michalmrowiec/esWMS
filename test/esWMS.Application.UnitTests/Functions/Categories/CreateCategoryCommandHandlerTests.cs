using AutoMapper;
using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Functions.Categories;
using esWMS.Application.Functions.Categories.Commands;
using esWMS.Application.Functions.Responses;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esWMS.Application.UnitTests.Functions.Categories
{
    public class CreateCategoryCommandHandlerTests
    {
        public static IEnumerable<object[]> ValidData => new List<object[]>
        {
            new object[]
            {
                new CreateCategoryCommand()
                {
                    CategoryName = "C1"
                },
                new Category()
                {
                    CategoryId = new Guid("00000001-0000-0000-0000-122000000000").ToString(),
                    CategoryName = "C1",
                    CreatedAt = new DateTime(2023, 10, 23)
                },
                new CategoryDto()
                {
                    CategoryId = new Guid("00000001-0000-0000-0000-122000000000").ToString(),
                    CategoryName = "C1",
                }
            }
        };

        [Theory]
        [MemberData(nameof(ValidData))]
        public async Task CreateCategoryCommandHandler_ForValidData_ReturnsSucced
            (CreateCategoryCommand categoryCommand, Category category, CategoryDto categoryDto)
        {
            var repositoryMock = new Mock<ICategoryRepository>();
            repositoryMock.Setup(m => m.CreateAsync(It.IsAny<Category>()))
                .ReturnsAsync(category);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<Category>(It.IsAny<CreateCategoryCommand>()))
                .Returns(category);
            mapperMock.Setup(m => m.Map<CategoryDto>(It.IsAny<Category>()))
                .Returns(categoryDto);

            var handler = new CreateCategoryCommandHandler(
                new CreateCategoryValidator(), repositoryMock.Object, mapperMock.Object);

            BaseResponse<CategoryDto> response = (BaseResponse<CategoryDto>)await handler.Handle(categoryCommand, new CancellationToken());

            response.Success.Should().BeTrue();
            response.Message.Should().BeNull();
            response.ValidationErrors.Should().BeEmpty();
            response.ReturnedObj.Should().BeEquivalentTo(categoryDto);
        }
    }
}
