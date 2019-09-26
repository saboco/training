using System.Collections.Generic;
using System.Linq;
using Xunit;
using Training.Tests.Common;

namespace Training.DataStructures.Tests
{    
    public class BinaryTreeTests
    {
        [Fact]
        public void Should_add_lesser_values_than_root_to_the_left()
        {
            var bt = new BinaryTree<int>(10);
            const int expected1 = 9;
            bt.Add(expected1);
            Assert.True(bt.Root.Left != null);
            Assert.Equal(expected1, bt.Root.Left.Data);

            const int expected2 = 8;
            bt.Add(expected2);
            Assert.True(bt.Root.Left.Left != null);
            Assert.Equal(expected2, bt.Root.Left.Left.Data);
        }

        [Fact]
        public void Should_add_equals_values_than_root_to_the_left()
        {
            var bt = new BinaryTree<int>(5);
            const int expected = 5;
            bt.Add(expected);
            Assert.True(bt.Root.Left != null);
            Assert.Equal(expected, bt.Root.Left.Data);

            bt.Add(expected);
            Assert.True(bt.Root.Left.Left != null);
            Assert.Equal(expected, bt.Root.Left.Left.Data);
        }

        [Fact]
        public void Should_add_greater_values_than_root_to_the_right()
        {
            var bt = new BinaryTree<int>(6);
            const int expected1 = 9;
            bt.Add(expected1);
            Assert.True(bt.Root.Right != null);
            Assert.Equal(expected1, bt.Root.Right.Data);

            const int expected2 = 10;
            bt.Add(expected2);
            Assert.True(bt.Root.Right.Right != null);
            Assert.Equal(expected2, bt.Root.Right.Right.Data);
        }

        [Fact]
        public void Should_print_in_order_when_calling_in_order_print()
        {
            var printer = new FakePrinter();
            var values = new[] {10, 10, 12, 5, 9, 15};
            var bt = CreateTreeWithValues(values);
            bt.PrintInOrder(printer);

            var lastValue = int.MinValue;
            foreach (var p in printer.Printed)
            {
                var value = (int) p;
                Assert.True(lastValue <= value);
                lastValue = value;
            }
            var orderedValues = values.OrderBy(i => i).ToArray();
            var printedValues = printer.Printed.Select(o => (int) o).ToArray();
            Assert.Equal(orderedValues, printedValues);
        }

        [Fact]
        public void Should_return_whether_or_not_it_contains_a_value()
        {
            var values = new[] {10, 10, 12, 5, 9, 15};
            var bt = CreateTreeWithValues(values);

            Assert.True(bt.Contains(10));
            Assert.True(bt.Contains(12));
            Assert.True(bt.Contains(5));
            Assert.True(bt.Contains(9));
            Assert.True(bt.Contains(15));

            Assert.False(bt.Contains(11));
            Assert.False(bt.Contains(13));
            Assert.False(bt.Contains(7));
            Assert.False(bt.Contains(20));
        }

        private static BinaryTree<int> CreateTreeWithValues(IEnumerable<int> values)
        {
            BinaryTree<int> bt = null;
            foreach (var value in values)
            {
                if (bt == null)
                {
                    bt = new BinaryTree<int>(value);
                    continue;
                }
                bt.Add(value);
            }
            return bt;
        }
    }
}