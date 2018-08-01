using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NReco.PdfGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.EntityFrameworkCore;
//using System.Web.Mvc;

namespace tibs.stem.Web.Controllers
{
    public abstract class PdfControllerBase : stemControllerBase
    {
        private readonly IAppFolders _appFolders;
        stemDbContext _stemDbContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public static IConfigurationRoot Configuration { get; set; }
        public PdfControllerBase
            (
            IAppFolders appFolders, 
            stemDbContext stemDbContext, 
            IHostingEnvironment hostingEnvironment
            )
        {
            _appFolders = appFolders;
            _stemDbContext = stemDbContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public FileResult Download(int QuotationId)
        {
            string webRootPath = _hostingEnvironment.WebRootPath + "\\Common\\PDF\\StandardPDF\\"; 
            string fileName = "StandardQuotationPDF_" + QuotationId + ".pdf"; 
            byte[] fileBytes = System.IO.File.ReadAllBytes(webRootPath + fileName);
            return File(fileBytes, "application/x-msdownload", fileName);
        }
        public FileResult DownloadExcel(int QuotationId)
        {
            string webRootPath = _hostingEnvironment.WebRootPath + "\\Common\\Excel\\";
            string fileName = "StandardPreviewExcel_" + QuotationId + ".xlsx"; 
            byte[] fileBytes = System.IO.File.ReadAllBytes(webRootPath + fileName);
            return File(fileBytes, "application/x-msdownload", fileName);
        }
        public FileResult DownloadEmp(int QuotationId)
        {
            string webRootPath = _hostingEnvironment.WebRootPath + "\\Common\\PDF\\PhotoEmphasisPDF\\";
            string fileName = "QuotationPhotoEmphasisPDF_" + QuotationId + ".pdf"; 
            byte[] fileBytes = System.IO.File.ReadAllBytes(webRootPath + fileName);
            return File(fileBytes, "application/x-msdownload", fileName);
        }
        public FileResult DownloadprodPdf(int QuotationId)
        {
            string webRootPath = _hostingEnvironment.WebRootPath + "\\Common\\PDF\\ProductCategoryPDF\\";
            string fileName = "QuotationProductCategoryPDF_" + QuotationId + ".pdf"; 
            byte[] fileBytes = System.IO.File.ReadAllBytes(webRootPath + fileName);
            return File(fileBytes, "application/x-msdownload", fileName);
        }
        public FileResult PreviewDownload(int QuotationId)
        {
            string webRootPath = _hostingEnvironment.WebRootPath + "\\Common\\PDF\\PreviewPDF\\";
            string fileName = "PreviewQuotationPDF_" + QuotationId + ".pdf";
            byte[] fileBytes = System.IO.File.ReadAllBytes(webRootPath + fileName);
            return File(fileBytes, "application/x-msdownload", fileName);
        }
        public FileResult StandardQuotationDownload(int QuotationId)
        {
            string webRootPath = _hostingEnvironment.WebRootPath + "\\Common\\PDF\\StandardQuotation\\";
            string fileName = "StandardQuotation_" + QuotationId + ".pdf";
            byte[] fileBytes = System.IO.File.ReadAllBytes(webRootPath + fileName);
            return File(fileBytes, "application/x-msdownload", fileName);
        }
        public FileResult OptionalQuotationDownload(int QuotationId)
        {
            string webRootPath = _hostingEnvironment.WebRootPath + "\\Common\\PDF\\OptionalQuotationPDF\\";
            string fileName = "OptionalQuotationPDF_" + QuotationId + ".pdf";
            byte[] fileBytes = System.IO.File.ReadAllBytes(webRootPath + fileName);
            return File(fileBytes, "application/x-msdownload", fileName);
        }

        public string ExportQuotation(int QuotationId)
        {

            var quotation = (from r in _stemDbContext.Quotations where r.Id == QuotationId select r).FirstOrDefault();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());



            // Header Section
            string header = string.Empty;
            var headerfinder = builder.GetFileProvider().GetFileInfo("Stdheader.html");
            string headerpath = headerfinder.PhysicalPath;
            using (StreamReader reader = new StreamReader(headerpath, System.Text.Encoding.UTF8))
            {
                header = reader.ReadToEnd();
            }
            header = header.Replace("{Date}", quotation.CreationTime.ToString("dd-MMM-yyyy"));
            header = header.Replace("{Ref_No}", quotation.RefNo);
            var Inquiryy = (from r in _stemDbContext.Inquirys where r.Id == quotation.InquiryId select r).FirstOrDefault();

            header = header.Replace("{Inq_SubMmissionId}", Inquiryy.SubMmissionId);
            if (quotation.NewCompanyId != null)
            {
                var company = (from r in _stemDbContext.NewCompanys where r.Id == quotation.NewCompanyId select r).FirstOrDefault();
                header = header.Replace("{Client}", company.Name);
                header = header.Replace("{CustomerID}", company.CustomerId);
                header = header.Replace("{TRN}", company.TRNnumber);
                header = header.Replace("{LcNumber}", Inquiryy.LCNumber ?? "");
                header = header.Replace("{Title}", quotation.Name ?? "Quotation");
            }
            if (quotation.RFQNo != null && quotation.RFQNo != "")
            {
                header = header.Replace("{RFQ_No}", "<td style='text-align:left;border: 0px solid transparent;color: #ce4646;'>" + quotation.RFQNo + "</td>");
            }
            else
            {
                header = header.Replace("{RFQ_No}", "");
            }
            if (quotation.RefQNo != null && quotation.RefQNo != "")
            {
                header = header.Replace("{RefQNo}", "<th style='text-align:center;border: 2px solid #9a9a9a;'><b>" + quotation.RefQNo + "</b></th>");
            }
            else
            {
                header = header.Replace("{RefQNo}", "");
            }
            if (quotation.AttentionContactId != null)
            {
                try
                {
                    var contact = (from r in _stemDbContext.NewContacts where r.Id == quotation.AttentionContactId select r).FirstOrDefault();
                    var salutation = (from r in _stemDbContext.NewContacts where r.Id == quotation.AttentionContactId select r.TitleOfCourtesies).FirstOrDefault();
                    var person = salutation.Name + ". " + contact.Name + " " + contact.LastName;
                    header = header.Replace("{Attention}", person);
                }
                catch (Exception ex)
                {

                }
            }
            header = header.Replace("{Telphone}", quotation.MobileNumber ?? "");
            header = header.Replace("{Email}", quotation.Email ?? "");
            var location = (from r in _stemDbContext.Inquirys where r.Id == quotation.InquiryId select r.Locations).FirstOrDefault();
            header = header.Replace("{Address}", location != null ? location.LocationName : "Dubai, UAE");
            if (quotation.SalesPersonId != null)
            {
                var user = (from r in _stemDbContext.Users where r.Id == quotation.SalesPersonId select r).FirstOrDefault();
                header = header.Replace("{Sales_Person}", user.Name);
            }
            else
            {
                header = header.Replace("{Sales_Person}", "");
            }

            // body Section
            string body = string.Empty;
            var filefinder = builder.GetFileProvider().GetFileInfo("StdQuotation.html");
            string filepath = filefinder.PhysicalPath;
            using (StreamReader reader = new StreamReader(filepath, System.Text.Encoding.UTF8))
            {
                body = reader.ReadToEnd();
            }
            if (quotation.NewCompanyId != null)
            {
                var company = (from r in _stemDbContext.NewCompanys where r.Id == quotation.NewCompanyId select r).FirstOrDefault();
                body = body.Replace("{Client}", company.Name);
                body = body.Replace("{CustomerID}", company.CustomerId);
                body = body.Replace("{TRN}", company.TRNnumber);
                body = body.Replace("{LcNumber}", Inquiryy.LCNumber ?? "");
            }
            if (quotation.AttentionContactId != null)
            {
                //try
                //{
                //    var contact = (from r in _stemDbContext.NewContacts where r.Id == quotation.AttentionContactId select r).FirstOrDefault();
                //    var salutation = (from r in _stemDbContext.NewContacts where r.Id == quotation.AttentionContactId select r.TitleOfCourtesies).FirstOrDefault();
                //    var person = salutation.Name + ": " + contact.Name + " " + contact.LastName;
                //    body = body.Replace("{Attention}", person);
                //}
                //catch (Exception ex)
                //{

                //}


            }

            var quotationproducttotalamt = (from a in _stemDbContext.QuotationProducts where a.QuotationId == QuotationId && a.IsDeleted == false select a).ToArray();

            decimal totalamt = (quotation.Optional == true && quotationproducttotalamt.Count() == 0) ? quotation.Total : 0;
            if (quotation.IsVat == true)
            {
                totalamt = totalamt + Math.Round(quotation.VatAmount, 2);
            }
            decimal totaldiscount = 0;
            decimal totalgross = (quotation.Optional == true && quotationproducttotalamt.Count() == 0) ? quotation.Total : 0;

            foreach (var dat in quotationproducttotalamt)
            {
                if (dat.Approval == true)
                {
                    totalamt = totalamt + dat.TotalAmount;
                    totaldiscount = totaldiscount + dat.OverAllDiscount;
                }
                else
                {
                    totalamt = totalamt + dat.OverAllPrice;
                }

                totalgross = totalgross + dat.OverAllPrice;
            }

            if (quotation.SalesPersonId > 0)
            {
                var user = (from r in _stemDbContext.Users where r.Id == quotation.SalesPersonId select r).FirstOrDefault();
                body = body.Replace("{CreatorName}", user.FullName);
                var userDesignation = (from r in _stemDbContext.UserDesignations where r.Id == user.UserDesignationId select r).FirstOrDefault();
                body = body.Replace("{CreatorRole}", userDesignation != null ? userDesignation.Name : "");
            }
            else if (quotation.CreatorUserId > 0)
            {
                var user = (from r in _stemDbContext.Users where r.Id == quotation.CreatorUserId select r).FirstOrDefault();
                body = body.Replace("{CreatorName}", user.FullName);
                var userDesignation = (from r in _stemDbContext.UserDesignations where r.Id == user.UserDesignationId select r).FirstOrDefault();
                body = body.Replace("{CreatorRole}", userDesignation != null ? userDesignation.Name : "");
            }


            body = body.Replace("{Date}", quotation.CreationTime.ToString("dd-MMM-yyyy"));
            body = body.Replace("{Ref_No}", quotation.RefNo);

            if (quotation.SalesPersonId != null)
            {
                var user = (from r in _stemDbContext.Users where r.Id == quotation.SalesPersonId select r).FirstOrDefault();
                body = body.Replace("{Sales_Person}", user.Name);
            }
            else
            {
                body = body.Replace("{Sales_Person}", "");
            }
            var userquery = (from q in _stemDbContext.Quotations
                             join enq in _stemDbContext.Inquirys on q.InquiryId equals enq.Id
                             join enqDetail in _stemDbContext.EnquiryDetails on enq.Id equals enqDetail.InquiryId
                             join team in _stemDbContext.Team on enqDetail.TeamId equals team.Id
                             join usr in _stemDbContext.Users on team.SalesManagerId equals usr.Id
                             where q.Id == QuotationId
                             select usr
                                ).FirstOrDefault();

            if (userquery != null)
            {
                body = body.Replace("{SM}", userquery.FullName);
            }
            else
            {
                body = body.Replace("{SM}", "");
            }

            var Inquiry = (from r in _stemDbContext.Inquirys where r.Id == quotation.InquiryId select r).FirstOrDefault();
            body = body.Replace("{Inq_SubMmissionId}", Inquiry.SubMmissionId);

            body = body.Replace("{Telphone}", quotation.MobileNumber != null ? quotation.MobileNumber : "");
            body = body.Replace("{Email}", quotation.Email != null ? quotation.Email : "");
            body = body.Replace("{Address}", location != null ? location.LocationName : "Dubai, UAE");

            body = body.Replace("{Terms And Conditions}", quotation.TermsandCondition);
            if(quotation.OverAllDiscountAmount > 0 && quotation.Negotiation == true)
            {
                totalamt = totalamt - quotation.OverAllDiscountAmount;
            }
            body = body.Replace("{Total_Amount}", totalamt.ToString("N", new CultureInfo("en-US")));
          
            var decPlaces = (int)(((decimal)totalamt % 1) * 100);
            body = body.Replace("{fill}", decPlaces > 0 ? decPlaces.ToString(): "0");

            if (quotation.VatAmount > 0 && quotation.IsVat == true)
            {
                body = body.Replace("{Vat_Amount}", "<tr style='page -break-inside:avoid;page-break-after:auto;background:rgb(217,217,217);'><td colspan='2' style='text-align:right;border:2px solid #000;'> " + quotation.Vat + "% Vat </td><td colspan='1' style='text-align:right;border:2px solid #000;'>" + quotation.VatAmount.ToString("N", new CultureInfo("en-US")) + "</td></tr>");
            }
            else
            {
                body = body.Replace("{Vat_Amount}", "");
            }

            float Discount = 0;
            if (totalgross > 0)
                Discount = (float)Math.Round((totaldiscount * 100) / totalgross, 2);
            if (totaldiscount > 0)
            {
                body = body.Replace("{Discount_Amount}", "<tr><td colspan='2' style='text-align:right;border:2px solid #000;'> Total Discount Amount</td><td colspan='1' style='text-align:right;border:2px solid #000;'><Span style='color:red'>  </span>" + totaldiscount.ToString("N", new CultureInfo("en-US")) + "</td></tr>");
            }
            else
            {
                body = body.Replace("{Discount_Amount}", "");
            }
            if (quotation.OverAllDiscountAmount > 0)
            {
                body = body.Replace("{Discount}", "<tr><td colspan='2' style='text-align:right;border:2px solid #000;background:rgb(217,217,217)'> Negotiated Amount</td><td colspan='1' style='text-align:right;border:2px solid #000;background:rgb(217,217,217);'>" + quotation.OverAllDiscountAmount.ToString("N", new CultureInfo("en-US")) + "</td></tr>");
            }
            else
            {
                body = body.Replace("{Discount}", "");
            }


            decimal TotalNew = totalgross - totaldiscount; ;  // 24.02.18           
            body = body.Replace("{Total_AfterDiscount}", TotalNew.ToString("N", new CultureInfo("en-US")));

            body = body.Replace("{Gross_Amount}", totalgross.ToString("N", new CultureInfo("en-US")));

            NumberToWordsConverter converter = new NumberToWordsConverter();
            long daet = decimal.ToInt64(totalamt);
            string TotalString = converter.Convert(daet);
            body = body.Replace("{Amount_Words}", TotalString);
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("sp_QuotationProductsPdf", conn);
                sqlComm.Parameters.AddWithValue("@QuotationId", QuotationId);
                sqlComm.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {
                    da.Fill(ds);
                }
            }

