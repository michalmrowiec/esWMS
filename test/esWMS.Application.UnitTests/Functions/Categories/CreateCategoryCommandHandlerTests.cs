﻿using AutoMapper;
using esWMS.Domain.Entities.WarehouseEnviroment;
using esWMS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Functions.Categories;
using esWMS.Application.Functions.Categories.Commands.CreateCategory;
using esWMS.Application.Functions.Categories.Queries.GetSortedFilteredCategories;
using esWMS.Application.Responses;
using FluentAssertions;
using MediatR;
using Moq;

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

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetSortedFilteredCategoriesQuery>(), new CancellationToken()))
                .ReturnsAsync(new BaseResponse<PagedResult<CategoryDto>>(new PagedResult<CategoryDto>([], 1, 1, 1)));

            var handler = new CreateCategoryCommandHandler(repositoryMock.Object, mapperMock.Object, mediatorMock.Object);

            BaseResponse<CategoryDto> response = await handler.Handle(categoryCommand, new CancellationToken());

            response.IsSuccess().Should().BeTrue();
            response.Message.Should().BeNull();
            response.ValidationErrors.Should().BeEmpty();
            response.ReturnedObj.Should().BeEquivalentTo(categoryDto);
        }
    }
}
