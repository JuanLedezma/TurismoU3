using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TurismoU3
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.ExplorarView());
            var navigationPage = Application.Current.MainPage as NavigationPage;
            navigationPage.BarBackgroundColor = Color.FromHex("#0AB927");
            navigationPage.BarTextColor = Color.White;
            
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
