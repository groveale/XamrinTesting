﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planit01.Models
{
    public class DeviceParing
    {
        public int DeviceParingId { get; set; }
        public int DeviceParingUserId { get; set; }
        public string DeviceId { get; set; }
    }
}
