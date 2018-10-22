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
    public class UsuariosController : Controller
    {
        private ActionResult result = default(ActionResult);
        private UnitOfWork _repo;

        public UsuariosController()
        {
            _repo = new UnitOfWork(new Propietaria2Context());
        }
        // GET: Usuarios
        public ActionResult Index()
        {
            result = default(ActionResult);
            try
            {
                if (UserUtil.IsLogged((Usuario)Session["User"]))
                {
                    if (UserUtil.IsInRole("Admin", UserUtil.GetUsuario((Usuario)Session["user"]).idUsuario))
                    {
                        var model = _repo.UsuarioRepository.GetAll();
                        result = View(model);
                    }
                    else
                    {
                        TempData["permit"] = "No tiene permisos para ver usuarios";
                        result = RedirectToAction("Permit", "ErrorManagement");
                    }
                }
                else
                    result = RedirectToAction("Index", "Auth");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo visualizar la vista de usuarios";
                result = RedirectToAction("Error", "ErrorManagement");

            }
            return result;
        }

        public ActionResult Create()
        {
            try
            {
                ViewBag.Msg = "Crear Nuevo Usuario";
                ViewBag.Title = "Crear Usuario";
                ViewBag.Btn = "Crear";
                ViewBag.Method = "Create";
                ViewBag.idRol = _repo.RolRepository.Transform(x => new SelectListItem()
                {
                    Text = x.nombre,
                    Value = x.idRol.ToString()
                });
                return View("CreateEdit");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        [HttpPost]
        public ActionResult Create(Usuario user)
        {
            try
            {
                _repo.UsuarioRepository.Add(user);
                _repo.Save();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                    return RedirectToAction("Index");
                var model = _repo.UsuarioRepository.GetSingle(x => x.idUsuario == id);
                ViewBag.Msg = $"Editar Usuario {model.nombre}";
                ViewBag.Title = "Editar Usuario";
                ViewBag.Btn = "Editar";
                ViewBag.Method = "Edit";
                ViewBag.idRol = _repo.RolRepository.Transform(x => new SelectListItem()
                {
                    Text = x.nombre,
                    Value = x.idRol.ToString()
                });
                return View("CreateEdit", model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        [HttpPost]
        public ActionResult Edit(Usuario user)
        {
            try
            {
                _repo.UsuarioRepository.Update(user);
                _repo.Save();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public ActionResult Delete(int? id)
        {
            try
            {
                _repo.UsuarioRepository.Remove(_repo.UsuarioRepository.GetSingle(x => x.idUsuario == id));
                _repo.Save();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}