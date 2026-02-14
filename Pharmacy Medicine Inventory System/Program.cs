namespace Pharmacy_Medicine_Inventory_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var utility = new MedicineUtility();

            while (true)
            {
                Console.WriteLine("1. Display all medicines");
                Console.WriteLine("2. Update medicine price");
                Console.WriteLine("3. Add medicine");
                Console.WriteLine("4. Exit");
                Console.Write("Enter choice: ");

          
                string? choice=Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        DisplayAllMedicines(utility);
                        break;
                    case "2":
                        UpdateMedicinePrice(utility);
                        break;
                    case "3":
                        AddMedicine(utility);
                        break;
                    case "4":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
                Console.WriteLine();
            }
        }

        static void DisplayAllMedicines(MedicineUtility utility)
        {
            var medicines = utility.GetAllMedicines();
            if (medicines.Count == 0)
            {
                Console.WriteLine("No medicines in inventory.");
                return;
            }
            foreach (var m in medicines)
                Console.WriteLine(m.ToString());
        }

        static void UpdateMedicinePrice(MedicineUtility utility)
        {
            Console.Write("Enter Medicine ID: ");
            string? id = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }
            Console.Write("Enter new price: ");
            if (!int.TryParse(Console.ReadLine(), out int newPrice))
            {
                Console.WriteLine("Invalid price.");
                return;
            }
            try
            {
                utility.UpdateMedicinePrice(id, newPrice);
                Console.WriteLine("Price updated successfully.");
            }
            catch (InvalidPriceException ex) { Console.WriteLine(ex.Message); }
            catch (MedicineNotFoundException ex) { Console.WriteLine(ex.Message); }
        }

        static void AddMedicine(MedicineUtility utility)
        {
            Console.WriteLine("Enter: MedicineID Name Price ExpiryYear");
            string? line = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(line))
            {
                Console.WriteLine("Invalid input.");
                return;
            }
            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 4)
            {
                Console.WriteLine("Invalid format. Use: MedicineID Name Price ExpiryYear");
                return;
            }
            string medicineId = parts[0];
            if (!int.TryParse(parts[^2], out int price))
            {
                Console.WriteLine("Invalid price.");
                return;
            }
            if (!int.TryParse(parts[^1], out int expiryYear))
            {
                Console.WriteLine("Invalid expiry year.");
                return;
            }
            string name = string.Join(" ", parts[1..^2]);
            var medicine = new Medicine { Id = medicineId, Name = name, Price = price, ExpiryYear = expiryYear };
            try
            {
                utility.AddMedicine(medicine);
                Console.WriteLine("Medicine added successfully.");
            }
            catch (InvalidPriceException ex) { Console.WriteLine(ex.Message); }
            catch (DuplicateMedicineException ex) { Console.WriteLine(ex.Message); }
            catch (InvalidExpiryYearException ex) { Console.WriteLine(ex.Message); }
        }
    }
}
