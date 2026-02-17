namespace Generic_Swap_Utility__Type_safe_
{
    internal class Program
    {
        public static void Main()   // Entry point
        {
            int a = 10;            
            int b = 20;            

            Swap<int>(ref a, ref b);   // Calling generic swap explicitly
            Console.WriteLine($"a={a}, b={b}");   // Expected: a=20, b=10

            string x = "Gopi";       // Example string
            string y = "Suresh";     // Example string

            Swap(ref x, ref y);      // Generic type inference
            Console.WriteLine($"x={x}, y={y}");   // Expected: x=Suresh, y=Gopi
        }

        // Generic Swap Method
        public static void Swap<T>(ref T left, ref T right)
        {
            T temp = left;   // Store left value
            left = right;    // Assign right to left
            right = temp;    // Assign temp to right
        }
    }
}
