using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAplicacion.Dtos.TopesDescripcion_Dto
{
    public class TopesDescripcionDto
    {
        /// <summary>
        /// Identificador del tope de descripción.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre asignado al tope de descrición para relacionarlo con un objeto
        /// </summary>
        /// <remarks>
        /// Referencias:
        /// Objeto Cabaña = cab
        /// Objeto Tipo Cabaña = tc
        /// Objeto Mantenimiento = man
        /// </remarks>
        [Required (ErrorMessage = "El nombre es requerido")]
        public string NombreObj { get; set; }

        /// <summary>
        /// Tope minimo de caracteres para la descripción
        /// </summary>
        [Required(ErrorMessage = "El tope mínimo es requerido")]
        public int TopeMin { get; set; }

        /// <summary>
        /// Tope maximo de caracteres para la descripción
        /// </summary>
        [Required(ErrorMessage = "El tope máximo es requerido")]
        public int TopeMax { get; set; }
    }
}
