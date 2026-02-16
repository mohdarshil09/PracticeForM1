namespace Sort_Characters_by_Frequency__desc_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the string: ");
            string s = Console.ReadLine();
            Console.WriteLine("Sorted string: ");
            Console.WriteLine(Solve(s));
        }
        static string Solve(string s)
        {
            Dictionary<char, int> freq = new Dictionary<char, int>();
            foreach(char it in s)
            {
                freq[it] = freq.ContainsKey(it)?freq[it]+1:1;
            }
            var sorted = freq
                .OrderByDescending(p => p.Value)
                .ThenBy(p => p.Key);


            return string.Concat(sorted.SelectMany(x => Enumerable.Repeat(x.Key, x.Value)));
        }
    }
}
