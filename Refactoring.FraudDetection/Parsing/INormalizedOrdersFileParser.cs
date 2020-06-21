namespace Refactoring.FraudDetection.Parsing
{
    using System.Collections.Generic;
    using Refactoring.FraudDetection.Entities;

    public interface INormalizedOrdersFileParser
    {
        IEnumerable<NormalizedOrder> ParseOrdersFile(string filePath);
    }
}
