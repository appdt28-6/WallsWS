using System;
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
using System.Net.Mime;

namespace WallsWS.Controllers
{
    public class AgendaController : Controller
    {
        private AppDTEntities db = new AppDTEntities();

        public ActionResult Agenda()
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

        public ActionResult AGENDA_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<vis_AGENDA> agenda = db.vis_AGENDA.Where(q => q.agen_status == 0);
            DataSourceResult result = agenda.ToDataSourceResult(request, aGENDA => new {
                agen_id = aGENDA.agen_id,
                barb_id = aGENDA.barb_id,
                barb_name = aGENDA.barb_name,
                serv_id = aGENDA.serv_id,
                serv_name = aGENDA.serv_name,
                serv_price = aGENDA.serv_price,
                cust_name = aGENDA.cust_name,
                cust_phone = aGENDA.cust_phone,
                cust_mail = aGENDA.cust_mail,
                agen_date = aGENDA.agen_date,
                hrdi_hora = aGENDA.hrdi_hora,
                agen_status = aGENDA.agen_status
            });

            return Json(result);
        }

        public ActionResult AGENDA_Barbero_Read([DataSourceRequest]DataSourceRequest request,int barb)
        {

            try
            {
                IQueryable<vis_AGENDA> agenda = db.vis_AGENDA.Where(q => q.barb_id == barb && q.agen_status == 0);
                DataSourceResult result = agenda.ToDataSourceResult(request, aGENDA => new
                {
                    agen_id = aGENDA.agen_id,
                    barb_id = aGENDA.barb_id,
                    barb_name = aGENDA.barb_name,
                    serv_id = aGENDA.serv_id,
                    serv_name = aGENDA.serv_name,
                    serv_price = aGENDA.serv_price,
                    cust_name = aGENDA.cust_name,
                    cust_phone = aGENDA.cust_phone,
                    cust_mail = aGENDA.cust_mail,
                    agen_date = aGENDA.agen_date,
                    hrdi_hora = aGENDA.hrdi_hora,
                    agen_status = aGENDA.agen_status
                });

                return Json(result);
            }
            catch (Exception x)
            {
                return View();
            }
        }

        public ActionResult AGENDA_Sin_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<vis_AGENDA> agenda = db.vis_AGENDA.Where(q=>q.agen_status==1);
            DataSourceResult result = agenda.ToDataSourceResult(request, aGENDA => new {
                agen_id = aGENDA.agen_id,
                barb_id = aGENDA.barb_id,
                barb_name = aGENDA.barb_name,
                serv_id = aGENDA.serv_id,
                serv_name = aGENDA.serv_name,
                serv_price = aGENDA.serv_price,
                cust_name = aGENDA.cust_name,
                cust_phone = aGENDA.cust_phone,
                cust_mail = aGENDA.cust_mail,
                agen_date = aGENDA.agen_date,
                hrdi_hora = aGENDA.hrdi_hora,
                agen_status = aGENDA.agen_status
            });

            return Json(result);
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


        public ActionResult GetCobro(int agen_id,int serv_id,int barb_id)
        {
            var producto = db.SERVICIOS.Where(q => q.serv_id == serv_id).ToList();
            var prod=producto[0].serv_id;
            double price=Convert.ToDouble(producto[0].serv_price);

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
                barb_id= barb_id
            };

            context.TICKETS.Add(CrearTicket);
            context.SaveChanges();

            var idTicket = db.TICKETS.Max(t => t.Ticket_Id);

            var CrearVenta = new VENTASTICKET //Make sure you have a table called test in DB
            {
                Ticket_Id = idTicket,
                Prod_Id = prod,
                Venta_Cantidad =1,
                Prod_Price = 1,
                Venta_Importe =1*price
            };

            context.VENTASTICKET.Add(CrearVenta);
            context.SaveChanges();

            var totalbarbero =Convert.ToDouble(db.sp_Barberos_Pagos(barb_id));

            var CrearPago = new BARBERO_PAGO //Make sure you have a table called test in DB
            {
                sucu_Id = 1,
                barb_Id = barb_id,
                pago_total = totalbarbero,
                pago_procent=0,
                pago_date = DateTime.Now.Date,
            };

            context.VENTASTICKET.Add(CrearVenta);
            context.SaveChanges();

            var idTicketv = db.VENTASTICKET.Max(t => t.Venta_Id);

            if (idTicketv==0)
            {
               
                //  Send "false"
                return Json(new { success = false, responseText = "Error en la solicitud." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                try { 
                var stud = (from s in context.AGENDA
                            where s.agen_id == agen_id
                            select s).FirstOrDefault();

                stud.agen_status = 1;

                int num = context.SaveChanges();

                double total =Convert.ToDouble(db.VENTASTICKET.Where(q=>q.Ticket_Id==idTicket).Sum(q => q.Venta_Importe));

                var stud2 = (from s in context.TICKETS
                            where s.Ticket_Id == idTicket
                            select s).FirstOrDefault();

                stud2.Ticket_Subtotal = total;

                int num2 = context.SaveChanges();
                    GetTotal();
                }
                catch(Exception e) { }
                //  Send "Success"
                // return Json(new { success = true, responseText = "Venta registrada!" }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Tickets", "Ticket",new {agenid=agen_id,serid=serv_id, barid=barb_id });
            }
        }

        public ActionResult GetTotal()
        {
            var total = db.TICKETS.Sum(q => q.Ticket_Subtotal);
            ViewData["Total"] = total;
            return PartialView("_Total");

        }

        public ActionResult GetProductos()
        {
            //int comp = int.Parse(Session["comp_identifier"].ToString());
            var routes = db.SERVICIOS.Where(a => a.sucu_id == 1).ToList().Select(route => new SERVICIOS()
            {
                serv_id = route.serv_id,
                serv_name = route.serv_name
                //id_last = route.id_last
            });
            return Json(routes, JsonRequestBehavior.AllowGet);
        }


    }
}
