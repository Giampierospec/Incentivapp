using Incentivapp.Models;
using Incentivapp.Repository;
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
            try
            {
                var model = _repo.PremioRepository.GetAll();
                return View(model);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public ActionResult Create()
        {
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
                return View("CreateEdit");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        [HttpPost]
        public ActionResult Create(Premio pr)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repo.PremioRepository.Add(pr);
                    _repo.Save();
                    return RedirectToAction("Index");
                }
                else
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
                return View("CreateEdit", model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        [HttpPost]
        public ActionResult Edit(Premio pr)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repo.PremioRepository.Update(pr);
                    _repo.Save();
                    return RedirectToAction("Index");
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
                    return View("CreateEdit", model);
                }
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
                _repo.PremioRepository.Remove(_repo.PremioRepository.GetSingle(x => x.idPremio == id));
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