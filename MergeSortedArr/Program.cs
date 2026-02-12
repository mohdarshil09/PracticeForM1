namespace MergeSortedArr
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr1 = { 1, 3, 4, 6 };
            int[] arr2 = { 2, 4, 5, 6, 7 };
            int m = arr1.Length;
            int n = arr2.Length;
            int[] mergeArr = new int[m + n];
            int i = 0; int j = 0; int k = 0;
            while (i < m && j < n)
            {
                if (arr1[i] < arr2[j])
                {
                    mergeArr[k++] = arr1[i++];
                }
                else
                {
                    mergeArr[k++] = arr2[j++];
                }
            }
            while (i < m)
            {
                mergeArr[k++] = arr1[i++];
            }
            while (j < n)
            {
                mergeArr[k++] = arr2[j++];
            }
            Console.WriteLine(string.Join(", ", mergeArr));
        
    }
    }
}
