namespace tibs.stem.Authorization
{
    /// <summary>
    /// Defines string constants for application's permission names.
    /// <see cref="AppAuthorizationProvider"/> for permission definitions.
    /// </summary>
    public static class AppPermissions
    {
        //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

        public const string Pages = "Pages";

        public const string Pages_DemoUiComponents= "Pages.DemoUiComponents";
        public const string Pages_Administration = "Pages.Administration";

        public const string Pages_Assignation = "Pages.Assignation";
        public const string Pages_Assignation_SalesManager = "Pages.Assignation.SalesManager";
        public const string Pages_Assignation_Salesperson = "Pages.Assignation.Salesperson";
        public const string Pages_Assignation_Designer = "Pages.Assignation.Designer";
        public const string Pages_Assignation_Coordinator = "Pages.Assignation.Coordinator";


        public const string Pages_Administration_Roles = "Pages.Administration.Roles";
        public const string Pages_Administration_Roles_Create = "Pages.Administration.Roles.Create";
        public const string Pages_Administration_Roles_Edit = "Pages.Administration.Roles.Edit";
        public const string Pages_Administration_Roles_Delete = "Pages.Administration.Roles.Delete";

        public const string Pages_Administration_UserDesignation = "Pages.Administration.UserDesignation";

        public const string Pages_Administration_UserDesignation_Create = "Pages.Administration.UserDesignation.Create";
        public const string Pages_Administration_UserDesignation_Edit = "Pages.Administration.UserDesignation.Edit";
        public const string Pages_Administration_UserDesignation_Delete = "Pages.Administration.UserDesignation.Delete";

        public const string Pages_Administration_Users = "Pages.Administration.Users";
        public const string Pages_Administration_Users_Create = "Pages.Administration.Users.Create";
        public const string Pages_Administration_Users_Edit = "Pages.Administration.Users.Edit";
        public const string Pages_Administration_Users_Delete = "Pages.Administration.Users.Delete";
        public const string Pages_Administration_Users_ChangePermissions = "Pages.Administration.Users.ChangePermissions";
        public const string Pages_Administration_Users_Impersonation = "Pages.Administration.Users.Impersonation";

        public const string Pages_Administration_Languages = "Pages.Administration.Languages";
        public const string Pages_Administration_Languages_Create = "Pages.Administration.Languages.Create";
        public const string Pages_Administration_Languages_Edit = "Pages.Administration.Languages.Edit";
        public const string Pages_Administration_Languages_Delete = "Pages.Administration.Languages.Delete";
        public const string Pages_Administration_Languages_ChangeTexts = "Pages.Administration.Languages.ChangeTexts";

        public const string Pages_Administration_AuditLogs = "Pages.Administration.AuditLogs";

        public const string Pages_Administration_OrganizationUnits = "Pages.Administration.OrganizationUnits";
        public const string Pages_Administration_OrganizationUnits_ManageOrganizationTree = "Pages.Administration.OrganizationUnits.ManageOrganizationTree";
        public const string Pages_Administration_OrganizationUnits_ManageMembers = "Pages.Administration.OrganizationUnits.ManageMembers";

        public const string Pages_Administration_HangfireDashboard = "Pages.Administration.HangfireDashboard";

        //TENANT-SPECIFIC PERMISSIONS

        public const string Pages_Tenant_Dashboard = "Pages.Tenant.Dashboard";

        public const string Pages_Tenant_Dashboard_Sales = "Pages.Tenant.Dashboard.Sales";

        public const string Pages_Tenant_Dashboard_Marketing = "Pages.Tenant.Dashboard.Marketing";

        public const string Pages_Administration_Tenant_Settings = "Pages.Administration.Tenant.Settings";

        public const string Pages_Administration_Tenant_SubscriptionManagement = "Pages.Administration.Tenant.SubscriptionManagement";

        //HOST-SPECIFIC PERMISSIONS

        public const string Pages_Editions = "Pages.Editions";
        public const string Pages_Editions_Create = "Pages.Editions.Create";
        public const string Pages_Editions_Edit = "Pages.Editions.Edit";
        public const string Pages_Editions_Delete = "Pages.Editions.Delete";

