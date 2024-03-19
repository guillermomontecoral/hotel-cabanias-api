using Hotel.LogicaNegocio.ExcepcionesEntidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaNegocio.Entidades.ValueObjects.TipoCabanha
{
    [Owned]
    public class NombreTipoCabanaha : IEquatable<NombreTipoCabanaha>
    {

        public string Value { get; private set; }

        #region Constructores
        protected NombreTipoCabanaha() {
        }

        public NombreTipoCabanaha(string nombre)
        {
            ValidarDatos(nombre);
            Value = nombre.Trim();
        }
        #endregion

        #region Validaciones
        /// <summary>
        /// Valida que el nombre de la cabaña no sea null y que sea correcto 
        /// </summary>
        /// <param name="nombre"></param>
        /// <exception cref="TipoCabanhaException"></exception>
        private static void ValidarDatos(string nombre)
        {
            //Verifica que el nombre no sea nulo
            if (string.IsNullOrEmpty(nombre))
                throw new TipoCabanhaException("El nombre de la cabaña no puede estar vacío.");

            //Verifica que el nombre no contenga numeros.            
            foreach (Char c in nombre)
            {
                if (!Char.IsLetter(c) && c != 32)
                {
                    throw new TipoCabanhaException("El nombre solo puede incluir caracteres alfabéticos.");
                }
            }
        }
        #endregion

        #region Igualdad del Value Object
        public bool Equals(NombreTipoCabanaha? other)
        {
            if (other == null)
                throw new TipoCabanhaException("El nombre no se puede comparar con null");

            return Value.Equals(other.Value);
        }

        public override bool Equals(object? obj)
        {
            var other = obj as NombreTipoCabanaha;
            if (other == null)
                throw new TipoCabanhaException("El nombre no se puede comparar con null");

            return Value.Equals(other.Value);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
        #endregion

        #region Métodos de formateo.
        public override string ToString()
        {
            return Value;
        }

        [NotMapped]
        public string NombreCompleto
        {
            get
            {
                return Value;
            }
        }
        #endregion
    }
}
