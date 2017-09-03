using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WallsWS.Models
{
    public class vis_AGENDA_BARBEROS_Result
    {
        public int agen_id { get; set; }
        public Nullable<int> barb_id { get; set; }
        public Nullable<int> serv_id { get; set; }
        public string serv_name { get; set; }
        public string cust_name { get; set; }
        public string agen_date { get; set; }
        public string hrdi_hora { get; set; }
        public Nullable<int> agen_status { get; set; }
    }
}