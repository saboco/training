using System;
using System.Collections.Generic;
using System.Linq;

namespace Training.CrackingCodingInterview
{
    public class CircusTower
    {
        public static int MaxHeight((int, int)[] people)
        {
            var comparer = Comparer<(int, int)>.Create((a, b) => a.Item1 == b.Item1 ? 0 : a.Item1 < b.Item1 ? -1 : 1);
            Array.Sort(people, comparer);

            var towers = new List<(int, int)[]>();
            if (!WeightIsInOrder(people))
            {
                MaxHeight(people, 0, new List<(int, int)>(), towers);
            }
            else
            {
                towers.Add(people);
            }
            return towers.Max(t => t.Length);
        }

        private static void MaxHeight((int, int)[] people, int index, List<(int, int)> current, List<(int, int)[]> towers)
        {
            if (index == people.Length)
            {
                if (WeightIsInOrder(current))
                {
                    towers.Add(current.ToArray());
                }
            }

            for (var i = index; i < people.Length; i++)
            {
                current.Add(people[i]);
                MaxHeight(people, i + 1, current, towers);
                current.RemoveAt(current.Count - 1);
            }
        }

        private static bool WeightIsInOrder(IList<(int, int)> people)
        {
            var (_, prev) = people[0];
            for (var i = 1; i < people.Count; i++)
            {
                var (_, current) = people[i];
                if (prev > current)
                {
                    return false;
                }
                prev = current;
            }
            return true;
        }
    }
}
