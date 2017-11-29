using System;
using System.Threading;

namespace Training.Threads
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // give this
            for (var i = 1; i <= 4; i++)
            {
                var t = new Thread(delegate() { Console.Write(i); });
                t.Start();
            }
            // find the bug and corret it:

            for (var i = 1; i <= 4; i++)
            {
                var j = i; // should make a closure to corret the bug in the code above
                var t = new Thread(delegate() { Console.Write(j); });
                t.Start();
            }
        }
    }
}