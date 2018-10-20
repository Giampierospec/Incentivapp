using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Incentivapp.Controllers
{
    public class ErrorManagementController : Controller
    {
        // GET: ErrorManagement
        public ActionResult Error()
        {
            ViewBag.err = TempData["err"];
            return View();
        }
        public ActionResult Permit()
        {
            ViewBag.permit = TempData["permit"];
            return View();

        }
    }
}