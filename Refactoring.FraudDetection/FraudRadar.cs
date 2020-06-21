// <copyright file="FraudRadar.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

namespace Refactoring.FraudDetection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Refactoring.FraudDetection.Entities;
    using Refactoring.FraudDetection.FraudRules;

    public class FraudRadar : IFraudRadar
    {
        private readonly List<IFraudRule> _fraudRules;

        public FraudRadar(
            IEnumerable<IFraudRule> fraudRules
        ) {
            _fraudRules = fraudRules?.ToList() ?? throw new ArgumentNullException(nameof(fraudRules));

            if (fraudRules.Count() == 0)
                throw new ArgumentException("The list of fraud rules should not be empty", nameof(fraudRules));
        }

        // The check fraud mechanism was refactored but not changed
        // since the domain rules and final expectations are not clear enough
        // There are several improvements that can be applied to decrease the computational complexity of the algorithm
        // But I guess the more important task is the refactoring
        public IEnumerable<FraudResult> Check(IEnumerable<NormalizedOrder> orders)
        {
            var ordersList = orders.ToList();
            var fraudResults = new List<FraudResult>();

            // CHECK FRAUD
            for (int i = 0; i < ordersList.Count; i++)
            {
                var current = ordersList[i];

                for (int j = i + 1; j < ordersList.Count; j++)
                {
                    if (_fraudRules.Any(fraudRule => fraudRule.HaveFraudulentData(current, ordersList[j])))
                    {
                        fraudResults.Add(new FraudResult(ordersList[j].OrderId, true));
                    }
                }
            }

            return fraudResults;
        }
    }
}