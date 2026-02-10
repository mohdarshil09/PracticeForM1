using System.Runtime.CompilerServices;

namespace EcommerceDiscount
{
    internal class Program
    {
        abstract class DiscountPolicy
        {
            public abstract double CalculateDiscount(double price);
        }

        class FestivalDiscount : DiscountPolicy
        {
            public override double CalculateDiscount(double price)
            {
                if (price >= 5000)
                {
                    price -= price * 0.10;
                }
                else
                {
                    price -= price * 0.05;
                }
                return price;
            }
        }
        class MemberDiscount : DiscountPolicy
        {
            public override double CalculateDiscount(double price)
            {
                if (price >= 2000)
                {
                    price -=300;
                }
                
                
                return price;
            }
        }




        static void Main(string[] args)
        {
            Console.WriteLine("Enter the amount: ");
            int amount=int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the price of the product: ");
            string policy=Console.ReadLine();
            DiscountPolicy discountPolicy;
            if(policy.Equals("Festival", StringComparison.OrdinalIgnoreCase))
            {
                discountPolicy = new FestivalDiscount();
            }
            else if (policy.Equals("Member", StringComparison.OrdinalIgnoreCase))
            {
                discountPolicy= new MemberDiscount();
            }
            else {
                Console.WriteLine("Invalid");
                return;

            }
            double finalAmount=discountPolicy.CalculateDiscount(amount);
            Console.WriteLine("Fianl Amount: " + finalAmount);

        }
    }
}
