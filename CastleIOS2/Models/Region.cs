using CoreLocation;
using Edison.Castle.Clients.Data.Models;
using Foundation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CastleIOS2.Models
{
    public static class Region
    {
        
        static Region()
        {

        }

        public static CLBeaconRegion InitializeRegion(Lock lockInfo)
        {
            CLBeaconRegion beaconRegion;

            beaconRegion = new CLBeaconRegion(new NSUuid(lockInfo.LockUUID.ToString()), "Edison Properties Castle Region");
            beaconRegion.NotifyEntryStateOnDisplay = true;
            beaconRegion.NotifyOnEntry = true;
            beaconRegion.NotifyOnExit = true;

            return beaconRegion;
           
        }

    }
}
