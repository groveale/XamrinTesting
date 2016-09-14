using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Planit01
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            //MainPage = new NavigationPage(new HomePage())
            MainPage = new NavigationPage(new CalendarPage())
            {
                BarBackgroundColor = Color.FromHex("#3B4550"),
                BarTextColor = Color.FromHex("#E7623C")
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
    }
}
