namespace Refactoring.FraudDetection.Tests.Entities
{
    using System;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Refactoring.FraudDetection.Entities;
    using Refactoring.FraudDetection.Exceptions;

    [TestClass]
    public class OrdersTests
    {
        [TestMethod]
        public void Ctor_GvenValidParameters_ThenCreateEntityWithValues()
        {
            // Arrange
            int orderId = 1;
            int dealId = 2;
            string email = "a@a.com";
            string street = "street";
            string city = "city";
            string state = "state";
            string zipCode = "32500";
            string creditCard = "1234-1234-1234-1234";

            // Act
            var order = new Order(
                    orderId,
                    dealId,
                    email,
                    street,
                    city,
                    state,
                    zipCode,
                    creditCard
                );

            // Assert
            order.Should().NotBeNull();
            order.OrderId.Should().Be(orderId);
            order.DealId.Should().Be(dealId);
            order.Email.Should().Be(email);
            order.Street.Should().Be(street);
            order.City.Should().Be(city);
            order.State.Should().Be(state);
            order.ZipCode.Should().Be(zipCode);
            order.CreditCard.Should().Be(creditCard);
        }

        [DataTestMethod]
        [DataRow(-1, 2, "a@a.com", "street", "city", "state", "32500", "1234-1234-1234-1234")]
        [DataRow(0, 2, "a@a.com", "street", "city", "state", "32500", "1234-1234-1234-1234")]
        [DataRow(1, -2, "a@a.com", "street", "city", "state", "32500", "1234-1234-1234-1234")]
        [DataRow(1, 0, "a@a.com", "street", "city", "state", "32500", "1234-1234-1234-1234")]
        [DataRow(1, 2, null, "street", "city", "state", "32500", "1234-1234-1234-1234")]
        [DataRow(1, 2, "", "street", "city", "state", "32500", "1234-1234-1234-1234")]
        [DataRow(1, 2, "a@a.com", null, "city", "state", "32500", "1234-1234-1234-1234")]
        [DataRow(1, 2, "a@a.com", "", "city", "state", "32500", "1234-1234-1234-1234")]
        [DataRow(1, 2, "a@a.com", "street", null, "state", "32500", "1234-1234-1234-1234")]
        [DataRow(1, 2, "a@a.com", "street", "", "state", "32500", "1234-1234-1234-1234")]
        [DataRow(1, 2, "a@a.com", "street", "city", null, "32500", "1234-1234-1234-1234")]
        [DataRow(1, 2, "a@a.com", "street", "city", "", "32500", "1234-1234-1234-1234")]
        [DataRow(1, 2, "a@a.com", "street", "city", "state", null, "1234-1234-1234-1234")]
        [DataRow(1, 2, "a@a.com", "street", "city", "state", "", "1234-1234-1234-1234")]
        [DataRow(1, 2, "a@a.com", "street", "city", "state", "32500", null)]
        [DataRow(1, 2, "a@a.com", "street", "city", "state", "32500", "")]
        public void Ctor_GivenInvalidParameters_ThenThrowValidationException(
            int orderId,
            int dealId,
            string email,
            string street,
            string city,
            string state,
            string zipCode,
            string creditCard
        ) {
            // Act
            Action action = () => new Order(orderId, dealId, email, street, city, state, zipCode, creditCard);

            // Assert
            action.Should().Throw<ValidationException>();
        }
    }
}
