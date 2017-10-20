using System;

namespace Training.Common
{
    public static class Boxing
    {
        public static void Box(int i)
        {
            object a = i; // the obvious boxing operation

            // less obvious boxing operations
            IEquatable<int> b = i;
            IComparable c = i;
            dynamic d = i;

            // strictly speaking not a boxing operation (on IL) 
            // but still there is an allocation on the heap 
            // and the value is copied to an internal field/property of the class
            MyBoxingClass e = i;

            Console.WriteLine(i);
            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.WriteLine(c);
            Console.WriteLine(d);
            Console.WriteLine(e);
        }
    }

    public class MyBoxingClass
    {
        private int Value { get; }

        private MyBoxingClass(int i)
        {
            Value = i;
        }

        public static implicit operator MyBoxingClass(int i)
        {
            return new MyBoxingClass(i);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}