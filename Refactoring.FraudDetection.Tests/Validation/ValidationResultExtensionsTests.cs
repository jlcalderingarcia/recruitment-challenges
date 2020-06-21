namespace Refactoring.FraudDetection.Tests.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Refactoring.FraudDetection.Exceptions;
    using Refactoring.FraudDetection.Validation;

    [TestClass]
    public class ValidationResultExtensionsTests
    {
        private const string FieldName = "field";
        private const string ErrorMessage = "error";

        [TestMethod]
        public void Add_GivenInvalidValidationErrorArguments_ThenThrowArgumentException()
        {
            // Arrange
            var validValidationResult = new ValidationResult();

            // Act
            Action action = () => ValidationResultExtensions.Add(validValidationResult, null, null);

            // Assert
            action.Should().NotBeNull();
            action.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void Add_GivenValidValidationErrorArguments_ThenErrorAddedToValidationResult()
        {
            // Arrange
            var validationResult = new ValidationResult();

            // Act
            Action action = () => ValidationResultExtensions.Add(validationResult, FieldName, ErrorMessage);

            // Assert
            action.Should().NotBeNull();
            action.Should().NotThrow<ArgumentException>();
            validationResult.IsValid.Should().BeFalse();
            validationResult.ValidationErrors.Should().HaveCount(1);
            validationResult.ValidationErrors.First().FieldName.Should().Be(FieldName);
            validationResult.ValidationErrors.First().ErrorMessage.Should().Be(ErrorMessage);
        }

        [TestMethod]
        public void ThrowIfInvalid_GivenValidValidationResult_ThenNoExceptionThrown()
        {
            // Arrange
            var validValidationResult = new ValidationResult();

            // Act
            Action action = () => ValidationResultExtensions.ThrowIfInvalid(validValidationResult);

            // Assert
            action.Should().NotBeNull();
            action.Should().NotThrow<ValidationException>();
        }

        [TestMethod]
        public void ThrowIfInvalid_GivenInvalidValidationResult_ThenThrowValidationException()
        {
            // Arrange
            var invalidValidationResult = new ValidationResult(
                    new List<ValidationError> {
                        new ValidationError(FieldName, ErrorMessage)
                    }
                );

            // Act
            Action action = () => ValidationResultExtensions.ThrowIfInvalid(invalidValidationResult);

            // Assert
            action.Should().NotBeNull();
            action.Should().Throw<ValidationException>();
        }
    }
}
