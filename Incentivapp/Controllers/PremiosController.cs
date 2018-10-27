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
    public class PremiosController : Controller
    {
        private UnitOfWork _repo;

        public PremiosController()
        {
            _repo = new UnitOfWork(new Propietaria2Context());
        }
        // GET: Premios
        public ActionResult Index()
        {
            var result = default(ActionResult);
            try
            {
               
                if (UserUtil.IsLogged((Usuario)Session["User"]))
                {
                    var model = _repo.PremioRepository.GetAll();
                    result = View(model);
                }
                else
                    result = RedirectToAction("Index", "Auth");
            }
            catch(Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo visualizar la vista";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
        public ActionResult Create()
        {
            var result = default(ActionResult);
            try
            {
                if (UserUtil.IsLogged((Usuario)Session["User"]))
                {
                    ViewBag.Msg = "Crear Nuevo Premio";
                    ViewBag.Title = "Crear Premio";
                    ViewBag.Btn = "Crear";
                    ViewBag.Method = "Create";
                    ViewBag.idTipoPremio = _repo.TipoPremioRepository.Transform(x => new SelectListItem()
                    {
                        Text = x.tipo,
                        Value = x.idTipoPremio.ToString()
                    });
                    result = View("CreateEdit");
                }
                else
                    result = RedirectToAction("Index", "Auth");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo visualizar la vista de crear premios";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
        [HttpPost]
        public ActionResult Create(Premio pr)
        {
            var result = default(ActionResult);
            try
            {
                ViewBag.Msg = "Crear Nuevo Premio";
                ViewBag.Title = "Crear Premio";
                ViewBag.Btn = "Crear";
                ViewBag.Method = "Create";
                ViewBag.idTipoPremio = _repo.TipoPremioRepository.Transform(x => new SelectListItem()
                {
                    Text = x.tipo,
                    Value = x.idTipoPremio.ToString()
                });
                if (!_repo.PremioRepository.Exists(x => x.nombre.Trim().ToLower() == pr.nombre.Trim().ToLower()))
                {
                    if (ModelState.IsValid)
                    {
                        pr.idUser = UserUtil.GetUsuario((Usuario)Session["User"]).idUsuario;
                        _repo.PremioRepository.Add(_repo.PremioRepository.UpperCaseValues(pr));
                        _repo.Save();
                        result = RedirectToAction("Index");
                    }
                    else
                        result = View("CreateEdit");
                }
                else
                {
                    ModelState.AddModelError("error", "El premio ya existe");
                    result = View("CreateEdit");
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo crear los premios";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
        public ActionResult Edit(int? id)
        {
            var result = default(ActionResult);
            try
            {

                if (UserUtil.IsLogged((Usuario)Session["User"]))
                {
                    var model = _repo.PremioRepository.GetSingle(x => x.idPremio == id);
                    ViewBag.Msg = $"Editar Premio: {model.nombre}";
                    ViewBag.Title = "Editar Premio";
                    ViewBag.Btn = "Editar";
                    ViewBag.Method = "Edit";
                    ViewBag.idTipoPremio = _repo.TipoPremioRepository.Transform(x => new SelectListItem()
                    {
                        Text = x.tipo,
                        Value = x.idTipoPremio.ToString(),
                    });
                    result = View("CreateEdit", model);
                }
                else
                    result = RedirectToAction("Index","Auth");
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
        public ActionResult Edit(Premio pr)
        {
            var result = default(ActionResult);
            try
            {
                if (ModelState.IsValid)
                {
                    pr.idUser = UserUtil.GetUsuario((Usuario)Session["User"]).idUsuario;
                    _repo.PremioRepository.Update(_repo.PremioRepository.UpperCaseValues(pr));
                    _repo.Save();
                    result = RedirectToAction("Index");
                }
                else
                {
                    var model = _repo.PremioRepository.GetSingle(x => x.idPremio == pr.idPremio);
                    ViewBag.Msg = $"Editar Premio: {model.nombre}";
                    ViewBag.Title = "Editar Premio";
                    ViewBag.Btn = "Editar";
                    ViewBag.Method = "Edit";
                    ViewBag.idTipoPremio = _repo.TipoPremioRepository.Transform(x => new SelectListItem()
                    {
                        Text = x.tipo,
                        Value = x.idTipoPremio.ToString()
                    });
                    result = View("CreateEdit", model);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo Editar el premio";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
        public ActionResult Delete(int? id)
        {
            var result = default(ActionResult);
            try
            {

                var usuario = _repo.PremioRepository.GetSingle(x => x.idPremio == id);
                usuario.idUser = UserUtil.GetUsuario((Usuario)Session["User"]).idUsuario;
                _repo.PremioRepository.Remove(usuario);
                _repo.Save();
                result = RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo Eliminar al usuario";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
    }
}