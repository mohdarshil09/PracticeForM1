using System;

namespace InterfaceExample
{
    internal class Program
    {
        interface IExample
        {
            int DoSomething(int a)
            {
                return a * 2;
            }
        }

        interface IAnotherExample
        {
            void DoSomethingElse();
        }

        
        class ExampleImplementation:IExample, IAnotherExample
        {
            public void DosomethingElse()
            {
                Console.WriteLine("Doing something else!");
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Interface Example");

            ExampleImplementation ex = new ExampleImplementation();
            ex.DoSomethingElse();

            // CALL DEFAULT INTERFACE METHOD USING INTERFACE REFERENCE
            IExample ie = ex;
            Console.WriteLine(ie.DoSomething(10));
        }
    }
}
