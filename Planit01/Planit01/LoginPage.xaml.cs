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
    public partial class LoginPage : ContentPage
    {
        string authCodeFromServer = "0000";
        int userId = 2002;

        public LoginPage()
        {
            InitializeComponent();
        }

        void OnAuthButtonClicked(object sender, EventArgs args)
        {
            // Send number to WebAPI
            var phoneNumber = inputPhone.Text;
            
            bool foundNumber = true;

            // -- All done on back end --
            // isNumber in DB, if yes, user ID comes back



            //userId =  

            if (userId == 0)
            {
                foundNumber = false;
            }

            if (!foundNumber)
            {
                PhoneNumberNotFound();
            }
            else
            {
                // -- All done on back end --
                // Generate Authentication code
                // Send as SMS to number


                // Response received from server that auth code has been sent
                //authCodeFromServer = "Value from server";
                

                // Enable input Auth code field
                inputAuthCode.IsEnabled = true;
                inputAuthCode.IsVisible = true;
                inputPhone.IsEnabled = false;
                buttonAuth.IsEnabled = false;
                buttonAuth.IsVisible = false;
                buttonLogin.IsEnabled = true;
                buttonLogin.IsVisible = true;
            }
            
        }

        async void OnLoginButtonClicked(object sender, EventArgs args)
        {
            if (authCodeFromServer.Equals(inputAuthCode.Text))
            {

                // Save phone number to device (KeyPair)
                Application.Current.Properties["phoneNumber"] = inputPhone.Text;
                await Application.Current.SavePropertiesAsync();
                // Generate ID
                var deviceID = CrossDeviceInfo.Current.Id;


                // Send number and device pair to server to store for authentication
                CreateDeviceUserMapping(userId, deviceID);

                await Navigation.PushAsync(new ProfilePage(userId));
            }
            else
            {
                AuthNumberIncorrect();
            }
            
        }

        async void CreateDeviceUserMapping(int userId, string phoneID)
        {
            string RestUrl = "http://localhost:53615/Devices";
            var uri = new Uri(string.Format(RestUrl));

            try
            {

                var newDeviceParing = new DeviceParing() { DeviceId = phoneID, DeviceParingUserId = userId};
                var json = JsonConvert.SerializeObject(newDeviceParing);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    HttpResponseMessage response = null;
                    response = await client.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Authenticated", "Successfully Authenticated Device, click OK to go to your profile page", "OK");
                    }
                }

            }
            catch
            {
                await DisplayAlert("Error", "Unable to authenticate, click OK and try again", "OK");
            }
        }

        async void OnJoinButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new JoinPage());
        }

        void OnResetButtonClicked(object sender, EventArgs args)
        {
            inputPhone.Text = "";
            inputPhone.IsEnabled = true;
            inputAuthCode.IsVisible = false;
            inputAuthCode.Text = "";
            buttonAuth.IsVisible = true;
            buttonAuth.IsEnabled = true;
            buttonLogin.IsVisible = false;
            buttonJoin.IsVisible = false;
        }

        private void PhoneNumberNotFound()
        {
            DisplayAlert("Do you exist", "Your Number was not found on the System please re-enter or click Join to sign up", "OK");
            buttonJoin.IsEnabled = true;
            buttonJoin.IsVisible = true;
        }

        private void AuthNumberIncorrect()
        {
            // Clear entered Auth code
            inputAuthCode.Text = "";
            // Display alert
            DisplayAlert("Incorrect Code", "Incorrect Auth code entered, please check and try again", "OK");
        }
    }
}