        public const string Pages_Tenants = "Pages.Tenants";
        public const string Pages_Tenants_Create = "Pages.Tenants.Create";
        public const string Pages_Tenants_Edit = "Pages.Tenants.Edit";
        public const string Pages_Tenants_ChangeFeatures = "Pages.Tenants.ChangeFeatures";
        public const string Pages_Tenants_Delete = "Pages.Tenants.Delete";
        public const string Pages_Tenants_Impersonation = "Pages.Tenants.Impersonation";

        public const string Pages_Administration_Host_Maintenance = "Pages.Administration.Host.Maintenance";
        public const string Pages_Administration_Host_Settings = "Pages.Administration.Host.Settings";
        public const string Pages_Administration_Host_Dashboard = "Pages.Administration.Host.Dashboard";

        //GEOGRAPHY
        public const string Pages_Tenant_Geography = "Pages.Tenant.Geography";

        public const string Pages_Tenant_Geography_Country = "Pages.Tenant.Geography.Country";
        public const string Pages_Tenant_Geography_Country_Create = "Pages.Tenant.Geography.Country.Create";
        public const string Pages_Tenant_Geography_Country_Edit = "Pages.Tenant.Geography.Country.Edit";
        public const string Pages_Tenant_Geography_Country_Delete = "Pages.Tenant.Geography.Country.Delete";

        public const string Pages_Tenant_Geography_Region = "Pages.Tenant.Geography.Region";
        public const string Pages_Tenant_Geography_Region_Create = "Pages.Tenant.Geography.Region.Create";
        public const string Pages_Tenant_Geography_Region_Edit = "Pages.Tenant.Geography.Region.Edit";
        public const string Pages_Tenant_Geography_Region_Delete = "Pages.Tenant.Geography.Region.Delete";

        public const string Pages_Tenant_Geography_City = "Pages.Tenant.Geography.City";
        public const string Pages_Tenant_Geography_City_Create = "Pages.Tenant.Geography.City.Create";
        public const string Pages_Tenant_Geography_City_Edit = "Pages.Tenant.Geography.City.Edit";
        public const string Pages_Tenant_Geography_City_Delete = "Pages.Tenant.Geography.City.Delete";

        public const string Pages_Tenant_Geography_Location = "Pages.Tenant.Geography.Location";
        public const string Pages_Tenant_Geography_Location_Create = "Pages.Tenant.Geography.Location.Create";
        public const string Pages_Tenant_Geography_Location_Edit = "Pages.Tenant.Geography.Location.Edit";
        public const string Pages_Tenant_Geography_Location_Delete = "Pages.Tenant.Geography.Location.Delete";


        //PRODUCTFAMILY
        public const string Pages_Tenant_ProductFamily = "Pages.Tenant.ProductFamily";

        public const string Pages_Tenant_ProductFamily_ProductFamily = "Pages.Tenant.ProductFamily.ProductFamily";
        public const string Pages_Tenant_ProductFamily_ProductFamily_Create = "Pages.Tenant.ProductFamily.ProductFamily.Create";
        public const string Pages_Tenant_ProductFamily_ProductFamily_Edit = "Pages.Tenant.ProductFamily.ProductFamily.Edit";
        public const string Pages_Tenant_ProductFamily_ProductFamily_Delete = "Pages.Tenant.ProductFamily.ProductFamily.Delete";

        public const string Pages_Tenant_ProductFamily_ProductAttribute = "Pages.Tenant.ProductFamily.ProductAttribute";
        public const string Pages_Tenant_ProductFamily_ProductAttribute_Create = "Pages.Tenant.ProductFamily.ProductAttribute.Create";
        public const string Pages_Tenant_ProductFamily_ProductAttribute_Edit = "Pages.Tenant.ProductFamily.ProductAttribute.Edit";
        public const string Pages_Tenant_ProductFamily_ProductAttribute_Delete = "Pages.Tenant.ProductFamily.ProductAttribute.Delete";

        public const string Pages_Tenant_ProductFamily_ProductAttributeGroup = "Pages.Tenant.ProductFamily.ProductAttributeGroup";
        public const string Pages_Tenant_ProductFamily_ProductAttributeGroup_Create = "Pages.Tenant.ProductFamily.ProductAttributeGroup.Create";
        public const string Pages_Tenant_ProductFamily_ProductAttributeGroup_Edit = "Pages.Tenant.ProductFamily.ProductAttributeGroup.Edit";
        public const string Pages_Tenant_ProductFamily_ProductAttributeGroup_Delete = "Pages.Tenant.ProductFamily.ProductAttributeGroup.Delete";

