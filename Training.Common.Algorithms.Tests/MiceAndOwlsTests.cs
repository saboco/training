using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class MiceAndOwlsTests
    {
        [Fact]
        public void MaxMiceSavedTest()
        {
            var mice = new[]
            {
                (1,0),
                (0,1),
                (8,1),
                (12,0),
                (12,4),
                (15,5)
            };

            var holes = new []
            { 
                (1,1,1),
                (10,2,2),
                (14,5,1)
            };

            var miceAndOwls = new MiceAndOwls(mice, holes, 3);
            Assert.Equal(4, miceAndOwls.MaxMiceSaved());
        }
    }
}
