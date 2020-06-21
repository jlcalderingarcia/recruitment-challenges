namespace Refactoring.FraudDetection.Tests.FraudRules
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Refactoring.FraudDetection.Entities;
    using Refactoring.FraudDetection.FraudRules;

    [TestClass]
    public class SameIdAndEmailButDifferentCreditCardFraudRuleTests
    {
        private readonly SameIdAndEmailButDifferentCreditCardFraudRule _fraudRuleUnderTest;

        public SameIdAndEmailButDifferentCreditCardFraudRuleTests()
        {
            _fraudRuleUnderTest = new SameIdAndEmailButDifferentCreditCardFraudRule();
        }

        [TestMethod]
        public void HaveFraudulentData_GivenFraudulentOrder_ThenReturnsTrue()
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
                        "a@a.com",
                        "street2",
                        "city2",
                        "state2",
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
                        "1234-1234-1234-1234"
                    );

            // Act
            var result = _fraudRuleUnderTest.HaveFraudulentData(order1, order2);

            // Assert
            result.Should().BeFalse();
        }
    }
}