        public const string Pages_Tenant_ProductFamily_ProductType = "Pages.Tenant.ProductFamily.ProductType";
        public const string Pages_Tenant_ProductFamily_ProductType_Create = "Pages.Tenant.ProductFamily.ProductType.Create";
        public const string Pages_Tenant_ProductFamily_ProductType_Edit = "Pages.Tenant.ProductFamily.ProductType.Edit";
        public const string Pages_Tenant_ProductFamily_ProductType_Delete = "Pages.Tenant.ProductFamily.ProductType.Delete";

        public const string Pages_Tenant_ProductFamily_Collection = "Pages.Tenant.ProductFamily.Collection";
        public const string Pages_Tenant_ProductFamily_Collection_Create = "Pages.Tenant.ProductFamily.Collection.Create";
        public const string Pages_Tenant_ProductFamily_Collection_Edit = "Pages.Tenant.ProductFamily.Collection.Edit";
        public const string Pages_Tenant_ProductFamily_Collection_Delete = "Pages.Tenant.ProductFamily.Collection.Delete";

        public const string Pages_Tenant_ProductFamily_ProductGroup = "Pages.Tenant.ProductFamily.ProductGroup";
        public const string Pages_Tenant_ProductFamily_ProductGroup_Create = "Pages.Tenant.ProductFamily.ProductGroup.Create";
        public const string Pages_Tenant_ProductFamily_ProductGroup_Edit = "Pages.Tenant.ProductFamily.ProductGroup.Edit";
        public const string Pages_Tenant_ProductFamily_ProductGroup_Delete = "Pages.Tenant.ProductFamily.ProductGroup.Delete";

        public const string Pages_Tenant_ProductFamily_ProductSpecification = "Pages.Tenant.ProductFamily.ProductSpecification";
        public const string Pages_Tenant_ProductFamily_ProductSpecification_Create = "Pages.Tenant.ProductFamily.ProductSpecification.Create";
        public const string Pages_Tenant_ProductFamily_ProductSpecification_Edit = "Pages.Tenant.ProductFamily.ProductSpecification.Edit";
        public const string Pages_Tenant_ProductFamily_ProductSpecification_Delete = "Pages.Tenant.ProductFamily.ProductSpecification.Delete";

        public const string Pages_Tenant_ProductFamily_Products = "Pages.Tenant.ProductFamily.Products";
        public const string Pages_Tenant_ProductFamily_Products_Create = "Pages.Tenant.ProductFamily.Products.Create";
        public const string Pages_Tenant_ProductFamily_Products_Edit = "Pages.Tenant.ProductFamily.Products.Edit";
        public const string Pages_Tenant_ProductFamily_Products_Delete = "Pages.Tenant.ProductFamily.Products.Delete";

        public const string Pages_Tenant_ProductFamily_ProductCategory = "Pages.Tenant.ProductFamily.ProductCategory";
        public const string Pages_Tenant_ProductFamily_ProductCategory_Create = "Pages.Tenant.ProductFamily.ProductCategory.Create";
        public const string Pages_Tenant_ProductFamily_ProductCategory_Edit = "Pages.Tenant.ProductFamily.ProductCategory.Edit";
        public const string Pages_Tenant_ProductFamily_ProductCategory_Delete = "Pages.Tenant.ProductFamily.ProductCategory.Delete";

        //ADDRESSBOOK
        public const string Pages_Tenant_AddressBook = "Pages.Tenant.AddressBook";

        public const string Pages_Tenant_AddressBook_Company = "Pages.Tenant.AddressBook.Company";
        public const string Pages_Tenant_AddressBook_Company_Create = "Pages.Tenant.AddressBook.Company.Create";
        public const string Pages_Tenant_AddressBook_Company_Edit = "Pages.Tenant.AddressBook.Company.Edit";
        public const string Pages_Tenant_AddressBook_Company_Delete = "Pages.Tenant.AddressBook.Company.Delete";

        public const string Pages_Tenant_AddressBook_Contact = "Pages.Tenant.AddressBook.Contact";
        public const string Pages_Tenant_AddressBook_Contact_Create = "Pages.Tenant.AddressBook.Contact.Create";
        public const string Pages_Tenant_AddressBook_Contact_Edit = "Pages.Tenant.AddressBook.Contact.Edit";
        public const string Pages_Tenant_AddressBook_Contact_Delete = "Pages.Tenant.AddressBook.Contact.Delete";

