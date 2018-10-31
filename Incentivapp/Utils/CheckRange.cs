using Incentivapp.Models;
using Incentivapp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Incentivapp.Utils
{
    public class CheckRange
    {
        private static UnitOfWork _db = new UnitOfWork(new Propietaria2Context());
        private static Regex _IsANumber = new Regex(@"^\d+$");
        /// <summary>
        /// Verifica que este en el rango
        /// y retorna una lista de premios asociados
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Premio SelectRandomPremio(int rangoId)
        {
            var premios = default(List<Premio>);
            var rango = _db.RangoRepository.GetSingle(x => x.idRango == rangoId);
            if(rango != null)
            {
                if (_IsANumber.IsMatch(rango.Inicio) && _IsANumber.IsMatch(rango.Fin))
                {
                    int inicio = Convert.ToInt32(rango.Inicio), fin = Convert.ToInt32(rango.Fin);
                    premios = _db.PremioRepository.GetAll().Where(x => _IsANumber.IsMatch(x.valor) && (Convert.ToInt32(x.valor) >= inicio && (Convert.ToInt32(x.valor) <= fin))).ToList();
                }
                else
                    premios = _db.PremioRepository.GetAll().Where(x => new Regex($@"[{rango.Inicio}-{rango.Fin}]").IsMatch(x.valor)).ToList();
            }

            var randIndex = indexToBring(premios);
            return premios.FirstOrDefault(x => x.idPremio == randIndex);


        }
        /// <summary>
        /// Traera el indice correspondiente
        /// </summary>
        /// <param name="pr"></param>
        /// <returns></returns>
        private static int indexToBring(List<Premio> pr)
        {
            var rand = new Random();
            return pr.OrderBy(x => rand.Next()).Take(1).FirstOrDefault().idPremio;
        }
        public static bool IsLarger(Rango rango)
        {
            var isLarger = default(bool);
            if (_IsANumber.IsMatch(rango.Inicio) && _IsANumber.IsMatch(rango.Fin))
                isLarger = Convert.ToInt32(rango.Fin) > Convert.ToInt32(rango.Inicio);
            else
                isLarger = Convert.ToChar(rango.Fin) > Convert.ToChar(rango.Inicio);

            return isLarger;
        }
        public static Usuario SelectRandomUser()
        {
            var randomNum = new Random().Next(_db.UsuarioRepository.GetAll().Min(x => x.idUsuario), _db.UsuarioRepository.GetAll().Max(x => x.idUsuario));
            return _db.UsuarioRepository.GetSingle(x => x.idUsuario == randomNum);
        }
    }
}