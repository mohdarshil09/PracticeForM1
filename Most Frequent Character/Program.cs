namespace Most_Frequent_Character
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a string:");
            string s = Console.ReadLine();
            Console.WriteLine("Most Frequent character:");
            Console.WriteLine(Solve(s));

        }
        static string Solve(string s)
        {
            Dictionary<char, int> freq = new Dictionary<char, int>();
            foreach(char it in s)
            {
                freq[it] = freq.ContainsKey(it) ? freq[it] + 1 : 1;
            }
            var count = freq.OrderByDescending(p => p.Value)
                .ThenBy(p => p.Key)
                .First();

            return count.Key.ToString();
        }
        

        
    }
}
