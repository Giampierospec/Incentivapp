using Incentivapp.Models;
using Incentivapp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Incentivapp.Controllers
{
    public class TipoPremioController : ApiController
    {
        private UnitOfWork _repo;

        public TipoPremioController()
        {
            _repo = new UnitOfWork(new Propietaria2Context());
        }
        [HttpGet]
        [Route("/tipopremio")]
        IHttpActionResult GetTipoPremios()
        {
            try
            {
                return Ok(_repo.TipoPremioRepository.GetAll());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        [HttpPost]
        [Route("/tipopremio")]
        IHttpActionResult PostTipoPremios(TipoPremio tp)
        {
            try
            {
                _repo.TipoPremioRepository.Add(tp);
                _repo.Save();
                return Created("/tipopremio",tp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
