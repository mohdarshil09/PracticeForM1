namespace DateAndTime
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Dictionary<string, DateTime> reportMap = new Dictionary<string, DateTime>();

            Console.WriteLine("Enter number of reports to be added");
            int n = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the Forensic reports (Reporting Officer: Report Filed Date)");
            for (int i = 0; i < n; i++)
            {
                string input = Console.ReadLine();
                string[] parts = input.Split(':');

                string officer = parts[0];
                DateTime date = DateTime.Parse(parts[1]);

                reportMap.Add(officer, date);
            }

            Console.WriteLine("Enter the filed date to identify the reporting officers");
            DateTime searchDate = DateTime.Parse(Console.ReadLine());

            bool found = false;

            foreach (var item in reportMap)
            {
                if (item.Value.Date == searchDate.Date)
                {
                    Console.WriteLine(item.Key);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No reporting officer filed the report");
            }
        }
    }
}
