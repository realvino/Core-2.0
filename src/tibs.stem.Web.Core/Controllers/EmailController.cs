using Abp.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.EntityFrameworkCore;

namespace tibs.stem.Web.Controllers
{
   public class EmailController : stemControllerBase
    {
        private readonly IAppFolders _appFolders;
        stemDbContext _stemDbContext;

        public EmailController(IAppFolders appFolders)
        {
            _appFolders = appFolders;
        }


        public void SendMail(int EnquiryId)
        {
     
            string Body = string.Empty;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            var headerfinder = builder.GetFileProvider().GetFileInfo("DesignerRevision.html");
            string headerpath = headerfinder.PhysicalPath;
            using (StreamReader reader = new StreamReader(headerpath, System.Text.Encoding.UTF8))
            {
                Body = reader.ReadToEnd();
            }

            string viewquery = "SELECT Top(1) * FROM [dbo].[View_Enquiry_UserDetails] where Id  =" + EnquiryId;

            DataTable ds = new DataTable();
            ConnectionAppService db = new ConnectionAppService();
            try
            {
                SqlConnection con = new SqlConnection(db.ConnectionString());
                con.Open();
                SqlCommand cmd = new SqlCommand(viewquery, con);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(ds);
                }
                con.Close();
            }
            catch (Exception ex)
            {

            }
            var ManagerEmail = ds.Rows[0]["SEmail"].ToString();
            var ManagerPwd = ds.Rows[0]["SPWD"].ToString();
            var DesignerEmail = ds.Rows[0]["DEmail"].ToString();
            var DesignerPwd = ds.Rows[0]["DPWD"].ToString();

            Body = Body.Replace("{EnquiryRefNo}", ds.Rows[0]["SubMmissionId"].ToString());
            Body = Body.Replace("{Company}", ds.Rows[0]["CompanyName"].ToString());
            Body = Body.Replace("{Designer}", ds.Rows[0]["Designer"].ToString());
            Body = Body.Replace("{Sales}", ds.Rows[0]["Sales"].ToString());
            Body = Body.Replace("{Remarks}", ds.Rows[0]["Remarks"].ToString());
            Body = Body.Replace("{Title}", ds.Rows[0]["Name"].ToString());
            Body = Body.Replace("{root}", ds.Rows[0]["Id"].ToString());

            string sendFromEmail = "leadcentral@bafco.com";
            //string sendFromPassword = "Xap66191";

            using (SmtpClient client = new SmtpClient("smtp.office365.com", 587))
            {
                client.Credentials = new NetworkCredential(DesignerEmail, DesignerPwd);
                client.EnableSsl = true;
                client.TargetName = "STARTTLS/smtp.office365.com";

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(DesignerEmail);

                mail.To.Add(ManagerEmail);
                mail.CC.Add(sendFromEmail);

                mail.Subject = "Request for Design Job";
                mail.Body = Body;
                mail.IsBodyHtml = true;
                try
                {
                    client.Send(mail);
                }
                catch(Exception ex)
                {

                }
            }

        }
        public void SendResponceEmail(int EnquiryId,int TypeId)
        {

            string Body = string.Empty;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            var headerfinder = builder.GetFileProvider().GetFileInfo("Designerresponce.html");
            string headerpath = headerfinder.PhysicalPath;
            using (StreamReader reader = new StreamReader(headerpath, System.Text.Encoding.UTF8))
            {
                Body = reader.ReadToEnd();
            }

            string viewquery = "SELECT Top(1) * FROM [dbo].[View_Enquiry_UserDetails] where Id  =" + EnquiryId;

            DataTable ds = new DataTable();
            ConnectionAppService db = new ConnectionAppService();
            try
            {
                SqlConnection con = new SqlConnection(db.ConnectionString());
                con.Open();
                SqlCommand cmd = new SqlCommand(viewquery, con);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(ds);
                }
                con.Close();
            }
            catch (Exception ex)
            {

            }
            var ManagerEmail = ds.Rows[0]["SEmail"].ToString();
            var ManagerPwd = ds.Rows[0]["SPWD"].ToString();
            var DesignerEmail = ds.Rows[0]["DEmail"].ToString();
            var DesignerPwd = ds.Rows[0]["DPWD"].ToString();

