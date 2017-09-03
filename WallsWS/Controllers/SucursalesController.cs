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
    public class SucursalesController : Controller
    {
        private AppDTEntities db = new AppDTEntities();

        public ActionResult Sucursales()
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

        public ActionResult SUCURSAL_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<SUCURSAL> sucursal = db.SUCURSAL;
            DataSourceResult result = sucursal.ToDataSourceResult(request, sUCURSAL => new {
                sucu_id = sUCURSAL.sucu_id,
                sucu_name = sUCURSAL.sucu_name,
                sucu_addres = sUCURSAL.sucu_addres,
                sucu_lat = sUCURSAL.sucu_lat,
                sucu_lon = sUCURSAL.sucu_lon,
                sucu_phone = sUCURSAL.sucu_phone
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SUCURSAL_Create([DataSourceRequest]DataSourceRequest request, SUCURSAL sUCURSAL)
        {
            if (ModelState.IsValid)
            {
                var entity = new SUCURSAL
                {
                    sucu_name = sUCURSAL.sucu_name,
                    sucu_addres = sUCURSAL.sucu_addres,
                    sucu_lat = sUCURSAL.sucu_lat,
                    sucu_lon = sUCURSAL.sucu_lon,
                    sucu_phone = sUCURSAL.sucu_phone
                };

                db.SUCURSAL.Add(entity);
                db.SaveChanges();
                sUCURSAL.sucu_id = entity.sucu_id;
            }

            return Json(new[] { sUCURSAL }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SUCURSAL_Update([DataSourceRequest]DataSourceRequest request, SUCURSAL sUCURSAL)
        {
            if (ModelState.IsValid)
            {
                var entity = new SUCURSAL
                {
                    sucu_id = sUCURSAL.sucu_id,
                    sucu_name = sUCURSAL.sucu_name,
                    sucu_addres = sUCURSAL.sucu_addres,
                    sucu_lat = sUCURSAL.sucu_lat,
                    sucu_lon = sUCURSAL.sucu_lon,
                    sucu_phone = sUCURSAL.sucu_phone
                };

                db.SUCURSAL.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { sUCURSAL }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SUCURSAL_Destroy([DataSourceRequest]DataSourceRequest request, SUCURSAL sUCURSAL)
        {
            if (ModelState.IsValid)
            {
                var entity = new SUCURSAL
                {
                    sucu_id = sUCURSAL.sucu_id,
                    sucu_name = sUCURSAL.sucu_name,
                    sucu_addres = sUCURSAL.sucu_addres,
                    sucu_lat = sUCURSAL.sucu_lat,
                    sucu_lon = sUCURSAL.sucu_lon,
                    sucu_phone = sUCURSAL.sucu_phone
                };

                db.SUCURSAL.Attach(entity);
                db.SUCURSAL.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { sUCURSAL }.ToDataSourceResult(request, ModelState));
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
