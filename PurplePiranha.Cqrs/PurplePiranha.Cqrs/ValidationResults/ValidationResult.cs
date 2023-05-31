////using System.Collections.Generic;

////namespace PurplePiranha.Cqrs.ValidationResults
////{
////    public class ValidationResult
////    {
////        protected internal ValidationResult()
////        {
////            this.IsValid = true;
////            this.ValidationFailures = new List<string>();
////        }

////        protected internal ValidationResult(IEnumerable<string> validationFailures)
////        {
////            this.IsValid = false;
////            this.ValidationFailures = validationFailures;
////        }

////        public bool IsValid { get; }
////        public IEnumerable<string> ValidationFailures { get; }

////        public static ValidationResult ValidResult() => new();
////        public static ValidationResult NotValidResult(IEnumerable<string> validationFailures) => new(validationFailures);
////    }
////}