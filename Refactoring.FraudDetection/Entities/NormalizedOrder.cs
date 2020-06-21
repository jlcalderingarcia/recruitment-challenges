namespace Refactoring.FraudDetection.Entities
{
    // This entity is intended to be created only the the Normalization classs,
    // but it would increase the tests complexity
    public class NormalizedOrder : Order
    {
        public NormalizedOrder(
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
