using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFinderCommon
{
    public class UserAccount
    {
        private string _password;

        public string UserName { get; set; }

        public string Password
        {
            get => _password;
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length >= 6)
                {
                    _password = value;
                }
            }
        }

        public int Age { get; set; }

        public List<string> Favorites { get; set; } = new List<string>();
    }
}

