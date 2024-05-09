using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Interview.Api.Controllers;
using Interview.Data.Models;
using Interview.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Interview.Api.Test.Controllers;

public class CustomerControllerTests
{
    private static readonly Fixture _fixture = new();

    private readonly Mock<ICustomerService> _mockUserPreferenceService;

    private readonly CustomerController _controller;

    public CustomerControllerTests()
    {
        _mockUserPreferenceService = new Mock<ICustomerService>();

        _controller = new CustomerController(_mockUserPreferenceService.Object);
    }

    [Fact]
    public async Task AddOrUpdateCustomer_ShouldReturnOkResult()
    {
        // Arrange
        var customer = _fixture.Create<Customer>();

        _mockUserPreferenceService.Setup(x => x.AddOrUpdateCustomerAsync(customer));

        // Act
        var result = await _controller.AddOrUpdateCustomer(customer) as OkResult;

        // Assert
        result.Should().NotBeNull(); 

        _mockUserPreferenceService.Verify(x => x.AddOrUpdateCustomerAsync(customer), Times.Once);
    }

    [Fact]
    public async Task GetCustomer_ShouldReturnNotFoundResultWhenUserPreferenceServiceReturnsANullCustomer()
    {
        // Arrange
        var email = _fixture.Create<string>();

        _mockUserPreferenceService.Setup(x => x.GetCustomerAsync(email)).ReturnsAsync((Customer)null);

        // Act
        var result = await _controller.GetCustomer(email) as NotFoundResult;

        // Assert
        result.Should().NotBeNull();

        _mockUserPreferenceService.Verify(x => x.GetCustomerAsync(email), Times.Once);
    }

    [Fact]
    public async Task GetCustomer_ShouldReturnOkObjectResultWhenUserPreferenceServiceReturnsANullCustomer()
    {
        // Arrange
        var email = _fixture.Create<string>();

        var customer = _fixture.Create<Customer>();

        _mockUserPreferenceService.Setup(x => x.GetCustomerAsync(email)).ReturnsAsync(customer);

        // Act
        var result = await _controller.GetCustomer(email) as OkObjectResult;

        // Assert
        result.Should().NotBeNull();

        result.Value.Should().Be(customer);

        _mockUserPreferenceService.Verify(x => x.GetCustomerAsync(email), Times.Once);
    }
}
