using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Interview.Data.AutoMapper;
using Interview.Data.Entities;
using Interview.Data.Models;
using Xunit;

namespace Interview.Data.Test.AutoMapper;

public class CustomerProfileTests
{
    private static readonly Fixture _fixture = new();

    private readonly IMapper _mapper;

    public CustomerProfileTests()
    {
        _mapper = new MapperConfiguration(x => x.AddMaps(typeof(CustomerProfile))).CreateMapper();
    }

    [Fact]
    public void ShouldMapFromCustomerToCustomerEntity()
    {
        // Arrange
        var customer = _fixture.Create<Customer>();

        // Act
        var customerEntity = _mapper.Map<CustomerEntity>(customer);

        // Assert
        customerEntity.Email.Should().Be(customer.Email);
        customerEntity.LastProduct.Should().Be(customer.LastProduct);
    }

    [Fact]
    public void ShouldMapFromCustomerEntityToCustomer()
    {
        // Arrange
        var customerEntity = _fixture.Create<CustomerEntity>();

        // Act
        var customer = _mapper.Map<Customer>(customerEntity);

        // Assert
        customer.Email.Should().Be(customerEntity.Email);
        customer.LastProduct.Should().Be(customerEntity.LastProduct);
    }
}
