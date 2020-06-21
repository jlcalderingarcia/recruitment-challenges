namespace Refactoring.FraudDetection.Tests.Validation
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Refactoring.FraudDetection.Validation;

    [TestClass]
    public class ValidationResultTests
    {
        [TestMethod]
        public void Ctor_GivenNoArguments_ThenBasicObjectCreated()
        {
            // Act
            var validationResult = new ValidationResult();

            // Assert
            validationResult.Should().NotBeNull();
            validationResult.IsValid.Should().BeTrue();
            validationResult.ValidationErrors.Should().NotBeNull();
            validationResult.ValidationErrors.Should().HaveCount(0);
        }

        [TestMethod]
        public void Ctor_GivenListArgument_ThenBasicObjectCreated()
        {
            // Arrange
            var errors = new List<ValidationError>
            {
                new ValidationError("field1", "error1"),
                new ValidationError("field2", "error2")
            };

            // Act
            var validationResult = new ValidationResult(errors);

            // Assert
            validationResult.Should().NotBeNull();
            validationResult.IsValid.Should().BeFalse();
            validationResult.ValidationErrors.Should().NotBeNull();
            validationResult.ValidationErrors.Should().HaveCount(errors.Count);
            validationResult.ValidationErrors.Should().BeEquivalentTo(errors);
        }

        [TestMethod]
        public void Ctor_GivenNullArguments_ThenThrowArgumentNullException()
        {
            // Act
            Action action = () => new ValidationResult(null);

            // Assert
            action.Should().NotBeNull();
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Add_GivenNullValidationError_ThenThrowNullArgumentException()
        {
            // Arrange
            var errors = new List<ValidationError>
            {
                new ValidationError("field1", "error1"),
                new ValidationError("field2", "error2")
            };
            var validationResult = new ValidationResult(errors);

            // Act
            Action action = () => validationResult.Add(null);

            // Assert
            action.Should().NotBeNull();
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Add_GivenValidValidationError_ThenTheErrorIsAddedToTheValidationResult()
        {
            // Arrange
            var error = new ValidationError("field1", "error1");
            var validationResult = new ValidationResult();

            // Act
            validationResult.Add(error);

            // Assert
            validationResult.Should().NotBeNull();
            validationResult.IsValid.Should().BeFalse();
            validationResult.ValidationErrors.Should().NotBeNull();
            validationResult.ValidationErrors.Should().HaveCount(1);
            validationResult.ValidationErrors.Should().BeEquivalentTo(new List<ValidationError> { error });
        }
    }
}
