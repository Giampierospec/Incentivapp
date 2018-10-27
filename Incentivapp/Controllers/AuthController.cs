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
            var result = default(ActionResult);
            try
            {
                if (!UserUtil.IsLogged((Usuario)Session["User"]))
                    result = View();
                else
                    result = RedirectToAction("Index", "TipoPremios");
            }
            catch(Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo ingresar a la vista de login";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
        public ActionResult Login(Usuario user)
        {
            var result = default(ActionResult);
            try
            {

                if (UserUtil.HasValidCredentials(user))
                {
                    Session["User"] = UserUtil.GetUsuario(user);
                    result = RedirectToAction("Index", "TipoPremios");
                }
                else
                {
                    ModelState.AddModelError("error", "Usuario o contraseña incorrectos");
                    result = View("Index");
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo ingresar al usuario";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
        public ActionResult Logout()
        {
            var result = default(ActionResult);
            try
            {
                if (UserUtil.IsLogged((Usuario)Session["User"]))
                {
                    Session.Clear();
                    result = RedirectToAction("Index");
                }
                else
                {
                    TempData["err"] = "El usuario esta aun en sesión";
                    result = RedirectToAction("Error", "ErrorManagement");
                }
            }
            catch (Exception ex)
            {

                Logger.LogException(ex);
                TempData["err"] = "No se pudo desloggear al usuario";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
    }
}