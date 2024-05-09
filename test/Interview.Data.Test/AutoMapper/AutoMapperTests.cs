using AutoMapper;
using Interview.Data.AutoMapper;
using Xunit;

namespace Interview.Data.Test.AutoMapper;

public class AutoMapperTests
{
    [Fact]
    public void ShouldHaveValidConfiguration()
    {
        // Arrange
        var mapperConfiguration = new MapperConfiguration(x => x.AddMaps(typeof(CustomerProfile)));

        // Act & Assert
        mapperConfiguration.AssertConfigurationIsValid();
    }
}
