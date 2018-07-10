namespace tibs.stem.Emailing
{
    public interface IEmailTemplateProvider
    {
        string GetDefaultTemplate(int? tenantId);
        string DiscountEmailTemplate();
        string LostEmailTemplate();
    }
}
