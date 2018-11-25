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
    public class ExcelController : Controller
    {
        private ActionResult result;
        private UnitOfWork _uwork;

        public ExcelController()
        {
            _uwork = new UnitOfWork( new Propietaria2Context());
        }
        // GET: Excel
        public ActionResult Premios()
        {
            result = null;
            try
            {
                var model = _uwork.PremioRepository.Transform(x => new
                {
                    Valor = x.valor,
                    TipoPremio = x.TipoPremio.tipo,
                    Nombre = x.nombre
                });
                ExcelManagement.ListToExcel(model, Response);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo visualizar la vista";
                result = RedirectToAction("Error", "ErrorManagement");

            }
            return result;
        }
        public ActionResult TipoPremios()
        {
            result = null;
            try
            {
                var model = _uwork.TipoPremioRepository.Transform(x => new
                {
                    TipoPremio = x.tipo
                });
                ExcelManagement.ListToExcel(model, Response);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo visualizar la vista";
                result = RedirectToAction("Error", "ErrorManagement");

            }
            return result;
        }
        public ActionResult Medicion()
        {
            result = null;
            try
            {
                var model = _uwork.MedicionRepository.Transform(x => new
                {
                    NombreUsuario = $"{x.Usuario.nombre} {x.Usuario.apellido}",
                    ValorPremio = x.Premio.valor,
                    NombrePremio = x.Premio.nombre,
                    RangoInicio = x.Rango1.Inicio,
                    RangoFin = x.Rango1.Fin,
                    UsuarioModificador = $"{x.Usuario1.nombre} {x.Usuario1.apellido}"
                });
                ExcelManagement.ListToExcel(model, Response);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo visualizar la vista";
                result = RedirectToAction("Error", "ErrorManagement");

            }
            return result;
        }
        public ActionResult Usuarios()
        {
            result = null;
            try
            {
                var model = _uwork.UsuarioRepository.Transform(x => new
                {
                    NombreUsuario = $"{x.nombre} {x.apellido}",
                    Email = x.email,
                    Rol = x.Role.nombre
                });
                ExcelManagement.ListToExcel(model, Response);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                TempData["err"] = "No se pudo visualizar la vista";
                result = RedirectToAction("Error", "ErrorManagement");

            }
            return result;
        }
    }
}