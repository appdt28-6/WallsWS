using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using WallsWS.Models;

namespace WallsWS.Controllers
{
    public class GetAgendaController : ApiController
    {
        public HttpResponseMessage Index(int barbid, int servid,string cust_name, string cust_phone, string cust_mail, int agen_hora)
        {
            string message = "";
            using (AppDTEntities db = new AppDTEntities())
            {
                try
                {
                    var fecha = DateTime.Today.Date.ToString("yyyy-MM-dd");
                    var entity = new AGENDA
                    {
                        barb_id = barbid,
                        serv_id = servid,
                        sucu_id= 1,
                        cust_name = cust_name,
                        cust_phone = cust_phone,
                        cust_mail = cust_mail,
                        hrdi_id = agen_hora,
                        agen_date = fecha,
                        agen_status = 0
                    };

                    db.AGENDA.Add(entity);
                    db.SaveChanges();

                    ///update horario
                    var result2 = from r in db.HORARIO_DISPONIBLE where r.hrdi_id == agen_hora select r;
                    HORARIO_DISPONIBLE vISITA_ASSIGN = result2.First();
                    vISITA_ASSIGN.hrdi_status = 1;
                    db.SaveChanges();


                    message = "ok";
                }
                catch (Exception e)
                {
                    message = e.ToString();
                }

            };

            var resp = new HttpResponseMessage()
            {
                Content = new StringContent("[{\"Succes\":\"" + message + "\"}]")
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return resp;
        }
    }
}
