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
    public class BarberosController : Controller
    {
        private AppDTEntities db = new AppDTEntities();

        public ActionResult Barberos()
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

        public ActionResult BARBEROS_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<BARBEROS> barberos = db.BARBEROS.Where(q=>q.sucu_id==1);
            DataSourceResult result = barberos.ToDataSourceResult(request, bARBEROS => new {
                sucu_id = bARBEROS.sucu_id,
                barb_id = bARBEROS.barb_id,
                barb_name = bARBEROS.barb_name,
                barb_phone = bARBEROS.barb_phone,
                barb_mail = bARBEROS.barb_mail,
                barb_user = bARBEROS.barb_user,
                barb_password = bARBEROS.barb_password
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BARBEROS_Create([DataSourceRequest]DataSourceRequest request, BARBEROS bARBEROS)
        {
            if (ModelState.IsValid)
            {
                var entity = new BARBEROS
                {
                    sucu_id = 1,
                    barb_id = bARBEROS.barb_id,
                    barb_name = bARBEROS.barb_name,
                    barb_phone = bARBEROS.barb_phone,
                    barb_mail = bARBEROS.barb_mail,
                    barb_user = bARBEROS.barb_user,
                    barb_password = bARBEROS.barb_password
                };

                db.BARBEROS.Add(entity);
                db.SaveChanges();
                bARBEROS.barb_id = entity.barb_id;
            }

            return Json(new[] { bARBEROS }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BARBEROS_Update([DataSourceRequest]DataSourceRequest request, BARBEROS bARBEROS)
        {
            if (ModelState.IsValid)
            {
                var entity = new BARBEROS
                {
                    sucu_id = bARBEROS.sucu_id,
                    barb_id = bARBEROS.barb_id,
                    barb_name = bARBEROS.barb_name,
                    barb_phone = bARBEROS.barb_phone,
                    barb_mail = bARBEROS.barb_mail,
                    barb_user = bARBEROS.barb_user,
                    barb_password = bARBEROS.barb_password
                };

                db.BARBEROS.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { bARBEROS }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BARBEROS_Destroy([DataSourceRequest]DataSourceRequest request, BARBEROS bARBEROS)
        {
            if (ModelState.IsValid)
            {
                var entity = new BARBEROS
                {
                    sucu_id = bARBEROS.sucu_id,
                    barb_id = bARBEROS.barb_id,
                    barb_name = bARBEROS.barb_name,
                    barb_phone = bARBEROS.barb_phone,
                    barb_mail = bARBEROS.barb_mail,
                    barb_user = bARBEROS.barb_user,
                    barb_password = bARBEROS.barb_password
                };

                db.BARBEROS.Attach(entity);
                db.BARBEROS.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { bARBEROS }.ToDataSourceResult(request, ModelState));
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

        public ActionResult BARBEROS_Read_Pago([DataSourceRequest]DataSourceRequest request,int barb)
        {
            IQueryable<sp_Barberos_Pagos_Result> barberos = db.sp_Barberos_Pagos(barb).ToList().AsQueryable();
            DataSourceResult result = barberos.ToDataSourceResult(request, bARBEROS => new {
                total = bARBEROS.total
            });

            return Json(result);
        }

        public ActionResult BARBEROS_COMISION_Read([DataSourceRequest]DataSourceRequest request, int barb)
        {
            IQueryable<BARBERO_COMISION> barberosc = db.BARBERO_COMISION.Where(q => q.barb_Id == barb);
            DataSourceResult result = barberosc.ToDataSourceResult(request, bARBEROS => new {
                comi_Id = bARBEROS.comi_Id,
                sucu_Id = bARBEROS.sucu_Id,
                barb_Id = bARBEROS.barb_Id,
                serv_id = bARBEROS.serv_id,
                como_procent = bARBEROS.como_procent,
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BARBEROS_COMISION_Update([DataSourceRequest]DataSourceRequest request, BARBERO_COMISION bARBERO_COMISION, int barb)
        {
            if (ModelState.IsValid)
            {
                var entity = new BARBERO_COMISION
                {
                    comi_Id = bARBERO_COMISION.comi_Id,
                    sucu_Id = bARBERO_COMISION.sucu_Id,
                    barb_Id = bARBERO_COMISION.barb_Id,
                    serv_id = bARBERO_COMISION.serv_id,
                    como_procent = bARBERO_COMISION.como_procent,
                };

                db.BARBERO_COMISION.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { bARBERO_COMISION }.ToDataSourceResult(request, ModelState));
        }

    }
}
