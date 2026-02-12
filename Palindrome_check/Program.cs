namespace Palindrome_check
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a string to check if it's a palindrome:");
            string str = Console.ReadLine();
            int n = str.Length;
            int i= 0;int j = n - 1;
            bool isPalindrome = true;
            while (i < j)
            {
                if (str[i] != str[j])
                {
                    isPalindrome = false;
                    break;
                }
                else
                {
                    i++; j--;
                }
            }
            if (isPalindrome)
            {
                Console.WriteLine($"'{str}' is a palindrome.");
            }
            else
            {
                Console.WriteLine($"'{str}' is not a palindrome.");
            }
        }
    }
}
