using Hotel.LogicaNegocio.Entidades.ValueObjects.Mantenimiento;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAplicacion.Dtos.Mantenimiento_Dto
{
    public class MantenimientoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La descripción es requerida.")]
        public string Descripcion { get; set; }
        //public DescripcionMantenimiento Descripcion { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre de la persona encargada del mantenimiento")]
        public string RealizadoPor { get; set; }
        //public RealizadoPorMantenimiento RealizadoPor { get; set; }

        [Required(ErrorMessage = "Seleccione una fecha.")]
        //Ponerlo en el view model el display format
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
        //public FechaMantenimiento Fecha { get; set; }

        //Configuro el tipo de dato decimal para que en la BD quede como corresponde
        [Column(TypeName = "decimal(10,2)")]
        [Required(ErrorMessage = "Ingrese el costo de mantenimiento.")]
        public decimal CostoMantenimiento { get; set; }

        //[ForeignKey(nameof(Cabanha))]
        public int IdCabanha { get; set; }
        //public Cabanha Cabanha { get; set; }
    }
}
