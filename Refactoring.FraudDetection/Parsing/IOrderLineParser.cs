namespace Refactoring.FraudDetection.Parsing
{
    using Refactoring.FraudDetection.Entities;

    public interface IOrderLineParser
    {
        Order ParseOrderLine(string orderLine);
    }
}
