using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WallsWS.Models;

namespace WallsWS.Controllers
{
    public class SetHorarioController : ApiController
    {

        public List<HORARIO_Return> Index(int barbid)
        {
            var fecha = DateTime.Today.Date.ToString("yyyy-MM-dd");
            using (AppDTEntities db = new AppDTEntities())
            {
                return db.HORARIO_DISPONIBLE.Where(q => q.barb_id == barbid && q.hrdi_date == fecha && q.hrdi_status == 0).Select(barber => new HORARIO_Return()
                {
                    hrdi_id = barber.hrdi_id,
                    barb_id = barber.barb_id,
                    hrdi_hora = barber.hrdi_hora,
                    hrdi_date = barber.hrdi_date,
                    hrdi_status = barber.hrdi_status
                }).ToList();
            };
        }
    }
}
