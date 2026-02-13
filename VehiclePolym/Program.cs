using System.Numerics;

namespace VehiclePolym
{
    internal class Program
    {
        class Vehicle
        {
            public virtual void Drive()
            {
                Console.WriteLine("The vehicle is driving.");

            }
        }
        class Car : Vehicle
        {
            public override void Drive()
            {
                Console.WriteLine("The car is driving.");
            }
        }
        class Truck : Vehicle
        {
            public override void Drive()
            {
               Console.WriteLine("The truck is driving.");
            }
        }
        static void Main(string[] args)
        {
            Vehicle v = new Vehicle();
            Vehicle c = new Car();
            Vehicle t = new Truck();
            v.Drive();
            c.Drive();
            t.Drive();

        }
    }
}
