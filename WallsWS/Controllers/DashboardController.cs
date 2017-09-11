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
               
                GetTotal();

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