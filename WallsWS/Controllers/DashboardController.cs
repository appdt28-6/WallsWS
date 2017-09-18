using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WallsWS.Models;

namespace WallsWS.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        private AppDTEntities db = new AppDTEntities();
        public ActionResult Dashboard()
        {
            
            if (Session["user_id"] != null)
            {
                var fecha = DateTime.Today.Date.ToString("yyyy-MM-dd") + " 00:00:00";
                var totalefec = db.sp_Get_Ventas(1).ToList();
                var totaltar = db.sp_Get_Ventas(2).ToList();

                if (totalefec[0].Value == 0) { ViewData["efectivo"] = "0"; } else { ViewData["efectivo"] = totalefec[0].Value; }
                if (totaltar[0].Value == 0) { ViewData["tarjeta"] = "0"; } else { ViewData["tarjeta"] = totaltar[0].Value; }

                ViewData["gastos"] = db.GASTOS.Where(q => q.gast_date == fecha).Sum(b => b.gast_amount);
                return View();
            }
            else
            {
                return Redirect("~/Home/Login");
            }

        }

        public ActionResult GetTotal()
        {
            var total = db.TICKETS.Sum(q => q.Ticket_Subtotal);
            ViewData["Total"] = total;
            return PartialView("_Total");

        }
    }
}