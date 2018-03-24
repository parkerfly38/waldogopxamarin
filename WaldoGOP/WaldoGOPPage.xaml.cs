using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WaldoGOP.Models;
using Xamarin.Forms;

namespace WaldoGOP
{
    public partial class WaldoGOPPage : ContentPage
    {
        public WaldoGOPPage()
        {
            InitializeComponent();
            Services services = new Services();

            ObservableCollection<Event> events = new ObservableCollection<Event>();

            try {
                events = new ObservableCollection<Event>(services.getAllEvents().Where(x => x.eventDate >= DateTime.Now));
                if (events != null || events.Count > 0)
                {
                    App.Database.UpdateEvents(events.ToList());
                }
            } catch (Exception exception)
            {
                Console.Write(exception);
                events = new ObservableCollection<Event>(App.Database.GetEventDataAsync().Result.Where(x => x.eventDate >= DateTime.Now));
            }

            listEvents.ItemsSource = events;
            listEvents.ItemTapped += ListEvents_ItemTapped;

            listEvents.RefreshCommand = new Command(() =>
            {
                try
                {
                    events = new ObservableCollection<Event>(services.getAllEvents().Where(x => x.eventDate >= DateTime.Now));
                    if (events != null || events.Count > 0)
                    {
                        App.Database.UpdateEvents(events.ToList());
                    }
                }
                catch (Exception exception)
                {
                    Console.Write(exception);
                    events = new ObservableCollection<Event>(App.Database.GetEventDataAsync().Result.Where(x => x.eventDate >= DateTime.Now));
                }
                listEvents.ItemsSource = events;
                listEvents.IsRefreshing = false;
            });
        }

        void ListEvents_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Console.Write(e.Item);
            Models.Event eventForNav = (Models.Event)e.Item;
            Navigation.PushAsync(new EventDetail(eventForNav.eventID) {Title="Event Detail"});
        }
    }
}
