using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WallsWS.Models;

namespace WallsWS.Controllers
{
    public class SetHorarioBarberController : ApiController
    {
        public List<vis_AGENDA_BARBEROS_Result> Index(int barbid)
        {
            var fecha = DateTime.Today.Date.ToString("yyyy-MM-dd");
            using (AppDTEntities db = new AppDTEntities())
            {
                return db.vis_AGENDA_BARBEROS.Where(q => q.barb_id == barbid && q.agen_date == fecha).Select(barber => new vis_AGENDA_BARBEROS_Result()
                {
                    agen_id = barber.agen_id,
                    barb_id = barber.barb_id,
                    agen_date = barber.agen_date,
                    hrdi_hora = barber.hrdi_hora,
                    cust_name = barber.cust_name,
                    serv_id = barber.serv_id,
                    serv_name = barber.serv_name,
                    agen_status = barber.agen_status,
                }).ToList();
            };
        }
    }
}
