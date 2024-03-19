using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAplicacion.Dtos.Usuario_Dto
{
    public class UsuarioDto
    {
        public int Id { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Formato de correo electronico incorrecto")]
        //[RegularExpression(@"/[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/g")]
        public string Email { get; set; }

        public string Clave { get; set; }

        public string Token { get; set; }
    }
}
