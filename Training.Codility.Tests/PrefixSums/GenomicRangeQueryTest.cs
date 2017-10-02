using NUnit.Framework;

namespace Training.Codility.Tests.PrefixSums
{
    public class GenomicRangeQueryTest
    {
        [TestCase("CAGCCTA", new[] { 2, 5, 0 }, new[] { 4, 5, 6 }, ExpectedResult = new[] { 2, 4, 1 })]
        public int[] Should_return_the_minimal_impact_factor_for_a_given_segment_of_a_dna_sequence(string s, int[] p, int[] q)
        {
            return Codility.PrefixSums.GenomicRangeQuery.Solution.Solve(s, p, q);
        }
    }
}