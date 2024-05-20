using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SpinningTrainer.ViewModel
{
    class EnviaCorreoRecuperacionViewModel
    {
        public static (int, string) EnviarCorreo(string destino, string nombreArchivo, string numeroD)
        {
            int numeroAleatorio = GenerarCodigoAleatorio();

            SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com");
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true; 

            // Autenticación
            smtpClient.Credentials = new NetworkCredential("SpinCoach@hotmail.com", "Spin2024");

            // Creación del mensaje
            MailMessage message = new MailMessage();
            message.From = new MailAddress("SpinCoach@hotmail.com");
            message.To.Add(destino);
            message.Subject = "Código de Recuperación de la app SpinCoach";
            message.Body = "Su código de recuperación es: " + numeroAleatorio.ToString();

            try
            {
                smtpClient.Send(message);
                Console.WriteLine("Correo enviado correctamente.");
                return (numeroAleatorio,"");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el correo electrónico: " + ex.Message);
                return (0, ex.Message);
            }
        }

        private static int GenerarCodigoAleatorio()
        {
            Random random = new Random();
            
            int minValue = 10000000;
            int maxValue = 99999999;

            return (random.Next(minValue, maxValue + 1));
        }
    }
}
