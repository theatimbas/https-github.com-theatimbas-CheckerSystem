using System;
using System.Collections.Generic;
using System.Linq;

namespace ELibraryDataLogic
{
    public static class Library
    {
        private static readonly Dictionary<string, List<string>> genres = new()
        {
            { "Fantasy", new() { "Titan Academy", "Charm Academy", "Tantei High", "Olympus Academy" } },
            { "Drama", new() { "The Tempest", "A Wife's Cry", "Salamasim", "Taste of Sky" } },
            { "Fiction", new() { "The Great Gatsby", "To Kill a Mockingbird", "1984" } },
            { "Non-Fiction", new() { "Sapiens", "Educated", "The Wright Brothers" } },
            { "Science-Fiction", new() { "A Brief History of Time", "The Selfish Gene" } },
            { "Action", new() { "The Maze Runner", "The Hunger Games", "Divergent", "The Fifth Wave" } },
            { "History", new() { "Guns, Germs, and Steel", "The Silk Roads" } }
        };
        public static List<string> GetGenres()
        {
            return genres.Keys.ToList();
        }
        public static List<string> GetBooksByGenre(string genre)
        {
            foreach (var pair in genres)
            {
                if (string.Equals(pair.Key, genre, StringComparison.OrdinalIgnoreCase))
                    return pair.Value;
            }

            return new List<string>();
        }
        public static bool BookExists(string book)
        {
            foreach (var genre in genres.Values)
            {
                if (genre.Any(title => title.Equals(book, StringComparison.OrdinalIgnoreCase)))
                    return true;
            }

            return false;
        }
        public static List<string> SearchBooks(string keyword)
        {
            var result = new List<string>();
            if (string.IsNullOrWhiteSpace(keyword)) return result;

            foreach (var genre in genres.Values)
            {
                foreach (var book in genre)
                {
                    if (book.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 && !result.Contains(book))
                        result.Add(book);
                }
            }

            return result;
        }
    }
}
