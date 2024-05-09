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

public class CustomerServiceTests
{
    private static readonly Fixture _fixture = new();

    private readonly InterviewContext _context;
    private readonly IMapper _mapper;

    private readonly CustomerService _customerService;

    public CustomerServiceTests()
    {
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<InterviewContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());

        _context = new InterviewContext(dbContextOptionsBuilder.Options);

        _mapper = new MapperConfiguration(x => x.AddMaps(typeof(CustomerProfile))).CreateMapper();

        _customerService = new CustomerService(_context, _mapper);
    }

    [Fact]
    public async Task AddOrUpdateCustomerAsync_ShouldUpdateLastProductIfMatchingCustomerExists()
    {
        // Arrange
        var customerEntity = _fixture.Build<CustomerEntity>()
            .With(x => x.LastProduct, 1)
            .Create();

        await _context.Customers.AddAsync(customerEntity);
        await _context.SaveChangesAsync();

        var Customer = new Customer
        {
            Email = customerEntity.Email,
            LastProduct = 2
        };

        // Act
        await _customerService.AddOrUpdateCustomerAsync(Customer);

        // Assert
        customerEntity.LastProduct.Should().Be(2);

        var customerEntitiesCount = await _context.Customers.CountAsync();
        customerEntitiesCount.Should().Be(1);
    }

    [Fact]
    public async Task AddOrUpdateCustomerAsync_ShouldInsertNewCustomerEntityIfMatchingCustomerDoesNotExist()
    {
        // Arrange
        var customer = _fixture.Create<Customer>();

        // Act
        await _customerService.AddOrUpdateCustomerAsync(customer);

        // Assert
        var customerEntities = await _context.Customers.ToListAsync();

        customerEntities.Should().HaveCount(1);

        var customerEntity = customerEntities.Single();
        customerEntity.Email.Should().Be(customer.Email);
        customerEntity.LastProduct.Should().Be(customer.LastProduct);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task GetCustomerAsync_ShouldReturnNullIfEmailIsNullOrWhiteSpace(string email)
    {
        // Arrange & Act
        var customer = await _customerService.GetCustomerAsync(email);

        // Assert
        customer.Should().BeNull();
    }

    [Fact]
    public async Task GetCustomerAsync_ShouldReturnNullIfMatchingCustomerDoesNotExist()
    {
        // Arrange
        var email = _fixture.Create<string>();

        // Act
        var customer = await _customerService.GetCustomerAsync(email);

        // Customer
        customer.Should().BeNull();
    }

    [Fact]
    public async Task GetCustomerAsync_ShouldReturnCustomerIfMathcingCustomerExists()
    {
        // Arrange
        var customerEntity = _fixture.Create<CustomerEntity>();

        await _context.Customers.AddAsync(customerEntity);
        await _context.SaveChangesAsync(); 

        // Act
        var customer = await _customerService.GetCustomerAsync(customerEntity.Email);

        // Assert
        customer.Should().NotBeNull();
        customer.Email.Should().Be(customerEntity.Email);
        customer.LastProduct.Should().Be(customerEntity.LastProduct);
    }
}