        public const string Pages_Tenant_AddressBook_CustomerType = "Pages.Tenant.AddressBook.CustomerType";
        public const string Pages_Tenant_AddressBook_CustomerType_Create = "Pages.Tenant.AddressBook.CustomerType.Create";
        public const string Pages_Tenant_AddressBook_CustomerType_Edit = "Pages.Tenant.AddressBook.CustomerType.Edit";
        public const string Pages_Tenant_AddressBook_CustomerType_Delete = "Pages.Tenant.AddressBook.CustomerType.Delete";

        public const string Pages_Tenant_AddressBook_InfoType = "Pages.Tenant.AddressBook.InfoType";
        public const string Pages_Tenant_AddressBook_InfoType_Create = "Pages.Tenant.AddressBook.InfoType.Create";
        public const string Pages_Tenant_AddressBook_InfoType_Edit = "Pages.Tenant.AddressBook.InfoType.Edit";
        public const string Pages_Tenant_AddressBook_InfoType_Delete = "Pages.Tenant.AddressBook.InfoType.Delete";



        //MASTER
        public const string Pages_Tenant_Master = "Pages.Tenant.Master";

        public const string Pages_Tenant_Master_Source = "Pages.Tenant.Master.Source";
        public const string Pages_Tenant_Master_Source_Create = "Pages.Tenant.Master.Source.Create";
        public const string Pages_Tenant_Master_Source_Edit = "Pages.Tenant.Master.Source.Edit";
        public const string Pages_Tenant_Master_Source_Delete = "Pages.Tenant.Master.Source.Delete";

        public const string Pages_Tenant_Master_MileStone = "Pages.Tenant.Master.MileStone";
        public const string Pages_Tenant_Master_MileStone_Create = "Pages.Tenant.Master.MileStone.Create";
        public const string Pages_Tenant_Master_MileStone_Edit = "Pages.Tenant.Master.MileStone.Edit";
        public const string Pages_Tenant_Master_MileStone_Delete = "Pages.Tenant.Master.MileStone.Delete";

        public const string Pages_Tenant_Master_ActivityType = "Pages.Tenant.Master.ActivityType";
        public const string Pages_Tenant_Master_ActivityType_Create = "Pages.Tenant.Master.ActivityType.Create";
        public const string Pages_Tenant_Master_ActivityType_Edit = "Pages.Tenant.Master.ActivityType.Edit";
        public const string Pages_Tenant_Master_ActivityType_Delete = "Pages.Tenant.Master.ActivityType.Delete";

        public const string Pages_Tenant_Master_LeadType = "Pages.Tenant.Master.LeadType";
        public const string Pages_Tenant_Master_LeadType_Create = "Pages.Tenant.Master.LeadType.Create";
        public const string Pages_Tenant_Master_LeadType_Edit = "Pages.Tenant.Master.LeadType.Edit";
        public const string Pages_Tenant_Master_LeadType_Delete = "Pages.Tenant.Master.LeadType.Delete";

        public const string Pages_Tenant_Master_LeadReason = "Pages.Tenant.Master.LeadReason";
        public const string Pages_Tenant_Master_LeadReason_Create = "Pages.Tenant.Master.LeadReason.Create";
        public const string Pages_Tenant_Master_LeadReason_Edit = "Pages.Tenant.Master.LeadReason.Edit";
        public const string Pages_Tenant_Master_LeadReason_Delete = "Pages.Tenant.Master.LeadReason.Delete";

        public const string Pages_Tenant_Master_LeadStatus = "Pages.Tenant.Master.LeadStatus";
        public const string Pages_Tenant_Master_LeadStatus_Create = "Pages.Tenant.Master.LeadStatus.Create";
        public const string Pages_Tenant_Master_LeadStatus_Edit = "Pages.Tenant.Master.LeadStatus.Edit";
        public const string Pages_Tenant_Master_LeadStatus_Delete = "Pages.Tenant.Master.LeadStatus.Delete";

        public const string Pages_Tenant_Master_kanbanStage = "Pages.Tenant.Master.kanbanStage";
        public const string Pages_Tenant_Master_kanbanStage_Create = "Pages.Tenant.Master.kanbanStage.Create";
        public const string Pages_Tenant_Master_kanbanStage_Edit = "Pages.Tenant.Master.kanbanStage.Edit";
        public const string Pages_Tenant_Master_kanbanStage_Delete = "Pages.Tenant.Master.kanbanStage.Delete";

