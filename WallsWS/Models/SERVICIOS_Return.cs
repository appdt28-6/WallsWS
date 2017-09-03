using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WallsWS.Models
{
    public class SERVICIOS_Return
    {
        public int serv_id { get; set; }
        public string serv_name { get; set; }
        public string serv_desc { get; set; }
        public Nullable<double> serv_price { get; set; }
    }
}