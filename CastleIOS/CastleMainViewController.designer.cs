// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace CastleIOS
{
	[Register ("CastleMainViewController")]
	partial class CastleMainViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnLogin { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtLogin { get; set; }

		[Action ("btnLogin_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void btnLogin_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (btnLogin != null) {
				btnLogin.Dispose ();
				btnLogin = null;
			}
			if (txtLogin != null) {
				txtLogin.Dispose ();
				txtLogin = null;
			}
		}
	}
}
