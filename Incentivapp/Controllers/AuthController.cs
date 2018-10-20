using Incentivapp.Models;
using Incentivapp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Incentivapp.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(Usuario user)
        {
            try
            {
                var result = default(ActionResult);
                if (UserUtil.HasValidCredentials(user))
                {
                    Session["User"] = UserUtil.GetUsuario(user);
                    result = RedirectToAction("Index", "Premios");
                }
                else
                {
                    ModelState.AddModelError("error", "Usuario no existente");
                    result = View("Index");
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }
    }
}