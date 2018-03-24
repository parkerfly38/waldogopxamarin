using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WaldoGOP
{
    public partial class VerifiedLogin : ContentPage
    {
        public VerifiedLogin()
        {
            InitializeComponent();
            btnCancel.Clicked += BtnCancel_Clicked;
            btnLogin.Clicked += BtnLogin_Clicked;

            if ( App.Database.GetAppDataAsync().Result.Verified == true)
            {
                alreadyLoggedIn.Text = "You are already have a verified account.  You can login with another verified account.";
                alreadyLoggedIn.IsVisible = true;
            } else {
                alreadyLoggedIn.IsVisible = false;
            }
        }

        void BtnCancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        void BtnLogin_Clicked(object sender, EventArgs e)
        {
            Services services = new Services();

            if (services.getLoginTokens(login.Text, password.Text) == false)
            {
                DisplayAlert("Error", "Account not found or account not verified.", "OK");
            } else {
                Navigation.PopAsync();
            }
        }
    }
}