            Body = Body.Replace("{EnquiryRefNo}", ds.Rows[0]["SubMmissionId"].ToString());
            Body = Body.Replace("{Company}", ds.Rows[0]["CompanyName"].ToString());
            Body = Body.Replace("{Designer}", ds.Rows[0]["Designer"].ToString());
            Body = Body.Replace("{Sales}", ds.Rows[0]["Sales"].ToString());
            Body = Body.Replace("{Remarks}", ds.Rows[0]["Remarks"].ToString());
            Body = Body.Replace("{Title}", ds.Rows[0]["Name"].ToString());
            Body = Body.Replace("{root}", ds.Rows[0]["Id"].ToString());
            Body = Body.Replace("{Manager}", ds.Rows[0]["Manager"].ToString());

            if(TypeId ==1)
                Body = Body.Replace("{Subject}", "Accepted");

            if (TypeId == 2)
                Body = Body.Replace("{Subject}", "Rejected");

            string sendFromEmail = "leadcentral@bafco.com";
            //string sendFromPassword = "Xap66191";

            using (SmtpClient client = new SmtpClient("smtp.office365.com", 587))
            {
                client.Credentials = new NetworkCredential(ManagerEmail, ManagerPwd);
                client.EnableSsl = true;
                client.TargetName = "STARTTLS/smtp.office365.com";

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(ManagerEmail);

                mail.To.Add(DesignerEmail);
                mail.CC.Add(sendFromEmail);

                mail.Subject = "Response for Design Job";
                mail.Body = Body;
                mail.IsBodyHtml = true;
                try
                {
                    client.Send(mail);
                }
                catch (Exception ex)
                {

                }
            }

        }
        public void SendLostMail(int QuotationId)
        {

            string Body = string.Empty;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            var headerfinder = builder.GetFileProvider().GetFileInfo("lostemtemp.html");
            string headerpath = headerfinder.PhysicalPath;
            using (StreamReader reader = new StreamReader(headerpath, System.Text.Encoding.UTF8))
            {
                Body = reader.ReadToEnd();
            }

            string viewquery = "SELECT Top(1) * FROM [dbo].[View_Quotation_UserDetails] where QId  =" + QuotationId;

            DataTable ds = new DataTable();
            ConnectionAppService db = new ConnectionAppService();
            try
            {
                SqlConnection con = new SqlConnection(db.ConnectionString());
                con.Open();
                SqlCommand cmd = new SqlCommand(viewquery, con);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(ds);
                }
                con.Close();
            }
            catch (Exception ex)
            {

            }
            var ManagerEmail = ds.Rows[0]["MEmail"].ToString();
            var DesignerEmail = ds.Rows[0]["DEmail"].ToString();
            var CoordinatorEmail = ds.Rows[0]["CoEmail"].ToString();
            var SalesEmail = ds.Rows[0]["SEmail"].ToString();


            Body = Body.Replace("{EnquiryRefNo}", ds.Rows[0]["SubMmissionId"].ToString());
            Body = Body.Replace("{Company}", ds.Rows[0]["CompanyName"].ToString());
            Body = Body.Replace("{Designer}", ds.Rows[0]["Designer"].ToString());
            Body = Body.Replace("{Sales}", ds.Rows[0]["Sales"].ToString());
            Body = Body.Replace("{QuotationRefNo}", ds.Rows[0]["RefNo"].ToString());
            Body = Body.Replace("{Title}", ds.Rows[0]["Name"].ToString());
            Body = Body.Replace("{QId}", ds.Rows[0]["QId"].ToString());
            Body = Body.Replace("{Id}", ds.Rows[0]["Id"].ToString());
            Body = Body.Replace("{Remarks}", ds.Rows[0]["Remarks"].ToString());
            Body = Body.Replace("{Total}", ds.Rows[0]["Total"].ToString());

            Body = Body.Replace("{Competitor}", ds.Rows[0]["Compatitor"].ToString());
            Body = Body.Replace("{LostReason}", ds.Rows[0]["Reason"].ToString());


            string sendFromEmail = "leadcentral@bafco.com";
            string sendFromPassword = "Xap66191";

            using (SmtpClient client = new SmtpClient("smtp.office365.com", 587))
            {
                client.Credentials = new NetworkCredential(sendFromEmail, sendFromPassword);
                client.EnableSsl = true;
                client.TargetName = "STARTTLS/smtp.office365.com";

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(sendFromEmail);

                mail.To.Add(ManagerEmail);
                mail.To.Add(SalesEmail);

                if (DesignerEmail != "")
                    mail.To.Add(DesignerEmail);

                if (CoordinatorEmail != "")
                    mail.To.Add(CoordinatorEmail);

                mail.CC.Add(sendFromEmail);

                mail.Subject = "Lost Email for Opportunity : "+ ds.Rows[0]["SubMmissionId"].ToString();
                mail.Body = Body;
                mail.IsBodyHtml = true;
                try
                {
                    client.Send(mail);
                }
                catch (Exception ex)
                {

                }
            }

        }
        public void SendWonMail(int QuotationId)
        {

            string Body = string.Empty;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            var headerfinder = builder.GetFileProvider().GetFileInfo("wonemtemp.html");
            string headerpath = headerfinder.PhysicalPath;
            using (StreamReader reader = new StreamReader(headerpath, System.Text.Encoding.UTF8))
            {
                Body = reader.ReadToEnd();
            }

            string viewquery = "SELECT Top(1) * FROM [dbo].[View_Quotation_UserDetails] where QId  =" + QuotationId;

            DataTable ds = new DataTable();
            ConnectionAppService db = new ConnectionAppService();
            try
            {
                SqlConnection con = new SqlConnection(db.ConnectionString());
                con.Open();
                SqlCommand cmd = new SqlCommand(viewquery, con);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(ds);
                }
                con.Close();
            }
            catch (Exception ex)
            {

            }
            var ManagerEmail = ds.Rows[0]["MEmail"].ToString();
            var DesignerEmail = ds.Rows[0]["DEmail"].ToString();
            var CoordinatorEmail = ds.Rows[0]["CoEmail"].ToString();
            var SalesEmail = ds.Rows[0]["SEmail"].ToString();


            Body = Body.Replace("{EnquiryRefNo}", ds.Rows[0]["SubMmissionId"].ToString());
            Body = Body.Replace("{Company}", ds.Rows[0]["CompanyName"].ToString());
            Body = Body.Replace("{Designer}", ds.Rows[0]["Designer"].ToString());
            Body = Body.Replace("{Sales}", ds.Rows[0]["Sales"].ToString());
            Body = Body.Replace("{QuotationRefNo}", ds.Rows[0]["RefNo"].ToString());
            Body = Body.Replace("{Title}", ds.Rows[0]["Name"].ToString());
            Body = Body.Replace("{QId}", ds.Rows[0]["QId"].ToString());
            Body = Body.Replace("{Id}", ds.Rows[0]["Id"].ToString());
            Body = Body.Replace("{Remarks}", ds.Rows[0]["Remarks"].ToString());
            Body = Body.Replace("{Total}", ds.Rows[0]["Total"].ToString());


            string sendFromEmail = "leadcentral@bafco.com";
            string sendFromPassword = "Xap66191";

            using (SmtpClient client = new SmtpClient("smtp.office365.com", 587))
            {
                client.Credentials = new NetworkCredential(sendFromEmail, sendFromPassword);
                client.EnableSsl = true;
                client.TargetName = "STARTTLS/smtp.office365.com";

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(sendFromEmail);

                mail.To.Add(ManagerEmail);
                mail.To.Add(SalesEmail);

                if (DesignerEmail != "")
                    mail.To.Add(DesignerEmail);

                if (CoordinatorEmail != "")
                    mail.To.Add(CoordinatorEmail);

                mail.CC.Add(sendFromEmail);

                mail.Subject = "Won Email for Opportunity : " + ds.Rows[0]["SubMmissionId"].ToString();
                mail.Body = Body;
                mail.IsBodyHtml = true;
                try
                {
                    client.Send(mail);
                }
                catch (Exception ex)
                {

                }
            }

        }


    }
}
