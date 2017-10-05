using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WallsWS.Models;

namespace WallsWS.Controllers
{
    public class SetSucursalesController : ApiController
    {
        public List<Sucursal_Return> Index()
        {
            using (AppDTEntities db = new AppDTEntities())
            {
                return db.SUCURSAL.OrderByDescending(q => q.sucu_id).Select(barber => new Sucursal_Return()

                {
                    sucu_id = barber.sucu_id,
                    sucu_name = barber.sucu_name,
                    sucu_addres = barber.sucu_addres,
                    sucu_phone = barber.sucu_phone,
                    sucu_lat = barber.sucu_lat,
                    sucu_lon = barber.sucu_lon
                }).ToList();
            };
        }
    }
}
