using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Core.Exceptions.Validation
{
    public class OrganizerValidationException : ValidationException
    {
        public OrganizerValidationException(string message) : base(message) { }
    }
}
