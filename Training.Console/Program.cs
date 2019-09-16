namespace Training.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "hello" + "world";
            System.Console.WriteLine(s);

            //Test(1);
        }

        private static void Test(int i)
        {
            System.Console.WriteLine(i);
            Test(i + 1);
        }
    }
}