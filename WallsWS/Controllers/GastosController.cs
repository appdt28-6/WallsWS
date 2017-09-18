using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WallsWS.Models;

namespace WallsWS.Controllers
{
    public class GastosController : Controller
    {
        private AppDTEntities db = new AppDTEntities();
        // GET: Gastos
        public ActionResult Gastos()
        {
            if (Session["user_id"] != null)
            {
                //var total = db.TICKETS.Sum(q => q.Ticket_Subtotal);

                // ViewData["Total"] = total;

                return View();
            }
            else
            {
                return Redirect("~/Home/Login");
            }
        }

        public ActionResult GASTOS_Read([DataSourceRequest]DataSourceRequest request)
        {
            var fecha = DateTime.Today.Date.ToString("yyyy-MM-dd");
            IQueryable<GASTOS> barberos = db.GASTOS.Where(q => q.sucu_id == 1 && q.gast_date==fecha);
            DataSourceResult result = barberos.ToDataSourceResult(request, bARBEROS => new {
                sucu_id = bARBEROS.sucu_id,
                gast_id = bARBEROS.gast_id,
                gast_desc = bARBEROS.gast_desc,
                gast_amount = bARBEROS.gast_amount,
                gast_date = bARBEROS.gast_date,
            });

            return Json(result);
        }

        public ActionResult GASTOS_Create([DataSourceRequest]DataSourceRequest request, GASTOS gASTOS)
        {
            var fecha = DateTime.Today.Date.ToString("yyyy-MM-dd");
            var CrearGasto = new GASTOS //Make sure you have a table called test in DB
            {
                sucu_id = 1,
                gast_desc = gASTOS.gast_desc,
                gast_amount = gASTOS.gast_amount,
                gast_date = fecha,
            };

            db.GASTOS.Add(CrearGasto);
            db.SaveChanges();
            gASTOS.gast_id = CrearGasto.gast_id;

            return Json(new[] { gASTOS }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult GASTOS_Update([DataSourceRequest]DataSourceRequest request, GASTOS gASTOS)
        {
            var fecha = DateTime.Today.Date.ToString("yyyy-MM-dd");
            var CrearGasto = new GASTOS //Make sure you have a table called test in DB
            {
                gast_id = gASTOS.gast_id,
                sucu_id = gASTOS.sucu_id,
                gast_desc = gASTOS.gast_desc,
                gast_amount = gASTOS.gast_amount,
                gast_date = fecha,
            };

            db.GASTOS.Attach(CrearGasto);
            db.Entry(CrearGasto).State = EntityState.Modified;
            db.SaveChanges();

            return Json(new[] { gASTOS }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult GASTOS_Deleted([DataSourceRequest]DataSourceRequest request, GASTOS gASTOS)
        {
            var fecha = DateTime.Today.Date.ToString("yyyy-MM-dd");
            var CrearGasto = new GASTOS //Make sure you have a table called test in DB
            {
                gast_id = gASTOS.gast_id,
                sucu_id = gASTOS.sucu_id,
                gast_desc = gASTOS.gast_desc,
                gast_amount = gASTOS.gast_amount,
                gast_date = fecha,
            };

            db.GASTOS.Attach(CrearGasto);
            db.GASTOS.Remove(CrearGasto);
            db.SaveChanges();

            return Json(new[] { gASTOS }.ToDataSourceResult(request, ModelState));
        }
    }
}