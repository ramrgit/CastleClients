using CoreAnimation;
using CoreLocation;
using Edison.Castle.Clients.Data;
using Edison.Castle.Clients.Data.Models;
using Foundation;
using HomeKit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PerpetualEngine.Storage;
using SpriteKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using UIKit;

namespace CastleIOS2
{
    public partial class ViewController : UIViewController
    {
        private SimpleStorage storage = SimpleStorage.EditGroup("EdisonCastleCache");
        LockService _lockSvc;
        private IEnumerable<Lock> Locks { get; set; }

        private CLLocationManager _locationManager;
        private Guid NearestLockUuid;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            AboutButton.TouchUpInside += AboutButton_TouchUpInside;

            //set up the CoreLocation services
            _locationManager = new CLLocationManager();
            _locationManager.AuthorizationChanged += LocationManager_AuthorizationChanged;
            _locationManager.RequestAlwaysAuthorization();
            _locationManager.DidRangeBeacons += LocationManager_DidRangeBeacons; 
             


            //check if authToken is present and not empty
            string authToken = string.Empty;
            try
            {
                //storage.Put("AuthToken", string.Empty);  //for testing loing view
                authToken = storage.Get("AuthToken");
                if (string.IsNullOrEmpty(authToken))
                {
                    //show the Login Screen
                    ShowLoginView();
                    this.LoggedInLabel.Text = "Logged in as " + GetLoggedInUserName(authToken);
                }
                else
                {
                    ShowLocksView();

                    _lockSvc = new LockService(authToken);
                    LoggedInLabel.Text = "Logged in as " + GetLoggedInUserName(authToken);
                    IEnumerable<Lock> locksList;
                    locksList = storage.Get<IEnumerable<Lock>>("LocksList");
                    if(locksList == null)
                    {
                        GetLocks(authToken);
                    }
                    this.Locks = LoadLockDetails();
                    this.LocksTableView.Source = new TableSource(this.Locks);
                }
            }
            catch(Exception ex)
            {
                //handle exception
                DisplayAlert(ex.Message);
            }

        }


        private void AboutButton_TouchUpInside(object sender, EventArgs e)
        {
            var storyBoard = this.Storyboard;
            var aboutViewController = (AboutViewController)storyBoard.InstantiateViewController("AboutViewController");
            this.PresentViewController(aboutViewController, true, null);
        }

        private void LocationManager_DidRangeBeacons(object sender, CLRegionBeaconsRangedEventArgs e)
        {
            CLBeacon nearestBeacon = null;

            if(e.Beacons.Any(m => m.Rssi < 0))  //ignore any 0 values and find the nearest beacon.
            {
                foreach (CLBeacon beacon in e.Beacons)
                {
                    Debug.WriteLine("Found beacon {0} {1}.{2} - rssi {3}", beacon.ProximityUuid.AsString(), beacon.Major, beacon.Minor, beacon.Rssi);
                    //var nearestBeacon = e.Beacons.OrderBy(x => x.Rssi).FirstOrDefault();

                    if (nearestBeacon == null)
                    {
                        nearestBeacon = beacon;
                    }
                    else if (beacon.Rssi > nearestBeacon.Rssi)
                    {
                        nearestBeacon = beacon;
                    }
                    this.NearestLockUuid = Guid.Parse(nearestBeacon.ProximityUuid.AsString());
                    Debug.WriteLine("Nearest beacon is {0}", this.NearestLockUuid);

                    //now that we have the nearest beacon, check if rssi is > -50 (or config value) and then send unlock request
                    if(nearestBeacon.Rssi > -60)
                    {
                        //stop ranging -? nned to have the CLregion
                        //_locationManager.RangedRegions.Where(x => x.re)
                        StopAllRanging();

                        //send unlock requests
                        Debug.WriteLine("Sending Unlock request");
                        SendUnlockRequest();
                        Debug.WriteLine("Unlock request sent");


                        Thread.Sleep(15000);
                        //start ranging
                        StartAllRanging();
                    }
                }
            }

        }


