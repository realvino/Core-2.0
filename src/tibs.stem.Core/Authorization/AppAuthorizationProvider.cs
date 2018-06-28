using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;

namespace tibs.stem.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
 //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));
            //pages.CreateChildPermission(AppPermissions.Pages_DemoUiComponents, L("DemoUiComponents"));

            //Assination
            var assignation = pages.CreateChildPermission(AppPermissions.Pages_Assignation, L("Assignation"));

            assignation.CreateChildPermission(AppPermissions.Pages_Assignation_SalesManager, L("asSalesManager"));
            assignation.CreateChildPermission(AppPermissions.Pages_Assignation_Salesperson, L("asSalesperson"));
            //assignation.CreateChildPermission(AppPermissions.Pages_Assignation_Designer, L("asDesigner"));
            //assignation.CreateChildPermission(AppPermissions.Pages_Assignation_Coordinator, L("asCoordinator"));

            //Dashboard

            var dash = pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);
                dash.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard_Marketing, L("MDashboard"));
                dash.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard_Sales, L("SDashboard"));

            //Geography      

            var geography = pages.CreateChildPermission(AppPermissions.Pages_Tenant_Geography, L("Geography"));

            var country = geography.CreateChildPermission(AppPermissions.Pages_Tenant_Geography_Country, L("Country"));
                country.CreateChildPermission(AppPermissions.Pages_Tenant_Geography_Country_Create, L("CreateCountry"));
                country.CreateChildPermission(AppPermissions.Pages_Tenant_Geography_Country_Edit, L("EditCountry"));
                country.CreateChildPermission(AppPermissions.Pages_Tenant_Geography_Country_Delete, L("DeleteCountry"));

            var city = geography.CreateChildPermission(AppPermissions.Pages_Tenant_Geography_City, L("City"));
                city.CreateChildPermission(AppPermissions.Pages_Tenant_Geography_City_Create, L("CreateCity"));
                city.CreateChildPermission(AppPermissions.Pages_Tenant_Geography_City_Edit, L("EditCity"));
                city.CreateChildPermission(AppPermissions.Pages_Tenant_Geography_City_Delete, L("DeleteCity"));

            var region = geography.CreateChildPermission(AppPermissions.Pages_Tenant_Geography_Region, L("Region"));
                region.CreateChildPermission(AppPermissions.Pages_Tenant_Geography_Region_Create, L("CreateRegion"));
                region.CreateChildPermission(AppPermissions.Pages_Tenant_Geography_Region_Edit, L("EditRegion"));
                region.CreateChildPermission(AppPermissions.Pages_Tenant_Geography_Region_Delete, L("DeleteRegion"));

            var location = geography.CreateChildPermission(AppPermissions.Pages_Tenant_Geography_Location, L("Location"));
                location.CreateChildPermission(AppPermissions.Pages_Tenant_Geography_Location_Create, L("CreateLocation"));
                location.CreateChildPermission(AppPermissions.Pages_Tenant_Geography_Location_Edit, L("EditLocation"));
                location.CreateChildPermission(AppPermissions.Pages_Tenant_Geography_Location_Delete, L("DeleteLocation"));
