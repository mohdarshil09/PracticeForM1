namespace LibraryManagementSystem
{
    public class Book
    {

        //auto-implemented properties
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int PublicationYear { get; set; }

        /// <summary>
        /// Initializes a new instance of the Book class with the specified identifier, title, author, genre, and
        /// publication year.
        /// </summary>
        /// <param name="id">The unique identifier for the book.</param>
        /// <param name="title">The title of the book. Cannot be null or empty.</param>
        /// <param name="author">The name of the author of the book. Cannot be null or empty.</param>
        /// <param name="genre">The genre of the book. Cannot be null or empty.</param>
        /// <param name="publicationYear">The year the book was published.</param>
        /// 
        public Book(int id, string title, string author, string genre, int publicationYear)
        {
            Id = id;
            Title = title;
            Author = author;
            Genre = genre;
            PublicationYear = publicationYear;
            

        }


        public override string ToString()
        {
            return $"[{Id}] \"{Title}\" by {Author} ({Genre}, {PublicationYear})";
        }
    }
}
