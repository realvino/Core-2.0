using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Finish;
using tibs.stem.Finishedd.Dto;

namespace tibs.stem.Finishedd
{
    public class FinishedAppService : stemAppServiceBase, IFinishedAppService
    {
        private readonly IRepository<Finished> _FinishedRepository;
        private readonly IRepository<FinishedDetail> _FinishedDetailRepository;
        public FinishedAppService(IRepository<Finished> FinishedRepository, IRepository<FinishedDetail> FinishedDetailRepository)
        {
            _FinishedRepository = FinishedRepository;
            _FinishedDetailRepository = FinishedDetailRepository;
        }
        public async Task<PagedResultDto<FinishedList>> GetFinished(GetFinishedInput input)
        {
            var query = _FinishedRepository.GetAll()
                        .WhereIf(
                            !input.Filter.IsNullOrEmpty(),
                            p => p.Code.Contains(input.Filter) ||
                                 p.Name.Contains(input.Filter) ||
                                 p.Id.ToString().Contains(input.Filter)
                        );

            var Finish = (from a in query
                            select new FinishedList
                            {
                                Id = a.Id,
                                Code = a.Code,
                                Name = a.Name,
                                Description = a.Description
                            });

            var FinishedCount = Finish.Count();
            var Finishedlist = Finish.Take(input.MaxResultCount).Skip(input.SkipCount).ToList();
                                           
            var FinishedlistOutput = Finish.MapTo<List<FinishedList>>();

            return new PagedResultDto<FinishedList>(FinishedCount, FinishedlistOutput);
        }
        public async Task<FinishedList> GetFinishedForEdit(NullableIdDto input)
        {
            var query = _FinishedRepository.GetAll().Where(p => p.Id == input.Id);

            var Finish = (from a in query
                          select new FinishedList
                          {
                              Id = a.Id,
                              Code = a.Code,
                              Name = a.Name,
                              Description = a.Description
                          }).FirstOrDefault();

            var FinishedlistOutput = Finish.MapTo<FinishedList>();
            return FinishedlistOutput;
        }
        public async Task CreateOrUpdateFinished(FinishedInput input)
        {
            if (input.Id != 0)
            {
                await UpdateFinishedAsync(input);
            }
            else
            {
                await CreateFinishedAsync(input);
            }
        }
        public virtual async Task CreateFinishedAsync(FinishedInput input)
        {
            var Finish = input.MapTo<Finished>();
            var val = _FinishedRepository.GetAll().Where(p => p.Code == input.Code || p.Name == input.Name).FirstOrDefault();
            if (val == null)
            {
                await _FinishedRepository.InsertAsync(Finish);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Name '" + input.Name + "' or Code '" + input.Code + "'...");
            }
        }
        public virtual async Task UpdateFinishedAsync(FinishedInput input)
        {
            var Finish = await _FinishedRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, Finish);

            var val = _FinishedRepository.GetAll().Where(p => (p.Code == input.Code || p.Name == input.Name) && p.Id != input.Id).FirstOrDefault();
            if (val == null)
            {
                await _FinishedRepository.UpdateAsync(Finish);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Name '" + input.Name + "' or Code '" + input.Code + "'...");
            }
        }
        public async Task GetDeleteFinished(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 37);
                sqlComm.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {

                    da.Fill(ds);
                }

            }

            if (input.Id > 0)
            {
                var results = ds.Rows.Cast<DataRow>().Where(myRow => (int)myRow["Id"] == input.Id);
                if (results.Count() > 0)
                {
                    throw new UserFriendlyException("Ooops!", "Finished cannot be deleted '");
                }
                else
                {
                    await _FinishedRepository.DeleteAsync(input.Id);
                }
            }

        }
        public async Task<List<FinishedDetailList>> GetFinishedDetail(NullableIdDto input)
        {
            var query = _FinishedDetailRepository.GetAll().Where(p => p.ProductId == input.Id);
            var reg = (from a in query
                       select new FinishedDetailList
                       {
                           Id = a.Id,
                           GPCode = a.GPCode,
                           Price = a.Price,
                           FinishedId = a.FinishedId,
                           FinishedCode = a.Finishedd.Code,
                           FinishedName = a.Finishedd.Name,
                           FinishedDescription = a.Finishedd.Description,
                           ProductId = a.ProductId,
                           ProductCode = a.Products.ProductCode,
                           ProductName = a.Products.ProductName,
                           ProductSuspectCode = a.Products.SuspectCode,
                           ProductGpcode = a.Products.Gpcode,
                           ProductDescription = a.Products.Description,
                           ProductPrice = (decimal)a.Products.Price
                       }).ToList();

            var FinishedDetailListDtos = reg.MapTo<List<FinishedDetailList>>();
            return FinishedDetailListDtos;
        }
        public async Task<FinishedDetailList> GetFinishedDetailForEdit(NullableIdDto input)
        {
            var query = _FinishedDetailRepository.GetAll().Where(p => p.Id == input.Id);

            var Finish = (from a in query
                          select new FinishedDetailList
                          {
                              Id = a.Id,
                              GPCode = a.GPCode,
                              Price = a.Price,
                              FinishedId = a.FinishedId,
                              FinishedCode = a.Finishedd.Code,
                              FinishedName = a.Finishedd.Name,
                              FinishedDescription = a.Finishedd.Description,
                              ProductId = a.ProductId,
                              ProductCode = a.Products.ProductCode,
                              ProductName = a.Products.ProductName,
                              ProductSuspectCode = a.Products.SuspectCode,
                              ProductGpcode = a.Products.Gpcode,
                              ProductDescription = a.Products.Description,
                              ProductPrice = (decimal)a.Products.Price
                          }).FirstOrDefault();

            var FinishedDetailList = Finish.MapTo<FinishedDetailList>();
            
            return FinishedDetailList;
        }
        public async Task CreateOrUpdateFinishedDetail(FinishedDetailInput input)
        {
            if (input.Id != 0)
            {
                await UpdateFinishedDetailAsync(input);
            }
            else
            {
                await CreateFinishedDetailAsync(input);
            }
        }
        public virtual async Task CreateFinishedDetailAsync(FinishedDetailInput input)
        {
            var FinishDetail = input.MapTo<FinishedDetail>();
            var val = _FinishedDetailRepository.GetAll().Where(p => (p.FinishedId == input.FinishedId && p.ProductId == input.ProductId) || p.GPCode == input.GPCode).FirstOrDefault();
            if (val == null)
            {
                await _FinishedDetailRepository.InsertAsync(FinishDetail);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in FinishedDetail...");
            }
        }
        public virtual async Task UpdateFinishedDetailAsync(FinishedDetailInput input)
        {
            var FinishDetail = await _FinishedDetailRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, FinishDetail);

            var val = _FinishedDetailRepository.GetAll().Where(p => ((p.FinishedId == input.FinishedId && p.ProductId == input.ProductId) || p.GPCode == input.GPCode) && p.Id != input.Id).FirstOrDefault();
            if (val == null)
            {
                await _FinishedDetailRepository.UpdateAsync(FinishDetail);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in FinishedDetail...");
            }
        }
        public virtual async Task DeleteFinishedDetail(EntityDto input)
        {
            await _FinishedDetailRepository.DeleteAsync(input.Id);
        }

    }
}
