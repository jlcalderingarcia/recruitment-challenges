namespace Refactoring.FraudDetection.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ValidationResult
    {
        private List<ValidationError> _validationErrors;

        public ValidationResult() : this(new List<ValidationError>())
        {

        }

        public ValidationResult(
            IEnumerable<ValidationError> validationErrors
        )
        {
            _validationErrors = validationErrors?.ToList() ?? throw new ArgumentNullException(nameof(validationErrors)); ;
        }

        public bool IsValid => !ValidationErrors.Any();

        public IEnumerable<ValidationError> ValidationErrors => _validationErrors;

        public ValidationResult Add(ValidationError validationError)
        {
            if (validationError is null)
                throw new ArgumentNullException(nameof(validationError));

            _validationErrors.Add(validationError);

            return this;
        }
    }
}
