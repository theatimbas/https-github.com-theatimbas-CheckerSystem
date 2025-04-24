using System;


namespace ELibraryDataLogic
{
    public class UserAccount
    {
        private string _userName;
        private string _password = "default1";
        private int _age;

        public string UserName 
        { get => _userName; 
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _userName = value;
                }
                else
                {
                    throw new ArgumentException("Username cannot be empty or whitespace.");
                }
            }
        }

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

        public int Age 
        { get => _age;
            set
            {
                if (value >= 18 && value <= 120)
                {
                    _age = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Age must be between 18 and 120.");
                }
            }
        }
    }
}


