using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WallsWS.Models;

namespace WallsWS.Controllers
{
    public class SetBarberController : ApiController
    {
        public List<BARBEROS_Return> Index(int sucuid)
        {
            using (AppDTEntities db = new AppDTEntities())
            {
                return db.BARBEROS.Where(q=>q.sucu_id== sucuid).Select(barber => new BARBEROS_Return()
                {
                    barb_id = barber.barb_id,
                    barb_name = barber.barb_name,
                    barb_phone = barber.barb_phone,
                    barb_mail = barber.barb_mail,
                    barb_urlimage = barber.barb_urlimage

                }).ToList();
            };
        }
    }
}
