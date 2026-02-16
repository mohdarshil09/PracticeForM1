namespace Bank_Statement___Spend_by_Category
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<(string category, double amount)> transactions = new List<(string, double)>
            {
                ("Groceries", -150),
                ("Utilities", -80),
                ("Entertainment", -120),
                ("Groceries", -200),
                ("Utilities", -60),
                ("Entertainment", -90),
                ("Salary", 3000)
            };

            //var spendByCategory = transactions
            //    .Where(t => t.amount < 0)
            //    .GroupBy(t => t.category)
            //    .ToDictionary(
            //    g => g.Key,
            //        g => g.Sum(t => Math.Abs(t.amount))
            //    );


            var spendByCategory=transactions
                .Where(p=>p.amount<0)
                .GroupBy(p=>p.category)
                .ToDictionary(
                p=>p.Key,
                p=>p.Sum(t=>Math.Abs(t.amount))
                );

            foreach (var spend in spendByCategory)
            {
                Console.WriteLine($" {spend.Key}: {spend.Value}");
            }
        }
    }
   
}
