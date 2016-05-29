using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edison.Castle.Clients.Data.Models
{
    public class Lock
    {
        public Guid LockId { get; set; }

        public string LockName { get; set; }

        public Guid LockUUID { get; set; }

        public int LockMajor { get; set; }

        public int LockMinor { get; set; }
    }
}
