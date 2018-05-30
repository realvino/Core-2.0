using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.QuotationStatuss;
using tibs.stem.Views;
using tibs.stem.Viewss.Dto;

namespace tibs.stem.Viewss
{
    public class ViewAppService : stemAppServiceBase, IViewAppService
    {
        private readonly IRepository<View> _viewRepository;
        private readonly IRepository<ReportColumn> _ReportColumnRepository;
        private readonly IRepository<QuotationStatus> _quotationStatusRepository;
        private readonly IRepository<DateFilter> _DatefilterRepository;


        public ViewAppService(
            IRepository<View> viewRepository,
            IRepository<ReportColumn> ReportColumnRepository,
            IRepository<QuotationStatus> quotationStatusRepository,
            IRepository<DateFilter> DatefilterRepository
        )
        {
            _viewRepository = viewRepository;
            _ReportColumnRepository = ReportColumnRepository;
            _quotationStatusRepository = quotationStatusRepository;
            _DatefilterRepository = DatefilterRepository;
        }
        public ListResultDto<ViewListDto> GetViews(ViewListInput input)
        {
            var query = _viewRepository.GetAll()
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Name.Contains(input.Filter))
                        .ToList();

            var queryData = (from r in query
                             join ur in UserManager.Users on r.CreatorUserId equals ur.Id into urJoined
                             from ur in urJoined.DefaultIfEmpty()
                             select new ViewListDto
                             {
                                 Id = r.Id,
                                 Name = r.Name,
                                 Query = r.Query,
                                 CreationTime = r.CreationTime.ToString(),
                                 CreatedBy = ur != null ? ur.UserName : "",
                                 IsEditable = r.IsEditable                               
                             });
            return new ListResultDto<ViewListDto>(queryData.MapTo<List<ViewListDto>>());
        }
        public async Task<GetViews> GetViewForEdit(EntityDto input)
        {
            var output = new GetViews
            {
            };
            var query = (from r in _viewRepository.GetAll()
                         join ur in UserManager.Users on r.CreatorUserId equals ur.Id into urJoined
                         from ur in urJoined.DefaultIfEmpty()
                         join pr in UserManager.Users on r.AllPersonId equals pr.Id into prJoined
                         from pr in prJoined.DefaultIfEmpty()
                         join mr in _quotationStatusRepository.GetAll() on r.QuotationStatusId equals mr.Id into mrJoined
                         from mr in mrJoined.DefaultIfEmpty()
                         join or in _DatefilterRepository.GetAll() on r.DateFilterId equals or.Id into orJoined
                         from or in orJoined.DefaultIfEmpty()

                         where r.Id == input.Id
                         select new ViewListDto
                             {
                                 Id = r.Id,
                                 Name = r.Name,
                                 Query = r.Query,
                                 CreationTime = r.CreationTime.ToString(),
                                 CreatedBy = ur != null ? ur.UserName : "",
                                 IsEditable = r.IsEditable,
                                 IsEnquiry = r.IsEnquiry,
                                 AllPersonId = r.AllPersonId,
                                 PersonName = pr != null ? pr.UserName : "",
                                 DateFilterId = r.DateFilterId,
                                 FilterName = or != null ? or.Name : "",
                                 QuotationStatusId = r.QuotationStatusId,
                                 StatusName = mr != null ? mr.Name : "",
                                 GraterAmount = r.GraterAmount,
                                 LessAmount = r.LessAmount,
                                 EnqStatusId = r.EnqStatusId,
                                 ClosureDateFilterId = r.ClosureDateFilterId,
                                 LastActivityDateFilterId = r.LastActivityDateFilterId,
                                 ClosureDate = r.ClosureDate,
                                 LastActivity = r.LastActivity,
                                 QuotationCreateBy = r.QuotationCreateBy,
                                 QuotationStatus = r.QuotationStatus,
                                 Salesman = r.Salesman,
                                 InquiryCreateBy = r.InquiryCreateBy,
                                 PotentialCustomer = r.PotentialCustomer,
                                 MileStoneName = r.MileStoneName,
                                 EnquiryStatus = r.EnquiryStatus,
                                 TeamName = r.TeamName,
                                 Coordinator = r.Coordinator,
                                 Designer = r.Designer,
                                 DesignationName = r.DesignationName,
                                 Emirates = r.Emirates,
                                 DepatmentName = r.DepatmentName,
                                 Categories = r.Categories,
                                 Status = r.Status,
                                 WhyBafco = r.WhyBafco,
                                 Probability = r.Probability,
                                 QuotationCreation = r.QuotationCreation,
                                 InquiryCreation = r.InquiryCreation,
                                 StatusForQuotation = r.StatusForQuotation,
                         }).FirstOrDefault();
            output.Viewdatas = query.MapTo<ViewListDto>();
            return output;
        }
        public async Task CreateOrUpdateView(ViewInput input)
        {
            if (input.Id > 0)
            {
                await UpdateView(input);
            }
            else
            {
                await CreateView(input);
            }
        }
        public async Task CreateView(ViewInput input)
        {
            try
            {

                var value = _viewRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

                input.Id = 0;

                var Data = input.MapTo<View>();
                if (string.IsNullOrEmpty(input.Salesman) == true)
                {
                    if (string.IsNullOrEmpty(value.Salesman) == false)
                    {
                        Data.Salesman = value.Salesman;
                    }
                }
                if (string.IsNullOrEmpty(input.Designer) == true)
                {
                    if (string.IsNullOrEmpty(value.Designer) == false)
                    {
                        Data.Designer = value.Designer;
                    }
                }
                if (string.IsNullOrEmpty(input.Coordinator) == true)
                {
                    if (string.IsNullOrEmpty(value.Coordinator) == false)
                    {
                        Data.Coordinator = value.Coordinator;
                    }
                }
                if (string.IsNullOrEmpty(input.Probability) == true)
                {
                    if (string.IsNullOrEmpty(value.Probability) == false)
                    {
                        Data.Probability = value.Probability;
                    }
                }
                if (string.IsNullOrEmpty(input.Probability) == true)
                {
                    if (string.IsNullOrEmpty(value.Probability) == false)
                    {
                        Data.Probability = value.Probability;
                    }
                }
                if (string.IsNullOrEmpty(input.StatusForQuotation) == true)
                {
                    if (string.IsNullOrEmpty(value.StatusForQuotation) == false)
                    {
                        Data.StatusForQuotation = value.StatusForQuotation;
                    }
                }
                if (string.IsNullOrEmpty(input.MileStoneName) == true)
                {
                    if (string.IsNullOrEmpty(value.MileStoneName) == false)
                    {
                        Data.MileStoneName = value.MileStoneName;
                    }
                }
                if (string.IsNullOrEmpty(input.EnquiryStatus) == true)
                {
                    if (string.IsNullOrEmpty(value.EnquiryStatus) == false)
                    {
                        Data.EnquiryStatus = value.EnquiryStatus;
                    }
                }
                if (string.IsNullOrEmpty(input.TeamName) == true)
                {
                    if (string.IsNullOrEmpty(value.TeamName) == false)
                    {
                        Data.TeamName = value.TeamName;
                    }
                }
                if (string.IsNullOrEmpty(input.DepatmentName) == true)
                {
                    if (string.IsNullOrEmpty(value.DepatmentName) == false)
                    {
                        Data.DepatmentName = value.DepatmentName;
                    }
                }
                if (string.IsNullOrEmpty(input.Categories) == true)
                {
                    if (string.IsNullOrEmpty(value.Categories) == false)
                    {
                        Data.Categories = value.Categories;
                    }
                }
                if (string.IsNullOrEmpty(input.ClosureDate) == true)
                {
                    if (string.IsNullOrEmpty(value.ClosureDate) == false)
                    {
                        Data.ClosureDate = value.ClosureDate;
                    }
                }
                if (string.IsNullOrEmpty(input.LastActivity) == true)
                {
                    if (string.IsNullOrEmpty(value.LastActivity) == false)
                    {
                        Data.LastActivity = value.LastActivity;
                    }
                }
                if (string.IsNullOrEmpty(input.QuotationCreation) == true)
                {
                    if (string.IsNullOrEmpty(value.QuotationCreation) == false)
                    {
                        Data.QuotationCreation = value.QuotationCreation;
                    }
                }



                var val = _viewRepository
                 .GetAll().Where(p => p.Name == input.Name).FirstOrDefault();

                if (val == null)
                {
                    await _viewRepository.InsertAsync(Data);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Name '" + input.Name + "' ...");
                }
            }
            catch(Exception ex)
            {

            }
        }
        public async Task UpdateView(ViewInput input)
        {
            var val = _viewRepository
             .GetAll().Where(p => p.Name == input.Name && p.Id != input.Id).FirstOrDefault();
            if (val == null)
            {
                var viw = await _viewRepository.GetAsync(input.Id);
                ObjectMapper.Map(input, viw);
                await _viewRepository.UpdateAsync(viw);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Name '" + input.Name + "' ...");
            }
        }
        public async Task GetDeleteView(EntityDto input)
        {
            await _viewRepository.DeleteAsync(input.Id);
        }
        public async Task<GetColumnList> GetGridColumns(EntityDto input)
        {
            GetColumnList sr = new GetColumnList();
            var columns = new List<ColumnList>();
            var query = (from r in _viewRepository.GetAll().Where(p => p.Id == input.Id) select r.Query).FirstOrDefault();

            //ConnectionAppService db = new ConnectionAppService();
            //DataTable ds = new DataTable();
            //using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            //{
            //    SqlCommand sqlComm = new SqlCommand("", conn)
            //    {
            //        CommandText = query,
            //        CommandType = CommandType.Text
            //    };
            //    using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
            //    {
            //        da.Fill(ds);
            //    }

            //    foreach (DataColumn column in ds.Columns)
            //    {

            //    }
            //}
            string str = query;
            string[] strArr = null;
            int count = 0;
            char[] splitchar = { ',' };
            strArr = str.Split(splitchar);
            for (count = 0; count <= strArr.Length - 1; count++)
            {
                var ReportColumnName = (from r in _ReportColumnRepository.GetAll() where r.Name == strArr[count] select r.Code).FirstOrDefault();
                try
                {
                    columns.Add(new ColumnList
                    {
                        Id = count,
                        Name = strArr[count],
                        ColumnName = char.IsUpper(ReportColumnName[0]) ? char.ToLower(ReportColumnName[0]) + ReportColumnName.Substring(1) : ReportColumnName
                    });
                }
                catch(Exception ex)
                {

                }
               
            }
            sr.ListDtos = columns.ToArray();
            return sr;
        }
        public IEnumerable<DataTable> GetGridDatas(EntityDto input)
        {

            List<DataTable> list = new List<DataTable>();

            var query = (from r in _viewRepository.GetAll().Where(p => p.Id == input.Id) select r.Query).FirstOrDefault();

            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("", conn)
                {
                    CommandText = query,
                    CommandType = CommandType.Text
                };
                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {
                    da.Fill(ds);
                }
                    list.Add(ds.Rows[0].Table);
            }
            return list;
        }
        public ListResultDto<ReportColumnListDto> GetReportColumn(GetReportColumnInput input)
        {
            var ReportColumn = _ReportColumnRepository.GetAll()

                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                       u.Code.Contains(input.Filter) ||
                       u.Name.Contains(input.Filter) ||
                       u.Type.ToString().Contains(input.Filter) ||
                       u.Id.ToString().Contains(input.Filter))
                .ToList();

            return new ListResultDto<ReportColumnListDto>(ReportColumn.MapTo<List<ReportColumnListDto>>());
        }
        public async Task<GetReportColumn> GetReportColumnForEdit(NullableIdDto input)
        {
            var output = new GetReportColumn
            {
            };

            var ReportColumn = _ReportColumnRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            output.ReportColumn = ReportColumn.MapTo<ReportColumnListDto>();
            return output;
        }
        public async Task CreateOrUpdateReportColumn(ReportColumnInputDto input)
        {
            if (input.Id != 0)
            {
                await UpdateReportColumn(input);
            }
            else
            {
                await CreateReportColumn(input);
            }
        }
        public async Task CreateReportColumn(ReportColumnInputDto input)
        {
            var ReportColumn = input.MapTo<ReportColumn>();

            await _ReportColumnRepository.InsertAsync(ReportColumn);

        }
        public async Task UpdateReportColumn(ReportColumnInputDto input)
        {
            var ReportColumn = await _ReportColumnRepository.GetAsync(input.Id);

            ObjectMapper.Map(input, ReportColumn);

            await _ReportColumnRepository.UpdateAsync(ReportColumn);


        }
        public async Task DeleteReportColumn(EntityDto input)
        {
            await _ReportColumnRepository.DeleteAsync(input.Id);
        }
        public async Task UpdateViewColumns(UpdateViewInput input)
        {
            if(input.Id > 0)
            {
                if(string.IsNullOrWhiteSpace(input.Query) == true)
                {
                    input.Query = null;
                }
                var view = (from v in _viewRepository.GetAll() where v.Id == input.Id select v).FirstOrDefault();
                view.Query = input.Query;
                await _viewRepository.UpdateAsync(view);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Please select the view then hide Colums...");
            }

        }
    }

    public class ColumnList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ColumnName { get; set; }

    }
    public class GetColumnList
    {
        public ColumnList[] ListDtos { get; set; }
    }
    public class UpdateViewInput
    {
        public int Id { get; set; }
        public string Query { get; set; }
    }
}
