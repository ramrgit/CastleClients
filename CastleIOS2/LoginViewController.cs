using Edison.Castle.Clients.Data;
using Foundation;
using PerpetualEngine.Storage;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace CastleIOS2
{
	partial class LoginViewController : UIViewController
	{

		private SimpleStorage storage = SimpleStorage.EditGroup("EdisonCastleCache");

		public LoginViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();


			LoginButton.TouchUpInside += (sender, e) =>
			{
				AuthenticateUser();
			};
		}

		private async void AuthenticateUser()
		{
			AuthenticationService authSvc = new AuthenticationService();

			string authToken = await authSvc.Authenticate(LoginText.Text, PasswordText.Text);
			if (!string.IsNullOrEmpty(authToken))
			{
				storage.Put("AuthToken", authToken);
				this.DismissViewController(true, null);
			}
			else
			{
				//authentication failuer - display a failed message
			}
		}
	}
}
