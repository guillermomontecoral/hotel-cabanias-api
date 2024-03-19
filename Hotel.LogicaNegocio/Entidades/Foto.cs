using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesEntidades;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.LogicaNegocio.Entidades
{

    [Table("FotosCabanhas")]
    public class Foto : IValidarNombre
       
    {
        public int Id { get; set; }
        public string NombreFoto { get; set; }

        public int Secuenciador { get; set; }

        [ForeignKey(nameof(MiCabanha))]
        public int IdCabanha { get; set; }
        public Cabanha MiCabanha { get; set; }

        /// <see>LogicaNegocio.InterfacesEntidades.IValidarNombre#ValidarNombre(string)</see>
        public void ValidarNombre(string nombre)
        {
            //Verifica que el nombre no sea nulo
            if (string.IsNullOrEmpty(nombre))
                throw new InvalidOperationException("El nombre no puede estar vacío");

            //Verifica que el nombre no contenga numeros.            
            foreach (char c in nombre)
            {
                if (!char.IsLetter(c) && c != 32)
                {
                    throw new InvalidOperationException("El nombre solo puede incluir caracteres alfabéticos");
                }
            }
        }


        #region Overrides de Objectos
        public override string ToString()
        {
            return $"ID: {this.Id} | Nombre Foto: {this.NombreFoto}";
        }
        #endregion

    }

}

