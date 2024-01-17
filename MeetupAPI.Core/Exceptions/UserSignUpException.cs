using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Core.Exceptions
{
    public class UserSignUpException : Exception
    {
        public UserSignUpException() { }
        public UserSignUpException(string message) : base(message) { }
        public UserSignUpException(string message, Exception innerException) : base(message, innerException) { }
    }
}
