using LogicaNegocio.InterfacesEntidades;
using System.Collections.Generic;
using LogicaNegocio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;
using Hotel.LogicaNegocio.Entidades.ValueObjects.TipoCabanha;
using Hotel.LogicaNegocio.ExcepcionesEntidades;

namespace LogicaNegocio.Entidades
{
    //Le creo un indice al nombre del tipo de la cabaña para que sea unico
    //[Index(nameof(Nombre), Name = "INX_NombreTipoCabanha", IsUnique = true)]
    public class TipoCabanha : IValidable<Cabanha>
    {
        public int Id { get; set; }

        #region VALUE OBJECTS
        //[Required(ErrorMessage = "El nombre es requerido")]
        //public string Nombre { get; set; }
        public NombreTipoCabanaha Nombre { get; set; }

        //[Required(ErrorMessage = "La descripción es requerida")]
        //public string Descripcion { get; set; }
        public DescripcionTipoCabanha Descripcion { get; set; }
        #endregion

        //Configuro el tipo de dato decimal para que en la BD quede como corresponde
        [Column(TypeName = "decimal(10,2)")]
        [Required(ErrorMessage = "El costo por huesped es requerido")]
        public decimal? CostoPorHuesped { get; set; }

        /// <see>LogicaNegocio.InterfacesEntidades.IValidable<T>#ValidarDatos()</see>
        public void ValidarDatos()
        {
            //ValidarNombre(this.Nombre);

            if (CostoPorHuesped < 0)
                throw new TipoCabanhaException("El costo por huesped no puede ser negativo o ser nulo.");

        }

        /// <see>LogicaNegocio.InterfacesEntidades.IValidarNombre#ValidarNombre(string)</see>
        //public void ValidarNombre(string nombre)
        //{
        //    //Verifica que el nombre no sea nulo
        //    if (string.IsNullOrEmpty(nombre))
        //        throw new InvalidOperationException("El nombre no puede estar vacío");

        //    //Verifica que el nombre no contenga numeros.            
        //    foreach (Char c in nombre)
        //    {
        //        if (!Char.IsLetter(c) && c != 32)
        //        {
        //            throw new InvalidOperationException("El nombre solo puede incluir caracteres alfabéticos");
        //        }
        //    }
        //}

        //Pone la primer letra de una cadena de texto en mayúscula y las demas en minúsculas
        public static string UpperFirstChar(string texto)
        {
            if (string.IsNullOrEmpty(texto))
            {
                throw new InvalidOperationException("Error: El no hay texto.");
            }

            return char.ToUpper(texto[0]) + texto.Substring(1).ToLower();
        }


        #region Modificaciones
        /// <summary>
        /// Permite actualizar los datos del tipo de cabaña sustituyèndolos por los del paràmetro recibido
        /// </summary>
        /// <param name="nuevosDatos">Objeto autor con los nuevos datos cambiados</param>
        /// <remarks>El Id del autor no se cambia</remarks>
        public void Update(TipoCabanha nuevosDatos)
        {
            nuevosDatos.ValidarDatos();
            this.Nombre = new NombreTipoCabanaha(UpperFirstChar(nuevosDatos.Nombre.Value.Trim()));
            this.Descripcion = new DescripcionTipoCabanha(UpperFirstChar(nuevosDatos.Descripcion.Descripcion.Trim()));
            this.CostoPorHuesped = nuevosDatos.CostoPorHuesped;


        }

        //public void Update(string descripcion, decimal? costoHuesped)
        //{
        //    ValidarDatos();
        //    this.Descripcion = new DescripcionTipoCabanha(UpperFirstChar(descripcion.Trim()));
        //    this.CostoPorHuesped = costoHuesped;

        //}



        #endregion

        #region Overrides de Objectos
        public override string ToString()
        {
            return $"ID: {this.Id} | Nombre: {this.Nombre} | Descripción: {this.Descripcion} | Costo por huésped: {this.CostoPorHuesped}";
        }
        #endregion

    }

}

