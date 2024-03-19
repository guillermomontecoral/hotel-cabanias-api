using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaNegocio.ExcepcionesEntidades
{
    public class CabanhaException : Exception
    {
        public CabanhaException() : base()
        {
        }
        public CabanhaException(string? message) : base(message)
        {
        }

        public CabanhaException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
