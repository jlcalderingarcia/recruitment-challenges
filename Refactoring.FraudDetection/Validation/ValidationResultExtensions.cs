namespace Refactoring.FraudDetection.Validation
{
    using Refactoring.FraudDetection.Exceptions;

    public static class ValidationResultExtensions
    {
        public static ValidationResult Add(this ValidationResult validationResult, string fieldName, string errorMessage)
        {
            var validationError = new ValidationError(fieldName, errorMessage);
            validationResult.Add(validationError);

            return validationResult;
        }

        public static void ThrowIfInvalid(this ValidationResult validationResult)
        {
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);
        }
    }
}
