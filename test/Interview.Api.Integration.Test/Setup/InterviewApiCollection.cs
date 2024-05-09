using Xunit;

namespace Interview.Api.Integration.Test.Setup;

[CollectionDefinition(nameof(InterviewApiCollection))]
public class InterviewApiCollection : ICollectionFixture<InterviewApiWebApplicationFactory>
{
}
