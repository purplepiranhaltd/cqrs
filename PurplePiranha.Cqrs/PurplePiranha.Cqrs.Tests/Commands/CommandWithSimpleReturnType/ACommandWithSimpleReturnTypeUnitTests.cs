using PurplePiranha.Cqrs.Results;

namespace PurplePiranha.Cqrs.Tests.Commands.CommandWithSimpleReturnType
{
    public class ACommandWithSimpleReturnTypeUnitTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test_Success()
        {
            var command = new ACommandWithSimpleReturnType(100, 100); // For success, both inputs must be positive
            var handler = new ACommandWithSimpleReturnTypeHandler();
            var result = await handler.ExecuteAsync(command);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.IsSuccess, Is.True);
            Assert.Pass();
        }

        [Test]
        public async Task Test_ValidationFailureX1()
        {
            var command = new ACommandWithSimpleReturnType(-100, 100); // A negative input value should generate a validation failure
            var handler = new ACommandWithSimpleReturnTypeHandler();
            var result = await handler.ExecuteAsync(command);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.IsValidationFailure, Is.True);
            Assert.That(result.ValidationErrors, Is.Not.Null);
            Assert.That(result.ValidationErrors.Count, Is.EqualTo(1));
            Assert.Pass();
        }

        [Test]
        public async Task Test_ValidationFailureX2()
        {
            var command = new ACommandWithSimpleReturnType(-100, -1); // Two negative input values should generate two validation failures
            var handler = new ACommandWithSimpleReturnTypeHandler();
            var result = await handler.ExecuteAsync(command);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.IsValidationFailure, Is.True);
            Assert.That(result.ValidationErrors, Is.Not.Null);
            Assert.That(result.ValidationErrors.Count, Is.EqualTo(2));
            Assert.Pass();
        }

        [Test]
        public async Task Test_Error()
        {
            var command = new ACommandWithSimpleReturnType(Int32.MaxValue, Int32.MaxValue); // This will cause overflow
            var handler = new ACommandWithSimpleReturnTypeHandler();
            var result = await handler.ExecuteAsync(command);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.IsError, Is.True);
            Assert.That(result.Error, Is.Not.Null);
            Assert.That(result.Error, Is.EqualTo(ACommandWithSimpleReturnTypeErrors.ArithmeticOverFlow));
            Assert.Pass();
        }

        [Test]
        public async Task Test_Success_OnSuccessIsCalled()
        {
            var command = new ACommandWithSimpleReturnType(100, 100);
            var handler = new ACommandWithSimpleReturnTypeHandler();
            var result = await handler.ExecuteAsync(command);
            result.OnSuccess(() =>
            {
                Assert.Pass();
            });
            Assert.Fail();
        }

        [Test]
        public async Task Test_Success_OnSuccessResultIsCorrect()
        {
            var command = new ACommandWithSimpleReturnType(24, 63); // 24 + 63 = 87
            var handler = new ACommandWithSimpleReturnTypeHandler();
            var result = await handler.ExecuteAsync(command);
            result.OnSuccess(() =>
            {
                Assert.That(result.Value, Is.EqualTo(87));
                Assert.Pass();
            });
            Assert.Fail();
        }

        [Test]
        public async Task Test_Success_OnValidationFailureIsNotCalled()
        {
            var command = new ACommandWithSimpleReturnType(100, 100);
            var handler = new ACommandWithSimpleReturnTypeHandler();
            var result = await handler.ExecuteAsync(command);
            result.OnValidationFailure(() =>
            {
                Assert.Fail();
            });
            Assert.Pass();
        }

        [Test]
        public async Task Test_Success_OnErrorIsNotCalled()
        {
            var command = new ACommandWithSimpleReturnType(100, 100);
            var handler = new ACommandWithSimpleReturnTypeHandler();
            var result = await handler.ExecuteAsync(command);
            result.OnError(() =>
            {
                Assert.Fail();
            });
            Assert.Pass();
        }

        [Test]
        public async Task Test_ValidationFailure_OnSuccessIsNotCalled()
        {
            var command = new ACommandWithSimpleReturnType(-1, 100);
            var handler = new ACommandWithSimpleReturnTypeHandler();
            var result = await handler.ExecuteAsync(command);
            result.OnSuccess(() =>
            {
                Assert.Fail();
            });
            Assert.Pass();
        }

        [Test]
        public async Task Test_ValidationFailure_OnValidationFailureIsCalled()
        {
            var command = new ACommandWithSimpleReturnType(-1, 100);
            var handler = new ACommandWithSimpleReturnTypeHandler();
            var result = await handler.ExecuteAsync(command);
            result.OnValidationFailure(() =>
            {
                Assert.Pass();
            });
            Assert.Fail();
        }

        [Test]
        public async Task Test_ValidationFailure_OnErrorIsNotCalled()
        {
            var command = new ACommandWithSimpleReturnType(-1, 100);
            var handler = new ACommandWithSimpleReturnTypeHandler();
            var result = await handler.ExecuteAsync(command);
            result.OnError(() =>
            {
                Assert.Fail();
            });
            Assert.Pass();
        }

        [Test]
        public async Task Test_Error_OnSuccessIsNotCalled()
        {
            var command = new ACommandWithSimpleReturnType(Int32.MaxValue, Int32.MaxValue); // This will cause overflow
            var handler = new ACommandWithSimpleReturnTypeHandler();
            var result = await handler.ExecuteAsync(command);
            result.OnSuccess(() =>
            {
                Assert.Fail();
            });
            Assert.Pass();
        }

        [Test]
        public async Task Test_Error_OnValidationFailureIsNotCalled()
        {
            var command = new ACommandWithSimpleReturnType(Int32.MaxValue, Int32.MaxValue); // This will cause overflow
            var handler = new ACommandWithSimpleReturnTypeHandler();
            var result = await handler.ExecuteAsync(command);
            result.OnValidationFailure(() =>
            {
                Assert.Fail();
            });
            Assert.Pass();
        }



        [Test]
        public async Task Test_Error_OnErrorIsCalled()
        {
            var command = new ACommandWithSimpleReturnType(Int32.MaxValue, Int32.MaxValue); // This will cause overflow
            var handler = new ACommandWithSimpleReturnTypeHandler();
            var result = await handler.ExecuteAsync(command);
            result.OnError(() =>
            {
                Assert.Pass();
            });
            Assert.Fail();
        }
    }
}