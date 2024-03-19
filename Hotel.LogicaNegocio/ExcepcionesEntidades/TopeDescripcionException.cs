using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaNegocio.ExcepcionesEntidades
{
    public class TopeDescripcionException: Exception
    {
        public TopeDescripcionException() : base()
        {
        }
        public TopeDescripcionException(string? message) : base(message)
        {
        }

        public TopeDescripcionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
