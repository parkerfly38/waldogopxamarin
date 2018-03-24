using Xamarin.Forms;
using SQLitePCL;

namespace WaldoGOP
{
    public partial class App : Application
    {
        static AppDatabase database;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Home() { Title = "Home" })
            {
                BarBackgroundColor = Color.FromHex("F88379"),
                BarTextColor = Color.WhiteSmoke
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static AppDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new AppDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("appdata.db"));
                }
                return database;
            }
        }


    }
}
