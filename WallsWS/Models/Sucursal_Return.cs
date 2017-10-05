using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WallsWS.Models
{
    public class Sucursal_Return
    {
        public int sucu_id { get; set; }
        public string sucu_name { get; set; }
        public string sucu_addres { get; set; }
        public string sucu_lat { get; set; }
        public string sucu_lon { get; set; }

        public string sucu_phone { get; set; }

    }
}