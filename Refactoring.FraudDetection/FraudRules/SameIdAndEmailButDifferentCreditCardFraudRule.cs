namespace Refactoring.FraudDetection.FraudRules
{
    using Refactoring.FraudDetection.Entities;

    public class SameIdAndEmailButDifferentCreditCardFraudRule : IFraudRule
    {
        public bool HaveFraudulentData(NormalizedOrder baseOrder, NormalizedOrder comparedOrder)
        {
            return baseOrder.DealId == comparedOrder.DealId
                        && baseOrder.Email == comparedOrder.Email
                        && baseOrder.CreditCard != comparedOrder.CreditCard;
        }
    }
}
