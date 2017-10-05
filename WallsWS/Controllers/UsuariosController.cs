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
    public class UsuariosController : Controller
    {
        private AppDTEntities db = new AppDTEntities();

        public ActionResult Usuarios()
        {
            if (Session["user_id"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("~/Home/Login");
            }
        }

        public ActionResult USUARIOS_Read([DataSourceRequest]DataSourceRequest request)
        {
            int sucu = Convert.ToInt32(Session["sucu_id"].ToString());
            IQueryable<USUARIOS> usuarios = db.USUARIOS.Where(q=>q.sucu_id==sucu);
            DataSourceResult result = usuarios.ToDataSourceResult(request, uSUARIOS => new {
                user_id = uSUARIOS.user_id,
                sucu_id = uSUARIOS.sucu_id,
                user_name = uSUARIOS.user_name,
                user_phone = uSUARIOS.user_phone,
                user_mail = uSUARIOS.user_mail,
                user_user = uSUARIOS.user_user,
                user_password = uSUARIOS.user_password
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult USUARIOS_Create([DataSourceRequest]DataSourceRequest request, USUARIOS uSUARIOS)
        {
            int sucu = Convert.ToInt32(Session["sucu_id"].ToString());
            if (ModelState.IsValid)
            {
                var entity = new USUARIOS
                {
                    sucu_id = sucu,
                    user_name = uSUARIOS.user_name,
                    user_phone = uSUARIOS.user_phone,
                    user_mail = uSUARIOS.user_mail,
                    user_user = uSUARIOS.user_user,
                    user_password = uSUARIOS.user_password
                };

                db.USUARIOS.Add(entity);
                db.SaveChanges();
                uSUARIOS.user_id = entity.user_id;
            }

            return Json(new[] { uSUARIOS }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult USUARIOS_Update([DataSourceRequest]DataSourceRequest request, USUARIOS uSUARIOS)
        {
            if (ModelState.IsValid)
            {
                var entity = new USUARIOS
                {
                    user_id = uSUARIOS.user_id,
                    sucu_id = uSUARIOS.sucu_id,
                    user_name = uSUARIOS.user_name,
                    user_phone = uSUARIOS.user_phone,
                    user_mail = uSUARIOS.user_mail,
                    user_user = uSUARIOS.user_user,
                    user_password = uSUARIOS.user_password
                };

                db.USUARIOS.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { uSUARIOS }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult USUARIOS_Destroy([DataSourceRequest]DataSourceRequest request, USUARIOS uSUARIOS)
        {
            if (ModelState.IsValid)
            {
                var entity = new USUARIOS
                {
                    user_id = uSUARIOS.user_id,
                    sucu_id = uSUARIOS.sucu_id,
                    user_name = uSUARIOS.user_name,
                    user_phone = uSUARIOS.user_phone,
                    user_mail = uSUARIOS.user_mail,
                    user_user = uSUARIOS.user_user,
                    user_password = uSUARIOS.user_password
                };

                db.USUARIOS.Attach(entity);
                db.USUARIOS.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { uSUARIOS }.ToDataSourceResult(request, ModelState));
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
