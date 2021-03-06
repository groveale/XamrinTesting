﻿using Newtonsoft.Json;
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
    public partial class LoginPage : ContentPage
    {
        string authCodeFromServer = "0000";
        string userId = "135C88BB-DADA-4A21-9317-6A2EF0E1B2F8";

        public LoginPage()
        {
            InitializeComponent();
        }

        void OnAuthButtonClicked(object sender, EventArgs args)
        {
            // Send number to WebAPI
            var phoneNumber = inputPhone.Text;
            

            // -- All done on back end --
            // isNumber in DB, if yes, user ID comes back
            // check if user exists True or false
            GetUserID(phoneNumber);

            if (userId == "")
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
                

                // Now that device has been authenticated can send user ID

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

        async void GetUserID(string phoneNumber)
        {
            User result;

            // Make HTTP Get request to API
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Constants.restURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync("/User/" + phoneNumber);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<User>(content);
                        if (result.UserId != "00000000-0000-0000-0000-000000000000")
                        {
                            userId = result.UserId;
                        }
                        else
                        {
                            userId = "";
                        }
                    }

                }
                catch
                {
                    await DisplayAlert ("No Connection", "Unable to connect to server please check internet connection and try again", "OK");
                }
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

        async void CreateDeviceUserMapping(string userId, string phoneID)
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
