using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WallsWS.Models;

namespace WallsWS.Controllers
{
    public class HomeController : Controller
    {
        private AppDTEntities db = new AppDTEntities();
        public ActionResult Index()
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


        public ActionResult Login()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel model, string returnUrl)
        {

            var role_id = IsValidUser(model.UserName, model.Password);
            int comp = Convert.ToInt32(Session["sucu_id"].ToString());

            if (ModelState.IsValid)
            {
              
                FormsAuthentication.SetAuthCookie(model.UserName, true);

                return RedirectToAction(actionName: "Agenda", controllerName: "Agenda");
            }
            else
            {
                ViewData["Error"] = "Usuario o contraseña son incorrectos o la conexión no se pudo establecer.";
            }

            //if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))            

            return View(model);
        }
        protected string IsValidUser(string username, string password)
        {
            var role_id = "";

            try
            {

                //Creamos la conexion con la cadena especificada en el Context
                using (db)
                {
                    //Recuperamos los datos del SP
                    var user = db.USUARIOS.Where(u => u.user_user == username && u.user_password == password);
                    //Recorremos el resultado para validar la informacion
                    foreach (var result in user)
                    {
                        if (result.user_user != "")
                        {
                            Session["user_id"] = username;
                            Session["sucu_id"] = result.sucu_id;
                            role_id = result.user_id.ToString();
                        }
                    }
                    var fecha = DateTime.Today.Date.ToString("yyyy-MM-dd");
                    var barberlist = db.BARBEROS.ToList();
                    foreach (var bar in barberlist)
                    {
                        var getHorario = db.sp_SET_HORARIO(bar.barb_id, fecha);
                    }
                }

            }
            catch (Exception q)
            {
                role_id = null;

            }
            return role_id;
        }
    }
}
