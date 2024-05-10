using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Interview.Data.AutoMapper;
using Interview.Data.Entities;
using Interview.Data.Models;
using Xunit;

namespace Interview.Data.Test.AutoMapper;

public class ProductProfileTests
{
    private static readonly Fixture _fixture = new();

    private readonly IMapper _mapper;

    public ProductProfileTests()
    {
        _mapper = new MapperConfiguration(x => x.AddMaps(typeof(ProductProfile))).CreateMapper();
    }

    [Fact]
    public void ShouldMapFromProductToProductEntity()
    {
        // Arrange
        var product = _fixture.Create<Product>();

        // Act
        var productEntity = _mapper.Map<ProductEntity>(product);

        // Assert
        productEntity.Id.Should().Be(product.Id);
        productEntity.Name.Should().Be(product.Name);   
        productEntity.Description.Should().Be(product.Description);
    }

    [Fact]
    public void ShouldMapFromProductEntityToProduct()
    {
        // Arrange
        var productEntity = _fixture.Create<ProductEntity>();

        // Act
        var product = _mapper.Map<Product>(productEntity);

        // Assert
        product.Id.Should().Be(productEntity.Id);
        product.Name.Should().Be(productEntity.Name);
        product.Description.Should().Be(productEntity.Description);
    }
}
