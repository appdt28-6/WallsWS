using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WallsWS.Models;

namespace WallsWS.Controllers
{
    public class SetServicesController : ApiController
    {
        AppDTEntities db = new AppDTEntities();
        public IEnumerable<SERVICIOS_Return> Index(int sucuid)
        {
            return db.SERVICIOS.Where(q => q.sucu_id == sucuid&&q.serv_product==0).Select(g => new SERVICIOS_Return()
            {
                serv_id = g.serv_id,
                serv_name = g.serv_name,
                serv_desc = g.serv_desc,
                serv_price = g.serv_price

            }).ToList();
        }
    }
}
