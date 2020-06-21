namespace Refactoring.FraudDetection.Parsing
{
    using System;
    using Refactoring.FraudDetection.Entities;
    using Refactoring.FraudDetection.Validation;

    public class OrderLineParser : IOrderLineParser
    {
        public Order ParseOrderLine(string orderLine)
        {
            ValidateLine(orderLine);

            var items = orderLine.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            ValidateLineItems(items);

            var order = new Order
            (
                int.Parse(items[0]),
                int.Parse(items[1]),
                items[2].ToLower(),
                items[3].ToLower(),
                items[4].ToLower(),
                items[5].ToLower(),
                items[6],
                items[7]
            );

            return order;
        }

        private void ValidateLine(string orderLine)
        {
            var validationResult = new ValidationResult();

            if (string.IsNullOrWhiteSpace(orderLine))
                validationResult.Add(nameof(orderLine), "The order line should not be null or white space");

            if (orderLine?.Contains('\n') == true)
                validationResult.Add(nameof(orderLine), "The order should be contained in a single line");

            validationResult.ThrowIfInvalid();
        }

        private void ValidateLineItems(string[] lineItems)
        {
            var validationResult = new ValidationResult();

            if (lineItems.Length != 8)
                validationResult.Add(nameof(lineItems), $"The order line contains {lineItems.Length} instead of the 8 expected");

            if (!int.TryParse(lineItems[0], out _))
                validationResult.Add(nameof(Order.OrderId), "Order id should be a valid number");

            if (!int.TryParse(lineItems[1], out _))
                validationResult.Add(nameof(Order.DealId), "Deal id should be a valid number");

            validationResult.ThrowIfInvalid();
        }
    }
}
