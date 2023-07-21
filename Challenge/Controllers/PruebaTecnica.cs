using Challenge.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Challenge.Controllers
{
    [ApiController]
    [Route("api")]
    public class PruebaTecnica : ControllerBase
    {
        private Response response;

        public PruebaTecnica()
        {
            response = new Response();
        }

        [HttpGet]
        [Route("suma")]
        public int SumaNumeros([FromQuery] [Required] int numero1, [Required] int numero2)
        {
            int resultado;
            try
            {
                resultado = Suma(numero1, numero2);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            return resultado;
        }

        [HttpGet]
        [Route("archivo")]
        public IActionResult ArchivoInformacion([FromQuery] [Required] string nombre, [Required] string fechaNacimiento, [Required] string correo)
        {
            try
            {
                response = Informacion(nombre, fechaNacimiento, correo);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            return Ok(response);
        }

        public int Suma(int numero1, int numero2)
        {
            int resultado;
            try
            {
                resultado = numero1 + numero2;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            return resultado;
        }

        public Response Informacion(string nombre, string fechaNacimiento, string correo)
        {
            try
            {
                string nombreFichero = "Información " + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
                var expresionR = @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$";
                if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(fechaNacimiento) && !string.IsNullOrEmpty(correo) &&
                    Regex.IsMatch(correo, expresionR))
                {
                    StreamWriter writer = new StreamWriter(nombreFichero, true);
                    var fechaN = Convert.ToDateTime(fechaNacimiento).ToString("dd-MM-yyyy");
                    writer.WriteLine(string.Format("Nombre Completo: " + "{0}", nombre));
                    writer.WriteLine(string.Format("Fecha de Nacimiento: " + "{0}", fechaN));
                    writer.WriteLine(string.Format("Correo Electronico: " + "{0}", correo));
                    writer.Flush();
                    writer.Close();
                    response.Resultado = 1;
                }
                else
                {
                    response.Mensaje = "El archivo no puede ser creado, porque faltan datos por incluir o el correo no es valido ";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            return response;
        }
    }
}