            var ProductContent = ds.Rows[0]["data"].ToString();

            body = body.Replace("{Product_Content}", ProductContent);


            var rootpath = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");

            Configuration = rootpath.Build();

            var root = Configuration["App:ServerRootAddress"];
            header = header.Replace("{logourl}", root + "/Common/Images/logopdf.png");
            body = body.Replace("{url}", root);

            string QuotationPath = _hostingEnvironment.WebRootPath + "\\Common\\PDF\\StandardPDF\\";

            // Footer Section
            string footer = string.Empty;
            var footerfinder = builder.GetFileProvider().GetFileInfo("Stdfooter.html");
            string footerpath = footerfinder.PhysicalPath;
            using (StreamReader reader = new StreamReader(footerpath, System.Text.Encoding.UTF8))
            {
                footer = reader.ReadToEnd();
            }
            footer = footer.Replace("{footer_url}", root + "/Common/Images/footer.png");

            if (!Directory.Exists(QuotationPath))
            {
                Directory.CreateDirectory(QuotationPath);
            }
            string fileName = "StandardQuotationPDF_" + QuotationId + ".pdf"; ;

            if (System.IO.File.Exists(QuotationPath + fileName))
            {
                try
                {
                    System.IO.File.Delete(QuotationPath + fileName);
                }
                catch (System.IO.IOException e)
                {
                }
            }
            var generator = new NReco.PdfGenerator.HtmlToPdfConverter()
            {
                PageHeaderHtml = header,
                PageFooterHtml = footer
            };          
            try
            {
                generator.Margins = new PageMargins { Top = 70, Bottom = 20, Left = 10, Right = 10 };
                generator.Orientation = NReco.PdfGenerator.PageOrientation.Portrait;
                generator.Zoom = 0.9f;
                generator.CustomWkHtmlArgs = " --load-media-error-handling ignore ";
                generator.Size = NReco.PdfGenerator.PageSize.Letter;
                generator.GeneratePdf(body, null, QuotationPath + fileName);
            }
            catch (Exception ex)
            {

            }
            header = header.Replace("line-height: 6px", "line-height: 18px");
            body = body.Replace("margin-top:-20px;", "margin-top:0px;");
            body = body.Replace("{header_Content}", header);
            body = body.Replace("{footer_Content}", footer);
            return body;
        }
        public string ExportPhotoEmphasis(int QuotationId)
        {
            var quotation = (from r in _stemDbContext.Quotations where r.Id == QuotationId select r).FirstOrDefault();
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory());

            string header = string.Empty;
            var headerfinder = builder.GetFileProvider().GetFileInfo("PhotoEmphasisHeader.html");
            string headerpath = headerfinder.PhysicalPath;
            using (StreamReader reader = new StreamReader(headerpath, System.Text.Encoding.UTF8))
            {
                header = reader.ReadToEnd();
            }
            if (quotation.NewCompanyId != null)
            {
                var company = (from r in _stemDbContext.NewCompanys where r.Id == quotation.NewCompanyId select r).FirstOrDefault();
                header = header.Replace("{Client}", company.Name);
                header = header.Replace("{CustomerID}", company.CustomerId);
                header = header.Replace("{TRN}", company.TRNnumber);
                header = header.Replace("{Title}", quotation.Name ?? "Quotation");

            }
            if (quotation.RFQNo != null && quotation.RFQNo != "")
            {
                header = header.Replace("{RFQ_No}", "<td style='text-align:left;border: 0px solid transparent;color: #ce4646;'>" + quotation.RFQNo + "</td>");
            }
            else
            {
                header = header.Replace("{RFQ_No}", "");
            }
            if (quotation.RefQNo != null && quotation.RefQNo != "")
            {
                header = header.Replace("{RefQNo}", "<th style='text-align:center;border: 2px solid #9a9a9a;'><b>" + quotation.RefQNo + "</b></th>");
            }
            else
            {
                header = header.Replace("{RefQNo}", "");
            }
            if (quotation.AttentionContactId != null)
            {
                try
                {
                    var contact = (from r in _stemDbContext.NewContacts where r.Id == quotation.AttentionContactId select r).FirstOrDefault();
                    var salutation = (from r in _stemDbContext.NewContacts where r.Id == quotation.AttentionContactId select r.TitleOfCourtesies).FirstOrDefault();
                    var person = salutation.Name + ". " + contact.Name + " " + contact.LastName;
                    header = header.Replace("{Attention}", person);
                }
                catch (Exception ex)
                {

                }
            }
            header = header.Replace("{Date}", quotation.CreationTime.ToString("dd-MMM-yyyy"));
            header = header.Replace("{Ref_No}", quotation.RefNo);
            if (quotation.SalesPersonId != null)
            {
                var user = (from r in _stemDbContext.Users where r.Id == quotation.SalesPersonId select r).FirstOrDefault();
                header = header.Replace("{Sales_Person}", user.Name);
            }
            else
            {
                header = header.Replace("{Sales_Person}", "");
            }
            header = header.Replace("{Telphone}", quotation.MobileNumber != null ? quotation.MobileNumber : "");
            header = header.Replace("{Email}", quotation.Email != null ? quotation.Email : "");
            var location = (from r in _stemDbContext.Inquirys where r.Id == quotation.InquiryId select r.Locations).FirstOrDefault();
            header = header.Replace("{Address}", location != null ? location.LocationName : "Dubai, UAE");
            string body = string.Empty;
            var filefinder = builder.GetFileProvider().GetFileInfo("PhotoEmphasis.html");
            string filepath = filefinder.PhysicalPath;
            using (StreamReader reader = new StreamReader(filepath, System.Text.Encoding.UTF8))
            {
                body = reader.ReadToEnd();
            }
            var quotationproducttotalamt = (from a in _stemDbContext.QuotationProducts where a.QuotationId == QuotationId && a.IsDeleted == false select a).ToArray();

