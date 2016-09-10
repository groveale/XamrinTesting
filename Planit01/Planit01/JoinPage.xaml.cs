using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Planit01
{
    public partial class JoinPage : ContentPage
    {

        bool newPhoto = false;
        Stream photoStream = null; 

        public JoinPage()
        {
            InitializeComponent();

            inputImage.Source = ImageSource.FromFile("Images/default-user-image.png");
        }

        async void OnJoinButtonClicked(object sender, EventArgs args)
        {
            // Send number to WebAPI
            var phoneNumber = inputNumber.Text;

            bool foundNumber = false;

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
                Byte[] userImage = null;
                if (newPhoto)
                {
                    // User has chosen a new image
                    using (var memoryStream = new MemoryStream())
                    {
                        photoStream.CopyTo(memoryStream);
                        photoStream.Dispose();
                        userImage = memoryStream.ToArray();
                    }
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
                    photoStream = photo.GetStream();
                    photo.Dispose();
                    return photoStream;
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
