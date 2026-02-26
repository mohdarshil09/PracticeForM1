using System.Text.RegularExpressions;
namespace Validate_Email
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            string email = "mdsjf@gmail.com";

            bool m = Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            Console.WriteLine(m);
        }
    }
}
