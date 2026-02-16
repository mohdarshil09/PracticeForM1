namespace Count_Words_and_Print_by_Highest_Count
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the string:");
            string s = Console.ReadLine();
            Console.WriteLine("Result:");
            Console.WriteLine(Solve(s));
        }
        static string Solve(string s)
        {
            Dictionary<string, int> freq = new Dictionary<string, int>();
            string[] words = s.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                string w=word.ToLower();
                freq[w] = freq.ContainsKey(w) ? freq[w] + 1 : 1;
            }
            var sorted = freq
                .OrderByDescending(p => p.Value)
                .ThenBy(p => p.Key);
            return string.Join(Environment.NewLine, sorted.Select(p => $"{p.Key}: {p.Value}"));
        }
    }
}
