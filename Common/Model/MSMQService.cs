using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;



namespace Common.Model
{
    public class MSMQService
    {
        MessageQueue messageQueue = new MessageQueue();
        public void SendMessage(string Token)
        {
            messageQueue.Path = @".\Private$\MyQueue";
            try
            {
                if (!MessageQueue.Exists(messageQueue.Path))
                {
                    MessageQueue.Create(messageQueue.Path);
                }
                
                    messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                    messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;
                    messageQueue.Send(Token);
                    messageQueue.BeginReceive();
                    messageQueue.Close();

                }
            catch (Exception ex)
            {
              throw ex;
            }

        }

        private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var mailTO= "omkarbhor3472@gmail.com"; 
                var mail = "imomkarbhor@gmail.com";
                var pass = "gjan wnbf uebb qevq";
              



                var msg = messageQueue.EndReceive(e.AsyncResult);
                string Token = msg.Body.ToString();
                string subject = "Password Reset Link";


                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(mail,pass)
                   

                };

                smtpClient.Send(mail,mailTO,subject,$"The Password  reset link is : {Token}");
                messageQueue.BeginReceive();
            }
            catch (MessageQueueException qexception)
            { 
               throw qexception;
            }
        }
    }
}
