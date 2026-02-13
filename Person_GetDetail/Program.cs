using System.Security.Cryptography.X509Certificates;

namespace Person_GetDetail
{
    internal class Program
    {
        class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }

            public Person(int id, string name, int age)
            {
                Id = id;
                Name = name;
                Age = age;
            }
            public virtual void GetDetails()
            {
                Console.WriteLine($"Id: {Id}, Name: {Name}, Age: {Age}");

            }

            public virtual double Calculatesalary()
            {
                return 0;
            }
        }
        class Teacher : Person
        {
            public string Subject { get; set; }
            public double MonthlySalary { get; set; }

            public Teacher(int id, string name,int age,string subject,double monthlySalary) : base(id, name, age)
            {
                Subject = subject;
                MonthlySalary = monthlySalary;

            }
            public override void GetDetails()
            {
                Console.WriteLine($"Id: {Id}, Name: {Name}, Age: {Age}, Subject: {Subject}, Monthly Salary: {MonthlySalary}"); 
            } 
            public override double Calculatesalary() {
                return MonthlySalary;
            }
            
            
        }
        class Student : Person
        {
            public string Grade { get; set; }
            public double Feepaid { get; set; }
            public Student(int id,string name,int age,string grade,double fee) : base(id, name, age)
            {
                Grade = grade;
                Feepaid = fee;

            }
            public override void GetDetails()
            {
                Console.WriteLine($"Id: {Id}, Name: {Name}, Age: {Age}, Grade: {Grade}, Fee Paid: {Feepaid}");
            }
            public override double Calculatesalary()
            {

                return -Feepaid;
            }

        }

        public static void Main(string[] args)
        {
            List<Person> people = new List<Person>
        {
            new Teacher(1, "Mr. Sharma", 40, "Math", 50000),
            new Student(2, "Arjun", 20, "A", 20000),
            new Teacher(3, "Ms. Verma", 35, "Physics", 60000),
            new Student(4, "Riya", 19, "B+", 18000)
        };

            foreach (Person p in people)
            {
                p.GetDetails();
                Console.WriteLine("Financial Impact: " + p.Calculatesalary());
                Console.WriteLine("-------------------------");
            }

        }
    }
}
