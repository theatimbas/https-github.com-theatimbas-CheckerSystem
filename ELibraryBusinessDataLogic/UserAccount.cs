using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibraryBusinessDataLogic
{
    public class UserAccount
    {
        private string _password = "default1";

        public string UserName { get; set; }

        public string Password
        {
            get => _password;
            set
            {
                // Optional: Add validation
                if (!string.IsNullOrWhiteSpace(value) && (value.Length == 6 || value.Length == 8))
                {
                    _password = value;
                }
                else
                {
                    throw new ArgumentException("Password must be 6 or 8 characters long.");
                }
            }
        }

        public int Age { get; set; }
    }
}