//Master
            var master = pages.CreateChildPermission(AppPermissions.Pages_Tenant_Master, L("Master"));

            var source = master.CreateChildPermission(AppPermissions.Pages_Tenant_Master_Source, L("Source"));
                source.CreateChildPermission(AppPermissions.Pages_Tenant_Master_Source_Create, L("CreateSource"));
                source.CreateChildPermission(AppPermissions.Pages_Tenant_Master_Source_Edit, L("EditSource"));
                source.CreateChildPermission(AppPermissions.Pages_Tenant_Master_Source_Delete, L("DeleteSource"));

            var mileStone = master.CreateChildPermission(AppPermissions.Pages_Tenant_Master_MileStone, L("KanbanMileStone"));
                mileStone.CreateChildPermission(AppPermissions.Pages_Tenant_Master_MileStone_Create, L("CreateMileStone"));
                mileStone.CreateChildPermission(AppPermissions.Pages_Tenant_Master_MileStone_Edit, L("EditMileStone"));
                mileStone.CreateChildPermission(AppPermissions.Pages_Tenant_Master_MileStone_Delete, L("DeleteMileStone"));

            var stage = master.CreateChildPermission(AppPermissions.Pages_Tenant_Master_kanbanStage, L("KanbanStage"));
                stage.CreateChildPermission(AppPermissions.Pages_Tenant_Master_kanbanStage_Create, L("CreateStage"));
                stage.CreateChildPermission(AppPermissions.Pages_Tenant_Master_kanbanStage_Edit, L("EditStage"));
                stage.CreateChildPermission(AppPermissions.Pages_Tenant_Master_kanbanStage_Delete, L("DeleteStage"));

            var leadstatus = master.CreateChildPermission(AppPermissions.Pages_Tenant_Master_LeadStatus, L("LeadStatus"));
                leadstatus.CreateChildPermission(AppPermissions.Pages_Tenant_Master_LeadStatus_Create, L("CreateLeadStatus"));
                leadstatus.CreateChildPermission(AppPermissions.Pages_Tenant_Master_LeadStatus_Edit, L("EditLeadStatus"));
                leadstatus.CreateChildPermission(AppPermissions.Pages_Tenant_Master_LeadStatus_Delete, L("DeleteLeadStatus"));

            var activityType = master.CreateChildPermission(AppPermissions.Pages_Tenant_Master_ActivityType, L("ActivityType"));
                activityType.CreateChildPermission(AppPermissions.Pages_Tenant_Master_ActivityType_Create, L("CreateActivityType"));
                activityType.CreateChildPermission(AppPermissions.Pages_Tenant_Master_ActivityType_Edit, L("EditActivityType"));
                activityType.CreateChildPermission(AppPermissions.Pages_Tenant_Master_ActivityType_Delete, L("DeleteActivityType"));

            var leadType = master.CreateChildPermission(AppPermissions.Pages_Tenant_Master_LeadType, L("LeadCategory"));
                leadType.CreateChildPermission(AppPermissions.Pages_Tenant_Master_LeadType_Create, L("CreateLeadCategory"));
                leadType.CreateChildPermission(AppPermissions.Pages_Tenant_Master_LeadType_Edit, L("EditLeadCategory"));
                leadType.CreateChildPermission(AppPermissions.Pages_Tenant_Master_LeadType_Delete, L("DeleteLeadCategory"));

            var leadReason = master.CreateChildPermission(AppPermissions.Pages_Tenant_Master_LeadReason, L("LostReason"));
                leadReason.CreateChildPermission(AppPermissions.Pages_Tenant_Master_LeadReason_Create, L("CreateReason"));
                leadReason.CreateChildPermission(AppPermissions.Pages_Tenant_Master_LeadReason_Edit, L("EditReason"));
                leadReason.CreateChildPermission(AppPermissions.Pages_Tenant_Master_LeadReason_Delete, L("DeleteReason"));

            var department = master.CreateChildPermission(AppPermissions.Pages_Tenant_Master_Department, L("Division"));
                department.CreateChildPermission(AppPermissions.Pages_Tenant_Master_Department_Create, L("CreateDivision"));
                department.CreateChildPermission(AppPermissions.Pages_Tenant_Master_Department_Edit, L("EditDivision"));
                department.CreateChildPermission(AppPermissions.Pages_Tenant_Master_Department_Delete, L("DeleteDivision"));

            var industry = master.CreateChildPermission(AppPermissions.Pages_Tenant_Master_Industry, L("Industry"));
                industry.CreateChildPermission(AppPermissions.Pages_Tenant_Master_Industry_Create, L("CreateIndustry"));
                industry.CreateChildPermission(AppPermissions.Pages_Tenant_Master_Industry_Edit, L("EditIndustry"));
                industry.CreateChildPermission(AppPermissions.Pages_Tenant_Master_Industry_Delete, L("DeleteIndustry"));

            var team = master.CreateChildPermission(AppPermissions.Pages_Tenant_Master_Team, L("Team"));
                team.CreateChildPermission(AppPermissions.Pages_Tenant_Master_Team_Create, L("CreateTeam"));
                team.CreateChildPermission(AppPermissions.Pages_Tenant_Master_Team_Edit, L("EditTeam"));
                team.CreateChildPermission(AppPermissions.Pages_Tenant_Master_Team_Edit_SalesPerson_Delete, L("DeleteSalesPreson"));
                team.CreateChildPermission(AppPermissions.Pages_Tenant_Master_Team_Delete, L("DeleteTeam"));

            var opportunitysource = master.CreateChildPermission(AppPermissions.Pages_Tenant_Master_OpportunitySource, L("OpportunitySource"));
                opportunitysource.CreateChildPermission(AppPermissions.Pages_Tenant_Master_OpportunitySource_Create, L("CreateOpportunitySource"));
                opportunitysource.CreateChildPermission(AppPermissions.Pages_Tenant_Master_OpportunitySource_Edit, L("EditOpportunitySource"));
                opportunitysource.CreateChildPermission(AppPermissions.Pages_Tenant_Master_OpportunitySource_Delete, L("DeleteOpportunitySource"));

            var whybafco = master.CreateChildPermission(AppPermissions.Pages_Tenant_Master_Whybafco, L("Whybafco"));
                whybafco.CreateChildPermission(AppPermissions.Pages_Tenant_Master_Whybafco_Create, L("CreateWhybafco"));
                whybafco.CreateChildPermission(AppPermissions.Pages_Tenant_Master_Whybafco_Edit, L("EditWhybafco"));
                whybafco.CreateChildPermission(AppPermissions.Pages_Tenant_Master_Whybafco_Delete, L("DeleteWhybafco"));

            var emaildomain = master.CreateChildPermission(AppPermissions.Pages_Tenant_Master_EmailDomain, L("IgnoreDomain"));
                emaildomain.CreateChildPermission(AppPermissions.Pages_Tenant_Master_EmailDomain_Create, L("CreateEmailDomain"));
                emaildomain.CreateChildPermission(AppPermissions.Pages_Tenant_Master_EmailDomain_Edit, L("EditEmailDomain"));
                emaildomain.CreateChildPermission(AppPermissions.Pages_Tenant_Master_EmailDomain_Delete, L("DeleteEmailDomain"));

