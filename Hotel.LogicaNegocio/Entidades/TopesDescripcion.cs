using LogicaNegocio.InterfacesEntidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
using Hotel.LogicaNegocio.ExcepcionesEntidades;
using Hotel.LogicaNegocio.Entidades.ValueObjects.TopesDescripcion;
using Hotel.LogicaNegocio.Entidades.ValueObjects.TipoCabanha;
using LogicaNegocio.Entidades;

namespace Hotel.LogicaNegocio.Entidades
{
    //[Index(nameof(NombreObj), Name = "INX_NombreObjeto", IsUnique = true)]
    public class TopesDescripcion : IValidable<TopesDescripcion>
    {
        public int Id { get; set; }

        #region VALUE OBJECTS
        //public string NombreObj { get; set; }
        public NombreTopeDescripcion NombreTope { get; set; }

        //[Required(ErrorMessage = "El tope mínimo es requerido")]
        //public int TopeMin { get; set; }

        //[Required(ErrorMessage = "El tope máximo es requerido")]
        //public int TopeMax { get; set; }
        public RangoTopesDescripcion Rangos { get; set; }
        #endregion

        public void ValidarDatos()
        {
            if (NombreTope == null)
                throw new TopeDescripcionException("El nombre no puede ser nulo.");

            if (Rangos == null)
                throw new TopeDescripcionException("Los rangos no pueden ser nulos");

        }

        #region Overrides de Objectos
        public override string ToString()
        {
            return $"ID: {this.Id} | Nombre Obj: {this.NombreTope} | Tope Mínimo: {this.Rangos.Min} | Tope Máximo: {this.Rangos.Max}";
        }

        public void Update(TopesDescripcion nuevosDatos)
        {
            nuevosDatos.ValidarDatos();
            this.NombreTope = new NombreTopeDescripcion(nuevosDatos.NombreTope.Nombre.Trim());
            this.Rangos = nuevosDatos.Rangos;


        }
        #endregion
    }
}
