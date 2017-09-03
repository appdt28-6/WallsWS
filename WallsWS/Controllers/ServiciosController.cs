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
    public class ServiciosController : Controller
    {
        private AppDTEntities db = new AppDTEntities();

        public ActionResult Servicios()
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

        public ActionResult SERVICIOS_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<SERVICIOS> servicios = db.SERVICIOS.Where(q=>q.sucu_id==1);
            DataSourceResult result = servicios.ToDataSourceResult(request, sERVICIOS => new {
                sucu_id = sERVICIOS.sucu_id,
                serv_id = sERVICIOS.serv_id,
                serv_sku = sERVICIOS.serv_sku,
                serv_name = sERVICIOS.serv_name,
                serv_desc = sERVICIOS.serv_desc,
                serv_price = sERVICIOS.serv_price
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SERVICIOS_Create([DataSourceRequest]DataSourceRequest request, SERVICIOS sERVICIOS)
        {
            if (ModelState.IsValid)
            {
                var entity = new SERVICIOS
                {
                    serv_id = sERVICIOS.serv_id,
                    sucu_id = 1,
                    serv_sku = sERVICIOS.serv_sku,
                    serv_name = sERVICIOS.serv_name,
                    serv_desc = sERVICIOS.serv_desc,
                    serv_price = sERVICIOS.serv_price
                };

                db.SERVICIOS.Add(entity);
                db.SaveChanges();
                sERVICIOS.serv_id = entity.serv_id;
            }

            return Json(new[] { sERVICIOS }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult PRODUCTOS_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<PRODUCTOS> servicios = db.PRODUCTOS.Where(q => q.sucu_id == 1);
            DataSourceResult result = servicios.ToDataSourceResult(request, sERVICIOS => new {
                sucu_id = sERVICIOS.sucu_id,
                prod_id = sERVICIOS.prod_id,
                prod_sku = sERVICIOS.prod_sku,
                prod_name = sERVICIOS.prod_name,
                prod_desc = sERVICIOS.prod_desc,
                prod_price = sERVICIOS.prod_price,
                prod_stock = sERVICIOS.prod_stock
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SERVICIOS_Update([DataSourceRequest]DataSourceRequest request, SERVICIOS sERVICIOS)
        {
            if (ModelState.IsValid)
            {
                var entity = new SERVICIOS
                {
                    serv_id = sERVICIOS.serv_id,
                    sucu_id = sERVICIOS.sucu_id,
                    serv_sku = sERVICIOS.serv_sku,
                    serv_name = sERVICIOS.serv_name,
                    serv_desc = sERVICIOS.serv_desc,
                    serv_price = sERVICIOS.serv_price
                };

                db.SERVICIOS.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { sERVICIOS }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SERVICIOS_Destroy([DataSourceRequest]DataSourceRequest request, SERVICIOS sERVICIOS)
        {
            if (ModelState.IsValid)
            {
                var entity = new SERVICIOS
                {
                    serv_id = sERVICIOS.serv_id,
                    sucu_id = sERVICIOS.sucu_id,
                    serv_sku = sERVICIOS.serv_sku,
                    serv_name = sERVICIOS.serv_name,
                    serv_desc = sERVICIOS.serv_desc,
                    serv_price = sERVICIOS.serv_price
                };

                db.SERVICIOS.Attach(entity);
                db.SERVICIOS.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { sERVICIOS }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PRODUCTOS_Create([DataSourceRequest]DataSourceRequest request, PRODUCTOS pRODUCTOS)
        {
            if (ModelState.IsValid)
            {
                var entity = new PRODUCTOS
                {
                    sucu_id = pRODUCTOS.sucu_id,
                    prod_id = pRODUCTOS.prod_id,
                    prod_sku = pRODUCTOS.prod_sku,
                    prod_name = pRODUCTOS.prod_name,
                    prod_desc = pRODUCTOS.prod_desc,
                    prod_price = pRODUCTOS.prod_price,
                    prod_stock = pRODUCTOS.prod_stock
                };

                db.PRODUCTOS.Add(entity);
                db.SaveChanges();
                pRODUCTOS.prod_id = entity.prod_id;
            }

            return Json(new[] { pRODUCTOS }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PRODUCTOS_Update([DataSourceRequest]DataSourceRequest request, PRODUCTOS pRODUCTOS)
        {
            if (ModelState.IsValid)
            {
                var entity = new PRODUCTOS
                {
                    sucu_id = pRODUCTOS.sucu_id,
                    prod_id = pRODUCTOS.prod_id,
                    prod_sku = pRODUCTOS.prod_sku,
                    prod_name = pRODUCTOS.prod_name,
                    prod_desc = pRODUCTOS.prod_desc,
                    prod_price = pRODUCTOS.prod_price,
                    prod_stock = pRODUCTOS.prod_stock
                };

                db.PRODUCTOS.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { pRODUCTOS }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PRODUCTOS_Destroy([DataSourceRequest]DataSourceRequest request, PRODUCTOS pRODUCTOS)
        {
            if (ModelState.IsValid)
            {
                var entity = new PRODUCTOS
                {
                    sucu_id = pRODUCTOS.sucu_id,
                    prod_id = pRODUCTOS.prod_id,
                    prod_sku = pRODUCTOS.prod_sku,
                    prod_name = pRODUCTOS.prod_name,
                    prod_desc = pRODUCTOS.prod_desc,
                    prod_price = pRODUCTOS.prod_price,
                    prod_stock = pRODUCTOS.prod_stock
                };

                db.PRODUCTOS.Attach(entity);
                db.PRODUCTOS.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { pRODUCTOS }.ToDataSourceResult(request, ModelState));
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
