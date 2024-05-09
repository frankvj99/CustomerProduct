using System.Threading.Tasks;
using AutoFixture;
using Interview.Api.Integration.Test.Setup;
using Xunit;

namespace Interview.Api.Integration.Test.Controllers.ProductsControllerTests;

[Collection(nameof(InterviewApiCollection))]
public class GetProductTests
{
    private const string BASE_ROUTE = "api/product";

    private static readonly Fixture _fixture = new();

    private readonly InterviewApiWebApplicationFactory _factory;

    public GetProductTests(InterviewApiWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact(Skip = "Not Implemented")]
    public async Task ShouldReturnOkResponseWithProductWhenIdMatchesAnExistingProduct()
    {
        //// Arrange
        //var client = _factory.CreateClient();

        //using var serviceScope = _factory.Services.CreateScope();

        //var context = serviceScope.ServiceProvider.GetRequiredService<InterviewContext>();

        //var productEntity = _fixture.Create<ProductEntity>();

        //await context.Products.AddAsync(productEntity);
        //await context.SaveChangesAsync();

        //// Act
        //var response = await client.GetAsync($"{BASE_ROUTE}?id={productEntity.id}");

        //// Assert
        //response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
     
    [Fact(Skip = "Not Implemented")]
    public async Task ShouldReturnNotFoundResponseWhenIdDoesNotMatchAnExistingProduct()
    {
        //// Arrange
        //var client = _factory.CreateClient();

        //var id = _fixture.Create<int>();

        //// Act
        //var response = await client.GetAsync($"{BASE_ROUTE}?id={id}");

        //// Assert
        //response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
