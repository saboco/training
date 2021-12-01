using System;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class ElementaryMathProblemTests
    {
        [Fact]
        public void GetOperatorsTest()
        {
            var pairs1 = new[]
            {
                (1,5),
                (3,3),
                (-1,-6),
                (2,2)
            };

            var operators1 = ElementaryMathProblem.GetOperationsSoResultIsDifferent(pairs1);
            var expected1 = new[] { 1, 1, 1, 2 };
            Common.AssertEqual(expected1, operators1);

            var pairs2 = new[]
            {
                (1,2),
                (2,1),
                (1,2),
                (2,1),
                (1,2)
            };

            var operators2 = ElementaryMathProblem.GetOperationsSoResultIsDifferent(pairs2);
            var expected2 = Array.Empty<int>();
            Common.AssertEqual(expected2, operators2);
        }
    }
}
