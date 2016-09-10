using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Planit01
{
    public partial class JoinPage : ContentPage
    {

        bool newPhoto = false;

        public JoinPage()
        {
            InitializeComponent();

            inputImage.Source = ImageSource.FromFile("Images/default-user-image.png");
        }

        async void OnJoinButtonClicked(object sender, EventArgs args)
        {
            // Send number to WebAPI
            var phoneNumber = inputNumber.Text;

            bool foundNumber = true;

            // -- All done on back end --
            // isNumber in DB
            //foundNumber = 

            if (foundNumber)
            {
                PhoneNumberAlreadyExists();
            }
            else
            {
                var userName = inputName.Text;
                if (newPhoto)
                {
                    // User has chosen a new image
                    var image = inputImage;
                }
                
                // send json to server to create user
                // Response received from server that user has been created

                // Go to profile page
                await Navigation.PushAsync(new ProfilePage());

            }
        }

        async void OnPhotoTapped(object sender, EventArgs args)
        {
            // Select a photo from gallery 
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                var photo = await CrossMedia.Current.PickPhotoAsync();
                inputImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = photo.GetStream();
                    photo.Dispose();
                    return stream;
                });

                newPhoto = true;
            }
                
        }

        async void OnLoginButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private void PhoneNumberAlreadyExists()
        {
            labelUserAlreadyExists.IsEnabled = true;
            labelUserAlreadyExists.IsVisible = true;
            buttonLogin.IsEnabled = true;
            buttonLogin.IsVisible = true;
        }

    }
}
