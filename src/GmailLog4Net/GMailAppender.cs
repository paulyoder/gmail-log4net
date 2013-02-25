using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Appender;
using System.IO;
using System.Net.Mail;
using System.Net;
using log4net.Core;

namespace YoderSolutions.Libs.GmailLog4Net
{
    public class GMailAppender : SmtpAppender
    {
        protected override void SendBuffer(LoggingEvent[] events)
        {
            try
            {
                var triggeredMessage = "";
                var memStream = new MemoryStream();
                var writer = new StreamWriter(memStream);

                if (!String.IsNullOrEmpty(Layout.Header))
                    writer. Write(Layout.Header);
                foreach (var @event in events)
                {
                    RenderLoggingEvent(writer, @event);
                    if (Evaluator.IsTriggeringEvent(@event))
                        triggeredMessage = string.Format("{0} - {1}", 
                            @event.LoggerName, 
                            @event.RenderedMessage);
                }
                if (!String.IsNullOrEmpty(Layout.Footer))
                    writer.Write(Layout.Footer);
                
                var message = new MailMessage(From, To, Subject, triggeredMessage);
                //Attach the log events to the email
                writer.Flush();
                memStream.Seek(0, SeekOrigin.Begin);
                message.Attachments.Add(new Attachment(memStream, "log.txt"));

                var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Credentials = new NetworkCredential(Username, Password);
                smtpClient.Send(message);
            }
            catch (Exception e)
            {
                ErrorHandler.Error("Error occurred while sending email notification", e);
            }
        }
    }
}
