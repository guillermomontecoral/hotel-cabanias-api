﻿using Hotel.LogicaNegocio.Entidades.ValueObjects.TipoCabanha;
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
    public class NombreTopeDescripcion : IEquatable<NombreTopeDescripcion>
    {

        public string Nombre { get; private set; }

        #region Constructores
        protected NombreTopeDescripcion() { }

        public NombreTopeDescripcion(string nombre)
        {
            ValidarDatos(nombre);
            Nombre = nombre.Trim();
        }
        #endregion

        #region Validaciones
        private static void ValidarDatos(string nombre)
        {
            //Verifica que el nombre no sea nulo
            if (string.IsNullOrEmpty(nombre))
                throw new TopeDescripcionException("El nombre de la cabaña no puede estar vacío.");

            //Verifica que el nombre no contenga numeros.            
            //foreach (Char c in nombre)
            //{
            //    if (!Char.IsLetter(c) && c != 32)
            //    {
            //        throw new TipoCabanhaException("El nombre solo puede incluir caracteres alfabéticos.");
            //    }
            //}
        }
        #endregion

        #region Igualdad del Value Object
        public bool Equals(NombreTopeDescripcion? other)
        {
            if (other == null)
                throw new TopeDescripcionException("El nombre no se puede comparar con null");

            return Nombre.Equals(other.Nombre);
        }

        public override bool Equals(object? obj)
        {
            var other = obj as NombreTopeDescripcion;
            if (other == null)
                throw new TopeDescripcionException("El nombre no se puede comparar con null");

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
