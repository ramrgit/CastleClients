using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edison.Castle.Clients.Data
{
    public class LockService
    {
        public LockService()
        {

        }

        public bool Unlock(Guid lockId)
        {

            //make a rest call the api to unlock this specific lock

            //the api will need the authenticated header in the rest call

            return false;

        }
    }
}
