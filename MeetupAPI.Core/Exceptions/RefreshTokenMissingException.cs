using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Core.Exceptions
{
    public class RefreshTokenMissingException : Exception
    {
        public RefreshTokenMissingException() { }
        public RefreshTokenMissingException(string message) : base(message) { }
        public RefreshTokenMissingException(string message, Exception innerException) : base(message, innerException) { }   
    }
}
