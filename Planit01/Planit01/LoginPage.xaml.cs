using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Planit01
{
    public partial class LoginPage : ContentPage
    {
        string authCodeFromServer = "0000";
        string userId = "2001";

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
            // isNumber in DB
            //foundNumber = 

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
                //userId = "Value from server" 

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
                var phoneID = CrossDeviceInfo.Current.Id;

                // -- All done on back end --
                // Send number and device pair to server to use for authentication



                await Navigation.PushAsync(new ProfilePage(userId));
            }
            else
            {
                AuthNumberIncorrect();
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
