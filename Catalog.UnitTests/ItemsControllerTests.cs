using System;
using Catalog.Api.Repositories;
using Catalog.Api.Models;
using Catalog.Api.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Runtime.CompilerServices;
namespace Catalog.UnitTests;

public class ItemsControllerTests
{
    //  UnitOfWork_StateUnderTest_ExpectedBehavior
    private readonly Mock<IItemsRepository> repositoryStub = new();
    private readonly Mock<ILogger<ItemsController>> loggerStub = new();

    [Fact]
    public async Task GetItemAsync_NoItemExists_NotFound()
    {
        //Arrange
        repositoryStub.Setup(r => r.GetItemAsync(It.IsAny<Guid>())).ReturnsAsync((Item)null);
        var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);

        //Act
        var result = await controller.GetItemAsync(Guid.NewGuid());

        //Assert
        Assert.IsType<NotFoundResult>(result.Result);

    }
}