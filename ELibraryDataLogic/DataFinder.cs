using ELibraryBusinessDataLogic;
using System.Collections.Generic;

namespace ELibraryDataLogic
{
    public static class DataFinder
    {
        private static List<UserAccount> accounts = new List<UserAccount>();

        // Static constructor to initialize default accounts
        static DataFinder()
        {
            CreateDefaultAccounts();
        }

        private static void CreateDefaultAccounts()
        {
            accounts.Add(new UserAccount
            {
                UserName = "lachimolala",
                Password = "nabiee",
                Age = 18
            });

            accounts.Add(new UserAccount
            {
                UserName = "@spring_sensation",
                Password = "password",
                Age = 21
            });
        }

        public static bool ValidateAccount(string userName, string password, int age)
        {
            foreach (var account in accounts)
            {
                if (account.UserName == userName && account.Password == password && account.Age == age)
                {
                    return true;
                }
            }
            return false;
        }

        public static List<UserAccount> GetAccounts()
        {
            return accounts;
        }

        public static bool IsUserAlreadyRegistered(string userName)
        {
            return accounts.Exists(a => a.UserName == userName);
        }

        public static bool RegisterAccount(string userName, string password, int age)
        {
            if (IsUserAlreadyRegistered(userName))
                return false;

            UserAccount newUser = new UserAccount
            {
                UserName = userName,
                Password = password,
                Age = age
            };

            accounts.Add(newUser);
            return true;
        }
    }
}
