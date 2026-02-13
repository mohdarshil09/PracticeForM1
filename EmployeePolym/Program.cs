namespace EmployeePolym
{
    internal class Program
    {
        class Employee
        {
           public virtual double GetSalary()
            {
                return 0;
            }
        }
        class FullTimeEmployee:Employee
        {
            public override double GetSalary()
            {
                return 50000;
            }

        }
        class PartTimeEmployee : Employee
        {
            public override double GetSalary()
            {
                return 20000;
            }
        }
        static void Main(string[] args)
        {
            Employee emp1 = new FullTimeEmployee();
            Console.WriteLine($"Full Time Employee Salary: {emp1.GetSalary()}");
            Employee emp2 = new PartTimeEmployee();
            Console.WriteLine($"Part time Employee Salary: {emp2.GetSalary()}");
           
        }
    }
}