//Product Family
            var productfamily = pages.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily, L("ProductFamily"));

            var productattribute = productfamily.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductAttribute, L("ProductAttribute"));
                productattribute.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductAttribute_Create, L("CreateProductAttribute"));
                productattribute.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductAttribute_Edit, L("EditProductAttribute"));
                productattribute.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductAttribute_Delete, L("DeleteProductAttribute"));

            var attributegroup = productfamily.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductAttributeGroup, L("ProductAttributeGroup"));
                attributegroup.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductAttributeGroup_Create, L("CreateProductAttributeGroup"));
                attributegroup.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductAttributeGroup_Edit, L("EditProductAttributeGroup"));
                attributegroup.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductAttributeGroup_Delete, L("DeleteProductAttributeGroup"));
                attributegroup.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductAttributeGroup_Edit_AttributeDetail, L("AttributeDetail"));
                attributegroup.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductAttributeGroup_Edit_AttributeDetail_Delete, L("DeleteAttributeDetail"));

            var collection = productfamily.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_Collection, L("Collection"));
                collection.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_Collection_Create, L("CreateCollection"));
                collection.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_Collection_Edit, L("EditCollection"));
                collection.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_Collection_Delete, L("DeleteCollection"));

            var productsfamily = productfamily.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductFamily, L("ProductFamily"));
                productsfamily.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductFamily_Create, L("CreateProductFamily"));
                productsfamily.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductFamily_Edit, L("EditProductFamily"));
                productsfamily.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductFamily_Delete, L("DeleteProductFamily"));

            var producttype = productfamily.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductType, L("ProductCatagory"));
                producttype.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductType_Create, L("CreateProductCatagory"));
                producttype.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductType_Edit, L("EditProductCatagory"));
                producttype.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductType_Delete, L("DeleteProductCatagory"));

            var productgroup = productfamily.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductGroup, L("ProductGroup"));
                productgroup.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductGroup_Create, L("CreateProductGroup"));
                productgroup.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductGroup_Edit, L("EditProductGroup"));
                productgroup.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductGroup_Delete, L("DeleteProductGroup"));

            var productspecification = productfamily.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductSpecification, L("ProductSpecification"));
                productspecification.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductSpecification_Create, L("CreateProductSpecification"));
                productspecification.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductSpecification_Edit, L("EditProductSpecification"));
                productspecification.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductSpecification_Delete, L("DeleteProductSpecification"));
                productspecification.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductSpecification_Edit_ProductSpecificationGroupDetail, L("ProductSpecificationGroupDetail"));
                productspecification.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_ProductSpecification_Edit_ProductSpecificationGroupDetail_Expand, L("SpecificationGroupDetailExpand"));

            var products = productfamily.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_Products, L("Products"));
                products.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_Products_Create, L("CreateProducts"));
                products.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_Products_Edit, L("EditProducts"));
                products.CreateChildPermission(AppPermissions.Pages_Tenant_ProductFamily_Products_Delete, L("DeleteProducts"));

