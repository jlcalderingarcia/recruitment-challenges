namespace Refactoring.FraudDetection.Validation
{
    using System;

    public class ValidationError
    {
        public ValidationError(
            string fieldName,
            string errorMessage
        ) {
            if (string.IsNullOrWhiteSpace(fieldName))
            {
                throw new ArgumentException("Field name cannot be null or white space", nameof(fieldName));
            }

            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                throw new ArgumentException("Error message cannot be null or white space", nameof(errorMessage));
            }

            FieldName = fieldName;
            ErrorMessage = errorMessage;
        }

        public string FieldName { get; }
        public string ErrorMessage { get; }
    }
}
