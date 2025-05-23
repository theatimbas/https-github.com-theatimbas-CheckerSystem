using System;

namespace ELibraryDataLogic
{
    public class UserAccount
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public List<string> Favorites { get; set; } = new List<string>();
    }
}
