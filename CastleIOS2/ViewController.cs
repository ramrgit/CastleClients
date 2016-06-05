using CoreAnimation;
using Edison.Castle.Clients.Data;
using Edison.Castle.Clients.Data.Models;
using HomeKit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PerpetualEngine.Storage;
using SpriteKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UIKit;

namespace CastleIOS2
{
    public partial class ViewController : UIViewController
    {
        private SimpleStorage storage = SimpleStorage.EditGroup("EdisonCastleCache");

        private IEnumerable<Lock> Locks { get; set; }

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
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
                }
                else
                {
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


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void AboutButton_TouchUpInside(UIButton sender)
        {
            var storyBoard = this.Storyboard;
            var aboutViewController = (AboutViewController)storyBoard.InstantiateViewController("AboutViewController");
            this.PresentViewController(aboutViewController, true, null);
        }

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


        private string GetLoggedInUserName(string authToken)
        {
            AuthenticationService authSvc = new AuthenticationService();
            return (authSvc.GetLoggedInUser(authToken));
        }

        private async void GetLocks(string authToken)
        {
            LockService lockSvc = new LockService(authToken);
            //IEnumerable<Lock> locksList = await lockSvc.GetLocksWithHttpClient();
            string locksList = await lockSvc.GetLocksString();
            storage.Put("LocksList", locksList);
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
