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
    [Register ("LocksViewController")]
    partial class LocksViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView LocksTableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LoginLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LocksTableView != null) {
                LocksTableView.Dispose ();
                LocksTableView = null;
            }

            if (LoginLabel != null) {
                LoginLabel.Dispose ();
                LoginLabel = null;
            }
        }
    }
}