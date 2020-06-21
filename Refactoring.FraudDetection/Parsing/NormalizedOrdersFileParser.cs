namespace Refactoring.FraudDetection.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Refactoring.FraudDetection.Entities;
    using Refactoring.FraudDetection.Normalization;

    public class NormalizedOrdersFileParser : INormalizedOrdersFileParser
    {
        private readonly IOrdersParser _ordersParser;
        private readonly IOrderNormalizer _orderNormalizer;

        public NormalizedOrdersFileParser(
            IOrdersParser ordersParser,
            IOrderNormalizer orderNormalizer
        ){
            _ordersParser = ordersParser ?? throw new ArgumentNullException(nameof(ordersParser));
            _orderNormalizer = orderNormalizer ?? throw new ArgumentNullException(nameof(orderNormalizer));
        }

        public IEnumerable<NormalizedOrder> ParseOrdersFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("The file path should not be null or white space", nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException("The provided file does not exists", filePath);

            var lines = File.ReadAllLines(filePath);

            var orders = _ordersParser.ParseOrders(lines);

            var normalizedOrders = orders.Select(order => _orderNormalizer.Normalize(order)).ToList();

            return normalizedOrders;
        }
    }
}
