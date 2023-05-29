using Castle.Core.Smtp;
using PurplePiranha.Cqrs.Errors;
using PurplePiranha.Cqrs.Results;

namespace PurplePiranha.Cqrs.Tests.Results
{
    public class ResultsTUnitTests
    {
        private IEnumerable<string> DummyValidationErrors { get; }

        public ResultsTUnitTests() {
            var dummyValidationErrors = new List<string>();
            dummyValidationErrors.Add("Dummy Validation Error!");
            DummyValidationErrors = dummyValidationErrors;
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_SuccessResultWithObject_DoesReturnSuccess()
        {
            var result = Result.SuccessResult<int>(5);
            Assert.That(result.IsSuccess, Is.True);
        }

        [Test]
        public void Test_SuccessResultWithObject_HasResultObject()
        {
            var result = Result.SuccessResult<int>(5);
            Assert.That(result.Value, Is.EqualTo(5));
        }

        [Test]
        public void Test_SuccessResultWithObject_DoesTriggerOnSuccess()
        {
            var result = Result.SuccessResult<int>(5);
            result.OnSuccess(() => {
                Assert.Pass();
            });
            Assert.Fail();
        }

        [Test]
        public void Test_SuccessResultWithObject_DoesNotReturnError()
        {
            var result = Result.SuccessResult<int>(5);
            Assert.That(result.IsError, Is.False);
        }

        [Test]
        public void Test_SuccessResultWithObject_DoesNotTriggerOnError()
        {
            var result = Result.SuccessResult<int>(5);
            result.OnError(() => {
                Assert.Fail();
            });
            Assert.Pass();
        }

        [Test]
        public void Test_SuccessResultWithObject_DoesNotReturnValidationFailure()
        {
            var result = Result.SuccessResult<int>(5);
            Assert.That(result.IsValidationFailure, Is.False);
        }

        [Test]
        public void Test_SuccessResultWithObject_DoesNotTriggerOnValidationFailure()
        {
            var result = Result.SuccessResult<int>(5);
            result.OnValidationFailure(() => {
                Assert.Fail();
            });
            Assert.Pass();
        }

        [Test]
        public void Test_ErrorResultWithObject_DoesReturnError()
        {
            var result = Result.ErrorResult<int>(Error.NullValue);
            Assert.That(result.IsError, Is.True);
        }

        [Test]
        public void Test_ErrorResultWithObject_DoesTriggerOnError()
        {
            var result = Result.ErrorResult<int>(Error.NullValue);
            result.OnError(() => {
                Assert.Pass();
            });
            Assert.Fail();
        }

        [Test]
        public void Test_ErrorResultWithObject_DoesNotReturnSuccess()
        {
            var result = Result.ErrorResult<int>(Error.NullValue);
            Assert.That(result.IsSuccess, Is.False);
        }

        [Test]
        public void Test_ErrorResultWithObject_DoesNotTriggerOnSuccess()
        {
            var result = Result.ErrorResult<int>(Error.NullValue);
            result.OnSuccess(() => {
                Assert.Fail();
            });
            Assert.Pass();
        }

        [Test]
        public void Test_ErrorResultWithObject_DoesNotReturnValidationFailure()
        {
            var result = Result.ErrorResult<int>(Error.NullValue);
            Assert.That(result.IsValidationFailure, Is.False);
        }

        [Test]
        public void Test_ErrorResultWithObject_DoesNotTriggerOnValidationFailure()
        {
            var result = Result.ErrorResult<int>(Error.NullValue);
            result.OnValidationFailure(() => {
                Assert.Fail();
            });
            Assert.Pass();
        }

        [Test]
        public void Test_ValidationFailureResultWithObject_DoesReturnValidationFailure()
        {
            var result = Result.ValidationFailureResult<int>(DummyValidationErrors);
            Assert.That(result.IsValidationFailure, Is.True);
        }

        [Test]
        public void Test_ValidationFailureResultWithObject_DoesTriggerOnValidationFailure()
        {
            var result = Result.ValidationFailureResult<int>(DummyValidationErrors);
            result.OnValidationFailure(() => {
                Assert.Pass();
            });
            Assert.Fail();
        }

        [Test]
        public void Test_ValidationFailureResultWithObject_DoesNotReturnError()
        {
            var result = Result.ValidationFailureResult<int>(DummyValidationErrors);
            Assert.That(result.IsError, Is.False);
        }

        [Test]
        public void Test_ValidationFailureResultWithObject_DoesNotTriggerOnError()
        {
            var result = Result.ValidationFailureResult<int>(DummyValidationErrors);
            result.OnError(() => {
                Assert.Fail();
            });
            Assert.Pass();
        }

        [Test]
        public void Test_ValidationFailureResultWithObject_DoesNotReturnSuccess()
        {
            var result = Result.ValidationFailureResult<int>(DummyValidationErrors);
            Assert.That(result.IsSuccess, Is.False);
        }

        [Test]
        public void Test_ValidationFailureResultWithObject_DoesNotTriggerOnSuccess()
        {
            var result = Result.ErrorResult<int>(Error.NullValue);
            result.OnSuccess(() => {
                Assert.Fail();
            });
            Assert.Pass();
        }
    }
}
