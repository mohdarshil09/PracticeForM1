namespace PasswordGeneration
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the Username: ");
            string username = Console.ReadLine();
            if (username.Length != 8)
            {
                Console.WriteLine(username + " is a  invalid username.");
                return;
                
            }
            for(int i = 0; i < 4; i++)
            {
                if (!char.IsUpper(username[i]))
                {
                    Console.WriteLine(username + " is a  invalid username.");
                    return;

                }
            }
            if (username[4] != '@')
            {
                Console.WriteLine(username + " is a invalid username.");
            }
            string lastPart = username.Substring(5);
            int courseId;

            if (!int.TryParse(lastPart, out courseId) || courseId < 101 || courseId > 115)
            {
                Console.WriteLine(username + " is an invalid username");
                return;
            }

            int assisum = 0;
            for(int i = 0; i < 4; i++)
            {
                assisum += (int)char.ToLower(username[i]);
            }
            string lastTwo = lastPart.Substring(1);

            Console.WriteLine("Password: TECH_" + assisum + lastTwo);
        }
    }
}

