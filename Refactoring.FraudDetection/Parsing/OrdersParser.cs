namespace Refactoring.FraudDetection.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Refactoring.FraudDetection.Entities;
    using Refactoring.FraudDetection.Exceptions;
    using Refactoring.FraudDetection.Validation;

    public class OrdersParser : IOrdersParser
    {
        private readonly IOrderLineParser _orderLineParser;

        public OrdersParser(
            IOrderLineParser orderLineParser
        ) {
            _orderLineParser = orderLineParser ?? throw new ArgumentNullException(nameof(orderLineParser));
        }

        public IEnumerable<Order> ParseOrders(IEnumerable<string> lines)
        {
            ValidateOrdersInput(lines);

            var validationResult = new ValidationResult();

            var orders = new List<Order>();
            ParseLines(lines, validationResult, orders);

            ValidateOrderIds(orders, validationResult);

            validationResult.ThrowIfInvalid();

            return orders;
        }

        private void ParseLines(IEnumerable<string> lines, ValidationResult validationResult, List<Order> orders)
        {
            int index = 0;
            foreach (var line in lines)
            {
                try
                {
                    var order = _orderLineParser.ParseOrderLine(line);

                    orders.Add(order);
                }
                catch (ValidationException validationException)
                {
                    foreach (var validationError in validationException.ValidationResult.ValidationErrors)
                        validationResult.Add($"{nameof(lines)}[{index}].{validationError.FieldName}", validationError.ErrorMessage);
                }
                index++;
            }
        }

        // On this method we are vaidating the order id should in range and contains all the numbers from 1 to the number of lines
        // but the secuential order is not mandatory since it is not clear if it can be changed
        private void ValidateOrderIds(List<Order> orders, ValidationResult validationResult)
        {
            for (int i = 0; i < orders.Count; i++)
            {
                var order = orders[i];
                if (order.OrderId > orders.Count)
                    validationResult.Add($"lines[{i}].{nameof(NormalizedOrder.OrderId)}", "Order Id is greater than the orders count");
                if (orders.Where(o => o.OrderId == order.OrderId).Count() > 1)
                    validationResult.Add($"lines[{i}].{nameof(NormalizedOrder.OrderId)}", "Order Id is repeated in the orders collection");
            }
        }

        private void ValidateOrdersInput(IEnumerable<string> lines)
        {
            if (lines == null)
                throw new ArgumentNullException(nameof(lines), "Lines should not be null");
        }
    }
}
