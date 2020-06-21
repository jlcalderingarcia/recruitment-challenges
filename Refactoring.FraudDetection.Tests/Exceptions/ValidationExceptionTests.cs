namespace Refactoring.FraudDetection.Tests.Exceptions
{
    using System;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Refactoring.FraudDetection.Exceptions;
    using Refactoring.FraudDetection.Validation;

    [TestClass]
    public class ValidationExceptionTests
    {
        [TestMethod]
        public void Ctor_GivenNullValidationResult_ThenThrowArgumentNullException()
        {
            // Act
            Action action = () => new ValidationException(null);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Ctor_GivenValidValidationResult_ThenThrowInvalidOperationException()
        {
            // Act
            Action action = () => new ValidationException(new ValidationResult());

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void Ctor_GivenInvalidValidationResult_ThenCreateException()
        {
            // Arrange
            var validationResult = new ValidationResult();
            validationResult.Add(new ValidationError("fieldName", "ErrorMessage"));

            // Act
            var validationException = new ValidationException(validationResult);

            // Assert
            validationException.Should().NotBeNull();
            validationException.ValidationResult.Should().Be(validationResult);
        }
    }
}
