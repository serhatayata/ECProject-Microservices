using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.Messages
{
    public class ErrorMessages
    {
        #region AlreadyExists
        public static string AlreadyExists(string entity)
        {
            string message = $"{entity} already exists";
            return message;
        }
        #endregion

    }
}
