using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Core.Models.Jwt
{
    public class Token
    {
        public string TokenValue { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ExpiresTime { get; set; }

    }
}
