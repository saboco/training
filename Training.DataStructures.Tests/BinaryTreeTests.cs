using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Training.Tests.Common;

namespace Training.DataStructures.Tests
{
    [TestFixture]
    public class BinaryTreeTests
    {
        [Test]
        public void Should_add_lesser_values_than_root_to_the_left()
        {
            var bt = new BinaryTree<int>(10);
            const int expected1 = 9;
            bt.Add(expected1);
            Assert.IsTrue(bt.Root.Left != null);
            Assert.AreEqual(expected1, bt.Root.Left.Data);

            const int expected2 = 8;
            bt.Add(expected2);
            Assert.IsTrue(bt.Root.Left.Left != null);
            Assert.AreEqual(expected2, bt.Root.Left.Left.Data);
        }

        [Test]
        public void Should_add_equals_values_than_root_to_the_left()
        {
            var bt = new BinaryTree<int>(5);
            const int expected = 5;
            bt.Add(expected);
            Assert.IsTrue(bt.Root.Left != null);
            Assert.AreEqual(expected, bt.Root.Left.Data);

            bt.Add(expected);
            Assert.IsTrue(bt.Root.Left.Left != null);
            Assert.AreEqual(expected, bt.Root.Left.Left.Data);
        }

        [Test]
        public void Should_add_greater_values_than_root_to_the_right()
        {
            var bt = new BinaryTree<int>(6);
            const int expected1 = 9;
            bt.Add(expected1);
            Assert.IsTrue(bt.Root.Right != null);
            Assert.AreEqual(expected1, bt.Root.Right.Data);

            const int expected2 = 10;
            bt.Add(expected2);
            Assert.IsTrue(bt.Root.Right.Right != null);
            Assert.AreEqual(expected2, bt.Root.Right.Right.Data);
        }

        [Test]
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
                Assert.IsTrue(lastValue <= value);
                lastValue = value;
            }
            var orderedValues = values.OrderBy(i => i).ToArray();
            var printedValues = printer.Printed.Select(o => (int) o).ToArray();
            Assert.AreEqual(orderedValues, printedValues);
        }

        [Test]
        public void Should_return_whether_or_not_it_contains_a_value()
        {
            var values = new[] {10, 10, 12, 5, 9, 15};
            var bt = CreateTreeWithValues(values);
            
            Assert.IsTrue(bt.Contains(10));
            Assert.IsTrue(bt.Contains(12));
            Assert.IsTrue(bt.Contains(5));
            Assert.IsTrue(bt.Contains(9));
            Assert.IsTrue(bt.Contains(15));
            
            Assert.IsFalse(bt.Contains(11));
            Assert.IsFalse(bt.Contains(13));
            Assert.IsFalse(bt.Contains(7));
            Assert.IsFalse(bt.Contains(20));
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