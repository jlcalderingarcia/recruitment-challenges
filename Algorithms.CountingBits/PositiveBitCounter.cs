// <copyright file="PositiveBitCounter.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

namespace Algorithms.CountingBits
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PositiveBitCounter
    {
        public IEnumerable<int> Count(int input)
        {
            if (input < 0)
                throw new ArgumentException("The input should be a non-negative number", nameof(input));

            var result = new int[32];

            var bitsCount = 0;
            var missingBits = input;
            for (int currentBit = 0; missingBits != 0 && currentBit < 32; currentBit++)
            {
                if ((missingBits % 2) == 1)
                {
                    bitsCount++;
                    result[bitsCount] = currentBit;
                }
                missingBits >>= 1;
            }
            result[0] = bitsCount;

            return result.Take(result[0] + 1);
        }
    }
}
