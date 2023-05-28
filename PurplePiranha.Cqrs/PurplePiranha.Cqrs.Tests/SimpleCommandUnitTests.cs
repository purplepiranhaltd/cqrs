using PurplePiranha.Cqrs.Results;

namespace PurplePiranha.Cqrs.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test_SimpleCommand_Success()
        {
            var command = new SimpleCommand(100, 100); // For success, both inputs must be positive and equal
            var handler = new SimpleCommandHandler();
            var result = await handler.ExecuteAsync(command);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.IsSuccess, Is.True);
            Assert.Pass();
        }

        [Test]
        public async Task Test_SimpleCommand_ValidationFailureX1()
        {
            var command = new SimpleCommand(-100, 100); // A negative input value should generate a validation failure
            var handler = new SimpleCommandHandler();
            var result = await handler.ExecuteAsync(command);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.IsValidationFailure, Is.True);
            Assert.That(result.ValidationErrors, Is.Not.Null);
            Assert.That(result.ValidationErrors.Count, Is.EqualTo(1));
            Assert.Pass();
        }

        [Test]
        public async Task Test_SimpleCommand_ValidationFailureX2()
        {
            var command = new SimpleCommand(-100, -1); // Two negative input values should generate two validation failures
            var handler = new SimpleCommandHandler();
            var result = await handler.ExecuteAsync(command);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.IsValidationFailure, Is.True);
            Assert.That(result.ValidationErrors, Is.Not.Null);
            Assert.That(result.ValidationErrors.Count, Is.EqualTo(2));
            Assert.Pass();
        }

        [Test]
        public async Task Test_SimpleCommand_Error()
        {
            var command = new SimpleCommand(50, 66); // Not equal input values should generate a 'BothInputsMustBeEqual' error
            var handler = new SimpleCommandHandler();
            var result = await handler.ExecuteAsync(command);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.IsError, Is.True);
            Assert.That(result.Error, Is.Not.Null);
            Assert.That(result.Error, Is.EqualTo(SimpleCommandErrors.BothInputsMustBeEqual));
            Assert.Pass();
        }

        [Test]
        public async Task Test_SimpleCommand_Success_OnSuccessIsCalled()
        {
            var command = new SimpleCommand(100, 100);
            var handler = new SimpleCommandHandler();
            var result = await handler.ExecuteAsync(command);
            result.OnSuccess(() => {
                Assert.Pass();
            });
            Assert.Fail();
        }

        [Test]
        public async Task Test_SimpleCommand_Success_OnValidationFailureIsNotCalled()
        {
            var command = new SimpleCommand(100, 100);
            var handler = new SimpleCommandHandler();
            var result = await handler.ExecuteAsync(command);
            result.OnValidationFailure(() => {
                Assert.Fail();
            });
            Assert.Pass();
        }

        [Test]
        public async Task Test_SimpleCommand_Success_OnErrorIsNotCalled()
        {
            var command = new SimpleCommand(100, 100);
            var handler = new SimpleCommandHandler();
            var result = await handler.ExecuteAsync(command);
            result.OnError(() => {
                Assert.Fail();
            });
            Assert.Pass();
        }

        [Test]
        public async Task Test_SimpleCommand_ValidationFailure_OnSuccessIsNotCalled()
        {
            var command = new SimpleCommand(-1, 100);
            var handler = new SimpleCommandHandler();
            var result = await handler.ExecuteAsync(command);
            result.OnSuccess(() => {
                Assert.Fail();
            });
            Assert.Pass();
        }

        [Test]
        public async Task Test_SimpleCommand_ValidationFailure_OnValidationFailureIsCalled()
        {
            var command = new SimpleCommand(-1, 100);
            var handler = new SimpleCommandHandler();
            var result = await handler.ExecuteAsync(command);
            result.OnValidationFailure(() => {
                Assert.Pass();
            });
            Assert.Fail();
        }

        [Test]
        public async Task Test_SimpleCommand_ValidationFailure_OnErrorIsNotCalled()
        {
            var command = new SimpleCommand(-1, 100);
            var handler = new SimpleCommandHandler();
            var result = await handler.ExecuteAsync(command);
            result.OnError(() => {
                Assert.Fail();
            });
            Assert.Pass();
        }

        [Test]
        public async Task Test_SimpleCommand_Error_OnSuccessIsNotCalled()
        {
            var command = new SimpleCommand(6, 9);
            var handler = new SimpleCommandHandler();
            var result = await handler.ExecuteAsync(command);
            result.OnSuccess(() => {
                Assert.Fail();
            });
            Assert.Pass();
        }

        [Test]
        public async Task Test_SimpleCommand_Error_OnValidationFailureIsNotCalled()
        {
            var command = new SimpleCommand(6, 9);
            var handler = new SimpleCommandHandler();
            var result = await handler.ExecuteAsync(command);
            result.OnValidationFailure(() => {
                Assert.Fail();
            });
            Assert.Pass();
        }

        

        [Test]
        public async Task Test_SimpleCommand_Error_OnErrorIsCalled()
        {
            var command = new SimpleCommand(6, 9);
            var handler = new SimpleCommandHandler();
            var result = await handler.ExecuteAsync(command);
            result.OnError(() => {
                Assert.Pass();
            });
            Assert.Fail();
        }
    }
}