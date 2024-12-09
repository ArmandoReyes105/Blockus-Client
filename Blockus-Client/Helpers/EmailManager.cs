using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Blockus_Client.Helpers
{
    public class EmailManager
    {
        public static void SendVerificationEmail(string recipientEmail, string verificationCode)
        {
            try
            {
                string senderEmail = "blockusequipoocho@gmail.com"; // Cambia esto a tu correo
                string senderPassword = "cuiy hukr byhw jxhk";   // Cambia esto a tu contraseña
                string subject = "Código de Verificación";
                string body = $"Tu código de verificación es: {verificationCode}";

                // Configuración del cliente SMTP
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true
                };

                // Crear el mensaje de correo
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false // Cambia a true si usas HTML en el cuerpo del correo
                };

                // Agregar destinatario
                mailMessage.To.Add(recipientEmail);

                // Enviar el correo
                smtpClient.Send(mailMessage);

                Console.WriteLine("Correo enviado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
            }
        }
    }
}