        public const string Pages_Tenant_Master_Department = "Pages.Tenant.Master.Department";
        public const string Pages_Tenant_Master_Department_Create = "Pages.Tenant.Master.Department.Create";
        public const string Pages_Tenant_Master_Department_Edit = "Pages.Tenant.Master.Department.Edit";
        public const string Pages_Tenant_Master_Department_Delete = "Pages.Tenant.Master.Department.Delete";


        public const string Pages_Tenant_Master_Industry = "Pages.Tenant.Master.Industry";
        public const string Pages_Tenant_Master_Industry_Create = "Pages.Tenant.Master.Industry.Create";
        public const string Pages_Tenant_Master_Industry_Edit = "Pages.Tenant.Master.Industry.Edit";
        public const string Pages_Tenant_Master_Industry_Delete = "Pages.Tenant.Master.Industry.Delete";

        public const string Pages_Tenant_Master_PriceLevel = "Pages.Tenant.Master.PriceLevel";
        public const string Pages_Tenant_Master_PriceLevel_Create = "Pages.Tenant.Master.PriceLevel.Create";
        public const string Pages_Tenant_Master_PriceLevel_Edit = "Pages.Tenant.Master.PriceLevel.Edit";
        public const string Pages_Tenant_Master_PriceLevel_Delete = "Pages.Tenant.Master.PriceLevel.Delete";

        public const string Pages_Tenant_Master_Whybafco = "Pages.Tenant.Master.Whybafco";
        public const string Pages_Tenant_Master_Whybafco_Create = "Pages.Tenant.Master.Whybafco.Create";
        public const string Pages_Tenant_Master_Whybafco_Edit = "Pages.Tenant.Master.Whybafco.Edit";
        public const string Pages_Tenant_Master_Whybafco_Delete = "Pages.Tenant.Master.Whybafco.Delete";

        public const string Pages_Tenant_Master_EmailDomain = "Pages.Tenant.Master.EmailDomain";
        public const string Pages_Tenant_Master_EmailDomain_Create = "Pages.Tenant.Master.EmailDomain.Create";
        public const string Pages_Tenant_Master_EmailDomain_Edit = "Pages.Tenant.Master.EmailDomain.Edit";
        public const string Pages_Tenant_Master_EmailDomain_Delete = "Pages.Tenant.Master.EmailDomain.Delete";

        public const string Pages_Tenant_Master_OpportunitySource = "Pages.Tenant.Master.OpportunitySource";
        public const string Pages_Tenant_Master_OpportunitySource_Create = "Pages.Tenant.Master.OpportunitySource.Create";
        public const string Pages_Tenant_Master_OpportunitySource_Edit = "Pages.Tenant.Master.OpportunitySource.Edit";
        public const string Pages_Tenant_Master_OpportunitySource_Delete = "Pages.Tenant.Master.OpportunitySource.Delete";

        public const string Pages_Tenant_Master_Team = "Pages.Tenant.Master.Team";
        public const string Pages_Tenant_Master_Team_Create = "Pages.Tenant.Master.Team.Create";
        public const string Pages_Tenant_Master_Team_Edit = "Pages.Tenant.Master.Team.Edit";
        public const string Pages_Tenant_Master_Team_Edit_SalesPerson_Delete = "Pages.Tenant.Master.Team.Edit.SalesPerson.Delete";
        public const string Pages_Tenant_Master_Team_Delete = "Pages.Tenant.Master.Team.Delete";

        //ENQUIRY

        public const string Pages_Tenant_Enquiry = "Pages.Tenant.Enquiry";

       
        public const string Pages_Tenant_Enquiry_Enquiry = "Pages.Tenant.Enquiry.Enquiry";
        public const string Pages_Tenant_Enquiry_Enquiry_Create = "Pages.Tenant.Enquiry.Enquiry.Create";
        public const string Pages_Tenant_Enquiry_Enquiry_Gridview = "Pages.Tenant.Enquiry.Enquiry.Gridview";


