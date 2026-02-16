using System;
using System.Collections.Generic;
using System.Linq;

namespace Challenge_2Library_Book_Management_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Catalog<Book> library = new Catalog<Book>();

            Book book1 = new Book
            {
                ISBN = "978-3-16-148410-0",
                Title = "C# Programming",
                Author = "John Sharp",
                Genre = "Programming",
                IsAvailable = true
            };

            library.AddItem(book1);

            var programmingBooks = library["Programming"];
            Console.WriteLine(programmingBooks.Count); // 1

            var johnsBooks = library.FindBooks(b => b.Author.Contains("John"));
            Console.WriteLine(johnsBooks.Count()); // 1
        }
    }

    // Book Class
    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public bool IsAvailable { get; set; }
    }

    // 🔹 Generic Catalog
    public class Catalog<T> where T : Book
    {
        private List<T> _items = new List<T>();
        private HashSet<string> _isbnSet = new HashSet<string>();
        private SortedDictionary<string, List<T>> _genreIndex =
        new SortedDictionary<string, List<T>>();

        // Add Item
        public bool AddItem(T item)
        {
            // Check ISBN uniqueness
            if (_isbnSet.Contains(item.ISBN))
                return false;

            _items.Add(item);
            _isbnSet.Add(item.ISBN);

            // Add to genre index
            if (!_genreIndex.ContainsKey(item.Genre))
            {
                _genreIndex[item.Genre] = new List<T>();
            }

            _genreIndex[item.Genre].Add(item);

            return true;
        }

        // Indexer by Genre
        public List<T> this[string genre]
        {
            get
            {
                if (_genreIndex.ContainsKey(genre))
                    return _genreIndex[genre];

                return new List<T>();
            }
        }

        // LINQ Search
        public IEnumerable<T> FindBooks(Func<T, bool> predicate)
        {
            return _items.Where(predicate);
        }
    }
}
