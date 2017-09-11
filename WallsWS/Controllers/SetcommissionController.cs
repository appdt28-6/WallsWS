using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WallsWS.Models;

namespace WallsWS.Controllers
{
    public class SetcommissionController : ApiController
    {
        public List<commission_Return> Index(int barberid)
        {
            using (AppDTEntities db = new AppDTEntities())
            {
                return db.sp_Get_Commission(barberid).Select(barber => new commission_Return()
                {
                    comision = barber.comision.ToString()
                }).ToList();
            };
        }
    }
}
