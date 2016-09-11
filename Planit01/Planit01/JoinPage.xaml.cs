using Newtonsoft.Json;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
            var userNumber = inputNumber.Text;
            bool foundNumber = await DoesUserExist(userNumber);

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

            // Don't enable login button if server error
            if (labelUserAlreadyExists.Text.Contains("Login"))
            {
                buttonLogin.IsEnabled = true;
                buttonLogin.IsVisible = true;
            }
            
        }

        async Task<bool> DoesUserExist(string userNumber)
        {
            string result = "";
            bool foundNumber = true;

            // Make Get request to API
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:53615");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync("/User/" + userNumber);
                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }
                }
                catch
                {
                    result = "Unable to contact server";
                }
            }

            switch (result.ToString())
            {
                case "true":
                    foundNumber = true;
                    break;

                case "false":
                    foundNumber = false;
                    break;

                default:
                    labelUserAlreadyExists.Text = "Unable to connect to server please check internet connection and try again";
                    break;
            }

            return foundNumber;
        }

    }
}
