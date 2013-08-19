using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace HY.Frame.Core.Toolkit
{
    /// <summary>
    /// 需要配置configuration/system.net/mailSettings节点才能使用
    /// </summary>
    public class Email
    {
        /// <summary>
        /// 根据配置发送邮件
        /// </summary>
        /// <param name="tomail"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public void Send(string tomail, string subject, string body)
        {
            var client = new System.Net.Mail.SmtpClient();
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(tomail));
            msg.Body = body;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.Subject = subject;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            client.SendCompleted += new SendCompletedEventHandler(delegate(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
                {
                    if (e.Error == null)
                    {
                        this.Log4().InfoFormat("Email send to {0}", e.UserState);
                    }
                    else
                    {
                        this.Log4().Error("Email send to Error(" + e.UserState.ToString() + ")", e.Error);
                    }
                });
            client.SendAsync(msg, tomail);
        }
    }
}
