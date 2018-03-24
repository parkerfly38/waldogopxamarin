using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WaldoGOP
{
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
            //check for app login
            if (App.Database.GetAppDataAsync().Result.Verified == true)
            {
                lblAccount.Text = "Change accounts";
            } else {
                lblAccount.Text = "Login with Verified Account";
            }

            eventStack.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    eventStack.BackgroundColor = Color.WhiteSmoke;
                    Navigation.PushAsync(new WaldoGOPPage() { Title = "Events" });
                })
            });

            walkListStack.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    eventStack.BackgroundColor = Color.WhiteSmoke;
                    Navigation.PushAsync(new MyWalkList() { Title = "My Walk Lists" });
                })
            });

            loginStack.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    eventStack.BackgroundColor = Color.WhiteSmoke;
                    Navigation.PushAsync(new VerifiedLogin() { Title = "Verified Account Login" });
                })
            });
        }
    }
}
