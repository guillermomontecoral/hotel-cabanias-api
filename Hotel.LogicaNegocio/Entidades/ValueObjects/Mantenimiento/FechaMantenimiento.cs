using Hotel.LogicaNegocio.ExcepcionesEntidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaNegocio.Entidades.ValueObjects.Mantenimiento
{
    [Owned]
    public class FechaMantenimiento :IEquatable<FechaMantenimiento>
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; private set; }

        #region Constructores
        public FechaMantenimiento()
        {

        }

        public FechaMantenimiento(DateTime fecha)
        {
            ValidarDatos(fecha);
            Fecha = fecha;
        }
        #endregion

        /// <summary>
        /// Valida que la desciçión no sea null
        /// </summary>
        /// <param name="descripcion"></param>
        /// <exception cref="CabanhaException"></exception>
        #region Validaciones
        private static void ValidarDatos(DateTime fecha)
        {
            if (fecha == null)
                throw new MantenimientoException("Fecha no puede estar vacía.");

            if (fecha > DateTime.Today)
                throw new MantenimientoException($"La fecha ingresada no puede ser mayor a la del día actual. Hoy es: {DateTime.Today.ToString("dd/MM/yyyy")}");

        }

        public bool Equals(FechaMantenimiento? other)
        {
            if (other == null)
                throw new MantenimientoException("La descripción no se puede compara con null.");

            return Fecha.Equals(other.Fecha);
        }

        public override bool Equals(object? obj)
        {
            var other = obj as FechaMantenimiento;
            if (other == null)
                throw new MantenimientoException("La descripción no se puede compara con null.");

            return Fecha.Equals(other.Fecha);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Fecha);
        }
        #endregion

        #region Métodos de fomateo
        public override string ToString()
        {
            return Fecha.ToString();
        }
        #endregion
    }
}
