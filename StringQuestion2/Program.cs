


//First character must be an uppercase letter

//Last character must be a digit




//At least 1 special character from: @ # $ %

//Must NOT contain any spaces

//Must NOT contain two consecutive identical characters

//Return 1 if valid, else 0.


using System.Security.Cryptography.X509Certificates;

///<summary>
///question
///Given n strings, check whether each string is a Valid Secure Identifier.
///A string is valid if:
///Length is between 8 and 15
///Must contain:
///At least 1 lowercase letter
///At least 1 digit
///</summary>
using System;
using System.Collections.Generic;

namespace StringQuestion2
{
    internal class Program
    {
        static List<int> checkedList(List<string> list)
        {
            List<int> res = new List<int>();

            foreach (string str in list)
            {
                if (isvalid(str))
                    res.Add(1);
                else
                    res.Add(0);
            }

            return res;  // ✅ outside loop
        }

        static bool isvalid(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            if (str.Length < 8 || str.Length > 15)
                return false;

            if (!char.IsUpper(str[0]))
                return false;

            if (!char.IsDigit(str[str.Length - 1]))
                return false;

            bool hasLower = false;
            bool hasDigit = false;
            bool hasSpecial = false;

            string specialChars = "@#$%";

            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsWhiteSpace(str[i]))
                    return false;

                if (i > 0 && str[i] == str[i - 1])
                    return false;

                if (char.IsLower(str[i]))
                    hasLower = true;

                if (char.IsDigit(str[i]))
                    hasDigit = true;

                if (specialChars.Contains(str[i]))
                    hasSpecial = true;
            }

            return hasLower && hasDigit && hasSpecial;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter the TestCase");
            int n = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the Strings");
            List<string> list = new List<string>();

            for (int i = 0; i < n; i++)
            {
                list.Add(Console.ReadLine());
            }

            List<int> ans = checkedList(list);

            foreach (int val in ans)
            {
                Console.WriteLine(val);
            }
        }
    }
}
