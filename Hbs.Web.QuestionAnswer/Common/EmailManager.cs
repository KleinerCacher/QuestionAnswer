using Hbs.Web.QuestionAnswer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Hbs.Web.QuestionAnswer.Common
{
    public class EmailManager
    {
        private readonly string baseUrl;

        public EmailManager(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public void QuestionIsAnswered(int questionId, string questionTitle, string answerAuthor)
        {
            var subject = "Streit V1-Fragen: Deine Antwort wurde als korrekte Antwort ausgewählt";
            var body = String.Format("Der Fragensteller hat Deine Antwort als richtig markiert. <br/>" +
                                     "Du findest die Frage unter folgendem Link: <br/> <h2>{0}</h2>",
                                      GenerateQuestionDetailsLink(questionId, questionTitle));

            var to = GetMailAdress(answerAuthor);
            SendMail(new string[] { to }, subject, body);
        }

        public void NewQuestionCreated(int questionId, string questionTitle)
        {
            var subject = "Streit V1-Fragen: Es wurde eine neue Frage erstellt";
            var body = String.Format("Jemand hat eine neue Frage im Streit V1 - Fragencenter erstellt.<br/> "+
                                     "Schau doch mal ob Du vielleicht helfen kannst. <br/>" +
                                     "Du findest die Frage unter folgendem Link: <br/> <h2>{0}</h2>",
                                      GenerateQuestionDetailsLink(questionId, questionTitle));

            var to = ConfigurationManager.AppSettings["MailKeyuser"].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            SendMail(to, subject, body);
        }

        public void NewAnswerCreated(int questionId, string questionTitle, string questionAuthor)
        {
            var subject = "Streit V1-Fragen: Auf Deine Frage gibt es eine neue Antwort";
            var body = String.Format("Es gibt eine neue Antwort auf Deine Frage. <br/>"+
                                     "Schau doch mal ob Dir die Antwort bei Deinem Problem hilft. <br/>" +
                                     "Du findest die Frage unter folgendem Link: <br/> <h2>{0}</h2>",
                                      GenerateQuestionDetailsLink(questionId, questionTitle));

            var to = GetMailAdress(questionAuthor);
            SendMail(new string[] { to }, subject, body);
        }

        private string GenerateQuestionDetailsLink(int questionId, string questionTitle)
        {
            string url = new Uri(new Uri(baseUrl), "/Questions/Details/" + questionId).ToString();
            return String.Format(CultureInfo.InvariantCulture, "<a href='{0}'>{1}</a>", url, questionTitle);
        }

        private string GetMailAdress(string loginame)
        {
            const string mailPropertyName = "mail";

            if (loginame.Contains(@"\"))
            {
                loginame = loginame.Substring(loginame.IndexOf(@"\") + 1);
            }

            DirectorySearcher dirSearcher = new DirectorySearcher();
            DirectoryEntry entry = new DirectoryEntry(dirSearcher.SearchRoot.Path);
            dirSearcher.Filter = "(&(objectClass=user)(objectcategory=person)(mail=" + loginame + "*))";

            SearchResult account = dirSearcher.FindOne();
            if (!account.Properties.Contains(mailPropertyName))
            {
                throw new ArgumentException("account " + account.Properties["cn"] + "doens't contain mail property");
            }
            ResultPropertyValueCollection valColl = account.Properties[mailPropertyName];
            return valColl[0].ToString();
        }

        private static void SendMail(string[] to, string subject, string body)
        {
            var from = ConfigurationManager.AppSettings["MailFrom"];
            var host = ConfigurationManager.AppSettings["MailHost"];

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);

            foreach (var mailAddress in to)
            {
                mail.To.Add(mailAddress);
            }

            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = host;
            mail.IsBodyHtml = true;
            mail.Subject = subject;
            mail.Body = body;
            client.Send(mail);
        }
    }
}