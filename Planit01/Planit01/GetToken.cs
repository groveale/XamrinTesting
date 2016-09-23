using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planit01
{
    class GetToken
    {

        string phoneNumber;
        string userId;
        string phoneId;

        public GetToken(string number)
        {
            phoneNumber = number;
            phoneId = CrossDeviceInfo.Current.Id;
        }




    }
}
