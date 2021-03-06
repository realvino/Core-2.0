﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Citys;
using tibs.stem.Select2.Dtos;

namespace tibs.stem.Select2
{
    public interface ISelect2AppService : IApplicationService 
    {
        Task<Select2City> GetCity();
        Task<Select2Result> GetCountry();
        Task<Select2Result> GetProductSubGroup(NullableIdDto input);
        Task<Select3Result> GetAccountHandler();
        Task<Select2Result> GetLeadReason();
        Task<Select2Result> GetDesignation();
        Task<Select2Result> GetCompany(LocationInput input);
        Task<Select2Result> GetLocation(LocationInput input);
        Task<Select2Result> GetLineType();
        Task<Select2Result> GetAllLocation();
        Task<Select2Result> GetAllCompany(Select2Input input);
        Task<Select2sales> GetCompanyWithSales();
        Task<Select2Result> GetDepartment();
        Task<Select2Result> GetTitle();
        Task<Select2Result> GetActivityTypes();
        Task<Select2Result> GetContactTypeInfo();
        Task<Select2Result> GetCompanyTypeinfo();
        Task<Select2Result> GetNewCompanyType();
        Task<Select2Result> GetNewCustomerType();
        Task<Select2Contact> GetCompanyContacts(NullableIdDto input);
        Task<Select3Result> GetSalesman();
        Task<Select2Result> GetLeadType();
        Task<Select2Result> GetLeadSource();
        Task<Select2Result> GetCompatitorCompany();
        Task<Select3Result> GetSalesCoordinator();
        Task<Select3Result> GetDesigner();
        Task<Select2Result> GetDepartmentSales();
        Task<Select2Result> GetSalesFromDepatment(NullableIdDto input);
        Task<Select2Result> GetPriceLevel();
        Task<Select2Attribute> GetAttribute();
        Task<Select2Result> GetAttributeGroup();
        Task<Select2Result> GetProductGroup();
        Task<Select2Result> GetCollection();
        Task<Select2Result> GetProductFamily();
        Task<Select2Product> GetProductSpecification();
        Task<Select2product> GetProduct();
        Task<Select2Result> GetSection(NullableIdDto input);
        Task<Select2Result> GetContactEmail(NullableIdDto input);
        Task<Select2Result> GetContactMobile(NullableIdDto input);
        Task<Select2product> GetSpecProduct(NullableIdDto input);
        Task<Select2Result> GetInquiry();
        Task<Select3Result> GetSalesManager();
        Task<Select3Result> GetOtherSalesman();
        Task<Select3Result> GetOtherSalesManager();
        Task<Select3Result> GetTeamSalesman(NullableIdDto input);
        Task<Select2sales> GetCompanyWithSalesman();
        Task<Select2Inquiry> GetInquiryDetails(NullableIdDto input);
        Task<Select2Result> GetProductCategory();
        Task<Select2Result> GetEnquiryStatus();
        Task<Select2Result> GetWhyBafco();
        Task<Select2Result> GetOpportunitySource();
        Task<Select2Company> GetCompanyDetails(Select2CompanyInput input);
        Task<Select2Result> GetLeadStatus();
        Task<Select2Discount> GetCompanyDiscount(NullableIdDto input);
        Task<Select2productdetailsdto> GetTemporaryProducts(Select2Input input);
        Task<Select2Result> GetUserDesignation();
        Task<Select2View> GetViews();
        Task<Select2Column> GetReportColumn();
        Task<Select3Result> GetAllSalesman();
        Task<Select3UserResult> GetUserSalesManager();
        Task<Select3UserResult> GetUserSalesManagerToTeam();
        Task<Select3UserResult> GetUserSalesPerson();
        Task<Select3UserResult> GetUserSalesPersonfromTeam(NullableIdDto input);
        Task<Select3UserResult> GetUserSalesPersontoTeam();
        List<SliderDataList> GetSalesExecutive(NullableIdDto input);
        Task<SelectDResult> GetDashboardTeam();
        Task<Select3Result> GetDesigners();
        Task<Select2Result> GetFinishes();
        Task<Select3UserResult> GetTeamUserProfile(NullableIdDto input);
        Task<SalesPersonTeamDto> GetSalesPersonTeam();

    }
}
