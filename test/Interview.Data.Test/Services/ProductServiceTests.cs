using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Interview.Data.AutoMapper;
using Interview.Data.Contexts;
using Interview.Data.Entities;
using Interview.Data.Models;
using Interview.Data.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Interview.Data.Test.Services;

public class ProductServiceTests
{
    private static readonly Fixture _fixture = new();

    private readonly InterviewContext _context;
    private readonly IMapper _mapper;

    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<InterviewContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());

        _context = new InterviewContext(dbContextOptionsBuilder.Options);
        _mapper = new MapperConfiguration(x => x.AddMaps(typeof(ProductProfile))).CreateMapper();
        _productService = new ProductService(_context, _mapper);
    }

    [Fact]
    public async Task AddOrUpdateProductAsync_ShouldUpdateNameIfMatchingProductExists()
    {
        // Arrange
        var productEntity = _fixture.Build<ProductEntity>()
            .With(x => x.Name, "Test Name 1")
            .Create();

        await _context.Products.AddAsync(productEntity);
        await _context.SaveChangesAsync();

        var product = new Product
        {
            Id = productEntity.Id,
            Name = "Test Name 2"
        };

        // Act
        await _productService.AddOrUpdateProductAsync(product);

        // Assert
        productEntity.Name.Should().Be("Test Name 2");

        var productEntitiesCount = await _context.Products.CountAsync();
        productEntitiesCount.Should().Be(1);
    }

    [Fact]
    public async Task AddOrUpdateProductAsync_ShouldUpdateDescriptionIfMatchingProductExists()
    {
        // Arrange
        var productEntity = _fixture.Build<ProductEntity>()
            .With(x => x.Description, "Test Description 1")
            .Create();

        await _context.Products.AddAsync(productEntity);
        await _context.SaveChangesAsync();

        var product = new Product
        {
            Id = productEntity.Id,
            Description = "Test Description 2"
        };

        // Act
        await _productService.AddOrUpdateProductAsync(product);

        // Assert
        productEntity.Description.Should().Be("Test Description 2");

        var productEntitiesCount = await _context.Products.CountAsync();
        productEntitiesCount.Should().Be(1);
    }
    
    [Fact]
    public async Task AddOrUpdateProductAsync_ShouldInsertNewProductEntityIfMatchingProductDoesNotExist()
    {
        // Arrange
        var product = _fixture.Create<Product>();

        // Act
        await _productService.AddOrUpdateProductAsync(product);

        // Assert
        var productEntities = await _context.Products.ToListAsync();

        productEntities.Should().HaveCount(1);

        var productEntity = productEntities.Single();
        productEntity.Id.Should().Be(product.Id);
        productEntity.Name.Should().Be(product.Name);
        productEntity.Description.Should().Be(product.Description);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(0)]
    public async Task GetProductAsync_ShouldReturnNullIfIdIsNullOr0(int id)
    {
        // Arrange & Act
        var product = await _productService.GetProductAsync(id);

        // Assert
        product.Should().BeNull();
    }

}
