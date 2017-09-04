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
            return View();
        }

        public ActionResult TICKETS_Read([DataSourceRequest]DataSourceRequest request)
        {
            var ticket = db.TICKETS.Where(q => q.Sucu_Id == 1).Max(q => q.Ticket_Id);
            IQueryable<VENTASTICKET> tickets = db.VENTASTICKET.Where(q=>q.Ticket_Id== ticket);
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
        public ActionResult TICKETS_Create([DataSourceRequest]DataSourceRequest request, VENTASTICKET vENTASTICKET)
        {

            var CrearVenta = new VENTASTICKET //Make sure you have a table called test in DB
            {
                Ticket_Id = vENTASTICKET.Ticket_Id,
                Prod_Id = vENTASTICKET.Prod_Id,
                Venta_Cantidad = vENTASTICKET.Venta_Cantidad,
                Prod_Price = 1,
                Venta_Importe = 1 * (vENTASTICKET.Prod_Price)
            };

            db.VENTASTICKET.Add(CrearVenta);
            db.SaveChanges();

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
            var total = db.TICKETS.Sum(q => q.Ticket_Subtotal);
            ViewData["Total"] = total;
            return PartialView("_Total");

        }
    }
}
