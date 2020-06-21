using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Refactoring.FraudDetection.Entities;
using Refactoring.FraudDetection.FraudRules;

namespace Refactoring.FraudDetection.Tests
{
    [TestClass]
    public class FraudRadarTests
    {
        private readonly IFraudRadar _fraudRadar;
        private readonly List<IFraudRule> _fraudRules;
        private readonly Mock<IFraudRule> _fraudRuleMock;

        public FraudRadarTests()
        {
            _fraudRuleMock = new Mock<IFraudRule>();
            _fraudRules = new List<IFraudRule> {
                                _fraudRuleMock.Object
                            };

            _fraudRadar = new FraudRadar(_fraudRules);
        }

        [TestMethod]
        public void Ctor_GivenNullFraudRules_ThenThrowArgumentNullException()
        {
            // Act
            Action action = () => new FraudRadar(null);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Ctor_GivenEmptyFraudRules_ThenThrowArgumentException()
        {
            // Act
            Action action = () => new FraudRadar(new List<IFraudRule>());

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void Check_GivenSingleOperation_ThenReturnsNoFraudResult()
        {
            // Arrange
            var order = new NormalizedOrder(
                        1,
                        1,
                        "a@a.com",
                        "street",
                        "city",
                        "state",
                        "32500",
                        "1234-1234-1234-1234"
                    );

            // Act
            var result = _fraudRadar.Check(new List<NormalizedOrder> { order });

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(0);
            _fraudRuleMock.Verify(it => it.HaveFraudulentData(It.IsAny<NormalizedOrder>(), It.IsAny<NormalizedOrder>()), Times.Never);
        }

        [TestMethod]
        public void Check_GivenTwoValidOperation_ThenReturnsNoFraudResult()
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
                        2,
                        "a@a.com",
                        "street",
                        "city",
                        "state",
                        "32500",
                        "1234-1234-1234-1234"
                    );
            _fraudRuleMock.Setup(it => it.HaveFraudulentData(order1, order2))
                .Returns(false);

            // Act
            var result = _fraudRadar.Check(new List<NormalizedOrder> { order1, order2 });

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(0);
            _fraudRuleMock.Verify(it => it.HaveFraudulentData(order1, order2), Times.Once);
        }

        [TestMethod]
        public void Check_GivenTwoInvalidOperation_ThenReturnsOneFraudResult()
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
                        "a@a.com",
                        "street",
                        "city",
                        "state",
                        "32500",
                        "1234-1234-1234-1234"
                    );
            _fraudRuleMock.Setup(it => it.HaveFraudulentData(order1, order2))
                .Returns(true);

            // Act
            var result = _fraudRadar.Check(new List<NormalizedOrder> { order1, order2 });

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result.First().OrderId.Should().Be(2);
            _fraudRuleMock.Verify(it => it.HaveFraudulentData(order1, order2), Times.Once);
        }
    }
}
