using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WallsWS.Models
{
    public class HORARIO_Return
    {
        public int hrdi_id { get; set; }
        public Nullable<int> barb_id { get; set; }
        public string hrdi_hora { get; set; }
        public string hrdi_date { get; set; }
        public Nullable<int> hrdi_status { get; set; }
    }
}