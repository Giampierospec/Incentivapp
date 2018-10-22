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
    public class TipoPremiosController : Controller
    {
        private UnitOfWork _repo;
        private ActionResult result = default(ActionResult);
        public TipoPremiosController()
        {
            _repo = new UnitOfWork(new Propietaria2Context());
        }
        // GET: TipoPremios
        public ActionResult Index()
        {
            try
            {
                result = default(ActionResult);
                if (UserUtil.IsLogged((Usuario)Session["User"]))
                {
                    var model = _repo.TipoPremioRepository.GetAll();
                    result = View(model);
                }
                else
                    result = RedirectToAction("Index", "Auth");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo mostrar la vista de tipos de premio";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
        public ActionResult Create()
        {
            result = default(ActionResult);
            try
            {
                if (UserUtil.IsLogged((Usuario)Session["User"]))
                {
                    ViewBag.Msg = "Crear Nuevo Tipo de Premio";
                    ViewBag.Title = "Crear Tipo Premio";
                    ViewBag.Btn = "Crear";
                    ViewBag.Method = "Create";
                    result = View("CreateEdit");
                }
                else
                    result = RedirectToAction("Index", "Auth");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo mostrar la vista de crear tipos premio";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;

        }
        [HttpPost]
        public ActionResult Create(TipoPremio tp)
        {
            result = default(ActionResult);
            try
            {
                ViewBag.Msg = "Crear Nuevo Tipo de Premio";
                ViewBag.Title = "Crear Tipo Premio";
                ViewBag.Btn = "Crear";
                ViewBag.Method = "Create";
                if (!_repo.TipoPremioRepository.Exists(x => x.tipo.ToLower().Trim() == tp.tipo.Trim().ToLower()))
                {
                    if (ModelState.IsValid)
                    {
                        tp.idUser = UserUtil.GetUsuario((Usuario)Session["User"]).idUsuario;
                        _repo.TipoPremioRepository.Add(tp);
                        _repo.Save();
                        result = RedirectToAction("Index");
                    }
                    else
                        result = View("CreateEdit");
                }
                else
                {
                    ModelState.AddModelError("error", "El tipo de premio ya existe");
                    result = View("CreateEdit");
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo crear el premio";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;

        }
        public ActionResult Edit(int? id)
        {
            result = default(ActionResult);
            try
            {
                if (id == null)
                    result = RedirectToAction("Index");
                if (UserUtil.IsLogged((Usuario)Session["User"]))
                {
                    var model = _repo.TipoPremioRepository.GetSingle(x => x.idTipoPremio == id);
                    ViewBag.Msg = $"Editar el tipo de premio {model.tipo}";
                    ViewBag.Title = "Editar Tipo Premio";
                    ViewBag.Btn = "Editar";
                    ViewBag.Method = "Edit";
                    result = View("CreateEdit", model);
                }
                else
                    result = RedirectToAction("Index", "Auth");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo visualizar la vista de editar";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
        [HttpPost]
        public ActionResult Edit(TipoPremio tp)
        {
            try
            {
                result = default(ActionResult);
                if (ModelState.IsValid)
                {
                    tp.idUser = UserUtil.GetUsuario((Usuario)Session["User"]).idUsuario;
                    _repo.TipoPremioRepository.Update(tp);
                    _repo.Save();
                    result = RedirectToAction("Index");
                }
                else
                {
                    var model = _repo.TipoPremioRepository.GetSingle(x => x.idTipoPremio == tp.idTipoPremio);
                    ViewBag.Msg = $"Editar el tipo de premio {model.tipo}";
                    ViewBag.Title = "Editar Tipo Premio";
                    ViewBag.Btn = "Editar";
                    ViewBag.Method = "Edit";
                    result = View("CreateEdit", model);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo editar el tipo de premio";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            result = default(ActionResult);
            try
            {
                var tp = _repo.TipoPremioRepository.GetSingle(x => x.idTipoPremio == id);
                tp.idUser = UserUtil.GetUsuario((Usuario)Session["User"]).idUsuario;
                _repo.TipoPremioRepository.Remove(tp);
                _repo.Save();
                result = RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo eliminar el tipo de premio";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }

    }
}