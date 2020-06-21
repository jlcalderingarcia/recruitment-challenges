namespace Refactoring.FraudDetection.Tests.Validation
{
    using System;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Refactoring.FraudDetection.Validation;

    [TestClass]
    public class ValidationErrorTests
    {
        [TestMethod]
        public void Ctor_GivenValidArguments_ThenCreateInstanceWithValues()
        {
            // Arrange
            var field = "field";
            var errorMessage = "message";

            // Act
            var validationError = new ValidationError(field, errorMessage);

            // Assert
            validationError.Should().NotBeNull();
            validationError.FieldName.Should().Be(field);
            validationError.ErrorMessage.Should().Be(errorMessage);
        }

        [DataTestMethod]
        [DataRow(null, "error1")]
        [DataRow("", "error1")]
        [DataRow("   ", "error1")]
        [DataRow("field1", null)]
        [DataRow("field1", "")]
        [DataRow("field1", "  ")]
        public void Ctor_GivenInvalidArguments_ThenThrowsException(
            string fieldName,
            string errorMessage
        )
        {
            // Act
            Action action = () => new ValidationError(fieldName, errorMessage);

            // Assert
            action.Should().Throw<ArgumentException>();
        }
    }
}