            var totalamt = (quotation.Optional == true && quotationproducttotalamt.Count() == 0) ? quotation.Total : 0;
            if (quotation.IsVat == true)
            {
                totalamt = totalamt + Math.Round(quotation.VatAmount,2);
            }
            decimal totaldiscount = 0;
            var totalgross = (quotation.Optional == true && quotationproducttotalamt.Count() == 0) ? quotation.Total : 0;

            foreach (var dat in quotationproducttotalamt)
            {
                if (dat.Approval == true)
                {
                    totalamt = totalamt + dat.TotalAmount;
                    totaldiscount = totaldiscount + dat.OverAllDiscount;
                }
                else
                {
                    totalamt = totalamt + dat.OverAllPrice;
                }

                totalgross = totalgross + dat.OverAllPrice;
            }
            if (quotation.SalesPersonId > 0)
            {
                var user = (from r in _stemDbContext.Users where r.Id == quotation.SalesPersonId select r).FirstOrDefault();
                body = body.Replace("{CreatorName}", user.FullName);
                var userDesignation = (from r in _stemDbContext.UserDesignations where r.Id == user.UserDesignationId select r).FirstOrDefault();
                body = body.Replace("{CreatorRole}", userDesignation != null ? userDesignation.Name : "");
            }
            else if (quotation.CreatorUserId > 0)
            {
                var user = (from r in _stemDbContext.Users where r.Id == quotation.CreatorUserId select r).FirstOrDefault();
                body = body.Replace("{CreatorName}", user.FullName);
                var userDesignation = (from r in _stemDbContext.UserDesignations where r.Id == user.UserDesignationId select r).FirstOrDefault();
                body = body.Replace("{CreatorRole}", userDesignation != null ? userDesignation.Name : "");
            }

            var userquery = (from q in _stemDbContext.Quotations
                             join enq in _stemDbContext.Inquirys on q.InquiryId equals enq.Id
                             join enqDetail in _stemDbContext.EnquiryDetails on enq.Id equals enqDetail.InquiryId
                             join team in _stemDbContext.Team on enqDetail.TeamId equals team.Id
                             join usr in _stemDbContext.Users on team.SalesManagerId equals usr.Id
                             where q.Id == QuotationId
                             select usr
                              ).FirstOrDefault();

            if (userquery != null)
            {
                body = body.Replace("{SM}", userquery.FullName);
            }
            else
            {
                body = body.Replace("{SM}", "");
            }
            var Inquiry = (from r in _stemDbContext.Inquirys where r.Id == quotation.InquiryId select r).FirstOrDefault();
            body = body.Replace("{Inq_SubMmissionId}", Inquiry.SubMmissionId);

            body = body.Replace("{Terms And Conditions}", quotation.TermsandCondition);
            if (quotation.OverAllDiscountAmount > 0 && quotation.Negotiation == true)
            {
                totalamt = totalamt - quotation.OverAllDiscountAmount;
            }
            body = body.Replace("{Total_Amount}", totalamt.ToString("N", new CultureInfo("en-US")));

            var decPlaces = (int)(((decimal)totalamt % 1) * 100);
            body = body.Replace("{fill}", decPlaces > 0 ? decPlaces.ToString() : "0");

            if (quotation.VatAmount > 0 && quotation.IsVat == true)
            {
                body = body.Replace("{Vat_Amount}", "<tr style='page -break-inside:avoid;page-break-after:auto;background:rgb(217,217,217);'><td colspan='2' style='text-align:right;border:2px solid #000;'> " + quotation.Vat + "% Vat </td><td colspan='1' style='text-align:right;border:2px solid #000;'>" + quotation.VatAmount.ToString("N", new CultureInfo("en-US")) + "</td></tr>");
            }
            else
            {
                body = body.Replace("{Vat_Amount}", "");
            }
            float Discount = 0;
            if (totalgross > 0)
                Discount = (float)Math.Round((totaldiscount * 100) / totalgross, 2);
            if (totaldiscount > 0)
            {
                body = body.Replace("{Discount_Amount}", "<tr><td colspan='2' style='text-align:right;border:2px solid #000;'> Total Discount Amount </td><td colspan='1' style='text-align:right;border:2px solid #000;'><Span style='color:red'>  </span>" + totaldiscount.ToString("N", new CultureInfo("en-US")) + "</td></tr>");
            }
            else
            {
                body = body.Replace("{Discount_Amount}", "");
            }
            if (quotation.OverAllDiscountAmount > 0)
            {
                body = body.Replace("{Discount}", "<tr><td colspan='2' style='text-align:right;border:2px solid #000;background:rgb(217,217,217)'> Negotiated Amount</td><td colspan='1' style='text-align:right;border:2px solid #000;background:rgb(217,217,217);'>" + quotation.OverAllDiscountAmount.ToString("N", new CultureInfo("en-US")) + "</td></tr>");
            }
            else
            {
                body = body.Replace("{Discount}", "");
            }

            decimal TotalNew = totalgross - totaldiscount; ;
            body = body.Replace("{Total_AfterDiscount}", TotalNew.ToString("N", new CultureInfo("en-US")));

            body = body.Replace("{Gross_Amount}", totalgross.ToString("N", new CultureInfo("en-US")));

            NumberToWordsConverter converter = new NumberToWordsConverter();

            long daet = decimal.ToInt64(totalamt);
            string TotalString = converter.Convert(daet);
            body = body.Replace("{Amount_Words}", TotalString);
            header = header.Replace("{LcNumber}", Inquiry.LCNumber ?? "");
            try
            {
                ConnectionAppService db = new ConnectionAppService();
                DataTable ds = new DataTable();
                using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
                {
                    SqlCommand sqlComm = new SqlCommand("sp_ExportPhotoEmphasisPdf", conn);
                    sqlComm.Parameters.AddWithValue("@QuotationId", QuotationId);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                    {
                        da.Fill(ds);
                    }
                }
                var ProductContent = ds.Rows[0]["data"].ToString();
                body = body.Replace("{Product_Content}", ProductContent);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }


            var rootpath = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = rootpath.Build();
            var root = Configuration["App:ServerRootAddress"];
            body = body.Replace("{url}", root);
            header = header.Replace("{logourl}", root + "/Common/Images/logopdf.png");

            //string QuotationPath = @"C:\Bafco\ExportPhotoEmphasisPdf\";
            string QuotationPath = _hostingEnvironment.WebRootPath + "\\Common\\PDF\\PhotoEmphasisPDF\\";

            string footer = string.Empty;
            var footerfinder = builder.GetFileProvider().GetFileInfo("Stdfooter.html");
            string footerpath = footerfinder.PhysicalPath;
            using (StreamReader reader = new StreamReader(footerpath, System.Text.Encoding.UTF8))
            {
                footer = reader.ReadToEnd();
            }
            footer = footer.Replace("{footer_url}", root + "/Common/Images/footer.png");

            if (!Directory.Exists(QuotationPath))
            {
                Directory.CreateDirectory(QuotationPath);
            }
            string fileName = "QuotationPhotoEmphasisPDF_" + QuotationId + ".pdf"; ;

