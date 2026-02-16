//Scenario: Employee IDs are scanned at a gate. Duplicates occur when someone scans again.

using System.Runtime.Serialization;

namespace Attendance___First_Unique_Entry
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Entry of Employee");
            string input = Console.ReadLine();
            string[] strings = input.Split(',', StringSplitOptions.RemoveEmptyEntries);

            List<int> scans = new List<int>();
            foreach (var s in strings) {
                scans.Add(int.Parse(s.Trim()));
            }
            List<int> uniqueScans = getfirstUnique(scans);
            Console.WriteLine(string.Join(", ",uniqueScans));
        }
        static List<int> getfirstUnique(List<int> scans)
        {
            List<int> res = new List<int>();
            HashSet<int> unique = new HashSet<int>();
            foreach (int it in scans)
            {
                if (!unique.Contains(it)){
                    unique.Add(it);
                    res.Add(it);
                }
            }
            return res;
        }
     }

}
