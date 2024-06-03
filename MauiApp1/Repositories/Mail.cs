using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SpinningTrainer.Repository
{
    class Mail
    {
        private SmtpClient _smtpClient = new SmtpClient("smtp-mail.outlook.com", 587);

        /// <summary>
        /// Método constructor de la clase.
        /// </summary>
        public Mail()
        {
            _smtpClient.EnableSsl = true;
            _smtpClient.Credentials = new NetworkCredential("spincoach@hotmail.com", "Spin2024");
        }

        /// <summary>
        /// Clase para enviar correo con un código aleatorio de 8 dígitos, en caso de error retorna el número 0 y el mensaje del error.
        /// </summary>
        /// <param name="destination">Correo electrónico de destination</param>
        /// <returns>Devuelve el número enviado y mensaje de error en caso de que haya uno. (Si ocurre algún error el número retornara 0)</returns>
        public (string, string) SendMailRecoveryCode(string destination)
        {
            string randomNumber = GenerateRandomNumber();

            var (successfulMailSending, errorMessage) = SendMail(destination, "Código de Recuperación de la app SpinCoach", "Su código de recuperación es: " + randomNumber);

            if (successfulMailSending)
            {
                Console.WriteLine("Correo enviado correctamente.");
                return (randomNumber, "");
            }
            else
            {
                Console.WriteLine("Error al enviar el correo electrónico: " + errorMessage);
                return ("0", errorMessage);
            }
        }

        /// <summary>
        /// Envía un correo
        /// </summary>
        /// <param name="destination">Destinatario del correo.</param>
        /// <param name="subject">Asunto del correo.</param>
        /// <param name="body">Cuerpo del correo.</param>
        /// <returns>Devuelve resultado si el envió culmino exitosamente y un mensaje de error en caso de que no.</returns>
        public (bool, string) SendMail(string destination, string subject, string body)
        {
            // Creación del mensaje
            MailMessage message = new MailMessage();
            message.From = new MailAddress("spincoach@hotmail.com");
            message.To.Add(destination);
            message.Subject = subject;
            message.Body = body;

            try
            {
                _smtpClient.Send(message);
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
        /// Método para generar un número aleatorio.
        /// </summary>
        /// <returns>Devuelve un número aleatorio de 8 dígitos convertido a String</returns>
        private static string GenerateRandomNumber()
        {
            Random random = new Random();

            int minValue = 10000000;
            int maxValue = 99999999;

            return random.Next(minValue, maxValue + 1).ToString();
        }
    }
}
