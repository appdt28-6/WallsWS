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
    public class WallsCardController : Controller
    {
        private AppDTEntities db = new AppDTEntities();
        // GET: WallsCard
        public ActionResult WallsCard()
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

        public ActionResult WallsCard_Read([DataSourceRequest]DataSourceRequest request)
        {
            int sucu = Convert.ToInt32(Session["sucu_id"].ToString());
            IQueryable<WALLSCARD> walls = db.WALLSCARD.Where(q => q.sucu_id == sucu);
            DataSourceResult result = walls.ToDataSourceResult(request, wALLSCARD => new {
                sucu_id = wALLSCARD.sucu_id,
                wall_id = wALLSCARD.wall_id,
                cust_name = wALLSCARD.cust_name,
                cust_phone = wALLSCARD.cust_phone,
                cust_mail = wALLSCARD.cust_mail,
                cust_birth = wALLSCARD.cust_birth,
                cust_face = wALLSCARD.cust_face
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult WallsCard_Create([DataSourceRequest]DataSourceRequest request, WALLSCARD wALLSCARD)
        {
            int sucu = Convert.ToInt32(Session["sucu_id"].ToString());
            if (ModelState.IsValid)
            {
                var entity = new WALLSCARD
                {
                    sucu_id = sucu,
                    cust_name = wALLSCARD.cust_name,
                    cust_phone = wALLSCARD.cust_phone,
                    cust_mail = wALLSCARD.cust_mail,
                    cust_birth = wALLSCARD.cust_birth,
                    cust_face = wALLSCARD.cust_face
                };

                db.WALLSCARD.Add(entity);
                db.SaveChanges();
                wALLSCARD.wall_id = entity.wall_id;
            }

            return Json(new[] { wALLSCARD }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult WallsCard_Update([DataSourceRequest]DataSourceRequest request, WALLSCARD wALLSCARD)
        {
            if (ModelState.IsValid)
            {
                var entity = new WALLSCARD
                {
                    sucu_id = wALLSCARD.sucu_id,
                    wall_id = wALLSCARD.wall_id,
                    cust_name = wALLSCARD.cust_name,
                    cust_phone = wALLSCARD.cust_phone,
                    cust_mail = wALLSCARD.cust_mail,
                    cust_birth = wALLSCARD.cust_birth,
                    cust_face = wALLSCARD.cust_face
                };

                db.WALLSCARD.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { wALLSCARD }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult WallsCard_Destroy([DataSourceRequest]DataSourceRequest request, WALLSCARD wALLSCARD)
        {
            if (ModelState.IsValid)
            {
                var entity = new WALLSCARD
                {
                    sucu_id = wALLSCARD.sucu_id,
                    wall_id = wALLSCARD.wall_id,
                    cust_name = wALLSCARD.cust_name,
                    cust_phone = wALLSCARD.cust_phone,
                    cust_mail = wALLSCARD.cust_mail,
                    cust_birth = wALLSCARD.cust_birth,
                    cust_face = wALLSCARD.cust_face
                };

                db.WALLSCARD.Attach(entity);
                db.WALLSCARD.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { wALLSCARD }.ToDataSourceResult(request, ModelState));
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