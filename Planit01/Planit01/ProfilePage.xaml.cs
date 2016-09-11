using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Planit01
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();

            imageProfile.Source = ImageSource.FromFile("Images/default-user-image.png");
        }

        async void OnPhotoTapped(object sender, EventArgs args)
        {
            /*
            // Select a photo from gallery 
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                var photo = await CrossMedia.Current.PickPhotoAsync();
                inputImage.Source = ImageSource.FromStream(() =>
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
            */
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
            //await Navigation.PushAsync(new LoginPage());
        }
    }
}
