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

        public static bool ValidateAccount(string UserName, string Password, int Age)
        {
            foreach (var account in accounts)
            {
                if (account.UserName == UserName && account.Password == Password && account.Age == Age)
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

        public static bool IsUserAlreadyRegistered(string UserName)
        {
            return accounts.Exists(a => a.UserName == UserName);
        }

        public static bool RegisterAccount(string UserName, string Password, int Age)
        {
            if (IsUserAlreadyRegistered(UserName))
                return false;

            UserAccount NewUser = new UserAccount
            {
                UserName = UserName,
                Password = Password,
                Age = Age
            };

            accounts.Add(NewUser);
            return true;
        }
    }
}