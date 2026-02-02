using System;
using System.Collections.Generic;

namespace Collection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, double> orderMap = new Dictionary<string, double>();
            Console.WriteLine("Enter number of cake orders to be added");
            int n=int.Parse(Console.ReadLine());
            for(int i = 0; i < n; i++)
            {
                string input= Console.ReadLine();
                string[] parts = input.Split(':');
                string orderId=parts[0];
                double price= double.Parse(parts[1]);
                orderMap.Add(orderId, price);
            }
            Console.WriteLine("Enter the cost to search the cake orders");
            double searchCost = double.Parse(Console.ReadLine());

            bool found = false;

            foreach (var item in orderMap)
            {
                if (item.Value > searchCost)
                {
                    Console.WriteLine("Order ID: " + item.Key + ", Cake Cost: " + item.Value);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No cake orders found");
            }

        }
    }
}
