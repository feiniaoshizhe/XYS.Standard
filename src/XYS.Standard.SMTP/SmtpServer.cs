using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYS.Standard.SMTP
{
    public abstract class SmtpServer
    {
        public abstract string Name { get; }
        public abstract string Port { get; }
        public abstract string UserName { get; }
        public abstract string AuthCode { get; }
    }
}
