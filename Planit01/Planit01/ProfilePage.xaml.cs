using Newtonsoft.Json;
using Planit01.Models;
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
    public partial class ProfilePage : ContentPage
    {
        private int userId = 0;
        private User user;
        private string errorMsg;
        private Byte[] userImage = null;

        public ProfilePage(int userIdFromDB)
        {
            InitializeComponent();

            userId = userIdFromDB;

            GetUser(userId);
        }

        void populateUserDetails(User userFromDB)
        {
            labelName.Text = userFromDB.UserName;

            imageProfile.Source = convertByteToImageSource(userFromDB.UserPhoto);

        }

        private ImageSource convertByteToImageSource(Byte[] imageFromDB)
        {
            if (imageFromDB == null)
            {
                // if no image return default
                return ImageSource.FromFile("Images/default-user-image.png");
            }
            else
            {
                // Potentially just required for windows
                Stream stream = new MemoryStream(imageFromDB);
                // LAMDA
                return ImageSource.FromStream(() => stream);

            }
            
        }

        async void GetUser(int userId)
        {
            // Make HTTP Get request to API
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:53615");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync("/Users/" + userId);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<User>(content);

                        // Send user deets to interface
                        populateUserDetails(user);
                    }
                }
                catch
                {
                    errorMsg = "Unable to get user data server";
                }
            }
        }

        async void OnPhotoTapped(object sender, EventArgs args)
        {
            
            // Select a photo from gallery 
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                var photo = await CrossMedia.Current.PickPhotoAsync();
                imageProfile.Source = ImageSource.FromStream(() =>
                {
                    Stream photoStream = photo.GetStream();
                    return photoStream;
                });

                using (MemoryStream ms = new MemoryStream())
                {
                    photo.GetStream().CopyTo(ms);
                    photo.Dispose();
                    userImage = ms.ToArray();
                }
            }
            
        }

        async void OnCreateEventButtonClicked(object sender, EventArgs args)
        {
            // New Event Page
            //await Navigation.PushAsync(new LoginPage());
        }

        async void OnGroupButtonClicked(object sender, EventArgs args)
        {
            // Go To Group Page
            //await Navigation.PushAsync(new LoginPage());
        }

        async void OnCalendarButtonClicked(object sender, EventArgs args)
        {
            // Go to calendar page
            await Navigation.PushAsync(new CalendarPage());
        }
    }
}
