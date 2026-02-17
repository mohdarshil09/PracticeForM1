namespace Predicate_T__Filter_with_Generic_List
{
   public class Program
{
    public static void Main()
    {
        var nums = new List<int> { 2, 5, 8, 11, 14 };

        var evens = Filter(nums, n => n % 2 == 0);
        Console.WriteLine(string.Join(",", evens));   // Expected: 2,8,14

        var big = Filter(nums, n => n >= 10);
        Console.WriteLine(string.Join(",", big));     // Expected: 11,14
    }

    // Generic Filter Method
    public static List<T> Filter<T>(List<T> items, Predicate<T> match)
    {
        var result = new List<T>();

        foreach (var item in items)
        {
            if (match(item))   // If condition true
            {
                result.Add(item);
            }
        }

        return result;
    }
}
}
