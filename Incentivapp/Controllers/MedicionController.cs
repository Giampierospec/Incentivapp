using Incentivapp.Models;
using Incentivapp.Repository;
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

        public MedicionController()
        {
            _repo = new UnitOfWork(new Propietaria2Context());
        }
        // GET: Medicion
        public ActionResult Index()
        {
            try
            {
                var model = _repo.MedicionRepository.GetAll();
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
                return View("CreateEdit");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        [HttpPost]
        public ActionResult Create(Medicion md)
        {
            try
            {
                _repo.MedicionRepository.Add(md);
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
                return View("CreateEdit", model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        [HttpPost]
        public ActionResult Edit(Medicion md)
        {
            try
            {
                _repo.MedicionRepository.Update(md);
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
                _repo.MedicionRepository.Remove(_repo.MedicionRepository.GetSingle(x => x.idMedicion == id));
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