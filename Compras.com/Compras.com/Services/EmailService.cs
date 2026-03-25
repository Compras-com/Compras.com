using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Compras.com.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void Enviar(string destino, string assunto, string mensagem)
        {
            var servidor = _config["Email:Servidor"] ?? "";
            var portaStr = _config["Email:Porta"] ?? "587";
            var usuario = _config["Email:Usuario"] ?? "";
            var senha = _config["Email:Senha"] ?? "";

            int porta = int.TryParse(portaStr, out var p) ? p : 587;

            using (var smtp = new SmtpClient(servidor))
            {
                smtp.Port = porta;
                smtp.Credentials = new NetworkCredential(usuario, senha);
                smtp.EnableSsl = true;

                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress(usuario);
                    mail.Subject = assunto;
                    mail.Body = mensagem;
                    mail.To.Add(destino);

                    smtp.Send(mail);
                }
            }
        }
    }
}