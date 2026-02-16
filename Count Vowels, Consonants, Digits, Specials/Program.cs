namespace Count_Vowels__Consonants__Digits__Specials
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the String:");
            string s = Console.ReadLine();
            Console.WriteLine("Result: ");
            Console.WriteLine(Solve(s));
        }
        static string Solve(string s)
        {
            int vowels = 0, consonants = 0, digits = 0, specials = 0;
            foreach (char it in s)
            {
                if (char.IsLetter(it))
                {
                    char lower = char.ToLower(it);
                   if (lower == 'a' || lower == 'e' || lower == 'i' || lower == 'o' || lower == 'u')
                    {
                        vowels++;
                    }
                    else
                    {
                        consonants++;
                    }
                }
                else if (char.IsDigit(it))
                {
                    digits++;
                }
                else
                {
                    specials++;
                }
            }
            return $"Vowels: {vowels}\nConsonants: {consonants}\nDigits: {digits}\nSpecial Characters: {specials}";
        }
    }
}
