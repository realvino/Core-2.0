using Abp.IdentityServer4;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Drawing;
using tibs.stem.AbpSalesCoOrinators;
using tibs.stem.AcitivityTracks;
using tibs.stem.Activities;
using tibs.stem.ActivityTrackComments;
using tibs.stem.AttributeGroupDetails;
using tibs.stem.Authorization.Roles;
using tibs.stem.Authorization.Users;
using tibs.stem.Chat;
using tibs.stem.Citys;
using tibs.stem.Collections;
using tibs.stem.ColorCodes;
using tibs.stem.Companies;
using tibs.stem.CompanyContacts;
using tibs.stem.Countrys;
using tibs.stem.CustomerTypes;
using tibs.stem.Departments;
using tibs.stem.Designations;
using tibs.stem.Dimensions;
using tibs.stem.Discounts;
using tibs.stem.Editions;
using tibs.stem.Emaildomains;
using tibs.stem.EnquiryContacts;
using tibs.stem.EnquiryDetails;
using tibs.stem.EnquirySources;
using tibs.stem.EnquiryStatuss;
using tibs.stem.Friendships;
using tibs.stem.ImportHistorys;
using tibs.stem.Industrys;
using tibs.stem.Inquirys;
using tibs.stem.LeadDetails;
using tibs.stem.LeadReasons;
using tibs.stem.LeadSources;
using tibs.stem.LeadStatuss;
using tibs.stem.LeadTypes;
using tibs.stem.LineTypes;
using tibs.stem.Locations;
using tibs.stem.Milestones;
using tibs.stem.MultiTenancy;
using tibs.stem.MultiTenancy.Payments;
using tibs.stem.NewCustomerCompanys;
using tibs.stem.OpportunitySources;
using tibs.stem.Orientations;
using tibs.stem.PriceLevels;
using tibs.stem.ProductAttributeGroups;
using tibs.stem.ProductAttributes;
using tibs.stem.ProductCategorys;
using tibs.stem.ProductFamilys;
using tibs.stem.ProductGroups;
using tibs.stem.ProductImageUrls;
using tibs.stem.Products;
using tibs.stem.ProductSpecificationDetails;
using tibs.stem.ProductSpecifications;
using tibs.stem.ProductSubGroups;
using tibs.stem.ProductTypes;
using tibs.stem.ProdutSpecLinks;
using tibs.stem.QuotationProducts;
using tibs.stem.Quotations;
using tibs.stem.QuotationStatuss;
using tibs.stem.Region;
using tibs.stem.Sections;
using tibs.stem.Sources;
using tibs.stem.SourceTypes;
using tibs.stem.Stagestates;
using tibs.stem.Storage;
using tibs.stem.Team;
using tibs.stem.TeamDetails;
using tibs.stem.TemporaryProducts;
using tibs.stem.TitleOfCourtes;
using tibs.stem.UserDesignations;
using tibs.stem.Views;
using tibs.stem.Ybafcos;