        public const string Pages_Tenant_Enquiry_SalesEnquiry = "Pages.Tenant.Enquiry.SalesEnquiry";
        public const string Pages_Tenant_Enquiry_SalesEnquiry_Gridview = "Pages.Tenant.Enquiry.SalesEnquiry.Gridview";
        public const string Pages_Tenant_Enquiry_SalesEnquiry_Edit = "Pages.Tenant.Enquiry.SalesEnquiry.Edit";
        public const string Pages_Tenant_Enquiry_SalesEnquiry_Edit_Team = "Pages.Tenant.Enquiry.SalesEnquiry.Edit.Team";


        public const string Pages_Tenant_Enquiry_Leads = "Pages.Tenant.Enquiry.Leads";
        public const string Pages_Tenant_Enquiry_Leads_Edit = "Pages.Tenant.Enquiry.Leads.Edit";
        public const string Pages_Tenant_Enquiry_Leads_Delete = "Pages.Tenant.Enquiry.Leads.Delete";

        public const string Pages_Tenant_Enquiry_Junk = "Pages.Tenant.Enquiry.Junk";
        public const string Pages_Tenant_Enquiry_Junk_Edit = "Pages.Tenant.Enquiry.Junk.Edit";
        public const string Pages_Tenant_Enquiry_Junk_Delete = "Pages.Tenant.Enquiry.Junk.Delete";
        public const string Pages_Tenant_Enquiry_Junk_Revert = "Pages.Tenant.Enquiry.Junk.Revert";



        //ENQUIRYACTIVITY
        public const string Pages_Tenant_Activities = "Pages.Tenant.Activities";
        public const string Pages_Tenant_EnquiryActivity = "Pages.Tenant.EnquiryActivity";
        public const string Pages_Tenant_EnquiryActivity_AddComment = "Pages.Tenant.EnquiryActivity.AddComment";
        public const string Pages_Tenant_EnquiryActivity_OpenEnquiry = "Pages.Tenant.EnquiryActivity.OpenInquiry";
        public const string Pages_Tenant_EnquiryActivity_OpenActivity = "Pages.Tenant.EnquiryActivity.OpenActivity";

        public const string Pages_Tenant_JobActivity = "Pages.Tenant.JobActivity";
        public const string Pages_Tenant_JobActivity_Open_Enquiry = "Pages.Tenant.JobActivity.OpenInquiry";
        public const string Pages_Tenant_JobActivity_Open_Activity = "Pages.Tenant.JobActivity.OpenActivity";

        //Quotation
        public const string Pages_Tenant_Quotation = "Pages.Tenant.Quotation";


        public const string Pages_Tenant_Quotation_Quotation = "Pages.Tenant.Quotation.Quotation";
        public const string Pages_Tenant_Quotation_Quotation_Create = "Pages.Tenant.Quotation.Quotation.Create";
        public const string Pages_Tenant_Quotation_Quotation_Edit = "Pages.Tenant.Quotation.Quotation.Edit";
        public const string Pages_Tenant_Quotation_Quotation_Edit_QuotationDetails_ImportProduct = "Pages.Tenant.Quotation.Quotation.Edit.QuotationDetails.ImportProduct";
        public const string Pages_Tenant_Quotation_Quotation_Edit_QuotationDetails_AddQuotationProduct = "Pages.Tenant.Quotation.Quotation.Edit.QuotationDetails.AddQuotationProduct";
        public const string Pages_Tenant_Quotation_Quotation_Edit_QuotationDetails_AddSection = "Pages.Tenant.Quotation.Quotation.Edit.QuotationDetails.AddSection";
        public const string Pages_Tenant_Quotation_Quotation_Edit_QuotationProductLink = "Pages.Tenant.Quotation.Quotation.Edit.QuotationProductLink";
        public const string Pages_Tenant_Quotation_Quotation_Edit_QuotationproductDiscountApproval = "Pages.Tenant.Quotation.Quotation.Edit.QuotationproductDiscountApproval";
        public const string Pages_Tenant_Quotation_Quotation_Delete_Grid = "Pages.Tenant.Quotation.Grid.Delete";
        public const string Pages_Tenant_Quotation_Quotation_Edit_QuotationDetail = "Pages.Tenant.Quotation.Quotation.Edit.QuotationDetail";
        public const string Pages_Tenant_Quotation_Quotation_Edit_ImportHistory = "Pages.Tenant.Quotation.Quotation.Edit.ImportHistory";
        public const string Pages_Tenant_Quotation_Quotation_Edit_RevisedHistory = "Pages.Tenant.Quotation.Quotation.Edit.RevisedHistory";
        public const string Pages_Tenant_Quotation_Quotation_Edit_QuotationDetails_DiscountApprove = "Pages.Tenant.Quotation.Quotation.Edit.QuotationDetails.DiscountApprove";


