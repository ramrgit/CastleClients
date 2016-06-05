using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace CastleIOS2
{
	partial class AboutViewController : UIViewController
	{
		public AboutViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.CloseButton.TouchUpInside += (sender, e) =>
			{
				this.DismissViewController(true, null);
			};
		}
	}
}
