using Hotel.LogicaNegocio.Entidades;
using Hotel.LogicaNegocio.Entidades.ValueObjects.Cabanha;
using Hotel.LogicaNegocio.ExcepcionesEntidades;
using LogicaNegocio.InterfacesEntidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace LogicaNegocio.Entidades
{
    //Le creo un indice al nombre de la cabaña para que sea unico
    //[Index(nameof(Nombre), Name = "INX_NombreCabanha", IsUnique = true)] -> Hacerde en el context en el OnModelCreating con fluent api
    [Index(nameof(NumHabitacion), Name = "INX_NumHabitacion", IsUnique = true)]
    public class Cabanha : IValidable<Cabanha>
    {
        public int Id { get; set; }

        #region VALUE OBJECTS
        //[Required(ErrorMessage = "El nombre es requerido")]
        //public string Nombre{ get; set; }
        public NombreCabanha Nombre { get; set; }

        //[Required(ErrorMessage = "La descripción es requerido.")]
        //public string Descripcion{ get; set; }
        public DescripcionCabanha Descripcion { get; set; }
        #endregion

        [Required(ErrorMessage = "Debe seleccionar un tipo de cabaña.")]
        [ForeignKey(nameof(TipoCabanha))]
        public int IdTipoCabanha { get; set; }
        public TipoCabanha TipoCabanha { get; set; }

        [Required(ErrorMessage = "Es requerido.")]
        public bool Jacuzzi { get; set; }

        [Required(ErrorMessage = "Es requerido.")]
        public bool HabilitadaParaReservas { get; set; }

        [Required(ErrorMessage = "Es requerido el número de habitación.")]
        public int NumHabitacion { get; set; }

        [Required(ErrorMessage = "La cantidad de personas es requerido.")]
        public int CantMaxPersonas { get; set; }

        [Required(ErrorMessage = "La foto es requerida.")]
        public List<Foto> MisFotos { get; set; }

        public List<Mantenimiento> MisMantenimientos { get; set; }


        /// <see>LogicaNegocio.InterfacesEntidades.IValidable<T>#ValidarDatos()</see>
        public void ValidarDatos()
        {
            //ValidarNombre(this.Nombre);

            if (Nombre == null)
                throw new CabanhaException("El nombre no puede ser null.");

            if (Descripcion == null)
                throw new CabanhaException("La descripción no puede ser null.");

            if (CantMaxPersonas < 1)
                throw new CabanhaException("La cantidad de personas debe ser mayor a 0.");

            if (NumHabitacion < 1)
                throw new CabanhaException("El número de habitación debe ser mayor a 0.");
        }

        /// <see>LogicaNegocio.InterfacesEntidades.IValidarNombre#ValidarNombre(string)</see>
        //public void ValidarNombre(string nombre)
        //{
        //          //Verifica que el nombre no sea nulo
        //          if (string.IsNullOrEmpty(nombre))
        //              throw new InvalidOperationException("El nombre no puede estar vacío");

        //          //Verifica que el nombre no contenga numeros.            
        //          foreach (Char c in nombre)
        //          {
        //              if (!Char.IsLetter(c) && c != 32)
        //              {
        //                  throw new InvalidOperationException("El nombre solo puede incluir caracteres alfabéticos");
        //              }
        //          }
        //      }

        public static string UpperFirstChar(string texto)
        {
            if (string.IsNullOrEmpty(texto))
            {
                throw new InvalidOperationException("Error: El no hay texto.");
            }

            return char.ToUpper(texto[0]) + texto.Substring(1).ToLower();
        }

        #region Overrides de Objectos
        public override string ToString()
        {
            return $"ID: {this.Id} | Nombre: {this.Nombre} | Tipo Cabaña: {this.IdTipoCabanha} | Descripción: {this.Descripcion} | Jacuzzi: {this.Jacuzzi} | Habilitada Para Reservas: {this.HabilitadaParaReservas} | Número Habitacion: {this.NumHabitacion} | Cantidad Max Personas: {this.CantMaxPersonas}";
        }

        public void Update(Cabanha obj)
        {
            obj.ValidarDatos();
            this.Nombre = new NombreCabanha(UpperFirstChar(obj.Nombre.Nombre.Trim()));
            this.Descripcion = new DescripcionCabanha(UpperFirstChar(obj.Descripcion.Descripcion.Trim()));
            this.TipoCabanha = obj.TipoCabanha;
            this.CantMaxPersonas = obj.CantMaxPersonas;
            this.HabilitadaParaReservas = obj.HabilitadaParaReservas;
            this.Jacuzzi = obj.Jacuzzi;
            this.NumHabitacion = obj.NumHabitacion;
        }
        #endregion


    }

}