        public const string Pages_Tenant_Quotation_QuotationStatus = "Pages.Tenant.Quotation.QuotationStatus";
        public const string Pages_Tenant_Quotation_QuotationStatus_Create = "Pages.Tenant.Quotation.QuotationStatus.Create";
        public const string Pages_Tenant_Quotation_QuotationStatus_Edit = "Pages.Tenant.Quotation.QuotationStatus.Edit";
        public const string Pages_Tenant_Quotation_QuotationStatus_Delete = "Pages.Tenant.Quotation.QuotationStatus.Delete";
        public const string Pages_Tenant_Quotation_Quotation_Edit_QuotationDetails_EditSection = "Pages.Tenant.Quotation.Quotation.Edit.QuotationDetails.EditSection";

        public const string Pages_Tenant_Enquiry_Enquiry_Edit_OverAllActivity_LinkedContacts_AddContact = "Pages.Tenant.Enquiry.Enquiry.Edit.OverAllActivity.LinkedContacts.AddContact";
        public const string Pages_Tenant_Quotation_Quotation_Edit_QuotationDetails_QuotationProductLink = "Pages.Tenant.Quotation.Quotation.Edit.QuotationDetails.QuotationProductLink";
        public const string Pages_Tenant_Quotation_Quotation_Edit_QuotationDetails_QuotationproductDiscountApproval = "Pages.Tenant.Quotation.Quotation.Edit.QuotationDetails.QuotationproductDiscountApproval";
        public const string Pages_Tenant_Quotation_Quotation_Open = "Pages.Tenant.Quotation.Quotation.Open";
        public const string Pages_Tenant_Quotation_Quotation_Edit_RevisionHistory_Open = "Pages.Tenant.Quotation.Quotation.Edit.RevisionHistory.Open";


        public const string Pages_Tenant_Enquiry_SalesEnquiry_Create = "Pages.Tenant.Enquiry.SalesEnquiry.Create";
        public const string Pages_Tenant_Enquiry_SalesEnquiry_Gridview_Create = "Pages.Tenant.Enquiry.SalesEnquiry.Gridview.Create";
        public const string Pages_Tenant_Enquiry_SalesEnquiry_Gridview_Edit = "Pages.Tenant.Enquiry.SalesEnquiry.Gridview.Edit";
        public const string Pages_Tenant_Enquiry_SalesEnquiry_Gridview_Delete = "Pages.Tenant.Enquiry.SalesEnquiry.Gridview.Delete";

        //Export
        public const string Pages_Tenant_Export = "Pages.Tenant.Export";

       // ProductSpecification-

        public const string Pages_Tenant_ProductFamily_ProductSpecification_Edit_ProductSpecificationGroupDetail = "Pages.Tenant.ProductFamily.ProductSpecification.Edit.ProductSpecificationGroupDetail";
        public const string Pages_Tenant_ProductFamily_ProductSpecification_Edit_ProductSpecificationGroupDetail_Expand = "Pages.Tenant.ProductFamily.ProductSpecification.Edit.ProductSpecificationGroupDetail.Expand";

        //Attribute Group-
        public const string Pages_Tenant_ProductFamily_ProductAttributeGroup_Edit_AttributeDetail = "Pages.Tenant.ProductFamily.ProductAttributeGroup.Edit.AttributeDetail";
        public const string Pages_Tenant_ProductFamily_ProductAttributeGroup_Edit_AttributeDetail_Delete = "Pages.Tenant.ProductFamily.ProductAttributeGroup.Edit.AttributeDetail.Delete";

        //Company
        public const string Pages_Tenant_AddressBook_Company_Edit_ContactDetails = "Pages.Tenant.AddressBook.Company.Edit.ContactDetails";
        public const string Pages_Tenant_AddressBook_Company_Edit_ContactDetails_Create = "Pages.Tenant.AddressBook.Company.Edit.ContactDetails.Create";
        public const string Pages_Tenant_AddressBook_Company_Edit_ContactDetails_Edit = "Pages.Tenant.AddressBook.Company.Edit.ContactDetails.Edit";
        public const string Pages_Tenant_AddressBook_Company_Edit_ContactDetails_Delete = "Pages.Tenant.AddressBook.Company.Edit.ContactDetails.Delete";
        public const string Pages_Tenant_AddressBook_Company_Edit_CompanyApproval = "Pages.Tenant.AddressBook.Company.Edit.CompanyApproval";
        public const string Pages_Tenant_AddressBook_Company_Edit_Managedby = "Pages.Tenant.AddressBook.Company.Edit.Managedby";

