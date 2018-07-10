﻿using System.Text;
using Abp.Dependency;
using Abp.Extensions;
using Abp.IO.Extensions;
using Abp.Reflection.Extensions;
using tibs.stem.Url;

namespace tibs.stem.Emailing
{
    public class EmailTemplateProvider : IEmailTemplateProvider, ITransientDependency
    {
        private readonly IWebUrlService _webUrlService;

        public EmailTemplateProvider(
            IWebUrlService webUrlService)
        {
            _webUrlService = webUrlService;
        }

        public string GetDefaultTemplate(int? tenantId)
        {
            using (var stream = typeof(EmailTemplateProvider).GetAssembly().GetManifestResourceStream("tibs.stem.Emailing.EmailTemplates.default.html"))
            {
                var bytes = stream.GetAllBytes();
                var template = Encoding.UTF8.GetString(bytes, 3, bytes.Length - 3);
                return template.Replace("{EMAIL_LOGO_URL}", GetTenantLogoUrl(tenantId));
            }
        }

        private string GetTenantLogoUrl(int? tenantId)
        {
            if (!tenantId.HasValue)
            {
                return _webUrlService.GetServerRootAddress().EnsureEndsWith('/') + "TenantCustomization/GetTenantLogo";
            }

            return _webUrlService.GetServerRootAddress().EnsureEndsWith('/') + "TenantCustomization/GetTenantLogo?tenantId=" + tenantId.Value;
        }
        public string DiscountEmailTemplate()
        {
            using (var stream = typeof(EmailTemplateProvider).GetAssembly().GetManifestResourceStream("tibs.stem.Emailing.EmailTemplates.disemtemp.html"))
            {
                try
                {
                    var bytes = stream.GetAllBytes();
                    var template = Encoding.UTF8.GetString(bytes, 3, bytes.Length - 3);
                    return template;
                }
                catch (System.Exception ex)
                {

                    throw;
                }

            }
        }
        public string LostEmailTemplate()
        {
            using (var stream = typeof(EmailTemplateProvider).GetAssembly().GetManifestResourceStream("tibs.stem.Emailing.EmailTemplates.lostemtemp.html"))
            {
                try
                {
                    var bytes = stream.GetAllBytes();
                    var template = Encoding.UTF8.GetString(bytes, 3, bytes.Length - 3);
                    return template;
                }
                catch (System.Exception ex)
                {

                    throw;
                }

            }
        }
    }
}