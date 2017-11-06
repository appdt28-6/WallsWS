using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WallsWS.Models
{
    public class Login_Return
    {
        public int barb_id { get; set; }
        public string barb_name { get; set; }
        public string barb_phone { get; set; }
        public string barb_mail { get; set; }

        public string barb_urlimage { get; set; }
    }
}