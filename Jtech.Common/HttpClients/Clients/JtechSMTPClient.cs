using MassTransit;
using MassTransit.Initializers.TypeConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.HttpClients.Clients
{
    public class JtechSMTPClient
    {
        private readonly SmtpClient _client;
        private readonly MailSettings _setting;
        public JtechSMTPClient(MailSettings setting)
        {
            this._setting = setting;
            this._client = new SmtpClient(setting.Host,setting.Port);
            this._client.EnableSsl = setting.EnableSSL;
            this._client.UseDefaultCredentials = false;
            this._client.Credentials=new NetworkCredential(setting.UserName,setting.Password);  
        }

        public void SendEmail(string subject, string body, string to)
        {
            this._client.Send(this._setting.MailFrom, to, subject, body);
        }

        public void SendEmail(MailMessage message)
        {
            this._client.Send(message);
        }

    }
    public class MailSettings
    {     
        public string Host { get; set; } = "smtp.office365.com";
        public short Port { get; set; } = 587;
        public string UserName { get; set; } = "jarun@ctc-g.co.th";
        public string Password { get; set; } = "";
        public string MailFrom { get; set; } = "jarun@ctc-g.co.th";
        public bool EnableSSL { get; set; } = true;
    }
}
