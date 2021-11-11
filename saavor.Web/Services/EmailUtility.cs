using System; 
using System.Net.Mail;
 

namespace saavor.Web.Services
{
    public class EmailUtility
    {
        public static int SendEmail(string smtpClient, string strEmailFrom, string strEmailPassword, string strEmailTo, string strEmailSubject, string strEmailBody, bool blnIsHtml = false)
        {
            //strEmailFrom = "parkeeeapps@gmail.com";
             
            try
            {

                string password = strEmailPassword;

                MailMessage msg = new MailMessage();
                msg.To.Add(strEmailTo);
                msg.From = new MailAddress(strEmailFrom, strEmailSubject);
                msg.Subject = strEmailSubject;
                msg.Body = strEmailBody;

                msg.BodyEncoding = System.Text.Encoding.UTF8;// System.Text.Encoding.GetEncoding("utf-8");
                msg.SubjectEncoding = System.Text.Encoding.Default;


                msg.IsBodyHtml = blnIsHtml;

                //msg.SubjectEncoding = System.Text.Encoding.Default;

                //msg.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");

                /******** Using Parkeee Domain ********/
                string smtp = smtpClient;
                SmtpClient client = new SmtpClient(smtp, 25);
                client.Credentials = new System.Net.NetworkCredential(strEmailFrom, password);
                //client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                /************************************/

                client.Send(msg);




                ///******** Using Gmail Smtp server********/
                //var client = new SmtpClient("smtp.gmail.com", 587)
                //{
                //    Credentials = new NetworkCredential("parkeeeapps@gmail.com", "Parkeee#38"),
                //    EnableSsl = true
                //};
                //client.Send(msg);




                //Commented on 20/10/2016
                ///******** Using Gmail Domain ********/
                //SmtpClient client = new SmtpClient("dedrelay.secureserver.net", 25);
                //client.UseDefaultCredentials = false;
                //client.EnableSsl = false;
                ///************************************/

                ///******** Using Yahoo Domain ********/
                ////SmtpClient client = new SmtpClient("smtp.mail.yahoo.com", 25);
                ///************************************/

                //client.Send(msg);		  // Send our email.
                //end Comment on 20/10/2016

                msg = null;

                return 1;
            }
            catch
            {
                return -1;
            }
        }
    }
}
