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
    public class TicketsController : Controller
    {
        private AppDTEntities db = new AppDTEntities();

        public ActionResult Tickets(int agenid, int servid, int barbid)
        {

            if (Session["user_id"] != null)
            {
                Session["agenid"] = agenid;

                var context = new AppDTEntities();

                var CrearTicket = new TICKETS //Make sure you have a table called test in DB
                {
                    Ticket_Subtotal = 0,
                    Ticket_Factura = 0,
                    Ticket_Date = DateTime.Now,
                    Sucu_Id = 1,
                    Ticket_Status = "abierto",
                    Ticket_Pago = 0,
                    Ticket_Turno = 1,
                    barb_id = barbid
                };

                context.TICKETS.Add(CrearTicket);
                context.SaveChanges();

                var idTicket = db.TICKETS.Max(t => t.Ticket_Id);
                Session["ticket"] = idTicket;
                double pri = 0;
                double comi = 0;
                if (servid == 0) { }
                else { 
                var price = db.SERVICIOS.Where(a => a.serv_id == servid).ToList();
                pri = Convert.ToDouble(price[0].serv_price);
                comi = Convert.ToDouble(price[0].serv_comi);
               


                var CrearVenta = new VENTASTICKET //Make sure you have a table called test in DB
                {
                    Ticket_Id = idTicket,
                    Prod_Id = servid,
                    Venta_Cantidad = 1,
                    Prod_Price = pri,
                    Venta_Importe = 1 * pri,
                    serv_comi = comi,
                    venta_discount = 0,
                    disc_desc = ""
                };

                db.VENTASTICKET.Add(CrearVenta);
                db.SaveChanges();

                var total = db.VENTASTICKET.Where(q => q.Ticket_Id == idTicket).Sum(q => q.Venta_Importe);

                var stud = (from s in db.TICKETS
                            where s.Ticket_Id == idTicket
                            select s).FirstOrDefault();

                stud.Ticket_Subtotal = total;

                db.SaveChanges();
                    GetTotal();
                }

                Session["ticket"] = idTicket;
              
                return View();
            }
            else
            {
                return Redirect("~/Home/Login");
            }

        }

        public ActionResult TICKETS_Read([DataSourceRequest]DataSourceRequest request)
        {
            var ticket = db.TICKETS.Where(q => q.Sucu_Id == 1).Max(q => q.Ticket_Id);
            Session["ticket"] = ticket;
            IQueryable<vis_VENTASTICKET_PRODUCTOS> tickets = db.vis_VENTASTICKET_PRODUCTOS.Where(q=>q.Ticket_Id== ticket);
            DataSourceResult result = tickets.ToDataSourceResult(request, tICKETS => new
            {
                Venta_Id = tICKETS.Venta_Id,
                Ticket_Id = tICKETS.Ticket_Id,
                Prod_Id = tICKETS.Prod_Id,
                serv_name = tICKETS.serv_name,
                Venta_Cantidad = tICKETS.Venta_Cantidad,
                Prod_Price = tICKETS.Prod_Price,
                Venta_Importe = tICKETS.Venta_Importe,
                venta_discount = tICKETS.venta_discount,
                disc_desc = tICKETS.disc_desc,
            });

            return Json(result);
        }
        public ActionResult TICKETS_Create([DataSourceRequest]DataSourceRequest request, vis_VENTASTICKET_PRODUCTOS vENTASTICKET)
        {
            var ticket =Convert.ToInt32(Session["ticket"].ToString());
            var price = db.SERVICIOS.Where(a => a.serv_id == vENTASTICKET.Prod_Id).ToList() ;
            double pri = Convert.ToDouble(price[0].serv_price);
            double com = Convert.ToDouble(price[0].serv_comi);
            int p = Convert.ToInt32(price[0].serv_product);
            double di = Convert.ToDouble(vENTASTICKET.venta_discount);
          
            if (di == 0) { di = 0; } else { }

            var disc = ((di) / 100) * pri;
            double sub = ((vENTASTICKET.Venta_Cantidad) * (pri));

            int s = Convert.ToInt32(price[0].serv_stock);

            var CrearVenta = new VENTASTICKET //Make sure you have a table called test in DB
            {
              
                Ticket_Id = ticket,
                Prod_Id = vENTASTICKET.Prod_Id,
                Venta_Cantidad = vENTASTICKET.Venta_Cantidad,
                Prod_Price = pri,
                Venta_Importe =Convert.ToDouble(sub-disc),
                venta_discount =disc,
                disc_desc = (vENTASTICKET.disc_desc==null)?"": vENTASTICKET.disc_desc,
                serv_comi = com,
            };

            db.VENTASTICKET.Add(CrearVenta);
            db.SaveChanges();

            if (p == 1)
            {
                int stock = s - vENTASTICKET.Venta_Cantidad;

                var sto = (from st in db.SERVICIOS
                            where st.serv_id == vENTASTICKET.Prod_Id
                            select st).FirstOrDefault();

                sto.serv_stock = stock;
                db.SaveChanges();

            }
            else { }

            var total = db.VENTASTICKET.Where(q=>q.Ticket_Id==ticket).Sum(q => q.Venta_Importe);

            var stud = (from v in db.TICKETS
                        where v.Ticket_Id == ticket
                        select v).FirstOrDefault();

            stud.Ticket_Subtotal = total;
            db.SaveChanges();

            return Json(new[] { vENTASTICKET }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult TICKETS_Update([DataSourceRequest]DataSourceRequest request, vis_VENTASTICKET_PRODUCTOS vENTASTICKET)
        {
            var price = db.SERVICIOS.Where(a => a.serv_id == vENTASTICKET.Prod_Id).ToList();
            double pri = Convert.ToDouble(price[0].serv_price);
            int p = Convert.ToInt32(price[0].serv_product);
            double di = Convert.ToDouble(vENTASTICKET.venta_discount);
            if (di == 0) { di = 0; } else { }

            var disc = ((di) / 100) * pri;
            double sub = ((vENTASTICKET.Venta_Cantidad) * (pri));

            var venta = (from s in db.VENTASTICKET
                        where s.Venta_Id == vENTASTICKET.Venta_Id && s.Ticket_Id== vENTASTICKET.Ticket_Id
                         select s).FirstOrDefault();

            venta.Venta_Cantidad = vENTASTICKET.Venta_Cantidad;
            venta.Venta_Importe = sub-disc;
            venta.venta_discount = disc;
            venta.disc_desc = vENTASTICKET.disc_desc;

            db.SaveChanges();

            if (p == 1)
            {
                int s = Convert.ToInt32(price[0].serv_stock);
                int stock = vENTASTICKET.Venta_Cantidad - s;

                var sto = (from st in db.SERVICIOS
                           where st.serv_id == vENTASTICKET.Prod_Id
                           select st).FirstOrDefault();

                sto.serv_stock = stock;


                db.SaveChanges();

            }
            else { }

            var total = db.VENTASTICKET.Where(q => q.Ticket_Id == vENTASTICKET.Ticket_Id).Sum(q => q.Venta_Importe);

            var stud = (from s in db.TICKETS
                        where s.Ticket_Id == vENTASTICKET.Ticket_Id
                        select s).FirstOrDefault();

            stud.Ticket_Subtotal = total;


            db.SaveChanges();
           

            return Json(new[] { vENTASTICKET }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult TICKETS_Delete([DataSourceRequest]DataSourceRequest request, vis_VENTASTICKET_PRODUCTOS vENTASTICKET)
        {
            if (ModelState.IsValid)
            {
                var deleteVenta = new VENTASTICKET //Make sure you have a table called test in DB
                {
                    Ticket_Id = vENTASTICKET.Ticket_Id,
                    Prod_Id = vENTASTICKET.Prod_Id,
                    Venta_Cantidad = vENTASTICKET.Venta_Cantidad,
                    Prod_Price = vENTASTICKET.Prod_Price,
                    Venta_Importe = vENTASTICKET.Venta_Importe,
                    venta_discount = vENTASTICKET.venta_discount,
                    disc_desc = vENTASTICKET.disc_desc
                };

                db.VENTASTICKET.Attach(deleteVenta);
                db.VENTASTICKET.Remove(deleteVenta);
                db.SaveChanges();

                var total = db.VENTASTICKET.Where(q => q.Ticket_Id == vENTASTICKET.Ticket_Id).Sum(q => q.Venta_Importe);

                var stud = (from s in db.TICKETS
                            where s.Ticket_Id == vENTASTICKET.Ticket_Id
                            select s).FirstOrDefault();

                stud.Ticket_Subtotal = total;


                db.SaveChanges();


            }
           

            return Json(new[] { vENTASTICKET }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult TICKETS_Ventas_Read([DataSourceRequest]DataSourceRequest request)
        {
            //IQueryable<VENTASTICKET> tickets = db.VENTASTICKET;
            //DataSourceResult result = tickets.ToDataSourceResult(request, tICKETS => new {
            //    Ticket_Id = tICKETS.Ticket_Id,
            //    Ticket_Date = tICKETS.Ticket_Date,
            //    Ticket_Subtotal = tICKETS.Ticket_Subtotal,
            //    Ticket_Factura = tICKETS.Ticket_Factura,
            //    Sucu_Id = tICKETS.Sucu_Id,
            //    Ticket_Status = tICKETS.Ticket_Status,
            //    Ticket_Pago = tICKETS.Ticket_Pago,
            //    Ticket_Turno = tICKETS.Ticket_Turno
            //});

            //return Json(result);
            return View();
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

        public ActionResult GetTotal()
        {
            var ticket =Convert.ToInt32(Session["ticket"].ToString());
            var total = db.VENTASTICKET.Where(q=>q.Ticket_Id == ticket).Sum(q => q.Venta_Importe);
            ViewData["Total"] = total;
            return PartialView("_Total");

        }

        public ActionResult TICKETS_Close(int Tp)
        {
            try { 
            var ticket = Convert.ToInt32(Session["ticket"].ToString());
            var agenid = Convert.ToInt32(Session["agenid"].ToString());
            var stud = (from s in db.TICKETS
                        where s.Ticket_Id == ticket
                        select s).FirstOrDefault();

            stud.Ticket_Pago = Tp;
            stud.Ticket_Status = "cerrado";
                ////
                var total = db.VENTASTICKET.Where(q => q.Ticket_Id == ticket).Sum(q => q.Venta_Importe);

                var tot = (from s in db.TICKETS
                            where s.Ticket_Id == ticket
                            select s).FirstOrDefault();

                tot.Ticket_Subtotal = total;
                ////
                if (agenid != 0) {
                var agen = (from s in db.AGENDA
                        where s.agen_id == agenid
                        select s).FirstOrDefault();
                agen.agen_status = 1;
                db.SaveChanges();
                }

                return Json(new { success = true, responseText = "Venta terminada con exito" }, JsonRequestBehavior.AllowGet);
               // return RedirectToAction("Agenda","Agenda");


            }
            catch (Exception e)
            {
                return Json(new { success = false, responseText = "Error" }, JsonRequestBehavior.AllowGet);
            }

            //return RedirectToAction("Agenda", "Agenda");
        }

    }
}
