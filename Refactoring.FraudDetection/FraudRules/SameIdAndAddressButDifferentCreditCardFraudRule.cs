namespace Refactoring.FraudDetection.FraudRules
{
    using Refactoring.FraudDetection.Entities;

    public class SameIdAndAddressButDifferentCreditCardFraudRule : IFraudRule
    {
        public bool HaveFraudulentData(NormalizedOrder baseOrder, NormalizedOrder comparedOrder)
        {
            return baseOrder.DealId == comparedOrder.DealId
                        && baseOrder.State == comparedOrder.State
                        && baseOrder.ZipCode == comparedOrder.ZipCode
                        && baseOrder.Street == comparedOrder.Street
                        && baseOrder.City == comparedOrder.City
                        && baseOrder.CreditCard != comparedOrder.CreditCard;
        }
    }
}