//Addressbook
            var addressbook = pages.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook, L("AddressBook"));

            var company = addressbook.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_Company, L("Company"));
                company.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_Company_Create, L("CreateCompany"));

            var companyEdit = company.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_Company_Edit, L("EditCompany"));
                companyEdit.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_Company_Edit_CompanyApproval, L("CompanyApproval"));
                companyEdit.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_Company_Edit_Managedby, L("Edit ManagedBy"));

            var contactDetail = companyEdit.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_Company_Edit_ContactDetails, L("ContactDetails"));
                contactDetail.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_Company_Edit_ContactDetails_Create, L("CreateContact"));
                contactDetail.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_Company_Edit_ContactDetails_Edit, L("EditContact"));
                contactDetail.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_Company_Edit_ContactDetails_Delete, L("DeleteContact"));

                company.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_Company_Delete, L("DeleteCompany"));

           

            var contact = addressbook.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_Contact, L("Contact"));
                contact.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_Contact_Create, L("CreateContact"));
                contact.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_Contact_Edit, L("EditContact"));
                contact.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_Contact_Delete, L("DeleteContact"));

            var customertype = addressbook.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_CustomerType, L("CustomerType"));
                customertype.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_CustomerType_Create, L("CreateCustomerType"));
                customertype.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_CustomerType_Edit, L("EditCustomerType"));
                customertype.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_CustomerType_Delete, L("DeleteCustomerType"));

            var infotype = addressbook.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_InfoType, L("InfoType"));
                infotype.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_InfoType_Create, L("CreateInfoType"));
                infotype.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_InfoType_Edit, L("EditInfoType"));
                infotype.CreateChildPermission(AppPermissions.Pages_Tenant_AddressBook_InfoType_Delete, L("DeleteInfoType"));

//Activites
            var activities = pages.CreateChildPermission(AppPermissions.Pages_Tenant_Activities, L("Activities"));

            var enquiryactivity = activities.CreateChildPermission(AppPermissions.Pages_Tenant_EnquiryActivity, L("EnquiryActivity"));
                enquiryactivity.CreateChildPermission(AppPermissions.Pages_Tenant_EnquiryActivity_AddComment, L("AddComment"));
                enquiryactivity.CreateChildPermission(AppPermissions.Pages_Tenant_EnquiryActivity_OpenActivity, L("OpenActivity"));
                enquiryactivity.CreateChildPermission(AppPermissions.Pages_Tenant_EnquiryActivity_OpenEnquiry, L("OpenEnquiry"));

            var jobactivity = activities.CreateChildPermission(AppPermissions.Pages_Tenant_JobActivity, L("JobActivity"));
                jobactivity.CreateChildPermission(AppPermissions.Pages_Tenant_JobActivity_Open_Activity, L("OpenActivity"));
                jobactivity.CreateChildPermission(AppPermissions.Pages_Tenant_JobActivity_Open_Enquiry, L("OpenEnquiry"));

