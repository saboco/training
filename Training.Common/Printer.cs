using System;

namespace Training.Common
{
    public class Printer : IPrint
    {
        public void Print(object data)
        {
            Console.WriteLine(data);
        }
    }
}