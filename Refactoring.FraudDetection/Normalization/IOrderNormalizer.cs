namespace Refactoring.FraudDetection.Normalization
{
    using Refactoring.FraudDetection.Entities;

    public interface IOrderNormalizer
    {
        NormalizedOrder Normalize(Order order);
    }
}
