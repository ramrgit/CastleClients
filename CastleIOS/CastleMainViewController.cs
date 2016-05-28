using System;

using UIKit;

namespace CastleIOS
{
	public partial class CastleMainViewController : UIViewController
	{
		public CastleMainViewController () : base ("CastleMainViewController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
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


