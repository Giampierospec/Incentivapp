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
    public class MedicionController : Controller
    {
        private UnitOfWork _repo;
        private ActionResult result = default(ActionResult);
        public MedicionController()
        {
            _repo = new UnitOfWork(new Propietaria2Context());
        }
        // GET: Medicion
        public ActionResult Index()
        {
            result = default(ActionResult);
            try
            {
                var model = _repo.MedicionRepository.GetAll();
                result = View(model);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "La vista de Medicion no pudo ser mostrada";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
        public ActionResult Create()
        {
            var result = default(ActionResult);
            try
            {
                ViewBag.Premios = _repo.PremioRepository.Transform(x => new SelectListItem()
                {
                    Text = x.nombre,
                    Value = x.idPremio.ToString()
                });
                ViewBag.Rangos = _repo.RangoRepository.Transform(x => new SelectListItem()
                {
                    Text = $"{x.Inicio} - {x.Fin}",
                    Value = x.idRango.ToString()
                });
                ViewBag.Msg = "Crear Nueva Medicion";
                ViewBag.Title = "Crear Medicion";
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
                TempData["err"] = "La vista de Crear Medicion no pudo ser creada";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
        [HttpPost]
        public ActionResult Create(Medicion md)
        {
            result = default(ActionResult);
            try
            {
                ViewBag.Premios = _repo.PremioRepository.Transform(x => new SelectListItem()
                {
                    Text = x.nombre,
                    Value = x.idPremio.ToString()
                });
                ViewBag.Rangos = _repo.RangoRepository.Transform(x => new SelectListItem()
                {
                    Text = $"{x.Inicio} - {x.Fin}",
                    Value = x.idRango.ToString()
                });
                ViewBag.Msg = "Crear Nueva Medicion";
                ViewBag.Title = "Crear Medicion";
                ViewBag.Btn = "Crear";
                ViewBag.Method = "Create";
                if (ModelState.IsValid)
                {
                    md.createdBy = UserUtil.GetUsuario((Usuario)Session["User"]).idUsuario;
                    _repo.MedicionRepository.Add(md);
                    _repo.Save();
                    result = RedirectToAction("Index");
                }
                else
                    result = View("CreateEdit");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "no se pudo crear la nueva medicion";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
        public ActionResult Edit(int? id)
        {
            result = default(ActionResult);
            try
            {
                
                ViewBag.Premios = _repo.PremioRepository.Transform(x => new SelectListItem()
                {
                    Text = x.nombre,
                    Value = x.idPremio.ToString()
                });
                ViewBag.Rangos = _repo.RangoRepository.Transform(x => new SelectListItem()
                {
                    Text = $"{x.Inicio} - {x.Fin}",
                    Value = x.idRango.ToString()
                });
                var model = _repo.MedicionRepository.GetSingle(x => x.idMedicion == id);
                ViewBag.Msg = $"Editar Medicion no {model.idMedicion}";
                ViewBag.Title = "Editar Medicion";
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
                TempData["err"] = "no se pudo visualizar la vista de editar medicion";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
        [HttpPost]
        public ActionResult Edit(Medicion md)
        {
            result = default(ActionResult);
            try
            {
                ViewBag.Premios = _repo.PremioRepository.Transform(x => new SelectListItem()
                {
                    Text = x.nombre,
                    Value = x.idPremio.ToString()
                });
                ViewBag.Rangos = _repo.RangoRepository.Transform(x => new SelectListItem()
                {
                    Text = $"{x.Inicio} - {x.Fin}",
                    Value = x.idRango.ToString()
                });
                var model = _repo.MedicionRepository.GetSingle(x => x.idMedicion == md.idMedicion);
                ViewBag.Msg = $"Editar Medicion no {model.idMedicion}";
                ViewBag.Title = "Editar Medicion";
                ViewBag.Btn = "Editar";
                ViewBag.Method = "Edit";
                if (ModelState.IsValid)
                {
                    md.createdBy = UserUtil.GetUsuario((Usuario)Session["User"]).idUsuario;
                    _repo.MedicionRepository.Update(md);
                    _repo.Save();
                    result = RedirectToAction("Index");
                }
                else
                    result = View("CreateEdit");

            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "no se pudo editar la medición";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
        public ActionResult Delete(int? id)
        {
            result = default(ActionResult);
            try
            {
                _repo.MedicionRepository.Remove(_repo.MedicionRepository.GetSingle(x => x.idMedicion == id));
                _repo.Save();
                result = RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "no se pudo eliminar la medición";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
    }
}