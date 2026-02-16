using System.Text;

namespace Palindrome_Check__ignore_spaces___case_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the string:");
            string s = Console.ReadLine();
            Console.WriteLine(Solve(s));
        }
        static string Solve(string s)
        {
            StringBuilder cleaner=new StringBuilder();
            foreach(char it in s)
            {
                if(char.IsLetterOrDigit(it))
                {
                    cleaner.Append(char.ToLower(it));
                }
            }
           
            int i = 0;int j=cleaner.Length-1;
            while (i < j)
            {
                if (cleaner[i]!=cleaner[j])
                {
                    return "false";
                }
                else
                {
                    i++;j--;
                }
            }
            return "True";
        }

    }
}