//Enquiry

            var enquiry = pages.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry, L("Enquiry"));

            var enqury = enquiry.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_Enquiry, L("MakrtingKanban"));

                enqury.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_Enquiry_Create, L("CreateEnquiry"));

            var editEnquiry = enqury.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_Enquiry_Edit, L("EditEnquiry"));
            var ovrAct = editEnquiry.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_Enquiry_Edit_OverAllActivity, L("OverAllActivity"));
                ovrAct.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_Enquiry_Edit_OverAllActivity_CreateNewActivity, L("CreateNewActivity"));
                ovrAct.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_Enquiry_Edit_OverAllActivity_LinkedContacts_CreateContact, L("CreateContact"));
                ovrAct.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_Enquiry_Edit_OverAllActivity_LinkedContacts_AddContact, L("AddContact"));

            var gridview =  enqury.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_Enquiry_Gridview, L("GridviewEnquiry"));
                gridview.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_Enquiry_Gridview_Create, L("GridviewCreateEnquiry"));
                gridview.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_Enquiry_Gridview_Edit, L("GridviewEditEnquiry"));
                gridview.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_Enquiry_Gridview_Delete, L("GridviewDeleteEnquiry"));

            var salesenquiry = enquiry.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_SalesEnquiry, L("SalesKanban"));

            var salesenquiryedit = salesenquiry.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_SalesEnquiry_Edit, L("EditSalesEnquiry"));

            var squotations = salesenquiryedit.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotation_Enquiry, L("Quotation"));
                squotations.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_Enquiry_Edit_Details_CreateQuotation, L("CreateQuotation"));
                squotations.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotation_Edit_Kanban, L("EditQuotation"));
                squotations.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotation_Delete_Enquiry, L("DeleteQuotation"));

            salesenquiryedit.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_SalesEnquiry_Edit_Team, L("EditTeam"));

            var gridviewsales = salesenquiry.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_SalesEnquiry_Gridview, L("GridviewSalesEnquiry"));

            var salEnquiry = gridviewsales.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_Enquiry_Gridview_Enquiry, L("Leads"));
                salEnquiry.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_SalesEnquiry_Gridview_Edit, L("GridviewEditSalesEnquiry"));
                salEnquiry.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_SalesEnquiry_Gridview_Delete, L("GridviewDeleteSalesEnquiry"));

            var ClsEnquiry = gridviewsales.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_Enquiry_Gridview_ClosedEnquiry, L("ArchivedLeads"));
                ClsEnquiry.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_Enquiry_Gridview_ClosedEnquiry_Open, L("OpenClosedEnquiry"));
                ClsEnquiry.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_Enquiry_Gridview_ClosedEnquiry_Revert, L("RevertClosedEnquiry"));

            var eQuotation = gridviewsales.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_Enquiry_Gridview_Quotation, L("Quotation"));
                eQuotation.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotation_Edit_Grid, L("EditQuotation"));
                eQuotation.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotation_Delete_Grid, L("DeleteQuotation"));

                gridviewsales.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_SalesEnquiry_Create, L("CreateSalesEnquiry"));
               
            var leads = enquiry.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_Leads, L("Leads"));
                leads.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_Leads_Edit, L("EditLeads"));
                leads.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_Leads_Delete, L("DeleteLeads"));

            var junk = enquiry.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_Junk, L("Junk"));
                junk.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_Junk_Edit, L("EditJunk"));
                junk.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_Junk_Delete, L("DeleteJunk"));
                junk.CreateChildPermission(AppPermissions.Pages_Tenant_Enquiry_Junk_Revert, L("RevertJunk"));

