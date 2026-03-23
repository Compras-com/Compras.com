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
            var smtp = new SmtpClient(_config["Email:Servidor"])
            {
                Port = int.Parse(_config["Email:Porta"]),
                Credentials = new NetworkCredential(
                    _config["Email:Usuario"],
                    _config["Email:Senha"]
                ),
                EnableSsl = true
            };

            var mail = new MailMessage
            {
                From = new MailAddress(_config["Email:Usuario"]),
                Subject = assunto,
                Body = mensagem
            };

            mail.To.Add(destino);

            smtp.Send(mail);
        }
    }
}