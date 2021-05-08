using System.Collections.Generic;

namespace Training.CrackingCodingInterview
{
    public class SortStack
    {
        public static void SortUsingList(Stack s)
        {
            var l = new List<int>();
            while (!s.Empty)
            {
                l.Add(s.Pop());
            }
            l.Sort();
            for (var i = l.Count - 1; i > 0; i--)
            {
                s.Push(l[i]);
            }
        }

        public static Stack Sort(Stack s)
        {
            var sorted = new Stack();

            sorted.Push(s.Pop());
            while(!s.Empty)
            {   
                var tmp = s.Pop();
                while(!sorted.Empty && sorted.Peek() < tmp)
                {   
                    s.Push(sorted.Pop());
                }
                sorted.Push(tmp);
            }
            return sorted;
        }

        public static void SortWithoutReturning(Stack s)
        {
            var sorted = new Stack();

            sorted.Push(s.Pop());
            while(!s.Empty)
            {   
                var tmp = s.Pop();
                while(!sorted.Empty && sorted.Peek() > tmp)
                {   
                    s.Push(sorted.Pop());
                }
                sorted.Push(tmp);
            }
            while(!sorted.Empty)
            { 
                s.Push(sorted.Pop());
            }
        }
    }
}
