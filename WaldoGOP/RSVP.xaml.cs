using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WaldoGOP
{
    public partial class RSVP : ContentPage
    {
        int rsvpEventId;

        public RSVP(int eventid)
        {
            InitializeComponent();
            btnSendRsvp.Clicked += BtnSendRsvp_Clicked;
            btnCancel.Clicked += BtnCancel_Clicked;
            numberAttending.ValueChanged += NumberAttending_ValueChanged;
            rsvpEventId = eventid;
        }

        void BtnSendRsvp_Clicked(object sender, EventArgs e)
        {
            if (rsvpName.Text.Length <= 0)
            {
                DisplayAlert("Error","Must enter name of person attending.","OK");
            }
            if (numberAttending.Value < 1)
            {
                DisplayAlert("Error", "Must enter number of attendees.", "OK");
            }
            Models.EventRSVP rsvp = new Models.EventRSVP()
            {
                name = rsvpName.Text,
                numbercoming = (int)numberAttending.Value,
                contactemail = contactEmail.Text,
                contactphone = contactPhone.Text,
                eventId = rsvpEventId
            };
            Services services = new Services();
            services.addRSVP(rsvp);

            Navigation.PopAsync();
        }

        void BtnCancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        void NumberAttending_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            numberAttendingLabel.Text = numberAttending.Value.ToString();
        }
    }
}
