using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Planit01
{
    class GetToken
    {

        string phoneNumber;
        string userId;
        string phoneId;
        string error;

        public GetToken(string number)
        {
            phoneNumber = number;
            phoneId = CrossDeviceInfo.Current.Id;
        }


        async Task<bool> IsUserAuthenticated(String number, string id)
        {
            string result = "";
            bool foundNumber = true;

            // Make HTTP Get request to API
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:53615");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync("/User/" + number + "/" + id);
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
                    error = "Unable to connect to server please check internet connection and try again";
                    break;
            }

            return foundNumber;

        }


        async void GetUserToken(String number, string id)
        {
            bool userAuthed = await IsUserAuthenticated(number, id);

            /*
            if (userAuthed) // user-defined function, checks against a database
            {
                JwtSecurityToken token = AppServiceLoginHandler.CreateToken(new Claim[] { new Claim(JwtRegisteredClaimNames.Sub, assertion["username"]) },
                    mySigningKey,
                    myAppURL,
                    myAppURL,
                    TimeSpan.FromHours(24));
                return Ok(new LoginResult()
                {
                    AuthenticationToken = token.RawData,
                    User = new LoginResultUser() { UserId = userName.ToString() }
                });
            }
            else // user assertion was not valid
            {
                return this.Request.CreateUnauthorizedResponse();
            }
            */
        }

    }
}
