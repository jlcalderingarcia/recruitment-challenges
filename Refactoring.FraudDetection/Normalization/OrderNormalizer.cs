namespace Refactoring.FraudDetection.Normalization
{
    using System;
    using Refactoring.FraudDetection.Entities;

    public class OrderNormalizer : IOrderNormalizer
    {
        public NormalizedOrder Normalize(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order), "The given order should not be null");

            return new NormalizedOrder(
                    order.OrderId,
                    order.DealId,
                    NormalizeEmail(order.Email),
                    NormalizeStreet(order.Street),
                    order.City,
                    NormalizeState(order.State),
                    order.ZipCode,
                    order.CreditCard
                );
        }

        private static string NormalizeEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            return string.Join("@", new string[] { aux[0], aux[1] });
        }

        private static string NormalizeStreet(string street)
        {
            return street.Replace("st.", "street").Replace("rd.", "road");
        }

        private static string NormalizeState(string state)
        {
            return state.Replace("il", "illinois").Replace("ca", "california").Replace("ny", "new york");
        }
    }
}
