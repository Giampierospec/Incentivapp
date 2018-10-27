using Incentivapp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Incentivapp.Controllers.Api
{
    public class FillController : ApiController
    {
        private IHttpActionResult _result = default(IHttpActionResult);
        [HttpGet]
        public IHttpActionResult GetRandomPremio(int rangeId)
        {
            _result = default(IHttpActionResult);
            try
            {
                var randomPremio = CheckRange.SelectRandomPremio(rangeId);
                _result = Ok(new
                {
                    randomPremio.idPremio,
                    randomPremio.valor
                });
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                _result = BadRequest("No se pudo buscar un premio aleatorio");
            }
            return _result;
        }
    }
}
