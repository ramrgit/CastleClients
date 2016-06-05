using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Edison.Castle.Clients.Data.Models
{
    [DataContract]
    public class Lock
    {
        [DataMember]
        public Guid LockId { get; set; }
        [DataMember]
        public string LockName { get; set; }
        [DataMember]
        public Guid LockUUID { get; set; }
        [DataMember]
        public int LockMajor { get; set; }
        [DataMember]
        public int LockMinor { get; set; }
    }
}
