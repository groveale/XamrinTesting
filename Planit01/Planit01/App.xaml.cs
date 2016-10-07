using Newtonsoft.Json;
using Planit01.Models;
using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Planit01
{
    public partial class App : Application
    {

        public App()
        {

            MainPage = new NavigationPage(new HomePage())
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
