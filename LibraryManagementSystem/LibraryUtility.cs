using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem
{
    public class LibraryUtility
    {
        private readonly List<Book> _books = new();
        private int _nextId = 1;

        /// <summary>
        /// Adds a book with an auto-incremented ID.
        /// </summary>
        public void AddBook(string title, string author, string genre, int year)
        {
            var book = new Book(_nextId++, title, author, genre, year);
            _books.Add(book);
        }

        /// <summary>
        /// Groups books by genre alphabetically (SortedDictionary keys are sorted).
        /// </summary>
        public SortedDictionary<string, List<Book>> GroupBooksByGenre()
        {
            var grouped = new SortedDictionary<string, List<Book>>();
            foreach (var book in _books)
            {
                if (!grouped.ContainsKey(book.Genre))
                    grouped[book.Genre] = new List<Book>();
                grouped[book.Genre].Add(book);
            }
            return grouped;
        }

        /// <summary>
        /// Returns all books by the specified author (case-insensitive).
        /// </summary>
        public List<Book> GetBooksByAuthor(string author)
        {
            return _books
                .Where(b => string.Equals(b.Author, author, System.StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        /// <summary>
        /// Returns the total number of books in the library.
        /// </summary>
        public int GetTotalBooksCount()
        {
            return _books.Count;
        }
    }
}
