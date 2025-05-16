using System;
using System.Collections;
using System.Collections.Generic;

namespace IteratorPatternExample
{
    // 📘 Element
    public class Book
    {
        public string Title { get; }
        public string Author { get; }

        public Book(string title, string author)
        {
            Title = title;
            Author = author;
        }

        public void Display()
        {
            Console.WriteLine($"📘 \"{Title}\" by {Author}");
        }
    }

    // 📚 Aggregate
    public class BookCollection : IEnumerable<Book>
    {
        private readonly List<Book> _books = new();

        public void AddBook(Book book) => _books.Add(book);
        public void RemoveBook(Book book) => _books.Remove(book);

        public IEnumerator<Book> GetEnumerator() => _books.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator(); // non-generic fallback
    }

    // 🧪 Client
    class Program
    {
        static void Main()
        {
            var library = new BookCollection();
            library.AddBook(new Book("Clean Code", "Robert C. Martin"));
            library.AddBook(new Book("The Pragmatic Programmer", "Andrew Hunt & David Thomas"));
            library.AddBook(new Book("Design Patterns", "GoF"));

            Console.WriteLine("📚 Library Inventory:");
            foreach (var book in library)
            {
                book.Display();
            }
        }
    }
}