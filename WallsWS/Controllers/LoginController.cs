using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WallsWS.Models;

namespace WallsWS.Controllers
{
    public class LoginController : ApiController
    {
        public List<Login_Return> Index(string user, string password)
        {
            using (AppDTEntities db = new AppDTEntities())
            {
                var fecha = DateTime.Today.Date.ToString("yyyy-MM-dd");
                var id = db.BARBEROS.Where(q => q.barb_user == user && q.barb_password == password).ToList();
                if (id.Count() == 0) {
                    return db.BARBEROS.Where(q => q.barb_user == user && q.barb_password == password).Select(barber => new Login_Return()
                    {
                        barb_id = 0,
                        barb_name = null,
                        barb_phone = null,
                        barb_mail = null

                    }).ToList();
                }
                else
                {
                    if (id[0].barb_id != 0)
                    {
                        int barb = id[0].barb_id;
                        var si = db.HORARIO_DISPONIBLE.Where(q => q.barb_id == barb && q.hrdi_date == fecha).Count();
                        if (si == 0)
                        {
                            var inicio = db.sp_SET_HORARIO(Convert.ToInt32(id[0].barb_id));
                            return db.BARBEROS.Where(q => q.barb_user == user && q.barb_password == password).Select(barber => new Login_Return()
                            {
                                barb_id = barber.barb_id,
                                barb_name = barber.barb_name,
                                barb_phone = barber.barb_phone,
                                barb_mail = barber.barb_mail

                            }).ToList();
                        }
                        else
                        {
                            return db.BARBEROS.Where(q => q.barb_user == user && q.barb_password == password).Select(barber => new Login_Return()
                            {
                                barb_id = barber.barb_id,
                                barb_name = barber.barb_name,
                                barb_phone = barber.barb_phone,
                                barb_mail = barber.barb_mail

                            }).ToList();
                        }
                    }
                    else
                    {
                        return db.BARBEROS.Where(q => q.barb_user == user && q.barb_password == password).Select(barber => new Login_Return()
                        {
                            barb_id = 0,
                            barb_name = null,
                            barb_phone = null,
                            barb_mail = null

                        }).ToList();
                    }
                }
               
                

               
            };
        }
    }
}
