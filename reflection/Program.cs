using System.Reflection;
namespace reflection
{
    internal class Program
    {
        class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public void Greet()
            {
                Console.WriteLine("Hello from Employee");
            }
        }

        static void Main(string[] args)
        {
            Type type = typeof(Employee);

            Console.WriteLine("Class Name: " + type.Name);

            // Get Properties
            Console.WriteLine("\nProperties:");
            foreach (PropertyInfo prop in type.GetProperties())
            {
                Console.WriteLine(prop.Name);
            }

            // Get Methods
            Console.WriteLine("\nMethods:");
            foreach (MethodInfo method in type.GetMethods())
            {
                Console.WriteLine(method.Name);
            }

            // Create object dynamically
            object emp = Activator.CreateInstance(type);

            // Call method dynamically
            MethodInfo greetMethod = type.GetMethod("Greet");
            greetMethod.Invoke(emp, null);

        }
    }
}