using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using System.Net.Mail;
using System.Net;
using System.IO;

namespace CapaNegocio
{
    public class CN_Recursos
    {


        public static string GenerarClave() { 
            string clave = Guid.NewGuid().ToString("N").Substring(0, 6);// clave generada solo alfanumericos
            return clave;
        }

        public static string ConvertSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();


        }



        public static bool EnviarCorreo(string correo, string asunto, string mensaje) {

            bool resultado = false;

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("edu7vargas@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential("edu7vargas@gmail.com", "pfzjoocoaxxmxgbm"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                };
                smtp.Send(mail);
                return true;
            }
            catch (Exception ex) {

                resultado = false;

            }
            return resultado;

        }


    }
}
