using Incentivapp.Models;
using Incentivapp.Repository;
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

        public RangoController()
        {
            _repo = new UnitOfWork(new Propietaria2Context());
        }
        // GET: Rango
        public ActionResult Index()
        {
            try
            {
                var model = _repo.RangoRepository.GetAll();
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
                ViewBag.Msg = "Crear Nuevo Rango";
                ViewBag.Title = "Crear Rango";
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
        public ActionResult Create(Rango rg)
        {
            try
            {
                _repo.RangoRepository.Add(rg);
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
                var model = _repo.RangoRepository.GetSingle(x => x.idRango == id);
                ViewBag.Msg = $"Editar rango {model.idRango}";
                ViewBag.Title = "Editar Rango";
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
        public ActionResult Edit(Rango rg)
        {
            try
            {
                _repo.RangoRepository.Update(rg);
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
                _repo.RangoRepository.Remove(_repo.RangoRepository.GetSingle(x => x.idRango == id));
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