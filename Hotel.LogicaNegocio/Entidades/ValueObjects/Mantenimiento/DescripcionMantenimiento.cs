﻿using Hotel.LogicaNegocio.Entidades.ValueObjects.Cabanha;
using Hotel.LogicaNegocio.ExcepcionesEntidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaNegocio.Entidades.ValueObjects.Mantenimiento
{
    [Owned]
    public class DescripcionMantenimiento: IEquatable<DescripcionMantenimiento>
    {
        public string Descripcion { get; private set; }

        #region Constructores
        public DescripcionMantenimiento()
        {

        }

        public DescripcionMantenimiento(string descripcion)
        {
            ValidarDatos(descripcion);
            Descripcion = descripcion.Trim();
        }
        #endregion

        /// <summary>
        /// Valida que la desciçión no sea null
        /// </summary>
        /// <param name="descripcion"></param>
        /// <exception cref="CabanhaException"></exception>
        #region Validaciones
        private static void ValidarDatos(string descripcion)
        {
            if (descripcion == null)
                throw new MantenimientoException("La descripción no puede estar en null.");
        }

        public bool Equals(DescripcionMantenimiento? other)
        {
            if (other == null)
                throw new MantenimientoException("La descripción no se puede compara con null.");

            return Descripcion.Equals(other.Descripcion);
        }

        public override bool Equals(object? obj)
        {
            var other = obj as DescripcionMantenimiento;
            if (other == null)
                throw new MantenimientoException("La descripción no se puede compara con null.");

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
