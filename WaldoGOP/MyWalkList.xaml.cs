using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace WaldoGOP
{
    public partial class MyWalkList : ContentPage
    {
        public MyWalkList()
        {
            InitializeComponent();
            Services services = new Services();

            ObservableCollection<Models.MyWalkList> walkLists = new ObservableCollection<Models.MyWalkList>();

            try
            {
                walkLists = new ObservableCollection<Models.MyWalkList>(services.getMyWalkList());
                if (walkLists != null || walkLists.Count > 0)
                {
                    App.Database.AddWalkLists(walkLists.ToList());
                }
            }
            catch (Exception exception)
            {
                Console.Write(exception);
                walkLists = new ObservableCollection<Models.MyWalkList>(App.Database.GetMyWalkListsAsync().Result);
            }

            listWalkList.ItemsSource = walkLists;
            listWalkList.ItemTapped += ListWalkList_ItemTapped; ;

            listWalkList.RefreshCommand = new Command(() =>
            {
                try
                {
                    walkLists = new ObservableCollection<Models.MyWalkList>(services.getMyWalkList());
                    if (walkLists != null || walkLists.Count > 0)
                    {
                        App.Database.AddWalkLists(walkLists.ToList());
                    }
                }
                catch (Exception exception)
                {
                    Console.Write(exception);
                    walkLists = new ObservableCollection<Models.MyWalkList>(App.Database.GetMyWalkListsAsync().Result);
                }

                listWalkList.ItemsSource = walkLists;
                listWalkList.IsRefreshing = false;
            });
        }

        void ListWalkList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}