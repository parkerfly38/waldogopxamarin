using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace WaldoGOP
{
    public partial class EventDetail : ContentPage
    {
        Geocoder geocoder;
        Models.Event eventData;

        public EventDetail(int eventId)
        {
            InitializeComponent();

            Services services = new Services();

            eventData = new Models.Event();

            try
            {
                eventData = services.getEvent(eventId);
            }
            catch (Exception exception)
            {
                Console.Write(exception);
                eventData = App.Database.GetEventAsync(eventId).Result;
            }

            BindingContext = eventData;

            if (eventData.eventLink.Length <= 0)
            {
                btnLink.IsVisible = false;
            } else {
                btnLink.IsVisible = true;
            }

            btnLink.Clicked += BtnLink_Clicked;
            btnRsvp.Clicked += BtnRsvp_Clicked;

            geocoder = new Geocoder();

            DoGeoCode(eventData.eventLocation, eventData.eventLocationName);

        }

        private async void DoGeoCode(string address, string locationName)
        {
            var locations = await geocoder.GetPositionsForAddressAsync(address);
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = locations.First(),
                Label = locationName,
                Address = address
            };
            mapEvent.Pins.Add(pin);
            mapEvent.MoveToRegion(MapSpan.FromCenterAndRadius(locations.First(), Distance.FromMiles(1)));
        }

        void BtnLink_Clicked(object sender, EventArgs e)
        {
            string url = eventData.eventLink;
            Device.OpenUri(new Uri(url));
        }

        void BtnRsvp_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RSVP(eventData.eventID));
        }
    }
}