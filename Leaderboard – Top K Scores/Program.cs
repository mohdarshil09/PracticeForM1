namespace Leaderboard___Top_K_Scores
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<(string name,int score)> players = new List<(string , int)>
            {
                ("Alice", 95),
                ("Bob", 85),
                ("Charlie", 90),
                ("David", 80),
                ("Eve", 92)
            };
            int k = 3;
            var topScores=players.OrderByDescending(p=>p.score).ThenBy(p=>p.name).Take(k).ToList();
            Console.WriteLine("TopScores");
            foreach(var it in topScores)
            {
                Console.WriteLine($"{it.name}: {it.score}");
            }
        }
    }
}
