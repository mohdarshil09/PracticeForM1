namespace PredicateTFilter_a_List
{
    public class Program
    {
        public static void Main()
        {
            var numbers = new List<int> { 5, 10, 15, 20, 25 };                // sample data
            var filtered = FilterUtil.Filter(numbers, n => n >= 15);          // keep >= 15

            foreach (var n in filtered)                                       // print result
            {
                Console.WriteLine(n);                                         // output each
            }
        }
    }

    public static class FilterUtil
    {
        // ✅ TODO: Student must implement only this method
       
        public static List<T> Filter<T>(IEnumerable<T> items, Predicate<T> predicate)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var result = new List<T>();

            foreach (var item in items)
            {
                if (predicate(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

    }
}
