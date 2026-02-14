using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace StringManupulation
{
    internal class Program
    {

        static List<int> Checked(string[] input, int n) {
            List<int> ans = new List<int>();
            foreach(string it in input)
            {
                if(!string.IsNullOrEmpty(it) && it.Length>=3 && char.IsLetter(it[0]) && char.IsDigit(it[it.Length - 1]))
                {
                    ans.Add(1);
                }
                else
                {
                    ans.Add(0);
                }
            }
            return ans;
        }

        static void Main(string[] args)
        {
                Console.WriteLine("Number of testCase:");
                int n =int.Parse( Console.ReadLine());
            string[] input = new string[n];
            for(int i=0;i<n;i++)
            {
                input[i] = Console.ReadLine();
            }
            List<int> result = Checked(input, n);
            foreach (int val in result)
            {
                Console.WriteLine(val);
            }
            
        }
    }
}
