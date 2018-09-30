using Incentivapp.Models;
using Incentivapp.Repository;
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

        public TipoPremiosController()
        {
            _repo = new UnitOfWork(new Propietaria2Context());
        }
        // GET: TipoPremios
        public ActionResult Index()
        {
            try
            {
                var model = _repo.TipoPremioRepository.GetAll();
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
                ViewBag.Msg = "Crear Nuevo Tipo de Premio";
                ViewBag.Title = "Crear Tipo Premio";
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
        public ActionResult Create(TipoPremio tp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repo.TipoPremioRepository.Add(tp);
                    _repo.Save();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Msg = "Crear Nuevo Tipo de Premio";
                    ViewBag.Title = "Crear Tipo Premio";
                    ViewBag.Btn = "Crear";
                    ViewBag.Method = "Create";
                    return View("CreateEdit");
                }
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
                {
                    return RedirectToAction("Index");
                }
                var model = _repo.TipoPremioRepository.GetSingle(x => x.idTipoPremio == id);
                ViewBag.Msg = $"Editar el tipo de premio {model.tipo}";
                ViewBag.Title = "Editar Tipo Premio";
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
        public ActionResult Edit(TipoPremio tp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repo.TipoPremioRepository.Update(tp);
                    _repo.Save();
                    return RedirectToAction("Index");
                }
                else
                {
                    var model = _repo.TipoPremioRepository.GetSingle(x => x.idTipoPremio == tp.idTipoPremio);
                    ViewBag.Msg = $"Editar el tipo de premio {model.tipo}";
                    ViewBag.Title = "Editar Tipo Premio";
                    ViewBag.Btn = "Editar";
                    ViewBag.Method = "Edit";
                    return View("CreateEdit", model);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            try
            {
                _repo.TipoPremioRepository.Remove(_repo.TipoPremioRepository.GetSingle(x => x.idTipoPremio == id));
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