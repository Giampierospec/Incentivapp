using Incentivapp.Models;
using Incentivapp.Repository;
using Incentivapp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Incentivapp.Controllers
{
    public class RangoController : Controller
    {
        private UnitOfWork _repo;
        private ActionResult result = default(ActionResult);

        public RangoController()
        {
            _repo = new UnitOfWork(new Propietaria2Context());
        }
        // GET: Rango
        public ActionResult Index()
        {
            result = default(ActionResult);
            try
            {
                if (UserUtil.IsLogged((Usuario)Session["User"]))
                {
                    var model = _repo.RangoRepository.GetAll();
                    result = View(model);
                }
                else
                    result = RedirectToAction("Index", "Auth");
               
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo visualizar la vista de rangos";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
            
        }
        public ActionResult Create()
        {
            result = default(ActionResult);
            try
            {
                ViewBag.Msg = "Crear Nuevo Rango";
                ViewBag.Title = "Crear Rango";
                ViewBag.Btn = "Crear";
                ViewBag.Method = "Create";
                if (UserUtil.IsLogged((Usuario)Session["User"]))
                    result = View("CreateEdit");
                else
                    result = RedirectToAction("Index", "Auth");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo visualizar la vista de crear";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
        [HttpPost]
        public ActionResult Create(Rango rg)
        {
            result = default(ActionResult);
            try
            {
                ViewBag.Msg = "Crear Nuevo Rango";
                ViewBag.Title = "Crear Rango";
                ViewBag.Btn = "Crear";
                ViewBag.Method = "Create";
                if (!_repo.RangoRepository.Exists(x =>
                 (x.Inicio.Trim().ToLower() == rg.Inicio.Trim().ToLower())
                 ||
                 (x.Fin.Trim().ToLower() == rg.Fin.Trim().ToLower()))){
                    if (ModelState.IsValid)
                    {
                        _repo.RangoRepository.Add(_repo.RangoRepository.UpperCaseValues(rg));
                        _repo.Save();
                        result = RedirectToAction("Index");
                    }
                    else
                        result = View("CreateEdit");
                }
                else if (CheckRange.IsLarger(rg))
                {
                    ModelState.AddModelError("error", "El rango de fin es mayor al de Inicio");
                    result = View("CreateEdit");
                }
                else
                {
                    ModelState.AddModelError("error", "El rango ya existe");
                    result = View("CreateEdit");
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo visualizar la vista de crear";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
        public ActionResult Edit(int? id)
        {
            result = default(ActionResult);
            try
            {
                var model = _repo.RangoRepository.GetSingle(x => x.idRango == id);
                ViewBag.Msg = $"Editar rango {model.idRango}";
                ViewBag.Title = "Editar Rango";
                ViewBag.Btn = "Editar";
                ViewBag.Method = "Edit";
                if (id == null)
                    result = RedirectToAction("Index");
                else if (UserUtil.IsLogged((Usuario)Session["User"]))
                    result = View("CreateEdit", model);
                else
                    result = RedirectToAction("Index", "Auth");

            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo visualizar la vista de editar rango";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
        [HttpPost]
        public ActionResult Edit(Rango rg)
        {
            result = default(ActionResult);
            try
            {

                if (ModelState.IsValid)
                {
                    _repo.RangoRepository.Update(_repo.RangoRepository.UpperCaseValues(rg));
                    _repo.Save();
                    result = RedirectToAction("Index");
                }
                else
                    result = Edit(rg.idRango);

            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo visualizar la vista de editar rango";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
        public ActionResult Delete(int? id)
        {
            result = default(ActionResult);
            try
            {
                _repo.RangoRepository.Remove(_repo.RangoRepository.GetSingle(x => x.idRango == id));
                _repo.Save();
                result = RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo eliminar el rango";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
    }
}