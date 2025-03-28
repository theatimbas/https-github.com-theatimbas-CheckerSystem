using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibraryBusinessDataLogic
{
    public class E_LibraryProcedure
    {
        public static bool UserAction(string UserName, string AgeInput)
        {
            if (int.TryParse(AgeInput, out int UserAge))
            {
                if (UserAge > 17)
                {
                    return true;  
                }
                else
                {
                    return false; 
                }
            }
            return false;
        }
        public static bool BookGenre(string UserInput)
        {
            return true;
        }
    }
}