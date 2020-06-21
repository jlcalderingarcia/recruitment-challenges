using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Refactoring.FraudDetection.Tests")]

namespace Refactoring.FraudDetection.Entities
{
    public class NormalizedOrder : Order
    {
        internal NormalizedOrder(
            int orderId,
            int dealId,
            string email,
            string street,
            string city,
            string state,
            string zipCode,
            string creditCard
        ) : base(orderId, dealId, email, street, city, state, zipCode, creditCard)
        {
        }
    }
}