            if (System.IO.File.Exists(QuotationPath + fileName))
            {
                try
                {
                    System.IO.File.Delete(QuotationPath + fileName);
                }
                catch (Exception ex)
                {
                }
            }
            var generator = new NReco.PdfGenerator.HtmlToPdfConverter()
            {
                PageHeaderHtml = header,
                PageFooterHtml = footer
            };
            try
            {
                generator.Margins = new PageMargins { Top = 70, Bottom = 20, Left = 10, Right = 10 };
                generator.Orientation = NReco.PdfGenerator.PageOrientation.Portrait;
                generator.Zoom = 0.9f;
                generator.CustomWkHtmlArgs = " --load-media-error-handling ignore ";
                generator.Size = NReco.PdfGenerator.PageSize.Letter;
                generator.GeneratePdf(body, null, QuotationPath + fileName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            header = header.Replace("line-height: 6px", "line-height: 18px");
            body = body.Replace("margin-top:-20px;", "margin-top:0px;");
            body = body.Replace("{header_Content}", header);
            body = body.Replace("{footer_Content}", footer);
            return body;
        }
        public string QuotationProductCategory(int QuotationId)
        {
            var quotation = (from r in _stemDbContext.Quotations where r.Id == QuotationId select r).FirstOrDefault();
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory());

            string header = string.Empty;
            var headerfinder = builder.GetFileProvider().GetFileInfo("ProductCategoryHeader.html");
            string headerpath = headerfinder.PhysicalPath;
            using (StreamReader reader = new StreamReader(headerpath, System.Text.Encoding.UTF8))
            {
                header = reader.ReadToEnd();
            }

            if (quotation.NewCompanyId != null)
            {
                var company = (from r in _stemDbContext.NewCompanys where r.Id == quotation.NewCompanyId select r).FirstOrDefault();
                header = header.Replace("{Client}", company.Name);
                header = header.Replace("{CustomerID}", company.CustomerId);
                header = header.Replace("{TRN}", company.TRNnumber);
                header = header.Replace("{Title}", quotation.Name ?? "Quotation");


            }
            if (quotation.RFQNo != null && quotation.RFQNo != "")
            {
                header = header.Replace("{RFQ_No}", "<td style='text-align:left;border: 0px solid transparent;color: #ce4646;'>" + quotation.RFQNo + "</td>");
            }
            else
            {
                header = header.Replace("{RFQ_No}", "");
            }
            if (quotation.RefQNo != null && quotation.RefQNo != "")
            {
                header = header.Replace("{RefQNo}", "<th style='text-align:center;border: 2px solid #9a9a9a;'> <b>" + quotation.RefQNo + "</b></th>");
            }
            else
            {
                header = header.Replace("{RefQNo}", "");
            }
            if (quotation.AttentionContactId != null)
            {
                try
                {
                    var contact = (from r in _stemDbContext.NewContacts where r.Id == quotation.AttentionContactId select r).FirstOrDefault();
                    var salutation = (from r in _stemDbContext.NewContacts where r.Id == quotation.AttentionContactId select r.TitleOfCourtesies).FirstOrDefault();
                    var person = salutation.Name + ". " + contact.Name + " " + contact.LastName;
                    header = header.Replace("{Attention}", person);
                }
                catch (Exception ex)
                {

                }
            }
            header = header.Replace("{Date}", quotation.CreationTime.ToString("dd-MMM-yyyy"));
            header = header.Replace("{Ref_No}", quotation.RefNo);
            if (quotation.SalesPersonId != null)
            {
                var user = (from r in _stemDbContext.Users where r.Id == quotation.SalesPersonId select r).FirstOrDefault();
                header = header.Replace("{Sales_Person}", user.Name);
            }
            else
            {
                header = header.Replace("{Sales_Person}", "");
            }
            header = header.Replace("{Telphone}", quotation.MobileNumber != null ? quotation.MobileNumber : "");
            header = header.Replace("{Email}", quotation.Email != null ? quotation.Email : "");
            var location = (from r in _stemDbContext.Inquirys where r.Id == quotation.InquiryId select r.Locations).FirstOrDefault();
            header = header.Replace("{Address}", location != null ? location.LocationName : "Dubai, UAE");
            string body = string.Empty;
            var filefinder = builder.GetFileProvider().GetFileInfo("QuotationProductCategory.html");

            string filepath = filefinder.PhysicalPath;
            using (StreamReader reader = new StreamReader(filepath, System.Text.Encoding.UTF8))
            {
                body = reader.ReadToEnd();
            }

            var quotationproducttotalamt = (from a in _stemDbContext.QuotationProducts where a.QuotationId == QuotationId && a.IsDeleted == false select a).ToArray();

            var totalamt = (quotation.Optional == true && quotationproducttotalamt.Count() == 0) ? quotation.Total : 0;
            if (quotation.IsVat == true)
            {
                totalamt = totalamt + Math.Round(quotation.VatAmount,2);
            }
            decimal totaldiscount = 0;
            var totalgross = (quotation.Optional == true && quotationproducttotalamt.Count() == 0) ? quotation.Total : 0;

            foreach (var dat in quotationproducttotalamt)
            {
                if (dat.Approval == true)
                {
                    totalamt = totalamt + dat.TotalAmount;
                    totaldiscount = totaldiscount + dat.OverAllDiscount;
                }
                else
                {
                    totalamt = totalamt + dat.OverAllPrice;
                }

                totalgross = totalgross + dat.OverAllPrice;
            }

            if (quotation.SalesPersonId > 0)
            {
                var user = (from r in _stemDbContext.Users where r.Id == quotation.SalesPersonId select r).FirstOrDefault();
                body = body.Replace("{CreatorName}", user.FullName);
                var userDesignation = (from r in _stemDbContext.UserDesignations where r.Id == user.UserDesignationId select r).FirstOrDefault();
                body = body.Replace("{CreatorRole}", userDesignation != null ? userDesignation.Name : "");
            }
            else if (quotation.CreatorUserId > 0)
            {
                var user = (from r in _stemDbContext.Users where r.Id == quotation.CreatorUserId select r).FirstOrDefault();
                body = body.Replace("{CreatorName}", user.FullName);
                var userDesignation = (from r in _stemDbContext.UserDesignations where r.Id == user.UserDesignationId select r).FirstOrDefault();
                body = body.Replace("{CreatorRole}", userDesignation != null ? userDesignation.Name : "");
            }

            var userquery = (from q in _stemDbContext.Quotations
                             join enq in _stemDbContext.Inquirys on q.InquiryId equals enq.Id
                             join enqDetail in _stemDbContext.EnquiryDetails on enq.Id equals enqDetail.InquiryId
                             join team in _stemDbContext.Team on enqDetail.TeamId equals team.Id
                             join usr in _stemDbContext.Users on team.SalesManagerId equals usr.Id
                             where q.Id == QuotationId
                             select usr
                                ).FirstOrDefault();

            if (userquery != null)
            {
                body = body.Replace("{SM}", userquery.FullName);
            }
            else
            {
                body = body.Replace("{SM}", "");
            }

            var Inquiry = (from r in _stemDbContext.Inquirys where r.Id == quotation.InquiryId select r).FirstOrDefault();
            body = body.Replace("{Inq_SubMmissionId}", Inquiry.SubMmissionId);

            body = body.Replace("{Terms And Conditions}", quotation.TermsandCondition);
            if (quotation.OverAllDiscountAmount > 0 && quotation.Negotiation == true)
            {
                totalamt = totalamt - quotation.OverAllDiscountAmount;
            }
            body = body.Replace("{Total_Amount}", totalamt.ToString("N", new CultureInfo("en-US")));

            var decPlaces = (int)(((decimal)totalamt % 1) * 100);
            body = body.Replace("{fill}", decPlaces > 0 ? decPlaces.ToString() : "0");

            if (quotation.VatAmount > 0 && quotation.IsVat == true)
            {
                body = body.Replace("{Vat_Amount}", "<tr style='page -break-inside:avoid;page-break-after:auto;background:rgb(217,217,217);'><td colspan='2' style='text-align:right;border:2px solid #000;'> " + quotation.Vat + "% Vat </td><td colspan='1' style='text-align:right;border:2px solid #000;'>" + quotation.VatAmount.ToString("N", new CultureInfo("en-US")) + "</td></tr>");
            }
            else
            {
                body = body.Replace("{Vat_Amount}", "");
            }
            float Discount = 0;
            if (totalgross > 0)
                Discount = (float)Math.Round((totaldiscount * 100) / totalgross, 2);

            if (totaldiscount > 0)
            {
                body = body.Replace("{Discount_Amount}", "<tr><td colspan='2' style='text-align:right;border:2px solid #000;'> Total Discount Amount</td><td colspan='1' style='text-align:right;border:2px solid #000;'><Span style='color:red'>  </span>" + totaldiscount.ToString("N", new CultureInfo("en-US")) + "</td></tr>");
            }
            else
            {
                body = body.Replace("{Discount_Amount}", "");
            }
            if (quotation.OverAllDiscountAmount > 0)
            {
                body = body.Replace("{Discount}", "<tr><td colspan='2' style='text-align:right;border:2px solid #000;background:rgb(217,217,217)'> Negotiated Amount</td><td colspan='1' style='text-align:right;border:2px solid #000;background:rgb(217,217,217);'>" + quotation.OverAllDiscountAmount.ToString("N", new CultureInfo("en-US")) + "</td></tr>");
            }
            else
            {
                body = body.Replace("{Discount}", "");
            }
            decimal TotalNew = totalgross - totaldiscount; ;
            body = body.Replace("{Total_AfterDiscount}", TotalNew.ToString("N", new CultureInfo("en-US")));
            body = body.Replace("{Gross_Amount}", totalgross.ToString("N", new CultureInfo("en-US")));

            NumberToWordsConverter converter = new NumberToWordsConverter();

            long daet = decimal.ToInt64(totalamt);
            string TotalString = converter.Convert(daet);
            body = body.Replace("{Amount_Words}", TotalString);

            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("sp_QuotationProductCategoryPdf", conn);
                sqlComm.Parameters.AddWithValue("@QuotationId", QuotationId);
                sqlComm.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {
                    da.Fill(ds);
                }
            }
            var ProductContent = ds.Rows[0]["data"].ToString();
            body = body.Replace("{Product_Content}", ProductContent);
            header = header.Replace("{LcNumber}", Inquiry.LCNumber ?? "");
            var rootpath = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = rootpath.Build();
            var root = Configuration["App:ServerRootAddress"];
            body = body.Replace("{url}", root);
            header = header.Replace("{logourl}", root + "/Common/Images/logopdf.png");

            string footer = string.Empty;
            var footerfinder = builder.GetFileProvider().GetFileInfo("Stdfooter.html");
            string footerpath = footerfinder.PhysicalPath;
            using (StreamReader reader = new StreamReader(footerpath, System.Text.Encoding.UTF8))
            {
                footer = reader.ReadToEnd();
            }
            footer = footer.Replace("{footer_url}", root + "/Common/Images/footer.png");

            //string QuotationPath = @"C:\Bafco\QuotationProductCategoryPDF\";
            string QuotationPath = _hostingEnvironment.WebRootPath + "\\Common\\PDF\\ProductCategoryPDF\\";


            if (!Directory.Exists(QuotationPath))
            {
                Directory.CreateDirectory(QuotationPath);
            }
            string fileName = "QuotationProductCategoryPDF_" + QuotationId + ".pdf"; ;

            if (System.IO.File.Exists(QuotationPath + fileName))
            {
                try
                {
                    System.IO.File.Delete(QuotationPath + fileName);
                }
                catch (System.IO.IOException e)
                {
                }
            }

