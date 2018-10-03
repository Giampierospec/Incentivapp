using Incentivapp.Models;
using Incentivapp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Incentivapp.Controllers
{
    public class UsuariosController : Controller
    {
        private UnitOfWork _repo;

        public UsuariosController()
        {
            _repo = new UnitOfWork(new Propietaria2Context());
        }
        // GET: Usuarios
        public ActionResult Index()
        {
            try
            {
                var model = _repo.UsuarioRepository.GetAll();
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
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
                return View("CreateEdit",model);
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