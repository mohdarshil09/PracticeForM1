using System.Text.RegularExpressions;

namespace Extract_Numbers_from_a_Sentence
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            String st = "Order 45 items and 120 books";
            var mtch= Regex.Matches(st, @"\d+");
            foreach(Match m in mtch)
            {
                Console.WriteLine(m.Value);
            }
        }
    }
}