namespace tibs.stem.EntityFrameworkCore
{
    public class stemDbContext : AbpZeroDbContext<Tenant, Role, User, stemDbContext>, IAbpPersistedGrantDbContext
    {
        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }
        public virtual DbSet<Friendship> Friendships { get; set; }
        public virtual DbSet<ChatMessage> ChatMessages { get; set; }
        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }
        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }
        public DbSet<PersistedGrantEntity> PersistedGrants { get; set; }
        public virtual DbSet<Country> Countrys { get; set; }
        public virtual DbSet<City> Citys { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<ProductGroup> ProductGroups { get; set; }
        public virtual DbSet<ProductSubGroup> ProductSubGroups { get; set; }
        public virtual DbSet<Regions> Regions { get; set; }
        public virtual DbSet<RegionCity> RegionCitys { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<CustomerType> CustomerTypes { set; get; }
        public virtual DbSet<TitleOfCourtesy> TitleOfCourtes { get; set; }
        public virtual DbSet<Company> Companys { get; set; }
        public virtual DbSet<CompanyContact> CompanyContacts { get; set; }
        public virtual DbSet<SourceType> SourceTypes { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<MileStone> MileStones { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<LeadType> LeadTypes { get; set; }
        public virtual DbSet<LeadReason> LeadReasons { get; set; }
        public virtual DbSet<LineType> LineTypes { get; set; }
        public virtual DbSet<Inquiry> Inquirys { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<AcitivityTrack> AcitivityTracks { get; set; }
        public virtual DbSet<ActivityTrackComment> ActivityTrackComments { get; set; }
        public virtual DbSet<NewCustomerType> NewCustomerTypes { get; set; }
        public virtual DbSet<NewCompany> NewCompanys { get; set; }
        public virtual DbSet<NewContact> NewContacts { get; set; }
        public virtual DbSet<NewInfoType> NewInfoTypes { get; set; }
        public virtual DbSet<NewAddressInfo> NewAddressInfos { get; set; }
        public virtual DbSet<NewContactInfo> NewContactInfos { get; set; }
        public virtual DbSet<EnquirySource> EnquirySources { get; set; }
        public virtual DbSet<EnquiryContact> EnquiryContacts { get; set; }
        public virtual DbSet<EnquiryDetail> EnquiryDetails { get; set; }
        public virtual DbSet<EnquiryStatus> EnquiryStatuss { get; set; }
        public virtual DbSet<Industry> Industries { get; set; }
        public virtual DbSet<LeadSource> LeadSources { get; set; }
        public virtual DbSet<LeadDetail> LeadDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<PriceLevel> PriceLevels { get; set; }
        public virtual DbSet<ProductPricelevel> ProductPricelevels { get; set; }
        public virtual DbSet<Dimension> Dimensions { get; set; }
        public virtual DbSet<Orientation> Orientations { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<ColorCode> ColorCodes { get; set; }
        public virtual DbSet<ProductFamily> ProductFamilys { get; set; }
        public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }
        public virtual DbSet<ProductAttributeGroup> ProductAttributeGroups { get; set; }
        public virtual DbSet<AttributeGroupDetail> AttributeGroupDetails { get; set; }
        public virtual DbSet<ProductSpecification> ProductSpecifications { get; set; }
        public virtual DbSet<ProductSpecificationDetail> ProductSpecificationDetails { get; set; }
        public virtual DbSet<ProductGroupDetail> ProductGroupDetails { get; set; }
        public virtual DbSet<Collection> Collections { get; set; }
        public virtual DbSet<ProdutSpecLink> ProdutSpecLinks { get; set; }
        public virtual DbSet<ProductImageUrl> ProductImageUrls { get; set; }
        public virtual DbSet<QuotationStatus> QuotationStatuss { get; set; }
        public virtual DbSet<Quotation> Quotations { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<QuotationProduct> QuotationProducts { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<ImportHistory> ImportHistorys { get; set; }
        public virtual DbSet<AbpSalesCoOrinator> AbpSalesCoOrinators { get; set; }
        public virtual DbSet<Teams> Team { get; set; }
        public virtual DbSet<TeamDetail> TeamDetails { get;set;}
        public virtual DbSet<TemporaryProduct> TemporaryProducts { get; set; }
        public virtual DbSet<TemporaryProductImage> TemporaryProductImages { get; set; }
        public virtual DbSet<ProductCategory> ProductCategorys { get; set; }
        public virtual DbSet<StageDetails> StageDetailss { get; set; }
        public virtual DbSet<Ybafco> Ybafcos { get; set; }
        public virtual DbSet<OpportunitySource> OpportunitySources { get; set; }
        public virtual DbSet<Emaildomain> Emaildomains { get; set; }
        public virtual DbSet<JobActivity> JobActivities { get; set; }
        public virtual DbSet<Stagestate> Stagestates { get; set; }
        public virtual DbSet<LeadStatus> LeadStatuss { get; set; }
        public virtual DbSet<UserDesignation> UserDesignations { get; set; }
        public virtual DbSet<ProductStates> ProductStates { get; set; }
        public virtual DbSet<View> Views { get; set; }
        public virtual DbSet<ReportColumn> ReportColumns { get; set; }
        public virtual DbSet<DateFilter> DateFilters { get; set; }
        public stemDbContext(DbContextOptions<stemDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BinaryObject>(b =>
            {
                b.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { e.PaymentId, e.Gateway });
            });

            modelBuilder.ConfigurePersistedGrantEntity();
        }
    }
}
