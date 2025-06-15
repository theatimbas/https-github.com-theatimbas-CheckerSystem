using System;
using System.Collections.Generic;

namespace PFinderCommon
{
    public class UserAccount
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<string> Favorites { get; set; } = new List<string>();
    }
}