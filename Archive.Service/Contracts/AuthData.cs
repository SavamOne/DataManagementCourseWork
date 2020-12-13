using System;
using System.Collections.Generic;
using System.Text;

namespace Archive.Service.Contracts
{
    public class AuthData
    {
        public string Host { get; set; }

        public ushort Port { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
