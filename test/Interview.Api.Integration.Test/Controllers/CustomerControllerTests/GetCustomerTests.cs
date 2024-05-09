using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Interview.Api.Integration.Test.Setup;
using Interview.Data.Contexts;
using Interview.Data.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Interview.Api.Integration.Test.Controllers.CustomerControllerTests;

[Collection(nameof(InterviewApiCollection))]
public class GetCustomerTests
{
    private const string BASE_ROUTE = "api/customer";

    private static readonly Fixture _fixture = new();

    private readonly InterviewApiWebApplicationFactory _factory;

    public GetCustomerTests(InterviewApiWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ShouldReturnOkResponseWithCustomerWhenEmailMatchesAnExistingCustomer()
    {
        // Arrange
        var client = _factory.CreateClient();

        using var serviceScope = _factory.Services.CreateScope();

        var context = serviceScope.ServiceProvider.GetRequiredService<InterviewContext>();

        var CustomerEntity = _fixture.Create<CustomerEntity>();

        await context.Customers.AddAsync(CustomerEntity);
        await context.SaveChangesAsync();

        // Act
        var response = await client.GetAsync($"{BASE_ROUTE}?email={CustomerEntity.Email}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task ShouldReturnNotFoundResponseWhenEmailDoesNotMatchAnExistingCustomer()
    {
        // Arrange
        var client = _factory.CreateClient();

        var email = _fixture.Create<string>();

        // Act
        var response = await client.GetAsync($"{BASE_ROUTE}?email={email}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
