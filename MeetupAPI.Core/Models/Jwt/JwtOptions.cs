﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Core.Models.Jwt
{
    public class JwtOptions
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public short AccessTokenExpirationTime { get; set; }
        public int RefreshTokenExpirationTime { get; set; }
    }
}
