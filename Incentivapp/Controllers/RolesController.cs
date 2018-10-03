using Incentivapp.Models;
using Incentivapp.Repository;
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

        public RolesController()
        {
            _repo = new UnitOfWork(new Propietaria2Context());
        }
        // GET: Roles
        public ActionResult Index()
        {
            try
            {
                var model = _repo.RolRepository.GetAll();
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
                ViewBag.Msg = "Crear Nuevo Rol";
                ViewBag.Title = "Crear Rol";
                ViewBag.Btn = "Crear";
                ViewBag.Method = "Create";
                return View("CreateEdit");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        [HttpPost]
        public ActionResult Create(Role rol)
        {
            try
            {
                _repo.RolRepository.Add(rol);
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
                var model = _repo.RolRepository.GetSingle(x => x.idRol == id);
                ViewBag.Msg = $"Editar Rol {model.nombre}";
                ViewBag.Title = "Editar Rol";
                ViewBag.Btn = "Editar";
                ViewBag.Method = "Edit";
                return View("CreateEdit",model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        [HttpPost]
        public ActionResult Edit(Role rol)
        {
            try
            {

                _repo.RolRepository.Update(rol);
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
                _repo.RolRepository.Remove(_repo.RolRepository.GetSingle(x => x.idRol == id));
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