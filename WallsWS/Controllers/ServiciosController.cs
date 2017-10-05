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

                return View();
            }
            else
            {
                return Redirect("~/Home/Login");
            }

            //return View();
        }

        public ActionResult SERVICIOS_Read([DataSourceRequest]DataSourceRequest request)
        {
            int sucu = Convert.ToInt32(Session["sucu_id"].ToString());
            IQueryable<SERVICIOS> servicios = db.SERVICIOS.Where(q=>q.sucu_id==sucu&&q.serv_product==0);
            DataSourceResult result = servicios.ToDataSourceResult(request, sERVICIOS => new {
                sucu_id = sERVICIOS.sucu_id,
                serv_id = sERVICIOS.serv_id,
                serv_sku = sERVICIOS.serv_sku,
                serv_name = sERVICIOS.serv_name,
                serv_desc = sERVICIOS.serv_desc,
                serv_price = sERVICIOS.serv_price,
                serv_comi = sERVICIOS.serv_comi
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SERVICIOS_Create([DataSourceRequest]DataSourceRequest request, SERVICIOS sERVICIOS)
        {
            int sucu = Convert.ToInt32(Session["sucu_id"].ToString());
            if (ModelState.IsValid)
            {
                var entity = new SERVICIOS
                {
                    serv_id = sERVICIOS.serv_id,
                    sucu_id = sucu,
                    serv_sku = sERVICIOS.serv_sku,
                    serv_name = sERVICIOS.serv_name,
                    serv_desc = sERVICIOS.serv_desc,
                    serv_price = sERVICIOS.serv_price,
                    serv_comi = sERVICIOS.serv_comi,
                    serv_product = 0,
                    serv_stock=0
                };

                db.SERVICIOS.Add(entity);
                db.SaveChanges();
                sERVICIOS.serv_id = entity.serv_id;
            }

            return Json(new[] { sERVICIOS }.ToDataSourceResult(request, ModelState));
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
                    serv_price = sERVICIOS.serv_price,
                    serv_product = 0,
                    serv_comi = sERVICIOS.serv_comi,
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
                    serv_price = sERVICIOS.serv_price,
                    serv_comi = sERVICIOS.serv_comi
                };

                db.SERVICIOS.Attach(entity);
                db.SERVICIOS.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { sERVICIOS }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult PRODUCTOS_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<SERVICIOS> servicios = db.SERVICIOS.Where(q => q.sucu_id == 1 && q.serv_product == 1);
            DataSourceResult result = servicios.ToDataSourceResult(request, sERVICIOS => new {
                sucu_id = sERVICIOS.sucu_id,
                serv_id = sERVICIOS.serv_id,
                serv_sku = sERVICIOS.serv_sku,
                serv_name = sERVICIOS.serv_name,
                serv_desc = sERVICIOS.serv_desc,
                serv_price = sERVICIOS.serv_price,
                serv_product = sERVICIOS.serv_product,
                serv_stock = sERVICIOS.serv_stock,
                serv_comi = sERVICIOS.serv_comi

            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PRODUCTOS_Create([DataSourceRequest]DataSourceRequest request, SERVICIOS sERVICIOS)
        {
            if (ModelState.IsValid)
            {
                var entity = new SERVICIOS
                {
                    sucu_id = 1,
                    serv_sku = sERVICIOS.serv_sku,
                    serv_name = sERVICIOS.serv_name,
                    serv_desc = sERVICIOS.serv_desc,
                    serv_price = sERVICIOS.serv_price,
                    serv_product = 1,
                    serv_stock = 1,
                    serv_comi = sERVICIOS.serv_comi
                };

                db.SERVICIOS.Add(entity);
                db.SaveChanges();
                sERVICIOS.serv_id = entity.serv_id;
            }

            return Json(new[] { sERVICIOS }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PRODUCTOS_Update([DataSourceRequest]DataSourceRequest request, SERVICIOS sERVICIOS)
        {
            if (ModelState.IsValid)
            {

                var stock =db.SERVICIOS.Where(q => q.serv_id == sERVICIOS.serv_id).ToList();

                int st =Convert.ToInt32(stock[0].serv_stock);

                try
                {
                   

                    var stud = (from s in db.SERVICIOS
                                where s.serv_id == sERVICIOS.serv_id && s.sucu_id== sERVICIOS.sucu_id
                                select s).FirstOrDefault();

                    stud.serv_name = sERVICIOS.serv_name;
                    stud.serv_desc = sERVICIOS.serv_desc;
                    stud.serv_price = sERVICIOS.serv_price;
                    stud.serv_stock = st + sERVICIOS.serv_stock;
                    stud.serv_comi = sERVICIOS.serv_comi;


                    db.SaveChanges();
                }
                catch (Exception e) { }
            }

            return Json(new[] { sERVICIOS }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PRODUCTOS_Destroy([DataSourceRequest]DataSourceRequest request, SERVICIOS sERVICIOS)
        {
            if (ModelState.IsValid)
            {
                var entity = new SERVICIOS
                {
                    sucu_id = sERVICIOS.sucu_id,
                    serv_id = sERVICIOS.serv_id,
                    serv_sku = sERVICIOS.serv_sku,
                    serv_name = sERVICIOS.serv_name,
                    serv_desc = sERVICIOS.serv_desc,
                    serv_price = sERVICIOS.serv_price,
                    serv_stock = sERVICIOS.serv_stock,
                    serv_product = sERVICIOS.serv_product,
                    serv_comi = sERVICIOS.serv_comi
                };

                db.SERVICIOS.Attach(entity);
                db.SERVICIOS.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { sERVICIOS }.ToDataSourceResult(request, ModelState));
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
