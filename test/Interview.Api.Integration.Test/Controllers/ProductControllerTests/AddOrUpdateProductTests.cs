using System.Threading.Tasks;
using AutoFixture;
using Interview.Api.Integration.Test.Setup;
using Xunit;

namespace Interview.Api.Integration.Test.Controllers.ProductsControllerTests;

[Collection(nameof(InterviewApiCollection))]
public class AddOrUpdateProductTests
{
    private const string BASE_ROUTE = "api/product";

    private static readonly Fixture _fixture = new();

    private readonly InterviewApiWebApplicationFactory _factory;

    public AddOrUpdateProductTests(InterviewApiWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact(Skip = "Not Implemented")]
    public async Task ShouldReturnOkResponseWhenProductIsValid()
    {
        //// Arrange
        //var client = _factory.CreateClient();

        //var product = _fixture.Create<Product>();

        //// Act
        //var response = await client.PostAsJsonAsync(BASE_ROUTE, product);

        //// Assert
        //response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact(Skip = "Not Implemented")]
    public async Task ShouldReturnBadRequestResponseWhenProductIsNotValid()
    {
        //// Arrange
        //var client = _factory.CreateClient();

        //var product = _fixture.Build<Product>()
        //   .Without(x => x.Name)
        //   .Create();

        //// Act
        //var response = await client.PostAsJsonAsync(BASE_ROUTE, product);

        //// Assert
        //response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
