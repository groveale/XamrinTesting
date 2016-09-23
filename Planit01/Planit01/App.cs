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
        // obtained using device ID and querying DB
        private string userId = "";
        private string phoneNumber = "";

        public App()
        {
            // Check to see if user has already authenticated app
            phoneNumber = alreadyAuthenticated();
            // GrosserPhoto user for app
            userId = "";

            // If no number/device id found
            if (userId == "")
            {
                MainPage = new NavigationPage(new HomePage())
                {
                    BarBackgroundColor = Color.FromHex("#3B4550"),
                    BarTextColor = Color.FromHex("#E7623C")
                };
            }
            else
            {



                MainPage = new NavigationPage(new ProfilePage(userId))
                {
                    BarBackgroundColor = Color.FromHex("#3B4550"),
                    BarTextColor = Color.FromHex("#E7623C")
                };
            }          
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


        private string alreadyAuthenticated()
        {
            try
            {
                string phoneNumberFromPhone = Application.Current.Properties["phoneNumber"] as string;
                return phoneNumberFromPhone;
            }
            catch
            {
                return "";
            }
        }
    }
}
