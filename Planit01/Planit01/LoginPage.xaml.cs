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

                // Enable input Auth code field
                inputAuthCode.IsEnabled = true;
                inputAuthCode.IsVisible = true;
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
               // -- All done on back end --
               // Send response to server that user has been authenticated

               await Navigation.PushAsync(new ProfilePage());
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
            inputAuthCode.Text = "";
            buttonAuth.IsVisible = true;
            buttonAuth.IsEnabled = true;
            buttonLogin.IsVisible = false;
            buttonJoin.IsVisible = false;
            labelNumberNotFound.IsVisible = false;
            labelIncorrectAuth.IsVisible = false;
        }

        private void PhoneNumberNotFound()
        {
            labelNumberNotFound.IsEnabled = true;
            labelNumberNotFound.IsVisible = true;
            buttonJoin.IsEnabled = true;
            buttonJoin.IsVisible = true;
        }

        private void AuthNumberIncorrect()
        {
            // Clear entered Auth code
            inputAuthCode.Text = "";
            labelIncorrectAuth.IsEnabled = true;
            labelIncorrectAuth.IsVisible = true;
        }
    }
}
