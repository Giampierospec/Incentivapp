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
    public class RolesController : Controller
    {
        private UnitOfWork _repo;
        private ActionResult result = default(ActionResult);
        public RolesController()
        {
            _repo = new UnitOfWork(new Propietaria2Context());
        }
        // GET: Roles
        public ActionResult Index()
        {

            try
            {
                result = default(ActionResult);
                var usr = (Usuario)Session["User"];
                if (UserUtil.IsLogged(usr))
                {
                    if (UserUtil.IsInRole("Admin", UserUtil.GetUsuario(usr).idUsuario))
                    {
                        var model = _repo.RolRepository.GetAll();
                        result = View(model);
                    }
                    else
                    {
                        TempData["permit"] = "Administrar los Roles";
                        result = RedirectToAction("Permit", "ErrorManagement");
                    }
                }
                else
                    result = RedirectToAction("Index", "Auth");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo mostrar la pagina de roles";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
        public ActionResult Create()
        {
            result = default(ActionResult);
            try
            {
                var usr = (Usuario)Session["User"];
                ViewBag.Msg = "Crear Nuevo Rol";
                ViewBag.Title = "Crear Rol";
                ViewBag.Btn = "Crear";
                ViewBag.Method = "Create";
                if (UserUtil.IsLogged(usr))
                {
                    if (UserUtil.IsInRole("Admin", UserUtil.GetUsuario(usr).idUsuario))
                        result = View("CreateEdit");
                    else
                    {
                        TempData["permit"] = "Administrar los Roles";
                        result = RedirectToAction("Permit", "ErrorManagement");

                    }
                }
                else
                    result = RedirectToAction("Index", "Auth");
                 
              
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo mostrar la pagina de creacion de roles";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
        [HttpPost]
        public ActionResult Create(Role rol)
        {
            result = default(ActionResult);
            try
            {
                ViewBag.Msg = "Crear Nuevo Rol";
                ViewBag.Title = "Crear Rol";
                ViewBag.Btn = "Crear";
                ViewBag.Method = "Create";

                if (!_repo.RolRepository.Exists(x => x.nombre.Trim().ToLower() == rol.nombre.ToLower().Trim()))
                {
                    if (ModelState.IsValid)
                    {
                        _repo.RolRepository.Add(rol);
                        _repo.Save();
                        result = RedirectToAction("Index");
                    }
                    else
                        result = View("CreateEdit");
                }
              else
                {
                    ModelState.AddModelError("error", "El rol ya existe");
                    result = View("CreateEdit");
                }

            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo crear el rol";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
        public ActionResult Edit(int? id)
        {
            result = default(ActionResult);
            try
            {
                var usr = (Usuario)Session["User"];
                if (UserUtil.IsLogged(usr))
                {
                    if (id == null)
                        result = RedirectToAction("Index");
                    else if (UserUtil.IsInRole("Admin", UserUtil.GetUsuario(usr).idUsuario))
                    {
                        var model = _repo.RolRepository.GetSingle(x => x.idRol == id);
                        ViewBag.Msg = $"Editar Rol {model.nombre}";
                        ViewBag.Title = "Editar Rol";
                        ViewBag.Btn = "Editar";
                        ViewBag.Method = "Edit";
                        result = View("CreateEdit", model);
                    }
                    else
                    {
                        TempData["permit"] = "Administrar los Roles";
                        result = RedirectToAction("Permit", "ErrorManagement");
                    }
                }
                else
                    result = RedirectToAction("Index", "Auth");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo mostrar la vista de editar rol";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
        [HttpPost]
        public ActionResult Edit(Role rol)
        {
            try
            {
                result = default(ActionResult);
                
               

                if (ModelState.IsValid)
                {
                    _repo.RolRepository.Update(rol);
                    _repo.Save();
                    result = RedirectToAction("Index");
                }
                else
                {
                    result = Edit(rol.idRol);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo editar el rol";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                result = default(ActionResult);
                _repo.RolRepository.Remove(_repo.RolRepository.GetSingle(x => x.idRol == id));
                _repo.Save();
                result = RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo eliminar el rol";
                result = RedirectToAction("Error", "ErrorManagement");
            }
            return result;
        }
    }
}