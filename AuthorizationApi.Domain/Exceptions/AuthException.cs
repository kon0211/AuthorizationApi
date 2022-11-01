using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationApi.Domain.Exceptions
{
    /// <summary>
    /// Errors occurring during authentication and authorization
    /// </summary>
    public class AuthException : Exception
    {
        public AuthException(string message) : base(message)
        {

        }

        public AuthException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
