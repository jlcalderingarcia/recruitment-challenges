// <copyright file="PositiveBitCounter.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

namespace Algorithms.CountingBits
{
    using System;
    using System.Collections.Generic;

    public class PositiveBitCounter
    {
        public IEnumerable<int> Count(int input)
        {
            if (input < 0)
                throw new ArgumentException("The input should be a non-negative number", nameof(input));

            var result = new LinkedList<int>();

            var bitsCount = 0;
            var missingBits = input;
            for (int i = 0; missingBits != 0 && i < 32; i++)
            {
                if ((missingBits & 1) == 1)
                {
                    bitsCount++;
                    result.AddLast(i);
                }
                missingBits >>= 1;
            }
            result.AddFirst(bitsCount);

            return result;
        }
    }
}
