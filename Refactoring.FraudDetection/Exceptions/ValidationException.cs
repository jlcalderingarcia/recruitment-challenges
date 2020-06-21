namespace Refactoring.FraudDetection.Exceptions
{
    using System;
    using Refactoring.FraudDetection.Validation;

    public class ValidationException : Exception
    {
        public ValidationException(
            ValidationResult validationResult
        ) : base()
        {
            ValidationResult = validationResult ?? throw new ArgumentNullException(nameof(validationResult));

            if (validationResult.IsValid)
                throw new InvalidOperationException("There is no validation exception to be raised");
        }

        public ValidationResult ValidationResult { get; }
    }
}
