using Hotel.LogicaNegocio.ExcepcionesEntidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaNegocio.Entidades.ValueObjects.TipoCabanha
{
    [Owned]
    public class DescripcionTipoCabanha : IEquatable<DescripcionTipoCabanha>
    {
        public string Descripcion { get; private set; }

        #region Constructores
        public DescripcionTipoCabanha()
        {

        }

        public DescripcionTipoCabanha(string descripcion)
        {
            ValidarDatos(descripcion);
            Descripcion = descripcion.Trim();
        }
        #endregion

        /// <summary>
        /// Valida que la descipción no sea null
        /// </summary>
        /// <param name="descripcion"></param>
        /// <exception cref="TipoCabanhaException"></exception>
        #region Validaciones
        private static void ValidarDatos(string descripcion)
        {
            if (descripcion == null)
                throw new TipoCabanhaException("La descripción no puede estar en null.");
        }

        public bool Equals(DescripcionTipoCabanha? other)
        {
            if (other == null)
                throw new TipoCabanhaException("La descripción no se puede compara con null.");

            return Descripcion.Equals(other.Descripcion);
        }

        public override bool Equals(object? obj)
        {
            var other = obj as DescripcionTipoCabanha;
            if (other == null)
                throw new TipoCabanhaException("La descripción no se puede compara con null.");

            return Descripcion.Equals(other.Descripcion);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Descripcion);
        }
        #endregion

        #region Métodos de fomateo
        public override string ToString()
        {
            return Descripcion;
        }
        #endregion
    }
}
