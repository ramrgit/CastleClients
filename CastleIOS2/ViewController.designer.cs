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

namespace CastleIOS2
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton AboutButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView LocksTableView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LoggedInLabel { get; set; }

		[Action ("AboutButton_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void AboutButton_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (AboutButton != null) {
				AboutButton.Dispose ();
				AboutButton = null;
			}
			if (LocksTableView != null) {
				LocksTableView.Dispose ();
				LocksTableView = null;
			}
			if (LoggedInLabel != null) {
				LoggedInLabel.Dispose ();
				LoggedInLabel = null;
			}
		}
	}
}
