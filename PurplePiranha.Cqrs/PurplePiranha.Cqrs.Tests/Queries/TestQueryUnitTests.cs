namespace PurplePiranha.Cqrs.Tests.Queries
{
    public class TestQueryUnitTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test_ReturnsSuccess()
        {
            var query = new TestQuery(1);
            var handler = new TestQueryHandler();
            var result = await handler.ExecuteAsync(query);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.IsSuccess, Is.True);
            Assert.Pass();
        }
    }
}
