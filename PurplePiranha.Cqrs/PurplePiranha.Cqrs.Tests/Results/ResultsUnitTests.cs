using Castle.Core.Smtp;
using PurplePiranha.Cqrs.Errors;
using PurplePiranha.Cqrs.Results;

namespace PurplePiranha.Cqrs.Tests.Results
{
    public class ResultsUnitTests
    {
        private IEnumerable<string> DummyValidationErrors { get; }

        public ResultsUnitTests() {
            var dummyValidationErrors = new List<string>();
            dummyValidationErrors.Add("Dummy Validation Error!");
            DummyValidationErrors = dummyValidationErrors;
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_SuccessResultWithoutObject_DoesReturnSuccess()
        {
            var result = Result.SuccessResult();
            Assert.That(result.IsSuccess, Is.True);
        }

        [Test]
        public void Test_SuccessResultWithoutObject_DoesTriggerOnSuccess()
        {
            var result = Result.SuccessResult();
            result.OnSuccess(() => {
                Assert.Pass();
            });
            Assert.Fail();
        }

        [Test]
        public void Test_SuccessResultWithoutObject_DoesNotReturnError()
        {
            var result = Result.SuccessResult();
            Assert.That(result.IsError, Is.False);
        }

        [Test]
        public void Test_SuccessResultWithoutObject_DoesNotTriggerOnError()
        {
            var result = Result.SuccessResult();
            result.OnError(() => {
                Assert.Fail();
            });
            Assert.Pass();
        }

        [Test]
        public void Test_SuccessResultWithoutObject_DoesNotReturnValidationFailure()
        {
            var result = Result.SuccessResult();
            Assert.That(result.IsValidationFailure, Is.False);
        }

        [Test]
        public void Test_SuccessResultWithoutObject_DoesNotTriggerOnValidationFailure()
        {
            var result = Result.SuccessResult();
            result.OnValidationFailure(() => {
                Assert.Fail();
            });
            Assert.Pass();
        }

        [Test]
        public void Test_ErrorResultWithoutObject_DoesReturnError()
        {
            var result = Result.ErrorResult(Error.NullValue);
            Assert.That(result.IsError, Is.True);
            Assert.That(result.Error, Is.EqualTo(Error.NullValue));
        }

        [Test]
        public void Test_ErrorResultWithoutObject_DoesTriggerOnError()
        {
            var result = Result.ErrorResult(Error.NullValue);
            result.OnError(() => {
                Assert.Pass();
            });
            Assert.Fail();
        }

        [Test]
        public void Test_ErrorResultWithoutObject_DoesNotReturnSuccess()
        {
            var result = Result.ErrorResult(Error.NullValue);
            Assert.That(result.IsSuccess, Is.False);
        }

        [Test]
        public void Test_ErrorResultWithoutObject_DoesNotTriggerOnSuccess()
        {
            var result = Result.ErrorResult(Error.NullValue);
            result.OnSuccess(() => {
                Assert.Fail();
            });
            Assert.Pass();
        }

        [Test]
        public void Test_ErrorResultWithoutObject_DoesNotReturnValidationFailure()
        {
            var result = Result.ErrorResult(Error.NullValue);
            Assert.That(result.IsValidationFailure, Is.False);
        }

        [Test]
        public void Test_ErrorResultWithoutObject_DoesNotTriggerOnValidationFailure()
        {
            var result = Result.ErrorResult(Error.NullValue);
            result.OnValidationFailure(() => {
                Assert.Fail();
            });
            Assert.Pass();
        }

        [Test]
        public void Test_ValidationFailureResultWithoutObject_DoesReturnValidationFailure()
        {
            var result = Result.ValidationFailureResult(DummyValidationErrors);
            Assert.That(result.IsValidationFailure, Is.True);
        }

        [Test]
        public void Test_ValidationFailureResultWithoutObject_DoesTriggerOnValidationFailure()
        {
            var result = Result.ValidationFailureResult(DummyValidationErrors);
            result.OnValidationFailure(() => {
                Assert.Pass();
            });
            Assert.Fail();
        }

        [Test]
        public void Test_ValidationFailureResultWithoutObject_DoesNotReturnError()
        {
            var result = Result.ValidationFailureResult(DummyValidationErrors);
            Assert.That(result.IsError, Is.False);
        }

        [Test]
        public void Test_ValidationFailureResultWithoutObject_DoesNotTriggerOnError()
        {
            var result = Result.ValidationFailureResult(DummyValidationErrors);
            result.OnError(() => {
                Assert.Fail();
            });
            Assert.Pass();
        }

        [Test]
        public void Test_ValidationFailureResultWithoutObject_DoesNotReturnSuccess()
        {
            var result = Result.ValidationFailureResult(DummyValidationErrors);
            Assert.That(result.IsSuccess, Is.False);
        }

        [Test]
        public void Test_ValidationFailureResultWithoutObject_DoesNotTriggerOnSuccess()
        {
            var result = Result.ErrorResult(Error.NullValue);
            result.OnSuccess(() => {
                Assert.Fail();
            });
            Assert.Pass();
        }
    }
}