//Quotation
            var quotation = pages.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation, L("Quotation"));


            var quotations = quotation.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotation, L("Quotation"));

                quotations.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotation_Create, L("CreateQuotation"));

            var quotationedit = quotations.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotation_Edit, L("EditQuotation"));

            var quotationdetail = quotationedit.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotation_Edit_QuotationDetail, L("QuotationDetails"));
                quotationdetail.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotation_Edit_QuotationDetails_QuotationProductLink, L("QuotationProductLink"));
                quotationdetail.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotation_Edit_QuotationDetails_QuotationproductDiscountApproval, L("QuotationproductDiscountApproval"));
                quotationdetail.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotation_Edit_QuotationDetails_ImportProduct, L("ImportProduct"));
                quotationdetail.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotation_Edit_QuotationDetails_AddQuotationProduct, L("AddQuotationProduct"));
                quotationdetail.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotation_Edit_QuotationDetails_AddSection, L("AddSection"));
                quotationdetail.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotation_Edit_QuotationDetails_EditSection, L("EditSection"));
                quotationdetail.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotation_Edit_QuotationDetails_EditQuotationProduct, L("EditQuotationProduct"));
                quotationdetail.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotation_Edit_QuotationDetails_DeleteQuotationProduct, L("DeleteQuotationProduct"));
                quotationdetail.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotation_Edit_QuotationDetails_DiscountApprove, L("OverAllDiscountApprove"));

            quotationedit.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotation_Edit_ImportHistory, L("ProductImportHistory"));

            var revision = quotationedit.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotation_Edit_RevisedHistory, L("RevisionHistory"));
                revision.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotation_Edit_RevisionHistory_Open, L("OpenRevision"));

                quotations.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotation_Open, L("OpenQuotation"));
                quotations.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_Quotation_Delete, L("DeleteQuotation"));

            var status = quotation.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationStatus, L("QuotationStatus"));
                status.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationStatus_Create, L("CreateQuotationStatus"));
                status.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationStatus_Edit, L("EditQuotationStatus"));
                status.CreateChildPermission(AppPermissions.Pages_Tenant_Quotation_QuotationStatus_Delete, L("DeleteQuotationStatus"));


 //Administration
            var administration = pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));

            var roles = administration.CreateChildPermission(AppPermissions.Pages_Administration_Roles, L("Roles"));
                roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Create, L("CreatingNewRole"));
                roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Edit, L("EditingRole"));
                roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Delete, L("DeletingRole"));

            var userdesig = administration.CreateChildPermission(AppPermissions.Pages_Administration_UserDesignation, L("UserDesignation"));
                userdesig.CreateChildPermission(AppPermissions.Pages_Administration_UserDesignation_Create, L("CreatingUserDesignation"));
                userdesig.CreateChildPermission(AppPermissions.Pages_Administration_UserDesignation_Edit, L("EditingUserDesignation"));
                userdesig.CreateChildPermission(AppPermissions.Pages_Administration_UserDesignation_Delete, L("DeletingUserDesignation"));

            var users = administration.CreateChildPermission(AppPermissions.Pages_Administration_Users, L("Users"));
                users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Create, L("CreatingNewUser"));
                users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Edit, L("EditingUser"));
                users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Delete, L("DeletingUser"));
                users.CreateChildPermission(AppPermissions.Pages_Administration_Users_ChangePermissions, L("ChangingPermissions"));
                users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Impersonation, L("LoginForUsers"));
    
            var languages = administration.CreateChildPermission(AppPermissions.Pages_Administration_Languages, L("Languages"));
                languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Create, L("CreatingNewLanguage"));
                languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Edit, L("EditingLanguage"));
                languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Delete, L("DeletingLanguages"));
                languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeTexts, L("ChangingTexts"));

                administration.CreateChildPermission(AppPermissions.Pages_Administration_AuditLogs, L("AuditLogs"));

            var organizationUnits = administration.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits, L("OrganizationUnits"));
                organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree, L("ManagingOrganizationTree"));
                organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers, L("ManagingMembers"));
           
 //TENANT-SPECIFIC PERMISSIONS

                administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);
                administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement, L("Subscription"), multiTenancySides: MultiTenancySides.Tenant);
           
 //HOST-SPECIFIC PERMISSIONS

            var editions = pages.CreateChildPermission(AppPermissions.Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
                editions.CreateChildPermission(AppPermissions.Pages_Editions_Create, L("CreatingNewEdition"), multiTenancySides: MultiTenancySides.Host);
                editions.CreateChildPermission(AppPermissions.Pages_Editions_Edit, L("EditingEdition"), multiTenancySides: MultiTenancySides.Host);
                editions.CreateChildPermission(AppPermissions.Pages_Editions_Delete, L("DeletingEdition"), multiTenancySides: MultiTenancySides.Host);

            var tenants = pages.CreateChildPermission(AppPermissions.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
                tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Create, L("CreatingNewTenant"), multiTenancySides: MultiTenancySides.Host);
                tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Edit, L("EditingTenant"), multiTenancySides: MultiTenancySides.Host);
                tenants.CreateChildPermission(AppPermissions.Pages_Tenants_ChangeFeatures, L("ChangingFeatures"), multiTenancySides: MultiTenancySides.Host);
                tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Delete, L("DeletingTenant"), multiTenancySides: MultiTenancySides.Host);
                tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Impersonation, L("LoginForTenants"), multiTenancySides: MultiTenancySides.Host);

                administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Host);
                administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Maintenance, L("Maintenance"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
                administration.CreateChildPermission(AppPermissions.Pages_Administration_HangfireDashboard, L("HangfireDashboard"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
                administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Host);
                      
//Export
            var export = pages.CreateChildPermission(AppPermissions.Pages_Tenant_Export, L("Export"));

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, stemConsts.LocalizationSourceName);
        }
    }
}
