using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Refactoring.FraudDetection.Entities;
using Refactoring.FraudDetection.Normalization;
using Refactoring.FraudDetection.Parsing;

namespace Refactoring.FraudDetection.Tests.Parsing
{
    [TestClass]
    public class NormalizedOrdersFileParserTests
    {
        private readonly Mock<IOrdersParser> _ordersParserMock;
        private readonly Mock<IOrderNormalizer> _orderNormalizerMock;
        private readonly INormalizedOrdersFileParser _normalizedOrdersFileParser;

        public NormalizedOrdersFileParserTests()
        {
            _ordersParserMock = new Mock<IOrdersParser>();
            _orderNormalizerMock = new Mock<IOrderNormalizer>();

            _normalizedOrdersFileParser = new NormalizedOrdersFileParser(
                    _ordersParserMock.Object,
                    _orderNormalizerMock.Object
                );
        }

        [TestMethod]
        public void Ctor_GivenNullOrdersParser_ThenThrowArgumentNullException()
        {
            // Act
            Action action = () => new NormalizedOrdersFileParser(
                    null,
                    _orderNormalizerMock.Object
                );

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Ctor_GivenNullOrderNormalizer_ThenThrowArgumentNullException()
        {
            // Act
            Action action = () => new NormalizedOrdersFileParser(
                    _ordersParserMock.Object,
                    null
                );

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("   ")]
        public void ParseOrdersFile_GivenInvalidFilePath_ThenThrowArgumentException(string filePath)
        {
            // Act
            Action action = () => _normalizedOrdersFileParser.ParseOrdersFile(filePath);

            // Assert
            action.Should().NotBeNull();
            action.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void ParseOrdersFile_GivenUnexistingFile_ThenThrowFileNotFoundException()
        {
            // Act
            Action action = () => _normalizedOrdersFileParser.ParseOrdersFile("/non-existing-folder/non-existing-file.txt");

            // Assert
            action.Should().NotBeNull();
            action.Should().Throw<FileNotFoundException>();
        }

        [TestMethod]
        [DeploymentItem("./../Files/OneLineFile.txt", "Files")]
        public void ParseOrdersFile_GivenValidArguments_ThenReturnParsedAndNormalizedOrders()
        {
            // Arrange
            var filePath = Path.Combine(Environment.CurrentDirectory, "Files", "OneLineFile.txt");
            var order = new Order(1, 1, "a@a.com", "street", "city", "state", "32500", "1234-1234-1234-1234");
            var normalizedOrder = new NormalizedOrder(1, 1, "a@a.com", "street", "city", "state", "32500", "1234-1234-1234-1234");
            _ordersParserMock.Setup(it => it.ParseOrders(It.IsAny<IEnumerable<string>>()))
                .Returns(new List<Order> { order });
            _orderNormalizerMock.Setup(it => it.Normalize(order))
                .Returns(normalizedOrder);

            // Act
            var result = _normalizedOrdersFileParser.ParseOrdersFile(filePath);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result.Should().BeEquivalentTo(new List<NormalizedOrder> { normalizedOrder });
            _ordersParserMock.Verify(it => it.ParseOrders(It.IsAny<IEnumerable<string>>()), Times.Once);
            _orderNormalizerMock.Verify(it => it.Normalize(order), Times.Once);
        }

    }
}
