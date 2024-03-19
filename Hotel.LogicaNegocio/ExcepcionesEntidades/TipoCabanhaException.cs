using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaNegocio.ExcepcionesEntidades
{
    public class TipoCabanhaException:Exception
    {
        public TipoCabanhaException() : base()
        {
        }
        public TipoCabanhaException(string? message) : base(message)
        {
        }

        public TipoCabanhaException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
