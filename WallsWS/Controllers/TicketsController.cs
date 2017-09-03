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
            //inserta ticket y venta
            var producto = db.SERVICIOS.Where(q => q.serv_id == servid).ToList();
            var prod = producto[0].serv_id;
            double price = Convert.ToDouble(producto[0].serv_price);

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

            var CrearVenta = new VENTASTICKET //Make sure you have a table called test in DB
            {
                Ticket_Id = idTicket,
                Prod_Id = prod,
                Venta_Cantidad = 1,
                Prod_Price = 1,
                Venta_Importe = 1 * price,
                venta_discount = 0,
                disc_desc=""
            };

            context.VENTASTICKET.Add(CrearVenta);
            context.SaveChanges();

            var totalbarbero = Convert.ToDouble(db.sp_Barberos_Pagos(barbid));

            //var CrearPago = new BARBERO_PAGO //Make sure you have a table called test in DB
            //{
            //    sucu_Id = 1,
            //    barb_Id = barbid,
            //    pago_total = totalbarbero,
            //    pago_procent = 0,
            //    pago_date = DateTime.Now.Date,
            //};

            //context.VENTASTICKET.Add(CrearVenta);
            //context.SaveChanges();

            var idTicketv = db.VENTASTICKET.Max(t => t.Venta_Id);

            if (idTicketv == 0)
            {

                //  Send "false"
                return Json(new { success = false, responseText = "Error en la solicitud." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                try
                {
                    var stud = (from s in context.AGENDA
                                where s.agen_id == agenid
                                select s).FirstOrDefault();

                    stud.agen_status = 1;

                    int num = context.SaveChanges();

                    double total = Convert.ToDouble(db.VENTASTICKET.Where(q => q.Ticket_Id == idTicket).Sum(q => q.Venta_Importe));

                    var stud2 = (from s in context.TICKETS
                                 where s.Ticket_Id == idTicket
                                 select s).FirstOrDefault();

                    stud2.Ticket_Subtotal = total;

                    int num2 = context.SaveChanges();
                   // GetTotal();
                }
                catch (Exception e) { }
               
            }
            return View();
        }

        public ActionResult TICKETS_Read([DataSourceRequest]DataSourceRequest request)
        {

            var tiket = db.TICKETS.Max(a => a.Ticket_Id);

            IQueryable<VENTASTICKET> tickets = db.VENTASTICKET.Where(q=>q.Ticket_Id== tiket);
            DataSourceResult result = tickets.ToDataSourceResult(request, tICKETS => new
            {
                Venta_Id = tICKETS.Venta_Id,
                Ticket_Id = tICKETS.Ticket_Id,
                Prod_Id = tICKETS.Prod_Id,
                Venta_Cantidad = tICKETS.Venta_Cantidad,
                Prod_Price = tICKETS.Prod_Price,
                Venta_Importe = tICKETS.Venta_Importe,
                venta_discount = tICKETS.venta_discount,
                disc_desc = tICKETS.disc_desc,

            });

            return Json(result);


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
            var total = db.TICKETS.Sum(q => q.Ticket_Subtotal);
            ViewData["Total"] = total;
            return PartialView("_Total");

        }
    }
}
