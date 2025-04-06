using System;
using System.Collections.Generic;


namespace ELibraryBusinessDataLogic
{
    public class E_LibraryProcedure
    {
        private static List<string> AddedFavorites = new List<string>();

        public static bool UserAction(string AgeInput, string UserName, string UserPassword)
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(UserPassword))
            {
                return false;
            }
            return int.TryParse(AgeInput, out int UserAge) && UserAge > 17;
        }

        public static bool BookGenre(string UserInput)
        {
            return true;
        }

        public static List<string> MyFavorites()
        {
            return AddedFavorites;
        }

        public static bool AddToFavorites(string BookName)
        {
            if (string.IsNullOrWhiteSpace(BookName))
            {
                return false;
            }
            AddedFavorites.Add(BookName);
            return true;
        }

        public static bool RemoveFromFavorites(string BookName)
        {
            if (AddedFavorites.Contains(BookName))
            {
                AddedFavorites.Remove(BookName);
                return true;
            }
            return false;
        }
    }
}
