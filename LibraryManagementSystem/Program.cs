using System.Collections.Generic;

namespace LibraryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var library = new LibraryUtility();

            // 1. Add Fiction, Non-Fiction, Mystery books
            library.AddBook("1984", "George Orwell", "Fiction", 1949);
            library.AddBook("To Kill a Mockingbird", "Harper Lee", "Fiction", 1960);
            library.AddBook("Sapiens", "Yuval Noah Harari", "Non-Fiction", 2011);
            library.AddBook("The Selfish Gene", "Richard Dawkins", "Non-Fiction", 1976);
            library.AddBook("The Girl with the Dragon Tattoo", "Stieg Larsson", "Mystery", 2005);
            library.AddBook("Gone Girl", "Gillian Flynn", "Mystery", 2012);

            // 2. Display books grouped by genre
            Console.WriteLine("=== Books Grouped by Genre (alphabetically) ===\n");
            SortedDictionary<string, List<Book>> byGenre = library.GroupBooksByGenre();
            foreach (var genreGroup in byGenre)
            {
                Console.WriteLine($"Genre: {genreGroup.Key}");
                foreach (var book in genreGroup.Value)
                    Console.WriteLine($"  {book}");
                Console.WriteLine();
            }

            // 3. Search books by specific author
            string searchAuthor = "George Orwell";
            Console.WriteLine($"=== Books by {searchAuthor} ===\n");
            List<Book> byAuthor = library.GetBooksByAuthor(searchAuthor);
            foreach (var book in byAuthor)
                Console.WriteLine(book);
            if (byAuthor.Count == 0)
                Console.WriteLine("No books found.");
            Console.WriteLine();

            // 4. Show statistics (total books, books per genre)
            Console.WriteLine("=== Library Statistics ===\n");
            Console.WriteLine($"Total books: {library.GetTotalBooksCount()}");
            Console.WriteLine("Books per genre:");
            foreach (var genreGroup in library.GroupBooksByGenre())
                Console.WriteLine($"  {genreGroup.Key}: {genreGroup.Value.Count}");
        }
    }
}
