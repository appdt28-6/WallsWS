﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using WallsWS.Models;

namespace WallsWS.Controllers
{
    public class HorariosController : Controller
    {
        private AppDTEntities db = new AppDTEntities();

        public ActionResult Horarios()
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

        public ActionResult HORARIO_Read([DataSourceRequest]DataSourceRequest request)
        {
            int sucu = Convert.ToInt32(Session["sucu_id"].ToString());
            IQueryable<HORARIO> horario = db.HORARIO.Where(q=>q.sucu_id==sucu);
            DataSourceResult result = horario.ToDataSourceResult(request, hORARIO => new {
                sucu_id = hORARIO.sucu_id,
                hora_id = hORARIO.hora_id,
                hora_desc = hORARIO.hora_desc,
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult HORARIO_Create([DataSourceRequest]DataSourceRequest request, HORARIO hORARIO)
        {
            int sucu = Convert.ToInt32(Session["sucu_id"].ToString());
            if (ModelState.IsValid)
            {
                var entity = new HORARIO
                {
                    sucu_id =sucu,
                    hora_id = hORARIO.hora_id,
                    hora_desc = hORARIO.hora_desc,
                };

                db.HORARIO.Add(entity);
                db.SaveChanges();
                hORARIO.hora_id = entity.hora_id;
            }

            return Json(new[] { hORARIO }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult HORARIO_Update([DataSourceRequest]DataSourceRequest request, HORARIO hORARIO)
        {
            if (ModelState.IsValid)
            {
                var entity = new HORARIO
                {
                    sucu_id = hORARIO.sucu_id,
                    hora_id = hORARIO.hora_id,
                    hora_desc = hORARIO.hora_desc,
                };

                db.HORARIO.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { hORARIO }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult HORARIO_Destroy([DataSourceRequest]DataSourceRequest request, HORARIO hORARIO)
        {
            if (ModelState.IsValid)
            {
                var entity = new HORARIO
                {
                    sucu_id = hORARIO.sucu_id,
                    hora_id = hORARIO.hora_id,
                    hora_desc = hORARIO.hora_desc,
                };

                db.HORARIO.Attach(entity);
                db.HORARIO.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { hORARIO }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
    
        [HttpPost]
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
