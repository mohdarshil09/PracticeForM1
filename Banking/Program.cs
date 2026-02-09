namespace Banking
{
    internal class Program
    {
        class BankAcoount
        {
            private double balance;

            public BankAcoount(double initialBalance)
            {
                balance = initialBalance < 0 ? 0 : initialBalance;
            }
            public void Deposit(double amount)
            {
                if (amount > 0)
                {
                    balance += amount;
                    Console.WriteLine($"Deposited: {amount}, New Balance: {balance}");
                }
                else
                {
                    Console.WriteLine("Deposit amount must be positive.");
                }
            }
            public void withdraw(double amount)
            {
                if (amount > 0)
                {
                    if (amount <= balance)
                    {
                        balance -= amount;
                        Console.WriteLine($"Withdrew: {amount}, New Balance: {balance}");
                    }
                    else
                    {
                        Console.WriteLine("Insufficient funds.");
                    }
                }
                else
                {
                    Console.WriteLine("Withdrawal amount must be positive.");
                }
            }
            public void DisplayBalance()
            {
                Console.WriteLine($"Current Balance: {balance}");
            }

        }
        static void Main(string[] args)
        {
            BankAcoount account = new BankAcoount(1000);
            for(int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"Transaction {i}: Enter D/W nad amount");
                string type = Console.ReadLine();
                double amount = double.Parse(Console.ReadLine());
                if (type.ToUpper() == "D")
                {
                    account.Deposit(amount);
                }
                else if (type.ToUpper() == "W")
                {
                    account.withdraw(amount);
                }
                else
                {
                    Console.WriteLine("Invalid transaction type. Use 'D' for deposit and 'W' for withdrawal.");
                }

            }
        }
    }
}
