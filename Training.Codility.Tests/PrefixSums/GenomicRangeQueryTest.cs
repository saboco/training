using Xunit;

namespace Training.Codility.Tests.PrefixSums
{
    public class GenomicRangeQueryTest
    {
        [Theory]
        [InlineData("CAGCCTA", new[] { 2, 5, 0 }, new[] { 4, 5, 6 }, new[] { 2, 4, 1 })]
        public void Should_return_the_minimal_impact_factor_for_a_given_segment_of_a_dna_sequence(string s, int[] p, int[] q, int[] expected)
        {
            Assert.Equal(expected, Codility.PrefixSums.GenomicRangeQuery.Solution.Solve(s, p, q));
        }
    }
}