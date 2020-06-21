// <copyright file="FraudRadar.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

namespace Refactoring.FraudDetection
{
    public class FraudResult
    {
        internal FraudResult(
            int orderId,
            bool isFraudulent
        ) {
            OrderId = orderId;
            IsFraudulent = isFraudulent;
        }

        public int OrderId { get; }

        public bool IsFraudulent { get; }
    }
}