using Hotel.LogicaNegocio.ExcepcionesEntidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaNegocio.Entidades.ValueObjects.TopesDescripcion
{
    [Owned]
    public class RangoTopesDescripcion: IEquatable<RangoTopesDescripcion>
    {

        public int Min { get; private set; }
        public int Max { get; private set; }

        #region Constructores
        protected RangoTopesDescripcion() { }

        public RangoTopesDescripcion(int min, int max)
        {
            ValidarDatos(min, max);
            Min = min;
            Max = max;
        }
        #endregion

        #region Validaciones
        private static void ValidarDatos(int min, int max)
        {
            if (min < 0)
                throw new TopeDescripcionException("El tope mínimo debe de ser un número positivo.");

            if (max < 0)
                throw new TopeDescripcionException("El tope máximo debe de ser un número positivo.");

            if (min > max)
                throw new TopeDescripcionException("El tope mínimo debe de ser menor al tope máximo.");
        }
        #endregion

        #region Igualdad del Value Object
        public bool Equals(RangoTopesDescripcion? other)
        {
            if (other == null)
                throw new TopeDescripcionException("Los rangos no se puede comparar con null");

            return Min.Equals(other.Min) && Max.Equals(other.Max);
        }

        public override bool Equals(object? obj)
        {
            var other = obj as RangoTopesDescripcion;
            if (other == null)
                throw new TopeDescripcionException("Los rangos no se puede comparar con null");

            return Min.Equals(other.Min) && Max.Equals(other.Max);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Min, Max);
        }
        #endregion

        #region Métodos de formateo.
        public override string ToString()
        {
            return $"Tope Mínimo: {this.Min} | Tope Máximo: {this.Max}";
        }
        #endregion
    }
}
