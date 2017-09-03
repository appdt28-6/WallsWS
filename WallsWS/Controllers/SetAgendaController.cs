using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WallsWS.Models;

namespace WallsWS.Controllers
{
    public class SetAgendaController : ApiController
    {
        public List<Agenda_Return> Index(int barberid)
        {
            using (AppDTEntities db = new AppDTEntities())
            {
                return db.vis_Set_Agenda.Where(q=>q.barb_id==barberid).OrderByDescending(q=>q.agen_id).Select(barber => new Agenda_Return()

                {
                    

                agen_id = barber.agen_id,
                    barb_id = barber.barb_id,
                    serv_id = barber.serv_id,
                    serv_name = barber.serv_name,
                    agen_date = barber.agen_date,
                    agen_hora = barber.hrdi_hora,
                    cust_name = barber.cust_name,
                    cust_phone = barber.cust_phone,
                    cust_mail = barber.cust_mail,
                    agen_status = barber.agen_status
                }).ToList();
            };
        }
    }
}
