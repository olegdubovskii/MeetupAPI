using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Core.Exceptions.Validation
{
    public class MeetupValidationException : ValidationException
    {
        public MeetupValidationException(string message) : base(message) { }   
    }
}