        private void StopAllRanging()
        {
            foreach(Lock item in this.Locks)
            {
                CLBeaconRegion region = new CLBeaconRegion(new NSUuid(item.LockUUID.ToString()), item.LockName);
                _locationManager.StopRangingBeacons(region);
            }
        }

        private void StartAllRanging()
        {
            foreach (Lock item in this.Locks)
            {
                CLBeaconRegion region = new CLBeaconRegion(new NSUuid(item.LockUUID.ToString()), item.LockName);
                _locationManager.StartRangingBeacons(region);
            }

        }

        private async void SendUnlockRequest()
        {
            Lock nearestLock = this.Locks.Where(x => x.LockUUID == this.NearestLockUuid).FirstOrDefault();
            bool unlockResult = await _lockSvc.Unlock(nearestLock.LockId);
        }

        private void LocationManager_AuthorizationChanged(object sender, CLAuthorizationChangedEventArgs e)
        {
            if (this.Locks != null)
            {
                foreach (Lock item in this.Locks)
                {
                    CLBeaconRegion region = new CLBeaconRegion(new NSUuid(item.LockUUID.ToString()), item.LockName);
                    _locationManager.StartRangingBeacons(region);
                }
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        //partial void AboutButton_TouchUpInside(UIButton sender)
        //{
        //    var storyBoard = this.Storyboard;
        //    var aboutViewController = (AboutViewController)storyBoard.InstantiateViewController("AboutViewController");
        //    this.PresentViewController(aboutViewController, true, null);
        //}
   

        private void ShowLoginView()
        {
            Timer tmr = new Timer(new TimerCallback((state) =>
            {
                this.InvokeOnMainThread(new Action(() => 
                {
                    var storyBoard = this.Storyboard;
                    var loginViewController = (LoginViewController)Storyboard.InstantiateViewController("LoginViewController");
                    this.PresentViewController(loginViewController, true, null);
                }));
            }), null, 1000, Timeout.Infinite);

        }


        private void ShowLocksView()
        {
            Timer tmr = new Timer(new TimerCallback((state) =>
            {
                this.InvokeOnMainThread(new Action(() =>
                {
                    var storyBoard = this.Storyboard;
                    var locksViewController = (LocksViewController)Storyboard.InstantiateViewController("LocksViewController");
                    this.PresentViewController(locksViewController, true, null);
                }));
            }), null, 1000, Timeout.Infinite);
        }

        private string GetLoggedInUserName(string authToken)
        {
            AuthenticationService authSvc = new AuthenticationService();
            return (authSvc.GetLoggedInUser(authToken));
        }

        private async void GetLocks(string authToken)
        {
            
            //IEnumerable<Lock> locksList = await lockSvc.GetLocksWithHttpClient();
            string locksList = await _lockSvc.GetLocksString();
            storage.Put("LocksList", locksList);
        }


        private async void SendUnlockRequest(string authToken, Guid lockId)
        {
           
            bool retValue = await _lockSvc.Unlock(lockId);
        }

        private IEnumerable<Lock> LoadLockDetails()
        {
            string locksString = storage.Get("LocksList");
            if(!string.IsNullOrEmpty(locksString))
            {
                dynamic lockContext = JObject.Parse(locksString);
                IEnumerable<Lock> lockList = JsonConvert.DeserializeObject<IEnumerable<Lock>>(lockContext["Results"].ToString());
                return lockList;
            }
            else
            {
                return null;
            }
        }


        private void DisplayAlert(string message)
        {
            UIAlertView alert = new UIAlertView();
            alert.Title = "Application Error";
            alert.AddButton("OK");
            alert.AlertViewStyle = UIAlertViewStyle.Default;
            alert.Message = message;
            alert.Clicked += (object s, UIButtonEventArgs e) =>
            {


            };
            alert.Show();
        }
    }
}
