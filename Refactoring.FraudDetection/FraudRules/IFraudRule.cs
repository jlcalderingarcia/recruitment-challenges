namespace Refactoring.FraudDetection.FraudRules
{
    using Refactoring.FraudDetection.Entities;

    public interface IFraudRule
    {
        bool HaveFraudulentData(NormalizedOrder baseOrder, NormalizedOrder comparedOrder);
    }
}
