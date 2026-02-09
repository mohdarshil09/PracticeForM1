using System.ComponentModel.Design;

namespace CabFare
{
    internal class Program
    {

        class Cab
        {
            public virtual int cabfare(int fare)
            {
                return 0;
            }
        }
        class Mini : Cab
        {
            public override int cabfare(int fare)
            {
                return fare*12;
            }
        }
        class Sedan : Cab
        {
            public override int cabfare(int fare)
            {
                return fare * 15 + 50;
            }
        }
        class SUV : Cab
        {
            public override int cabfare(int fare)
            {
                return fare * 18 + 100;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            Console.WriteLine("Enter the car name: ");
            string car = Console.ReadLine();
            Console.WriteLine("Enter the Km: ");
            int km = int.Parse(Console.ReadLine());
            Cab cab;
            if(car.Equals("mini", StringComparison.OrdinalIgnoreCase))
            {
                cab = new Mini();

            }
            else if (car.Equals("sedan",StringComparison.OrdinalIgnoreCase)){
                cab = new Sedan();
            }


            else {
                cab = new SUV();
            }
            int lastfare= cab.cabfare(km);
            Console.WriteLine($"{car} is travel {km} with the cost of {lastfare}");
            




        }
    }
}
