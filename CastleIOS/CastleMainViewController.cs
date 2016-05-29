using Edison.Castle.Clients.Data;
using System;
using System.Diagnostics;
using System.Net.Http;
using UIKit;

namespace CastleIOS
{
	public partial class CastleMainViewController : UIViewController
	{
		public CastleMainViewController () : base ("CastleMainViewController", null)
		{
		}

		public async override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			try
			{
				//HttpClient client = new HttpClient

				AuthenticationService authSvc = new AuthenticationService();
				var result = await authSvc.Authenticate(@"Edison\ramr", "Good12345");
				if (result)
				{
					LockService lockSvc = new LockService();
					bool lockResult = await lockSvc.GetLocksWithHttpClient();
				}
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex);
			}
			

		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		partial void btnLogin_TouchUpInside (UIButton sender)
		{
			throw new NotImplementedException ();
		}
	}
}


