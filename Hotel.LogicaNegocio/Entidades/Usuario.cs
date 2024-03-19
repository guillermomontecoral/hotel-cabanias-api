using LogicaNegocio.InterfacesEntidades;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace LogicaNegocio.Entidades
{
    [Index(nameof(Email), Name = "INX_EmailUsuario", IsUnique = true)]
    public class Usuario : IValidable<Usuario>
    {
        public int Id { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Formato de correo electronico incorrecto")]
        [RegularExpression(@"/[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/g")]
        public string Email { get; set; }

        public string Clave { get; set; }

        public Usuario(string email, string clave)
        {
            Email = email;
            Clave = clave;
        }

        public Usuario()
        {
        }

        /// <see>LogicaNegocio.InterfacesEntidades.IValidable<T>#ValidarDatos()</see>
        public void ValidarDatos()
        {
            if (!IsValidEmail(this.Email))
                throw new Exception("Formato de correo electrónico incorrecto. Ejemplo: correo@mail.com. Verifique la precarga.");

            ValidarClave(this.Clave);
        }

        #region Validaciones
        public void ValidarClave(string clave)
        {
            int cantNum = 0;
            int cantMin = 0;
            int cantMay = 0;

            foreach (Char c in clave)
            {
                if (char.IsDigit(c))
                {
                    cantNum++;
                }
                else if (c == char.ToUpper(c))
                {
                    cantMay++;
                }
                else
                {
                    cantMin++;
                }
            }

            if (clave.Length <= 6 && cantMay < 1 && cantMin < 1 && cantNum < 1)
            {
                throw new InvalidOperationException("La contraseña debe contener al menos 6 caracteres que incluyan números, letras mayúsculas y minúsculas (al menos una de cada una)");
            }
        }

        //Metodo que valida un email, sacado de la página oficial de Microsoft
        /*
         * https://learn.microsoft.com/es-es/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
        */
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        #endregion

        #region Overrides de Objectos
        public override string ToString()
        {
            return $"ID: {this.Id} | Email: {this.Email} | Clave: {this.Clave}";
        }
        #endregion
    }

}

