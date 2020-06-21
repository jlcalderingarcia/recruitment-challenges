namespace Refactoring.FraudDetection.Tests.FraudRules
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Refactoring.FraudDetection.Entities;
    using Refactoring.FraudDetection.FraudRules;

    [TestClass]
    public class SameIdAndAddressButDifferentCreditCardFraudRuleTests
    {
        private readonly SameIdAndAddressButDifferentCreditCardFraudRule _fraudRuleUnderTest;

        public SameIdAndAddressButDifferentCreditCardFraudRuleTests()
        {
            _fraudRuleUnderTest = new SameIdAndAddressButDifferentCreditCardFraudRule();
        }

        [TestMethod]
        public void HaveFraudulentData_GivenFraudulentOrder_ThenReturnsTrue()
        {
            // Arrange
            var order1 = new NormalizedOrder(
                        1,
                        1,
                        "a@a.com",
                        "street",
                        "city",
                        "state",
                        "32500",
                        "1234-1234-1234-1234"
                    );
            var order2 = new NormalizedOrder(
                        2,
                        1,
                        "b@a.com",
                        "street",
                        "city",
                        "state",
                        "32500",
                        "1234-1234-1234-1235"
                    );

            // Act
            var result = _fraudRuleUnderTest.HaveFraudulentData(order1, order2);

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void HaveFraudulentData_GivenNotFraudulentOrder_ThenReturnsFalse()
        {
            // Arrange
            var order1 = new NormalizedOrder(
                        1,
                        1,
                        "a@a.com",
                        "street1",
                        "city1",
                        "state1",
                        "32500",
                        "1234-1234-1234-1234"
                    );
            var order2 = new NormalizedOrder(
                        2,
                        1,
                        "b@a.com",
                        "street2",
                        "city2",
                        "state2",
                        "32500",
                        "1234-1234-1234-1235"
                    );

            // Act
            var result = _fraudRuleUnderTest.HaveFraudulentData(order1, order2);

            // Assert
            result.Should().BeFalse();
        }
    }
}
