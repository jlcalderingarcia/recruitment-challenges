namespace Refactoring.FraudDetection.Parsing
{
    using System.Collections.Generic;
    using Refactoring.FraudDetection.Entities;

    public interface IOrdersParser
    {
        IEnumerable<Order> ParseOrders(IEnumerable<string> lines);
    }
}
