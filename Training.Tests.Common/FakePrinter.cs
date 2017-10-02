using System.Collections.Generic;
using Training.Common;

namespace Training.Tests.Common
{
    public class FakePrinter : IPrint
    {
        private readonly string _format;
        public List<object> Printed = new List<object>();

        public FakePrinter(string format = "")
        {
            _format = format;
        }

        public void Print(object data)
        {
            Printed.Add(_format == string.Empty
                ? data
                : string.Format(_format, data).Replace(",", "."));
        }
    }
}