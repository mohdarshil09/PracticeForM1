using System;

namespace Book_Store_Application
{
    internal class Program
    {
        // 🔹 Book Class
        class Book
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public int Price { get; set; }
            public int Stock { get; set; }
        }

        // 🔹 BookUtility Class
        class BookUtility
        {
            public Book book;

            // Constructor injection (Encapsulation)
            public BookUtility(Book book)
            {
                this.book = book;
            }

            public void GetBookDetails()
            {
                Console.WriteLine($"Details: {book.Id} {book.Title} {book.Price} {book.Stock}");
            }

            public void UpdateBookPrice(int newPrice)
            {
                book.Price = newPrice;
                Console.WriteLine($"Updated Price: {newPrice}");
            }

            public void UpdateBookStock(int newStock)
            {
                book.Stock = newStock;
                Console.WriteLine($"Updated Stock: {newStock}");
            }
        }

        static void Main(string[] args)
        {
            // 📥 Read initial book details
            string input = Console.ReadLine();
            string[] parts = input.Split(' ');

            Book book = new Book
            {
                Id = parts[0],
                Title = parts[1],
                Price = int.Parse(parts[2]),
                Stock = int.Parse(parts[3])
            };

            BookUtility utility = new BookUtility(book);

            while (true)
            {
                
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        utility.GetBookDetails();
                        break;

                    case 2:
                        int newPrice = int.Parse(Console.ReadLine());
                        utility.UpdateBookPrice(newPrice);
                        break;

                    case 3:
                        int newStock = int.Parse(Console.ReadLine());
                        utility.UpdateBookStock(newStock);
                        break;

                    case 4:
                        Console.WriteLine("Thank You");
                        return;
                }
            }
        }
    }
}