        //Quotation
        public const string Pages_Tenant_Quotation_Quotation_Edit_QuotationDetails_EditQuotationProduct = "Pages.Tenant.Quotation.Quotation.Edit.QuotationDetails.EditQuotationProduct";
        public const string Pages_Tenant_Quotation_Quotation_Edit_QuotationDetails_DeleteQuotationProduct = "Pages.Tenant.Quotation.Quotation.Edit.QuotationDetails.DeleteQuotationProduct";

        //Enquiry

        public const string Pages_Tenant_Enquiry_Enquiry_Edit = "Pages.Tenant.Enquiry.Enquiry.Edit";
        public const string Pages_Tenant_Enquiry_Enquiry_Gridview_Create = "Pages.Tenant.Enquiry.Enquiry.Gridview.Create";
        public const string Pages_Tenant_Enquiry_Enquiry_Gridview_Edit = "Pages.Tenant.Enquiry.Enquiry.Gridview.Edit";
        public const string Pages_Tenant_Enquiry_Enquiry_Gridview_Open = "Pages.Tenant.Enquiry.Enquiry.Gridview.Open";
        public const string Pages_Tenant_Enquiry_Enquiry_Gridview_Delete = "Pages.Tenant.Enquiry.Enquiry.Gridview.Delete";
        public const string Pages_Tenant_Enquiry_Enquiry_Edit_Details_CreateQuotation = "Pages.Tenant.Enquiry.Enquiry.Edit.Details.CreateQuotation";
        public const string Pages_Tenant_Enquiry_Enquiry_Edit_OverAllActivity = "Pages.Tenant.Enquiry.Enquiry.Edit.OverAllActivity";
        public const string Pages_Tenant_Enquiry_Enquiry_Edit_OverAllActivity_CreateNewActivity = "Pages.Tenant.Enquiry.Enquiry.Edit.OverAllActivity.CreateNewActivity";
        public const string Pages_Tenant_Enquiry_Enquiry_Edit_OverAllActivity_LinkedContacts_CreateContact = "Pages.Tenant.Enquiry.Enquiry.Edit.OverAllActivity.LinkedContacts.CreateContact";

        public const string Pages_Tenant_Enquiry_Enquiry_Gridview_Quotation = "Pages.Tenant.Enquiry.Enquiry.Gridview.Edit.Quotation";
        public const string Pages_Tenant_Enquiry_Enquiry_Gridview_ClosedEnquiry = "Pages.Tenant.Enquiry.Enquiry.Gridview.Edit.ClosedEnquiry";
        public const string Pages_Tenant_Enquiry_Enquiry_Gridview_ClosedEnquiry_Open = "Pages.Tenant.Enquiry.Enquiry.Gridview.Edit.ClosedEnquiry.Open";
        public const string Pages_Tenant_Enquiry_Enquiry_Gridview_ClosedEnquiry_Revert = "Pages.Tenant.Enquiry.Enquiry.Gridview.Edit.ClosedEnquiry.Revert";
        public const string Pages_Tenant_Enquiry_Enquiry_Gridview_Enquiry = "Pages.Tenant.Enquiry.Enquiry.Gridview.Edit.Enquiry";

        public static string Pages_Tenant_Quotation_Quotation_Edit_Kanban = "Pages.Tenant.Quotation.Quotation.Edit.Kanban";
        public static string Pages_Tenant_Quotation_Quotation_Edit_Grid = "Pages.Tenant.Quotation.Quotation.Edit.Grid";
        public static string Pages_Tenant_Quotation_Quotation_Enquiry = "Pages.Tenant.Quotation.Quotation.Enquiry";
        public static string Pages_Tenant_Quotation_Quotation_Delete_Enquiry = "Pages.Tenant.Quotation.Quotation.Delete.Enquiry";
        public static string Pages_Tenant_Quotation_Quotation_Delete = "Pages.Tenant.Quotation.Quotation.Delete";
    }
}