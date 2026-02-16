namespace First_Non_Repeating_Character
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the string: ");
            string s = Console.ReadLine();
            Console.WriteLine("Output:");
            Console.WriteLine(Solve(s));
        }
        static string Solve(string s)
        {
            Dictionary<char, int> freq = new Dictionary<char, int>();
            for(int i=0;i<s.Length;i++)
            {
                char it = s[i];
                freq[it] = freq.ContainsKey(it) ? freq[it] + 1 : 1;
            }
            foreach(char it in s)
            {
                if (freq[it] == 1)
                {
                    return char.ToString(it);
                }
            }
            return "-1";
        }
    }
}
