using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SpinningTrainer.ViewModel
{
    class EnviaCorreoViewModel
    {
        /// <summary>
        /// Clase para enviar correo con un código aleatorio de 8 dígitos, en caso de error retorna el número 0 y el mensaje del error.
        /// </summary>
        /// <param name="destino">Correo electronico de destino</param>
        /// <returns>Devuelve el número enviado y mensaje de error en caso de que haya uno. (Si ocurre algún error el número retornara 0)</returns>
        public static (string, string) EnviarCorreoCodigoRecuperacion(string destino)
        {
            string numeroAleatorio = GenerarCodigoAleatorio();

            SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com");
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true; 

            // Autenticación
            smtpClient.Credentials = new NetworkCredential("spincoach@hotmail.com", "Spin2024");

            // Creación del mensaje
            MailMessage message = new MailMessage();
            message.From = new MailAddress("spincoach@hotmail.com");
            message.To.Add(destino);
            message.Subject = "Código de Recuperación de la app SpinCoach";
            message.Body = "Su código de recuperación es: " + numeroAleatorio;

            try
            {
                smtpClient.Send(message);
                Console.WriteLine("Correo enviado correctamente.");
                return (numeroAleatorio,"");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el correo electrónico: " + ex.Message);
                return ("0", ex.Message);
            }
        }
        
        /// <summary>
        /// Envia un correo
        /// </summary>
        /// <param name="destino">Destinatario del correo.</param>
        /// <param name="subject">Asunto del correo.</param>
        /// <param name="body">Cuerpo del correo.</param>
        /// <returns>Devuelve resultado si el envio culmino exitosamente y un mensaje de error en caso de que no.</returns>
        public static (bool, string) EnviarCorreo(string destino, string subject, string body)
        {            
            SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com");
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;

            // Autenticación
            smtpClient.Credentials = new NetworkCredential("SpinCoach@hotmail.com", "Spin2024");

            // Creación del mensaje
            MailMessage message = new MailMessage();
            message.From = new MailAddress("SpinCoach@hotmail.com");
            message.To.Add(destino);
            message.Subject = subject;
            message.Body = body;

            try
            {
                smtpClient.Send(message);
                Console.WriteLine("Correo enviado correctamente.");
                return (true, "");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el correo electrónico: " + ex.Message);
                return (false, ex.Message);
            }
        }

        /// <summary>
        /// Metodo para generar un número aleatorio.
        /// </summary>
        /// <returns>Devuelve un número aleatorio de 8 dígitos convertido a String</returns>
        private static string GenerarCodigoAleatorio()
        {
            Random random = new Random();
            
            int minValue = 10000000;
            int maxValue = 99999999;

            return (random.Next(minValue, maxValue + 1).ToString());
        }
    }
}
