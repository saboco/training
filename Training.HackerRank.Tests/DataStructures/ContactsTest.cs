using Xunit;
using Training.HackerRank.DataStructures;
using Training.Tests.Common;

namespace Training.HackerRank.Tests.DataStructures
{    
    public class ContactsTest
    {
        [Fact]
        public void Should_treat_contact_operations_when_received()
        {
            var input = new[] { "add hack", "add hackerrank", "find hac", "find hak" };
            var output = new[] { 2, 0 };

            var printer = new FakePrinter("{0:0}");
            var contacts = new Contacts(printer);

            foreach (var s in input)
            {
                contacts.TreatTransaction(s);
            }

            var i = 0;
            foreach (var o in output)
            {
                Assert.Equal(o, int.Parse((string)printer.Printed[i++]));
            }
        }
    }
}