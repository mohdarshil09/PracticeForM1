namespace Generic_Constraint_Numeric_like_Aggregation__struct_
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine(Sum(new List<int> { 1, 2, 3 }));      // Expected: 6
            Console.WriteLine(Sum(new List<double> { 1.5, 2.5 }));  // Expected: 4.0

            // Console.WriteLine(Sum(new List<string> { "a", "b" })); // ❌ Should not compile (string is not struct)
        }

        // ✅ TODO: Students implement only this function
        public static T Sum<T>(IEnumerable<T> items) where T : struct
        {
            // TODO: Sum all items and return the sum
            dynamic total = default(T);

            foreach (var item in items)
            {
                total += (dynamic)item;
            }

            return (T)total;
        }
    }
}
