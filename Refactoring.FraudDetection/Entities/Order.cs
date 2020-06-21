namespace Refactoring.FraudDetection.Entities
{
    using Refactoring.FraudDetection.Validation;

    public class Order
    {
        public Order(
            int orderId,
            int dealId,
            string email,
            string street,
            string city,
            string state,
            string zipCode,
            string creditCard
        )
        {
            ValidateOrder(orderId, dealId, email, street, city, state, zipCode, creditCard);

            OrderId = orderId;
            DealId = dealId;
            Email = email;
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
            CreditCard = creditCard;
        }

        public int OrderId { get; }

        public int DealId { get; }

        public string Email { get; }

        public string Street { get; }

        public string City { get; }

        public string State { get; }

        public string ZipCode { get; }

        public string CreditCard { get; }

        private static void ValidateOrder(int orderId, int dealId, string email, string street, string city, string state, string zipCode, string creditCard)
        {
            var validationResult = new ValidationResult();

            if (orderId <= 0)
                validationResult.Add(nameof(orderId), "The order id should be a positive number");

            if (dealId <= 0)
                validationResult.Add(nameof(dealId), "The Deal id should be a positive number");

            if (string.IsNullOrWhiteSpace(email))
                validationResult.Add(nameof(email), "Email cannot be null or white space");

            if (string.IsNullOrWhiteSpace(street))
                validationResult.Add(nameof(street), "Street cannot be null or white space");

            if (string.IsNullOrWhiteSpace(city))
                validationResult.Add(nameof(city), "City cannot be null or white space");

            if (string.IsNullOrWhiteSpace(state))
                validationResult.Add(nameof(state), "State cannot be null or white space");

            if (string.IsNullOrWhiteSpace(zipCode))
                validationResult.Add(nameof(zipCode), "Zip code cannot be null or white space");

            if (string.IsNullOrWhiteSpace(creditCard))
                validationResult.Add(nameof(creditCard), "Credit card cannot be null or white space");

            // The following validations could be applied but the business rules are not clear
            // - Emails should match email regular expression
            // - State can only have 2 characters and be in a limited set of values
            // - Credit card should have specific format (Or might be a separated entity with more detailed validations)
            // - Zip Code might have specific value

            validationResult.ThrowIfInvalid();
        }
    }
}
