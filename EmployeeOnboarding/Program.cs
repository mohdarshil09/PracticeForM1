namespace EmployeeOnboarding
{
    internal class Program
    {
        class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public double Salary { get; set; }

            public Employee(int id, string name, string email, double salary)
            {
                Id = id;
                Name= name;
                Salary = salary<=0?30000:salary;
                Email = (email == null || !email.Contains("@")) ? "unknown@company.com" : email;
            }
            public void DisplayInfo()
            {
                Console.WriteLine($"ID: {Id}, Name: {Name}, Email: {Email}, Salary: {Salary}");
            }
        }
        
        static void Main(string[] args)
        {
            Employee emp1 = new Employee(1, "Alice", "alice@gmail.com",50000);
            Employee emp2 = new Employee(2, "Bob", null, -1000);
            Employee emp3 = new Employee(3, "Charlie", "charliecompany.com", 45000);
            emp1.DisplayInfo();
            emp2.DisplayInfo(); 
            emp3.DisplayInfo();


        }
    }
}
