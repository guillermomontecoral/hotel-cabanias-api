using LogicaNegocio.InterfacesEntidades;
using System;
using LogicaNegocio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Hotel.LogicaNegocio.Entidades.ValueObjects.Mantenimiento;
using Hotel.LogicaNegocio.Entidades.ValueObjects.Cabanha;

namespace LogicaNegocio.Entidades
{
    public class Mantenimiento : IValidable<Mantenimiento>
    {
        public int Id { get; set; }

        #region VALUE OBJECTS
        //[Required(ErrorMessage = "La descripción es requerida.")]
        //public string Descripcion { get; set; }
        public DescripcionMantenimiento Descripcion { get; set; }

        //[Required(ErrorMessage = "Ingrese el nombre de la persona encargada del mantenimiento")]
        //public string NomRealizoTrabajo { get; set; }
        public RealizadoPorMantenimiento RealizadoPor { get; set; }

        //[Required(ErrorMessage = "Seleccione una fecha.")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]
        //public DateTime Fecha { get; set; }
        public FechaMantenimiento Fecha { get; set; }
        #endregion

        //Configuro el tipo de dato decimal para que en la BD quede como corresponde
        [Column(TypeName = "decimal(10,2)")]
        [Required(ErrorMessage = "Ingrese el costo de mantenimiento.")]
        public decimal CostoMantenimiento { get; set; }        

        [ForeignKey(nameof(Cabanha))]
        public int IdCabanha { get; set; }
        public Cabanha Cabanha { get; set; }


        /// <see>LogicaNegocio.InterfacesEntidades.IValidable<T>#ValidarDatos()</see>
        public void ValidarDatos()
        {

            if (CostoMantenimiento < 0)
                throw new InvalidOperationException("El costo de mantenimiento no puede ser negativo.");
        }

        //private void ValidarFecha(DateTime? fecha)
        //{
        //    if (fecha == null)
        //        throw new ArgumentNullException("Fecha no puede estar vacía.");

        //    if (fecha > DateTime.Today)
        //        throw new InvalidOperationException($"La fecha ingresada no puede ser mayor a la del día actual. Hoy es: {DateTime.Today.ToString("dd/MM/yyyy")}");

        //}

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
            return $"ID: {this.Id} | Fecha: {this.Fecha}  | Descripción: {this.Descripcion} | Costo Mantenimiento: {this.CostoMantenimiento} | Nombre: {this.RealizadoPor.Nombre} | Id Cabaña: {this.IdCabanha} ";
        }

        public void Update(Mantenimiento obj)
        {
            obj.ValidarDatos();
            this.Descripcion = new DescripcionMantenimiento(UpperFirstChar(obj.Descripcion.Descripcion.Trim()));
            this.RealizadoPor = new RealizadoPorMantenimiento(UpperFirstChar(obj.RealizadoPor.Nombre.Trim()));
            this.Fecha = new FechaMantenimiento(obj.Fecha.Fecha);
            this.CostoMantenimiento = obj.CostoMantenimiento;
            this.IdCabanha = obj.IdCabanha;
        }
        #endregion

    }

}

