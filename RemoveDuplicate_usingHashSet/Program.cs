namespace RemoveDuplicate_usingHashSet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the number of elements in the array:"); int n = int.Parse(Console.ReadLine());
            int[] arr = new int[n];
            Console.WriteLine("Enter the elements of the array:");
            for (int i = 0; i < n; i++) {
                arr[i] = int.Parse(Console.ReadLine());
            }
            HashSet<int> uniqueElements = new HashSet<int>(arr); 
            Console.WriteLine("Array after removing duplicates:");
            foreach (int element in uniqueElements) {
                
                Console.Write(element + " ");
            }
        }
    }
}
