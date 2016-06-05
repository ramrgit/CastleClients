using CoreLocation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CastleIOS2.Models
{
    public class CoreLocation : CLLocationManagerDelegate
    {
        public override void RegionEntered(CLLocationManager manager, CLRegion region)
        {
            //base.RegionEntered(manager, region);    
            Console.WriteLine("Entered Region");
            manager.StartRangingBeacons(region as CLBeaconRegion);
        }

        public override void RegionLeft(CLLocationManager manager, CLRegion region)
        {
            //base.RegionLeft(manager, region);
            Console.WriteLine("Exited Region");
            manager.StopRangingBeacons(region as CLBeaconRegion);
        }

        public override void DidRangeBeacons(CLLocationManager manager, CLBeacon[] beacons, CLBeaconRegion region)
        {
            //base.DidRangeBeacons(manager, beacons, region);
            Console.WriteLine("Ranging Beacons");
            if (beacons.Length > 0)
            {
                CLBeacon selectedBeacon = (CLBeacon)beacons.GetValue(0);
                Console.WriteLine(selectedBeacon.ProximityUuid.ToString() + "|" + selectedBeacon.Proximity.ToString());
            }

        }

        public override void DidDetermineState(CLLocationManager manager, CLRegionState state, CLRegion region)
        {
            //base.DidDetermineState(manager, state, region); 
            if(state == CLRegionState.Inside)
            {
                manager.StartRangingBeacons(region as CLBeaconRegion);
            }
        }
    }
}
