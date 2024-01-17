using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Core.Exceptions
{
    public class SignInWasFailedException : Exception
    {
        public SignInWasFailedException() { }
        public SignInWasFailedException(string message) : base(message) { }
        public SignInWasFailedException(string message,  Exception innerException) : base(message, innerException) { } 
    }
}
