using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Interview.Api.Integration.Test.Setup;
using Interview.Data.Models;
using Xunit;

namespace Interview.Api.Integration.Test.Controllers.CustomerControllerTests;

[Collection(nameof(InterviewApiCollection))]
public class AddOrUpdateCustomerTests
{
    private const string BASE_ROUTE = "api/customer";

    private static readonly Fixture _fixture = new();

    private readonly InterviewApiWebApplicationFactory _factory;

    public AddOrUpdateCustomerTests(InterviewApiWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ShouldReturnOkResponseWhenCustomerIsValid()
    {
        // Arrange
        var client = _factory.CreateClient();

        var customer = _fixture.Create<Customer>();

        // Act
        var response = await client.PostAsJsonAsync(BASE_ROUTE, customer);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task ShouldReturnBadRequestResponseWhenCustomerIsNotValid()
    {
        // Arrange
        var client = _factory.CreateClient();

        var customer = _fixture.Build<Customer>() 
           .Without(x => x.Email)
           .Create();

        // Act
        var response = await client.PostAsJsonAsync(BASE_ROUTE, customer);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
