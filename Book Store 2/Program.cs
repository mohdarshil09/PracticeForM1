namespace Book_Store_2
{
    internal class Program
    {

        public class Book
        {
            public int ID { get; set; }
            public string Title { get; set; }

            public string Author { get; set; }
            public int Price { get; set; }
            public int Stock { get; set; }

        }
        public class BookUtility
        {
            public Book book;

            public  BookUtility(Book book)
            {
                this.book = book;
            }

            public void GetDetails()
            {
                Console.WriteLine($"Id:{book.ID} ,Title:{book.Title}, Price:{book.Price},Stocks:{book.Stock} ");
            }
            public void UpdatePrice(int newPrice)
            {
                book.Price = newPrice;
                Console.WriteLine($"Price Updated : {newPrice}");
            }
            public void UpdateStock(int newStock)
            {
                book.Stock = newStock;
                Console.WriteLine($"Stock Updated : {newStock}");
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the detail :<id><title><price><stock>");
            string input=Console.ReadLine();
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Book book= new Book()
            {
                ID = int.Parse(parts[0]),
                Title = parts[1],
                Price =int.Parse( parts[2]),
                Stock =int.Parse(parts[3])
            };
            BookUtility b=new BookUtility(book);
            Console.WriteLine("Enter a choice:");
            Console.WriteLine("1: Get Details");
            Console.WriteLine("2: Update Price");
            Console.WriteLine("3: Update Stocks");
            Console.WriteLine("4: Exit ,Thank you");

            while (true)
            {
                int choice=int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        b.GetDetails();
                        break;
                    case 2:
                        int newPrice = int.Parse(Console.ReadLine());
                        b.UpdatePrice(newPrice);
                        break;
                    case 3:
                        int newStock = int.Parse(Console.ReadLine());
                        b.UpdateStock(newStock); break;
                    case 4:
                        Console.WriteLine("Thank You");
                        return;
                }
            }


        }
    }
}
