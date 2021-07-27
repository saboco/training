using System;
using System.Collections.Generic;
using System.Linq;
using Training.Common.Algorithms;

namespace Training.HackerRank.Algorithms
{
    public class IceCreamParlor
    {
        private readonly IceCreamFlavor[] _iceCreamFlavors;
        private readonly int _money;

        public IceCreamParlor(int money, int[] iceCreamFlavors)
        {
            var tempFlavors = new List<IceCreamFlavor>();
            for (var i = 0; i < iceCreamFlavors.Length; i++)
            {
                tempFlavors.Add(new IceCreamFlavor(i, iceCreamFlavors[i]));
            }
            _iceCreamFlavors = tempFlavors.OrderBy(icf => icf.Cost).ToArray();
            _money = money;
        }

        public string GetFlavors()
        {
            var firstFlavor = IceCreamFlavor.Empty;
            var secondFlavor = IceCreamFlavor.Empty;

            foreach (var iceCreamFlavor in _iceCreamFlavors)
            {
                firstFlavor = iceCreamFlavor;
                secondFlavor = new IceCreamFlavor(-1, _money - iceCreamFlavor.Cost);
                if (secondFlavor.Cost <= 0)
                {
                    continue;
                }

                var secondFlavorIndex = BinarySearch.Search(_iceCreamFlavors, secondFlavor);

                if (secondFlavorIndex <= -1)
                {
                    continue;
                }

                secondFlavor = _iceCreamFlavors[secondFlavorIndex];
                break;
            }

            return firstFlavor.Index <  secondFlavor.Index 
                ? $"{firstFlavor.Index + 1} {secondFlavor.Index + 1}"
                : $"{secondFlavor.Index + 1} {firstFlavor.Index + 1}";
        }
        
        private class IceCreamFlavor : IComparable<IceCreamFlavor>, IComparable
        {
            public static readonly IceCreamFlavor Empty = new IceCreamFlavor(-1, 0);

            public int Index { get; }
            public int Cost { get; }

            public IceCreamFlavor(int index, int cost)
            {
                Index = index;
                Cost = cost;
            }

            public int CompareTo(IceCreamFlavor other)
            {
                if (ReferenceEquals(this, other))
                {
                    return 0;
                }

                if (ReferenceEquals(null, other))
                {
                    return 1;
                }

                return Cost.CompareTo(other.Cost);
            }

            public static bool operator <(IceCreamFlavor left, IceCreamFlavor right)
            {
                return Comparer<IceCreamFlavor>.Default.Compare(left, right) < 0;
            }

            public static bool operator >(IceCreamFlavor left, IceCreamFlavor right)
            {
                return Comparer<IceCreamFlavor>.Default.Compare(left, right) > 0;
            }

            public static bool operator <=(IceCreamFlavor left, IceCreamFlavor right)
            {
                return Comparer<IceCreamFlavor>.Default.Compare(left, right) <= 0;
            }

            public static bool operator >=(IceCreamFlavor left, IceCreamFlavor right)
            {
                return Comparer<IceCreamFlavor>.Default.Compare(left, right) >= 0;
            }

            public int CompareTo(object obj)
            {
                if (!(obj is IceCreamFlavor iceCreameFlavor))
                {
                    throw new InvalidOperationException(
                        $"Can't compare type of {obj.GetType().FullName} with {typeof(IceCreamFlavor)}");
                }

                return CompareTo(iceCreameFlavor);
            }
        }
    }
}