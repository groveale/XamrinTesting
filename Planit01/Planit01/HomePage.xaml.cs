using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Planit01
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            //ToDo
            // Figure out logic to get phone number + imei number from OS and login automatically
            // If already authenticated

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
    }
}
