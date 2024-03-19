using Hotel.LogicaNegocio.Entidades.ValueObjects.Cabanha;
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
    public class RealizadoPorMantenimiento : IEquatable<RealizadoPorMantenimiento>
    {
        public string Nombre { get; private set; }

        #region Constructores
        protected RealizadoPorMantenimiento() { }

        public RealizadoPorMantenimiento(string nombre)
        {
            ValidarDatos(nombre);
            Nombre = nombre.Trim();
        }
        #endregion

        #region Validaciones
        /// <summary>
        /// Valida que el nombre de la cabaña no sea null y que sea correcto 
        /// </summary>
        /// <param name="nombre"></param>
        /// <exception cref="CabanhaException"></exception>
        private static void ValidarDatos(string nombre)
        {
            //Verifica que el nombre no sea nulo
            if (string.IsNullOrEmpty(nombre))
                throw new MantenimientoException("El nombre no puede estar vacío.");

            //Verifica que el nombre no contenga numeros.            
            foreach (Char c in nombre)
            {
                if (!Char.IsLetter(c) && c != 32)
                {
                    throw new MantenimientoException("El nombre solo puede incluir caracteres alfabéticos.");
                }
            }
        }
        #endregion

        #region Igualdad del Value Object
        public bool Equals(RealizadoPorMantenimiento? other)
        {
            if (other == null)
                throw new MantenimientoException("El nombre no se puede comparar con null");

            return Nombre.Equals(other.Nombre);
        }

        public override bool Equals(object? obj)
        {
            var other = obj as RealizadoPorMantenimiento;
            if (other == null)
                throw new MantenimientoException("El nombre no se puede comparar con null");

            return Nombre.Equals(other.Nombre);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Nombre);
        }
        #endregion

        #region Métodos de formateo.
        public override string ToString()
        {
            return Nombre;
        }
        #endregion
    }
}
