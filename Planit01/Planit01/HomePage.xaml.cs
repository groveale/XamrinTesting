using Newtonsoft.Json;
using Planit01.Models;
using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Planit01
{
    public partial class HomePage : ContentPage
    {
        // obtained using device ID and querying DB
        private string userId = "";
        private bool match = false;

        public HomePage()
        {
            
            // Check to see if user has already authenticated app
            userId = alreadyAuthenticated();
            // If no number id found
            if (userId != "")
            {
                // check device ID matches user/device paring in db
                DeivceIDMatch(userId);
                if (match == true)
                {
                    Navigation.PushAsync(new ProfilePage(userId));
                }
            }

            InitializeComponent();

            // Login Button Clicked
            loginButton.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new LoginPage());
            };

            // Join Button Clicked
            joinButton.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new JoinPage());
            };
        }

       
        private string alreadyAuthenticated()
        {
            try
            {
                string phoneNumberFromPhone = Application.Current.Properties["userID"] as string;
                return phoneNumberFromPhone;
            }
            catch
            {
                return "";
            }
        }

        async void DeivceIDMatch(string userID)
        {
            DeviceParing result;

            // Make HTTP Get request to API
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.restURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage response = await client.GetAsync("/DeviceParing/" + userID);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<DeviceParing>(content);

                        if (result.DeviceId == CrossDeviceInfo.Current.Id)
                        {
                            match = true;
                        }
                        else
                        {
                            match = false;
                        }
                    }
                }
                catch
                {
                    await DisplayAlert("No Connection", "Unable to connect to server please check internet connection and try again", "OK");
                }
            }
        }
    }
}
