using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Training.Leetcode.Tests
{
    public class LoggerRateLimiterTests
    {
        [Fact]
        public void Logger_should_not_log_same_message_withing_10_secs()
        {
            var logger = new Logger();
            // logging string "foo" at timestamp 1
            Assert.True(logger.ShouldPrintMessage(1, "foo"));

            // logging string "bar" at timestamp 2
            Assert.True(logger.ShouldPrintMessage(2, "bar"));

            // logging string "foo" at timestamp 3
            Assert.False(logger.ShouldPrintMessage(3, "foo"));

            // logging string "bar" at timestamp 8
            Assert.False(logger.ShouldPrintMessage(8, "bar"));

            // logging string "foo" at timestamp 10
            Assert.False(logger.ShouldPrintMessage(10, "foo"));

            // logging string "foo" at timestamp 11
            Assert.True(logger.ShouldPrintMessage(11, "foo"));
            
            // logging string "foo" at timestamp 11
            Assert.True(logger.ShouldPrintMessage(12, "bar"));

            // logging string "foo" at timestamp 11
            Assert.False(logger.ShouldPrintMessage(12, "foo"));

            Assert.False(logger.ShouldPrintMessage(20, "foo"));
            Assert.True(logger.ShouldPrintMessage(21, "foo"));
        }
    }
}
