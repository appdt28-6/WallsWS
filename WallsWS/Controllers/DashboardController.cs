using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
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
                var totalgas = db.sp_Get_Gastos().ToList();

                if (totalefec[0].Value == 0) { ViewData["efectivo"] = "0"; } else { ViewData["efectivo"] = totalefec[0].Value; }
                if (totaltar[0].Value == 0) { ViewData["tarjeta"] = "0"; } else { ViewData["tarjeta"] = totaltar[0].Value; }
                if (totalgas[0].Value == 0) { ViewData["gastos"] = "0"; } else { ViewData["gastos"] = totalgas[0].Value; }


                ViewData["total"] = (totalefec[0].Value + totaltar[0].Value) - totalgas[0].Value;
                return View();
            }
            else
            {
                return Redirect("~/Home/Login");
            }

        }

        public ActionResult PRODUCTOS_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<vis_Get_Quanty_Product> tickets = db.vis_Get_Quanty_Product.Where(q => q.sucu_id == 1).ToList().AsQueryable();
            DataSourceResult result = tickets.ToDataSourceResult(request, tICKETS => new
            {
                sucu_id = tICKETS.sucu_id,
                Prod_Id = tICKETS.Prod_Id,
                serv_name = tICKETS.serv_name,
                productos = tICKETS.productos
            });

            return Json(result);
        }

        public ActionResult GetTotal()
        {
            var total = db.TICKETS.Sum(q => q.Ticket_Subtotal);
            ViewData["Total"] = total;
            return PartialView("_Total");

        }
    }
}