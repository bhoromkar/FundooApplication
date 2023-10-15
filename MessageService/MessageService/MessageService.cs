using MassTransit.Riders;
using MassTransit;
//using Common.Model;
using System.Threading.Tasks;
using Experimental.System.Messaging;
using System.Net.Mail;
using System.Net;
using System;
using Model;


namespace MessageService
{
    public class MessageService : IConsumer<ColabEmailModel>
    {
        public async Task Consume(ConsumeContext<ColabEmailModel> context)
        {
            await Console.Out.WriteLineAsync(context.Message.Email + " " + context.Message.ColabId);

            /*
            try
            {

                await Console.Out.WriteLineAsync(context.Message.Email+" "+context.Message.ColabId);

                var mailTO= "omkarbhor3472@gmail.com"; 
               var mail = "imomkarbhor@gmail.com";
               var pass = "gjan wnbf uebb qevq";

                MailMessage messageMade = new MailMessage(new MailAddress(fromEmail, mailTitle), new MailAddress(toEmail));


                var msg = context.Message.ColabId;

                string ServerURL = "https://localhost:44352/api/Colab/GetAll/" + context.Message.ColabId;
                string Subject = "Invitation of colaboration";
                EmailTemplate emalitemp = new EmailTemplate(ServerURL);

                messageMade.Subject = Subject;
                messageMade.Body = emalitemp.MakePage();
                messageMade.IsBodyHtml = true;


                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(mail, pass),
                    EnableSsl = true,

                };

                smtpClient.Send(messageMade);



            }
            catch (Exception)
            {

                throw;
            }

            */
        }
    }
}