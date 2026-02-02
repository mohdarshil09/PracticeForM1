using System;

namespace M1Exception
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EntryUtility utility = new EntryUtility();

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string input = Console.ReadLine();
                string[] parts = input.Split(':');

                string employeeId = parts[0];
                int duration = int.Parse(parts[2]);

                try
                {
                    utility.ValidateEmployeeId(employeeId);
                    utility.ValidateDuration(duration);

                    Console.WriteLine("Valid entry details");
                }
                catch (InvalidEntryException)
                {
                    Console.WriteLine("Invalid entry details");
                }
            }
        }
    }
}
