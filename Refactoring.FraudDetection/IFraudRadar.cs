namespace Refactoring.FraudDetection
{
    using System.Collections.Generic;
    using Refactoring.FraudDetection.Entities;

    public interface IFraudRadar
    {
        IEnumerable<FraudResult> Check(IEnumerable<NormalizedOrder> orders);
    }
}
