namespace Log_Analyzer___Most_Frequent_Error_Code
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> Codes = new List<string>
            {
                "E02","E01","E02","E01","E03","E02","E03","E02"
            };
            string result = Codes
                .GroupBy(c => c)
                .OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key)
                .First()
                .Key;
            
           Console.WriteLine($"Most Frequent Error: {result}");

        }
    }
}