            var generator = new NReco.PdfGenerator.HtmlToPdfConverter()
            {
                PageHeaderHtml = header,
                PageFooterHtml = footer
            };
            try
            {
                generator.Margins = new PageMargins { Top = 70, Bottom = 20, Left = 10, Right = 10 };
                generator.Orientation = NReco.PdfGenerator.PageOrientation.Portrait;
                generator.Zoom = 0.9f;
                generator.CustomWkHtmlArgs = " --load-media-error-handling ignore ";
                generator.Size = NReco.PdfGenerator.PageSize.Letter;
                generator.GeneratePdf(body, null, QuotationPath + fileName);
            }
            catch (Exception ex)
            {

            }
            header = header.Replace("line-height: 6px", "line-height: 18px");
            body = body.Replace("margin-top:-20px;", "margin-top:0px;");
            body = body.Replace("{header_Content}", header);
            body = body.Replace("{footer_Content}", footer);
            return body;
        }
        public string StandardQuotation(int QuotationId)
        {

            var quotation = (from r in _stemDbContext.Quotations where r.Id == QuotationId select r).FirstOrDefault();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            // Header Section
            string header = string.Empty;
            var headerfinder = builder.GetFileProvider().GetFileInfo("Header.html");
            string headerpath = headerfinder.PhysicalPath;
            using (StreamReader reader = new StreamReader(headerpath, System.Text.Encoding.UTF8))
            {
                header = reader.ReadToEnd();
            }
            header = header.Replace("{Date}", quotation.CreationTime.ToString("dd-MMM-yyyy"));
            header = header.Replace("{Ref_No}", quotation.RefNo);
            var Inquiryy = (from r in _stemDbContext.Inquirys where r.Id == quotation.InquiryId select r).FirstOrDefault();

            header = header.Replace("{Inq_SubMmissionId}", Inquiryy.SubMmissionId);
            if (quotation.NewCompanyId != null)
            {
                var company = (from r in _stemDbContext.NewCompanys where r.Id == quotation.NewCompanyId select r).FirstOrDefault();
                header = header.Replace("{Client}", company.Name);
                header = header.Replace("{CustomerID}", company.CustomerId);
                header = header.Replace("{TRN}", company.TRNnumber);
                header = header.Replace("{LcNumber}", Inquiryy.LCNumber ?? "");
                header = header.Replace("{Title}", quotation.Name ?? "Quotation");
            }
            if (quotation.RFQNo != null && quotation.RFQNo != "")
            {
                header = header.Replace("{RFQ_No}", "<td style='text-align:left;border: 0px solid transparent;color: #ce4646;'>" + quotation.RFQNo + "</td>");
            }
            else
            {
                header = header.Replace("{RFQ_No}", "");
            }
            if (quotation.RefQNo != null && quotation.RefQNo != "")
            {
                header = header.Replace("{RefQNo}", "<th style='text-align:center;border: 2px solid #9a9a9a;'><b>" + quotation.RefQNo + "</b></th>");
            }
            else
            {
                header = header.Replace("{RefQNo}", "");
            }
            if (quotation.AttentionContactId != null)
            {
                try
                {
                    var contact = (from r in _stemDbContext.NewContacts where r.Id == quotation.AttentionContactId select r).FirstOrDefault();
                    var salutation = (from r in _stemDbContext.NewContacts where r.Id == quotation.AttentionContactId select r.TitleOfCourtesies).FirstOrDefault();
                    var person = salutation.Name + ". " + contact.Name + " " + contact.LastName;
                    header = header.Replace("{Attention}", person);
                }
                catch (Exception ex)
                {

                }
            }
            header = header.Replace("{Telphone}", quotation.MobileNumber ?? "");
            header = header.Replace("{Email}", quotation.Email ?? "");
            var location = (from r in _stemDbContext.Inquirys where r.Id == quotation.InquiryId select r.Locations).FirstOrDefault();
            header = header.Replace("{Address}", location != null ? location.LocationName : "Dubai, UAE");
            if (quotation.SalesPersonId != null)
            {
                var user = (from r in _stemDbContext.Users where r.Id == quotation.SalesPersonId select r).FirstOrDefault();
                header = header.Replace("{Sales_Person}", user.Name);
            }
            else
            {
                header = header.Replace("{Sales_Person}", "");
            }

            // body Section
            string body = string.Empty;
            var filefinder = builder.GetFileProvider().GetFileInfo("StdQuotation.html");
            string filepath = filefinder.PhysicalPath;
            using (StreamReader reader = new StreamReader(filepath, System.Text.Encoding.UTF8))
            {
                body = reader.ReadToEnd();
            }
            if (quotation.NewCompanyId != null)
            {
                var company = (from r in _stemDbContext.NewCompanys where r.Id == quotation.NewCompanyId select r).FirstOrDefault();
                body = body.Replace("{Client}", company.Name);
                body = body.Replace("{CustomerID}", company.CustomerId);
                body = body.Replace("{TRN}", company.TRNnumber);

            }

            var quotationproducttotalamt = (from a in _stemDbContext.QuotationProducts where a.QuotationId == QuotationId && a.IsDeleted == false select a).ToArray();

            decimal totalamt = (quotation.Optional == true && quotationproducttotalamt.Count() == 0) ? quotation.Total : 0;
            if (quotation.IsVat == true)
            {
                totalamt = totalamt + quotation.VatAmount;
                //totalamt = totalamt + Math.Round(quotation.VatAmount);
            }
            decimal totaldiscount = 0;
            decimal totalgross = (quotation.Optional == true && quotationproducttotalamt.Count() == 0) ? quotation.Total : 0;

            foreach (var dat in quotationproducttotalamt)
            {
                if (dat.Approval == true)
                {
                    totalamt = totalamt + dat.TotalAmount;
                    totaldiscount = totaldiscount + dat.OverAllDiscount;
                }
                else
                {
                    totalamt = totalamt + dat.OverAllPrice;
                }

                totalgross = totalgross + dat.OverAllPrice;
            }

            if (quotation.SalesPersonId > 0)
            {
                var user = (from r in _stemDbContext.Users where r.Id == quotation.SalesPersonId select r).FirstOrDefault();
                body = body.Replace("{CreatorName}", user.FullName);
                var userDesignation = (from r in _stemDbContext.UserDesignations where r.Id == user.UserDesignationId select r).FirstOrDefault();
                body = body.Replace("{CreatorRole}", userDesignation != null ? userDesignation.Name : "");
            }
            else if (quotation.CreatorUserId > 0)
            {
                var user = (from r in _stemDbContext.Users where r.Id == quotation.CreatorUserId select r).FirstOrDefault();
                body = body.Replace("{CreatorName}", user.FullName);
                var userDesignation = (from r in _stemDbContext.UserDesignations where r.Id == user.UserDesignationId select r).FirstOrDefault();
                body = body.Replace("{CreatorRole}", userDesignation != null ? userDesignation.Name : "");
            }


            body = body.Replace("{Date}", quotation.CreationTime.ToString("dd-MMM-yyyy"));
            body = body.Replace("{Ref_No}", quotation.RefNo);

            if (quotation.SalesPersonId != null)
            {
                var user = (from r in _stemDbContext.Users where r.Id == quotation.SalesPersonId select r).FirstOrDefault();
                body = body.Replace("{Sales_Person}", user.Name);
            }
            else
            {
                body = body.Replace("{Sales_Person}", "");
            }
            var userquery = (from q in _stemDbContext.Quotations
                             join enq in _stemDbContext.Inquirys on q.InquiryId equals enq.Id
                             join enqDetail in _stemDbContext.EnquiryDetails on enq.Id equals enqDetail.InquiryId
                             join team in _stemDbContext.Team on enqDetail.TeamId equals team.Id
                             join usr in _stemDbContext.Users on team.SalesManagerId equals usr.Id
                             where q.Id == QuotationId
                             select usr
                                ).FirstOrDefault();

            if (userquery != null)
            {
                body = body.Replace("{SM}", userquery.FullName);
            }
            else
            {
                body = body.Replace("{SM}", "");
            }

            var Inquiry = (from r in _stemDbContext.Inquirys where r.Id == quotation.InquiryId select r).FirstOrDefault();
            body = body.Replace("{Inq_SubMmissionId}", Inquiry.SubMmissionId);

            body = body.Replace("{Telphone}", quotation.MobileNumber != null ? quotation.MobileNumber : "");
            body = body.Replace("{Email}", quotation.Email != null ? quotation.Email : "");
            body = body.Replace("{Address}", location != null ? location.LocationName : "Dubai, UAE");

            body = body.Replace("{Terms And Conditions}", quotation.TermsandCondition);
            if (quotation.OverAllDiscountAmount > 0 && quotation.Negotiation == true)
            {
                totalamt = totalamt - quotation.OverAllDiscountAmount;
            }
            body = body.Replace("{Total_Amount}", totalamt.ToString("N", new CultureInfo("en-US")));

            var decPlaces = (int)(((decimal)totalamt % 1) * 100);
            body = body.Replace("{fill}", decPlaces > 0 ? decPlaces.ToString() : "0");

            if (quotation.VatAmount > 0 && quotation.IsVat == true)
            {
                body = body.Replace("{Vat_Amount}", "<tr style='page -break-inside:avoid;page-break-after:auto;background:rgb(217,217,217);'><td colspan='2' style='text-align:right;border:2px solid #000;'> " + quotation.Vat + "% Vat </td><td colspan='1' style='text-align:right;border:2px solid #000;'>" + quotation.VatAmount.ToString("N", new CultureInfo("en-US")) + "</td></tr>");
            }
            else
            {
                body = body.Replace("{Vat_Amount}", "");
            }

            float Discount = 0;
            if (totalgross > 0)
                Discount = (float)Math.Round((totaldiscount * 100) / totalgross, 2);
            if (totaldiscount > 0)
            {
                body = body.Replace("{Discount_Amount}", "<tr><td colspan='2' style='text-align:right;border:2px solid #000;'> Total Discount Amount</td><td colspan='1' style='text-align:right;border:2px solid #000;'><Span style='color:red'>  </span>" + totaldiscount.ToString("N", new CultureInfo("en-US")) + "</td></tr>");
            }
            else
            {
                body = body.Replace("{Discount_Amount}", "");
            }
            if (quotation.OverAllDiscountAmount > 0)
            {
                body = body.Replace("{Discount}", "<tr><td colspan='2' style='text-align:right;border:2px solid #000;background:rgb(217,217,217)'> Negotiated Amount</td><td colspan='1' style='text-align:right;border:2px solid #000;background:rgb(217,217,217);'>" + quotation.OverAllDiscountAmount.ToString("N", new CultureInfo("en-US")) + "</td></tr>");
            }
            else
            {
                body = body.Replace("{Discount}", "");
            }


            decimal TotalNew = totalgross - totaldiscount; ;  // 24.02.18           
            body = body.Replace("{Total_AfterDiscount}", TotalNew.ToString("N", new CultureInfo("en-US")));

            body = body.Replace("{Gross_Amount}", totalgross.ToString("N", new CultureInfo("en-US")));

            NumberToWordsConverter converter = new NumberToWordsConverter();
            long daet = decimal.ToInt64(totalamt);
            string TotalString = converter.Convert(daet);
            body = body.Replace("{Amount_Words}", TotalString);
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_PreviewQuotationPdf", conn);
                sqlComm.Parameters.AddWithValue("@QuotationId", QuotationId);
                sqlComm.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {
                    da.Fill(ds);
                }
            }

            var ProductContent = ds.Rows[0]["data"].ToString();

            body = body.Replace("{Product_Content}", ProductContent);

            var rootpath = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");

            Configuration = rootpath.Build();

            var root = Configuration["App:ServerRootAddress"];
            header = header.Replace("{logourl}", root + "/Common/Images/logopdf.png");
            body = body.Replace("{url}", root);

            string QuotationPath = _hostingEnvironment.WebRootPath + "\\Common\\PDF\\PreviewPDF\\";

            // Footer Section
            string footer = string.Empty;
            var footerfinder = builder.GetFileProvider().GetFileInfo("Stdfooter.html");
            string footerpath = footerfinder.PhysicalPath;
            using (StreamReader reader = new StreamReader(footerpath, System.Text.Encoding.UTF8))
            {
                footer = reader.ReadToEnd();
            }
            footer = footer.Replace("{footer_url}", root + "/Common/Images/footer.png");

            if (!Directory.Exists(QuotationPath))
            {
                Directory.CreateDirectory(QuotationPath);
            }
            string fileName = "PreviewQuotationPDF_" + QuotationId + ".pdf"; ;

            if (System.IO.File.Exists(QuotationPath + fileName))
            {
                try
                {
                    System.IO.File.Delete(QuotationPath + fileName);
                }
                catch (System.IO.IOException e)
                {
                }
            }
            var generator = new NReco.PdfGenerator.HtmlToPdfConverter()
            {
                PageHeaderHtml = header,
                PageFooterHtml = footer
            };
            try
            {
                generator.Margins = new PageMargins { Top = 70, Bottom = 20, Left = 10, Right = 10 };
                generator.Orientation = NReco.PdfGenerator.PageOrientation.Portrait;
                generator.Zoom = 0.9f;
                generator.CustomWkHtmlArgs = " --load-media-error-handling ignore ";
                generator.Size = NReco.PdfGenerator.PageSize.Letter;
                generator.GeneratePdf(body, null, QuotationPath + fileName);
            }
            catch (Exception ex)
            {

            }
            header = header.Replace("line-height: 6px", "line-height: 18px");
            body = body.Replace("margin-top:-20px;", "margin-top:0px;");
            body = body.Replace("{header_Content}", header);
            body = body.Replace("{footer_Content}", footer);
            return body;
        }
        public string ExportStandardQuotation(int QuotationId)
        {

            var quotation = (from r in _stemDbContext.Quotations where r.Id == QuotationId select r).FirstOrDefault();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            // Header Section
            string header = string.Empty;
            var headerfinder = builder.GetFileProvider().GetFileInfo("StandardQuotationHeader.html");
            string headerpath = headerfinder.PhysicalPath;
            using (StreamReader reader = new StreamReader(headerpath))
            {
                header = reader.ReadToEnd();
            }
            header = header.Replace("{Date}", quotation.CreationTime.ToString("dd-MMM-yyyy"));
            header = header.Replace("{Ref_No}", quotation.RefNo);
            var Inquiryy = (from r in _stemDbContext.Inquirys where r.Id == quotation.InquiryId select r).FirstOrDefault();

            header = header.Replace("{Inq_SubMmissionId}", Inquiryy.SubMmissionId);
            if (quotation.NewCompanyId != null)
            {
                var company = (from r in _stemDbContext.NewCompanys where r.Id == quotation.NewCompanyId select r).FirstOrDefault();
                header = header.Replace("{Client}", company.Name);
                header = header.Replace("{CustomerID}", company.CustomerId);
                header = header.Replace("{TRN}", company.TRNnumber);
                header = header.Replace("{LcNumber}", Inquiryy.LCNumber ?? "");
                header = header.Replace("{Title}", quotation.Name ?? "Quotation");
            }
            if (quotation.RFQNo != null && quotation.RFQNo != "")
            {
                header = header.Replace("{RFQ_No}", "<td style='text-align:left;border: 0px solid transparent;color: #ce4646;'>" + quotation.RFQNo + "</td>");
            }
            else
            {
                header = header.Replace("{RFQ_No}", "");
            }
            if (quotation.RefQNo != null && quotation.RefQNo != "")
            {
                header = header.Replace("{RefQNo}", "<th style='text-align:center;border: 2px solid #9a9a9a;'><b>" + quotation.RefQNo + "</b></th>");
            }
            else
            {
                header = header.Replace("{RefQNo}", "");
            }
            if (quotation.AttentionContactId != null)
            {
                try
                {
                    var contact = (from r in _stemDbContext.NewContacts where r.Id == quotation.AttentionContactId select r).FirstOrDefault();
                    var salutation = (from r in _stemDbContext.NewContacts where r.Id == quotation.AttentionContactId select r.TitleOfCourtesies).FirstOrDefault();
                    var person = salutation.Name + ". " + contact.Name + " " + contact.LastName;
                    header = header.Replace("{Attention}", person);
                }
                catch (Exception ex)
                {

                }
            }
            header = header.Replace("{Telphone}", quotation.MobileNumber ?? "");
            header = header.Replace("{Email}", quotation.Email ?? "");
            var location = (from r in _stemDbContext.Inquirys where r.Id == quotation.InquiryId select r.Locations).FirstOrDefault();
            header = header.Replace("{Address}", location != null ? location.LocationName : "Dubai, UAE");
            if (quotation.SalesPersonId != null)
            {
                var user = (from r in _stemDbContext.Users where r.Id == quotation.SalesPersonId select r).FirstOrDefault();
                header = header.Replace("{Sales_Person}", user.Name);
            }
            else
            {
                header = header.Replace("{Sales_Person}", "");
            }

            // body Section
            string body = string.Empty;
            var filefinder = builder.GetFileProvider().GetFileInfo("StdQuotation.html");
            string filepath = filefinder.PhysicalPath;
            using (StreamReader reader = new StreamReader(filepath))
            {
                body = reader.ReadToEnd();
            }
            if (quotation.NewCompanyId != null)
            {
                var company = (from r in _stemDbContext.NewCompanys where r.Id == quotation.NewCompanyId select r).FirstOrDefault();
                body = body.Replace("{Client}", company.Name);
                body = body.Replace("{CustomerID}", company.CustomerId);
                body = body.Replace("{TRN}", company.TRNnumber);

            }

            var quotationproducttotalamt = (from a in _stemDbContext.QuotationProducts where a.QuotationId == QuotationId && a.IsDeleted == false select a).ToArray();

            decimal totalamt = (quotation.Optional == true && quotationproducttotalamt.Count() == 0) ? quotation.Total : 0;
            if (quotation.IsVat == true)
            {
                totalamt = totalamt + Math.Round(quotation.VatAmount, 2);
            }
            decimal totaldiscount = 0;
            decimal totalgross = (quotation.Optional == true && quotationproducttotalamt.Count() == 0) ? quotation.Total : 0;

            foreach (var dat in quotationproducttotalamt)
            {
                if (dat.Approval == true)
                {
                    totalamt = totalamt + dat.TotalAmount;
                    totaldiscount = totaldiscount + dat.OverAllDiscount;
                }
                else
                {
                    totalamt = totalamt + dat.OverAllPrice;
                }

                totalgross = totalgross + dat.OverAllPrice;
            }

            if (quotation.SalesPersonId > 0)
            {
                var user = (from r in _stemDbContext.Users where r.Id == quotation.SalesPersonId select r).FirstOrDefault();
                body = body.Replace("{CreatorName}", user.FullName);
                var userDesignation = (from r in _stemDbContext.UserDesignations where r.Id == user.UserDesignationId select r).FirstOrDefault();
                body = body.Replace("{CreatorRole}", userDesignation != null ? userDesignation.Name : "");
            }
            else if (quotation.CreatorUserId > 0)
            {
                var user = (from r in _stemDbContext.Users where r.Id == quotation.CreatorUserId select r).FirstOrDefault();
                body = body.Replace("{CreatorName}", user.FullName);
                var userDesignation = (from r in _stemDbContext.UserDesignations where r.Id == user.UserDesignationId select r).FirstOrDefault();
                body = body.Replace("{CreatorRole}", userDesignation != null ? userDesignation.Name : "");
            }


            body = body.Replace("{Date}", quotation.CreationTime.ToString("dd-MMM-yyyy"));
            body = body.Replace("{Ref_No}", quotation.RefNo);

            if (quotation.SalesPersonId != null)
            {
                var user = (from r in _stemDbContext.Users where r.Id == quotation.SalesPersonId select r).FirstOrDefault();
                body = body.Replace("{Sales_Person}", user.Name);
            }
            else
            {
                body = body.Replace("{Sales_Person}", "");
            }
            var userquery = (from q in _stemDbContext.Quotations
                             join enq in _stemDbContext.Inquirys on q.InquiryId equals enq.Id
                             join enqDetail in _stemDbContext.EnquiryDetails on enq.Id equals enqDetail.InquiryId
                             join team in _stemDbContext.Team on enqDetail.TeamId equals team.Id
                             join usr in _stemDbContext.Users on team.SalesManagerId equals usr.Id
                             where q.Id == QuotationId
                             select usr
                                ).FirstOrDefault();

            if (userquery != null)
            {
                body = body.Replace("{SM}", userquery.FullName);
            }
            else
            {
                body = body.Replace("{SM}", "");
            }

            var Inquiry = (from r in _stemDbContext.Inquirys where r.Id == quotation.InquiryId select r).FirstOrDefault();
            body = body.Replace("{Inq_SubMmissionId}", Inquiry.SubMmissionId);

            body = body.Replace("{Telphone}", quotation.MobileNumber != null ? quotation.MobileNumber : "");
            body = body.Replace("{Email}", quotation.Email != null ? quotation.Email : "");
            body = body.Replace("{Address}", location != null ? location.LocationName : "Dubai, UAE");

            body = body.Replace("{Terms And Conditions}", quotation.TermsandCondition);
            if (quotation.OverAllDiscountAmount > 0 && quotation.Negotiation == true)
            {
                totalamt = totalamt - quotation.OverAllDiscountAmount;
            }
            body = body.Replace("{Total_Amount}", totalamt.ToString("N", new CultureInfo("en-US")));

            var decPlaces = (int)(((decimal)totalamt % 1) * 100);
            body = body.Replace("{fill}", decPlaces > 0 ? decPlaces.ToString() : "0");

            if (quotation.VatAmount > 0 && quotation.IsVat == true)
            {
                body = body.Replace("{Vat_Amount}", "<tr style='page -break-inside:avoid;page-break-after:auto;background:rgb(217,217,217);'><td colspan='2' style='text-align:right;border:2px solid #000;'> " + quotation.Vat + "% Vat </td><td colspan='1' style='text-align:right;border:2px solid #000;'>" + quotation.VatAmount.ToString("N", new CultureInfo("en-US")) + "</td></tr>");
            }
            else
            {
                body = body.Replace("{Vat_Amount}", "");
            }

            float Discount = 0;
            if (totalgross > 0)
                Discount = (float)Math.Round((totaldiscount * 100) / totalgross, 2);
            if (totaldiscount > 0)
            {
                body = body.Replace("{Discount_Amount}", "<tr><td colspan='2' style='text-align:right;border:2px solid #000;'> Total Discount Amount</td><td colspan='1' style='text-align:right;border:2px solid #000;'><Span style='color:red'>  </span>" + totaldiscount.ToString("N", new CultureInfo("en-US")) + "</td></tr>");
            }
            else
            {
                body = body.Replace("{Discount_Amount}", "");
            }
            if (quotation.OverAllDiscountAmount > 0)
            {
                body = body.Replace("{Discount}", "<tr><td colspan='2' style='text-align:right;border:2px solid #000;background:rgb(217,217,217)'> Negotiated Amount</td><td colspan='1' style='text-align:right;border:2px solid #000;background:rgb(217,217,217);'>" + quotation.OverAllDiscountAmount.ToString("N", new CultureInfo("en-US")) + "</td></tr>");
            }
            else
            {
                body = body.Replace("{Discount}", "");
            }


            decimal TotalNew = totalgross - totaldiscount;
            body = body.Replace("{Total_AfterDiscount}", TotalNew.ToString("N", new CultureInfo("en-US")));

            body = body.Replace("{Gross_Amount}", totalgross.ToString("N", new CultureInfo("en-US")));

            NumberToWordsConverter converter = new NumberToWordsConverter();
            long daet = decimal.ToInt64(totalamt);
            string TotalString = converter.Convert(daet);
            body = body.Replace("{Amount_Words}", TotalString);
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("SP_StandardQuotationPdf", conn);
                sqlComm.Parameters.AddWithValue("@QuotationId", QuotationId);
                sqlComm.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {
                    da.Fill(ds);
                }
            }

            var ProductContent = ds.Rows[0]["data"].ToString();

            body = body.Replace("{Product_Content}", ProductContent);

            var rootpath = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");

            Configuration = rootpath.Build();

            var root = Configuration["App:ServerRootAddress"];
            header = header.Replace("{logourl}", root + "/Common/Images/logopdf.png");
            body = body.Replace("{url}", root);

            string QuotationPath = _hostingEnvironment.WebRootPath + "\\Common\\PDF\\StandardQuotation\\";

            // Footer Section
            string footer = string.Empty;
            var footerfinder = builder.GetFileProvider().GetFileInfo("Stdfooter.html");
            string footerpath = footerfinder.PhysicalPath;
            using (StreamReader reader = new StreamReader(footerpath))
            {
                footer = reader.ReadToEnd();
            }
            footer = footer.Replace("{footer_url}", root + "/Common/Images/footer.png");

            if (!Directory.Exists(QuotationPath))
            {
                Directory.CreateDirectory(QuotationPath);
            }
            string fileName = "StandardQuotation_" + QuotationId + ".pdf"; ;

            if (System.IO.File.Exists(QuotationPath + fileName))
            {
                try
                {
                    System.IO.File.Delete(QuotationPath + fileName);
                }
                catch (System.IO.IOException e)
                {
                }
            }
            var generator = new NReco.PdfGenerator.HtmlToPdfConverter()
            {
                PageHeaderHtml = header,
                PageFooterHtml = footer
            };
            try
            {
                generator.Margins = new PageMargins { Top = 70, Bottom = 20, Left = 10, Right = 10 };
                generator.Orientation = NReco.PdfGenerator.PageOrientation.Portrait;
                generator.Zoom = 0.9f;
                generator.CustomWkHtmlArgs = " --load-media-error-handling ignore ";
                generator.Size = NReco.PdfGenerator.PageSize.Letter;
                generator.GeneratePdf(body, null, QuotationPath + fileName);
            }
            catch (Exception ex)
            {

            }
            header = header.Replace("line-height: 6px", "line-height: 18px");
            body = body.Replace("margin-top:-20px;", "margin-top:0px;");
            body = body.Replace("{header_Content}", header);
            body = body.Replace("{footer_Content}", footer);
            return body;
        }
        public string OptionalQuotation(int QuotationId)
        {
            var quotation = (from r in _stemDbContext.Quotations where r.Id == QuotationId select r).FirstOrDefault();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            // Header Section
            string header = string.Empty;
            var headerfinder = builder.GetFileProvider().GetFileInfo("OptionalQuotationHeader.html");
            string headerpath = headerfinder.PhysicalPath;
            using (StreamReader reader = new StreamReader(headerpath, System.Text.Encoding.UTF8))
            {
                header = reader.ReadToEnd();
            }
            header = header.Replace("{Date}", quotation.CreationTime.ToString("dd-MMM-yyyy"));
            header = header.Replace("{Ref_No}", quotation.RefNo);
            var Inquiryy = (from r in _stemDbContext.Inquirys where r.Id == quotation.InquiryId select r).FirstOrDefault();

            header = header.Replace("{Inq_SubMmissionId}", Inquiryy.SubMmissionId);
            if (quotation.NewCompanyId != null)
            {
                var company = (from r in _stemDbContext.NewCompanys where r.Id == quotation.NewCompanyId select r).FirstOrDefault();
                header = header.Replace("{Client}", company.Name);
                header = header.Replace("{CustomerID}", company.CustomerId);
                header = header.Replace("{TRN}", company.TRNnumber);
                header = header.Replace("{LcNumber}", Inquiryy.LCNumber ?? "");
                header = header.Replace("{Title}", quotation.Name ?? "Quotation");
            }
            if (quotation.RFQNo != null && quotation.RFQNo != "")
            {
                header = header.Replace("{RFQ_No}", "<td style='text-align:left;border: 0px solid transparent;color: #ce4646;'>" + quotation.RFQNo + "</td>");
            }
            else
            {
                header = header.Replace("{RFQ_No}", "");
            }
            if (quotation.RefQNo != null && quotation.RefQNo != "")
            {
                header = header.Replace("{RefQNo}", "<th style='text-align:center;border: 2px solid #9a9a9a;'><b>" + quotation.RefQNo + "</b></th>");
            }
            else
            {
                header = header.Replace("{RefQNo}", "");
            }
            if (quotation.AttentionContactId != null)
            {
                try
                {
                    var contact = (from r in _stemDbContext.NewContacts where r.Id == quotation.AttentionContactId select r).FirstOrDefault();
                    var salutation = (from r in _stemDbContext.NewContacts where r.Id == quotation.AttentionContactId select r.TitleOfCourtesies).FirstOrDefault();
                    var person = salutation.Name + ". " + contact.Name + " " + contact.LastName;
                    header = header.Replace("{Attention}", person);
                }
                catch (Exception ex)
                {

                }
            }
            header = header.Replace("{Telphone}", quotation.MobileNumber ?? "");
            header = header.Replace("{Email}", quotation.Email ?? "");
            var location = (from r in _stemDbContext.Inquirys where r.Id == quotation.InquiryId select r.Locations).FirstOrDefault();
            header = header.Replace("{Address}", location != null ? location.LocationName : "Dubai, UAE");
            if (quotation.SalesPersonId != null)
            {
                var user = (from r in _stemDbContext.Users where r.Id == quotation.SalesPersonId select r).FirstOrDefault();
                header = header.Replace("{Sales_Person}", user.Name);
            }
            else
            {
                header = header.Replace("{Sales_Person}", "");
            }

            // body Section
            string body = string.Empty;
            var filefinder = builder.GetFileProvider().GetFileInfo("StdQuotation.html");
            string filepath = filefinder.PhysicalPath;
            using (StreamReader reader = new StreamReader(filepath, System.Text.Encoding.UTF8))
            {
                body = reader.ReadToEnd();
            }
            if (quotation.NewCompanyId != null)
            {
                var company = (from r in _stemDbContext.NewCompanys where r.Id == quotation.NewCompanyId select r).FirstOrDefault();
                body = body.Replace("{Client}", company.Name);
                body = body.Replace("{CustomerID}", company.CustomerId);
                body = body.Replace("{TRN}", company.TRNnumber);

            }

            var quotationproducttotalamt = (from a in _stemDbContext.QuotationProducts where a.QuotationId == QuotationId && a.IsDeleted == false select a).ToArray();

            decimal totalamt = (quotation.Optional == true && quotationproducttotalamt.Count() == 0) ? quotation.Total : 0;
            if (quotation.IsVat == true)
            {
                totalamt = totalamt + quotation.VatAmount;
                //totalamt = totalamt + Math.Round(quotation.VatAmount);
            }
            decimal totaldiscount = 0;
            decimal totalgross = (quotation.Optional == true && quotationproducttotalamt.Count() == 0) ? quotation.Total : 0;

            foreach (var dat in quotationproducttotalamt)
            {
                if (dat.Approval == true)
                {
                    totalamt = totalamt + dat.TotalAmount;
                    totaldiscount = totaldiscount + dat.OverAllDiscount;
                }
                else
                {
                    totalamt = totalamt + dat.OverAllPrice;
                }

                totalgross = totalgross + dat.OverAllPrice;
            }

            if (quotation.SalesPersonId > 0)
            {
                var user = (from r in _stemDbContext.Users where r.Id == quotation.SalesPersonId select r).FirstOrDefault();
                body = body.Replace("{CreatorName}", user.FullName);
                var userDesignation = (from r in _stemDbContext.UserDesignations where r.Id == user.UserDesignationId select r).FirstOrDefault();
                body = body.Replace("{CreatorRole}", userDesignation != null ? userDesignation.Name : "");
            }
            else if (quotation.CreatorUserId > 0)
            {
                var user = (from r in _stemDbContext.Users where r.Id == quotation.CreatorUserId select r).FirstOrDefault();
                body = body.Replace("{CreatorName}", user.FullName);
                var userDesignation = (from r in _stemDbContext.UserDesignations where r.Id == user.UserDesignationId select r).FirstOrDefault();
                body = body.Replace("{CreatorRole}", userDesignation != null ? userDesignation.Name : "");
            }


            body = body.Replace("{Date}", quotation.CreationTime.ToString("dd-MMM-yyyy"));
            body = body.Replace("{Ref_No}", quotation.RefNo);

            if (quotation.SalesPersonId != null)
            {
                var user = (from r in _stemDbContext.Users where r.Id == quotation.SalesPersonId select r).FirstOrDefault();
                body = body.Replace("{Sales_Person}", user.Name);
            }
            else
            {
                body = body.Replace("{Sales_Person}", "");
            }
            var userquery = (from q in _stemDbContext.Quotations
                             join enq in _stemDbContext.Inquirys on q.InquiryId equals enq.Id
                             join enqDetail in _stemDbContext.EnquiryDetails on enq.Id equals enqDetail.InquiryId
                             join team in _stemDbContext.Team on enqDetail.TeamId equals team.Id
                             join usr in _stemDbContext.Users on team.SalesManagerId equals usr.Id
                             where q.Id == QuotationId
                             select usr
                                ).FirstOrDefault();

            if (userquery != null)
            {
                body = body.Replace("{SM}", userquery.FullName);
            }
            else
            {
                body = body.Replace("{SM}", "");
            }

            var Inquiry = (from r in _stemDbContext.Inquirys where r.Id == quotation.InquiryId select r).FirstOrDefault();
            body = body.Replace("{Inq_SubMmissionId}", Inquiry.SubMmissionId);

            body = body.Replace("{Telphone}", quotation.MobileNumber != null ? quotation.MobileNumber : "");
            body = body.Replace("{Email}", quotation.Email != null ? quotation.Email : "");
            body = body.Replace("{Address}", location != null ? location.LocationName : "Dubai, UAE");

            body = body.Replace("{Terms And Conditions}", quotation.TermsandCondition);
            if (quotation.OverAllDiscountAmount > 0 && quotation.Negotiation == true)
            {
                totalamt = totalamt - quotation.OverAllDiscountAmount;
            }
            body = body.Replace("{Total_Amount}", totalamt.ToString("N", new CultureInfo("en-US")));

            var decPlaces = (int)(((decimal)totalamt % 1) * 100);
            body = body.Replace("{fill}", decPlaces > 0 ? decPlaces.ToString() : "0");

            if (quotation.VatAmount > 0 && quotation.IsVat == true)
            {
                body = body.Replace("{Vat_Amount}", "<tr style='page -break-inside:avoid;page-break-after:auto;background:rgb(217,217,217);'><td colspan='2' style='text-align:right;border:2px solid #000;'> " + quotation.Vat + "% Vat </td><td colspan='1' style='text-align:right;border:2px solid #000;'>" + quotation.VatAmount.ToString("N", new CultureInfo("en-US")) + "</td></tr>");
            }
            else
            {
                body = body.Replace("{Vat_Amount}", "");
            }

            float Discount = 0;
            if (totalgross > 0)
                Discount = (float)Math.Round((totaldiscount * 100) / totalgross, 2);
            if (totaldiscount > 0)
            {
                body = body.Replace("{Discount_Amount}", "<tr><td colspan='2' style='text-align:right;border:2px solid #000;'> Total Discount Amount</td><td colspan='1' style='text-align:right;border:2px solid #000;'><Span style='color:red'>  </span>" + totaldiscount.ToString("N", new CultureInfo("en-US")) + "</td></tr>");
            }
            else
            {
                body = body.Replace("{Discount_Amount}", "");
            }
            if (quotation.OverAllDiscountAmount > 0)
            {
                body = body.Replace("{Discount}", "<tr><td colspan='2' style='text-align:right;border:2px solid #000;background:rgb(217,217,217)'> Negotiated Amount</td><td colspan='1' style='text-align:right;border:2px solid #000;background:rgb(217,217,217);'>" + quotation.OverAllDiscountAmount.ToString("N", new CultureInfo("en-US")) + "</td></tr>");
            }
            else
            {
                body = body.Replace("{Discount}", "");
            }


            decimal TotalNew = totalgross - totaldiscount; ;  // 24.02.18           
            body = body.Replace("{Total_AfterDiscount}", TotalNew.ToString("N", new CultureInfo("en-US")));

            body = body.Replace("{Gross_Amount}", totalgross.ToString("N", new CultureInfo("en-US")));

            NumberToWordsConverter converter = new NumberToWordsConverter();
            long daet = decimal.ToInt64(totalamt);
            string TotalString = converter.Convert(daet);
            body = body.Replace("{Amount_Words}", TotalString);
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_OptionalQuotationPdf", conn);
                sqlComm.Parameters.AddWithValue("@QuotationId", QuotationId);
                sqlComm.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {
                    da.Fill(ds);
                }
            }

            var ProductContent = ds.Rows[0]["data"].ToString();

            body = body.Replace("{Product_Content}", ProductContent);

            var rootpath = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");

            Configuration = rootpath.Build();

            var root = Configuration["App:ServerRootAddress"];
            header = header.Replace("{logourl}", root + "/Common/Images/logopdf.png");
            body = body.Replace("{url}", root);

            string QuotationPath = _hostingEnvironment.WebRootPath + "\\Common\\PDF\\OptionalQuotationPDF\\";

            // Footer Section
            string footer = string.Empty;
            var footerfinder = builder.GetFileProvider().GetFileInfo("Stdfooter.html");
            string footerpath = footerfinder.PhysicalPath;
            using (StreamReader reader = new StreamReader(footerpath, System.Text.Encoding.UTF8))
            {
                footer = reader.ReadToEnd();
            }
            footer = footer.Replace("{footer_url}", root + "/Common/Images/footer.png");

            if (!Directory.Exists(QuotationPath))
            {
                Directory.CreateDirectory(QuotationPath);
            }
            string fileName = "OptionalQuotationPDF_" + QuotationId + ".pdf"; ;

            if (System.IO.File.Exists(QuotationPath + fileName))
            {
                try
                {
                    System.IO.File.Delete(QuotationPath + fileName);
                }
                catch (System.IO.IOException e)
                {
                }
            }
            var generator = new NReco.PdfGenerator.HtmlToPdfConverter()
            {
                PageHeaderHtml = header,
                PageFooterHtml = footer
            };
            try
            {
                generator.Margins = new PageMargins { Top = 70, Bottom = 20, Left = 10, Right = 10 };
                generator.Orientation = NReco.PdfGenerator.PageOrientation.Portrait;
                generator.Zoom = 0.9f;
                generator.CustomWkHtmlArgs = " --load-media-error-handling ignore ";
                generator.Size = NReco.PdfGenerator.PageSize.Letter;
                generator.GeneratePdf(body, null, QuotationPath + fileName);
            }
            catch (Exception ex)
            {

            }
            header = header.Replace("line-height: 6px", "line-height: 18px");
            body = body.Replace("margin-top:-20px;", "margin-top:0px;");
            body = body.Replace("{header_Content}", header);
            body = body.Replace("{footer_Content}", footer);
            return body;
        }

    }
}

