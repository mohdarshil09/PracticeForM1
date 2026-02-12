namespace ReverseStringWithinbuildFunction
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a string to reverse:");
            string input = Console.ReadLine();
            // Using built-in function to reverse the string string reversed = new string(input.Reverse().ToArray()); Console.WriteLine($"Reversed string: {reversed}");
            int n = input.Length;
            char[] reverse = new char[n];
            for(int i = 0; i < n; i++)
            {
                reverse[i] = input[n - 1 - i];
            } 
            string reversed = new string(reverse); 
            Console.WriteLine($"Reversed string: {reversed}");
        }
    }
}
