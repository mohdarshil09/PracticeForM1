using System.Security.Cryptography.X509Certificates;

namespace _01Strings
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the Sentence: ");
            string sent = Console.ReadLine();
            string[] words = sent.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int count = words.Length;
            //Array.Reverse(words);
            Console.WriteLine(string.Join(" ", words));
            for(int i = 0; i < count; i++)
            {
                char[] temp = words[i].ToCharArray();
                Array.Reverse(temp);
                words[i] = new string(temp);
            }
            Console.WriteLine(string.Join(" ", words));

            Array.Reverse(words);
            Console.WriteLine(string.Join(" ", words));





        }
    }
}
