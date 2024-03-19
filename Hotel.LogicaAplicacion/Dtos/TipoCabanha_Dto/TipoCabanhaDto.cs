using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAplicacion.Dtos.TipoCabanha_Dto
{
    public class TipoCabanhaDto
    {
        /// <summary>
        /// Id del tipo de cabaña. Es autonumérico, durante el alta dejarlo en 0.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del tipo de cabaña es unico y alfabético.
        /// </summary>
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        /// <summary>
        /// Breve descripción del tipo de cabaña.
        /// </summary>
        [Required(ErrorMessage = "La descripción es requerida")]
        public string Descripcion { get; set; }

        /// <summary>
        /// Costo por huesped, valor positivo.
        /// </summary>
        //Configuro el tipo de dato decimal para que en la BD quede como corresponde
        [Column(TypeName = "decimal(10,2)")]
        [Required(ErrorMessage = "El costo por huesped es requerido")]
        public decimal? CostoPorHuesped { get; set; }
    }
}
