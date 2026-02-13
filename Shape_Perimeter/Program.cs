namespace Shape_Perimeter
{
    internal class Program
    {
        class Shape
        {
            public virtual double GetPerimeter()
            {
                return 0;
            }
        }
        class Rectangle : Shape
        {
            private double Length, Width;
            public Rectangle(double length, double width)
            {
                Length = length;
                Width = width;
            }
            public override double GetPerimeter()
            {
                return 2 * (Length + Width);
            }
        }
        class Triangle : Shape
        {
            private double SideA, SideB, SideC;
            public Triangle(double sideA,double sideB,double sideC)
            {
                SideA = sideA;
                SideB = sideB;
                SideC = sideC;
            }
            public override double GetPerimeter()
            {
                return SideA + SideB + SideC;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Input for Triangle:" );
            string str=Console.ReadLine().Trim();
            string[] part = str.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            double sideA = double.Parse(part[0]);   
            double sideB = double.Parse(part[1]);
            double sideC = double.Parse(part[2]);
            Triangle triangle = new Triangle(sideA, sideB, sideC);
            Console.WriteLine($"perimeter of Triangle: {triangle.GetPerimeter()}");



        }
    }
}
