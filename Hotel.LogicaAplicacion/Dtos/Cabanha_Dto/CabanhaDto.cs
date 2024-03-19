using Hotel.LogicaNegocio.Entidades.ValueObjects.Cabanha;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAplicacion.Dtos.Cabanha_Dto
{
    public class CabanhaDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es requerido.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tipo de cabaña.")]
        public int IdTipoCabanha { get; set; }

        [Required(ErrorMessage = "Es requerido.")]
        public bool Jacuzzi { get; set; }

        [Required(ErrorMessage = "Es requerido.")]
        public bool HabilitadaParaReservas { get; set; }

        [Required(ErrorMessage = "Es requerido el número de habitación.")]
        public int NumHabitacion { get; set; }

        [Required(ErrorMessage = "La cantidad de personas es requerido.")]
        public int CantMaxPersonas { get; set; }

        [Required(ErrorMessage = "La foto es requerida")]
        public string NombreFoto { get; set; }

        //[Required(ErrorMessage = "La foto es requerida.")]
        //public List<Foto> MisFotos { get; set; }

        //public List<Mantenimiento> MisMantenimientos { get; set; }
    }
}
