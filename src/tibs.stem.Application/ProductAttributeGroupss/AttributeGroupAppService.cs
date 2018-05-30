using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using Abp.UI;
using System.Data.SqlClient;
using tibs.stem.ProductAttributeGroups;
using tibs.stem.ProductAttributes;
using tibs.stem.ProductAttributeGroupss.Dto;
using tibs.stem.AttributeGroupDetails;
using tibs.stem.AttributeGroupDetails.Dto;
using System.Data;
using tibs.stem.ProductAttributeGroupss.Exporting;
using tibs.stem.Dto;

namespace tibs.stem.ProductAttributeGroupss
{
    public class AttributeGroupAppService :stemAppServiceBase,IAttributeGroupAppService
    {
        private readonly IRepository<ProductAttributeGroup> _attributeGroupRepository;
        private readonly IRepository<ProductAttribute> _productAttributeRepository;
        private readonly IRepository<AttributeGroupDetail> _AttributeGroupDetailRepository;
        private readonly IAttributeGroupListExcelExporter _attributeGroupListExcelExporter;



        public AttributeGroupAppService(IRepository<ProductAttributeGroup> attributeGroupRepository, IAttributeGroupListExcelExporter attributeGroupListExcelExporter,
        IRepository<ProductAttribute> productAttributeRepository, IRepository<AttributeGroupDetail> AttributeGroupDetailRepository)
        {
            _attributeGroupRepository = attributeGroupRepository;
            _productAttributeRepository = productAttributeRepository;
            _AttributeGroupDetailRepository = AttributeGroupDetailRepository;
            _attributeGroupListExcelExporter = attributeGroupListExcelExporter;
        }

        public ListResultDto<AttributeGroupListDto> GetAttributeGroup(GetAttributeGroupInput input)
        {

            var query = _attributeGroupRepository.GetAll()
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.AttributeGroupName.Contains(input.Filter) ||
                        u.AttributeGroupCode.Contains(input.Filter) ||
                        u.Id.ToString().Contains(input.Filter) 
                       
                        )

                  .ToList();
            var reg = from r in query
                      select new AttributeGroupListDto
                      {
                          Id = r.Id,
                          AttributeGroupName = r.AttributeGroupName,
                          AttributeGroupCode = r.AttributeGroupCode
                         
                      };
            var AttributeGroupListDtos = reg.MapTo<List<AttributeGroupListDto>>();


            return new ListResultDto<AttributeGroupListDto>(AttributeGroupListDtos.MapTo<List<AttributeGroupListDto>>());
        }
        public async Task<GetAttributeGroup> GetAttributeGroupForEdit(EntityDto input)
        {
            var output = new GetAttributeGroup
            {
            };

            var attributeGroup = _attributeGroupRepository
                .GetAll().Where(u => u.Id == input.Id).FirstOrDefault();


            var AttributeGroupDetail = (from r in _AttributeGroupDetailRepository.GetAll() where r.AttributeGroupId == input.Id
                                        select new AttributeGroupDetailListDto
                                        {
                                            Id = r.Id,
                                            AttributeGroupId = r.AttributeGroupId,
                                            AttributeGroupName = r.AttributeGroups.AttributeGroupName,
                                            AttributeId = r.AttributeId,
                                            AttributeName = r.Attributes.AttributeName,
                                            AttributeCode = r.AttributeGroups.AttributeGroupCode,
                                            ImgPath = r.Attributes.Imageurl
                                        }).ToArray();

            output.attributeGroupDetails = AttributeGroupDetail;
            output.attributeGroup = attributeGroup.MapTo<CreateAttributeGroupInput>();


            //var data = attributeGroup.MapTo<CreateAttributeGroupInput>();

            // output = new GetAttributeGroup
            //{
            //   attributeGroupDetails = AttributeGroupDetail,
            //   attributeGroup = data
            // };

            return output;
        }

        public async Task CreateOrUpdateAttributeGroup(CreateAttributeGroupInput input)
        {
            if (input.Id != 0)
            {
                await UpdateAttributeGroup(input);
            }
            else
            {
                await CreateAttributeGroup(input);
            }
        }

        public async Task CreateAttributeGroup(CreateAttributeGroupInput input)
        {
            var attributeGroup = input.MapTo<ProductAttributeGroup>();
            var val = _attributeGroupRepository
             .GetAll().Where(u => u.AttributeGroupName == input.AttributeGroupName || u.AttributeGroupCode == input.AttributeGroupCode).FirstOrDefault();

            if (val == null)
            {
                await _attributeGroupRepository.InsertAsync(attributeGroup);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in AttributeGroupName '" + input.AttributeGroupName + "' or AttributeGroupCode '" + input.AttributeGroupCode + "'...");
            }
        }
        public async Task UpdateAttributeGroup(CreateAttributeGroupInput input)
        {
            var attributeGroup = input.MapTo<ProductAttributeGroup>();


            var val = _attributeGroupRepository
            .GetAll().Where(u => (u.AttributeGroupName == input.AttributeGroupName || u.AttributeGroupCode == input.AttributeGroupCode) && u.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _attributeGroupRepository.UpdateAsync(attributeGroup);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in AttributeGroupName '" + input.AttributeGroupName + "' or AttributeGroupCode '" + input.AttributeGroupCode + "'...");
            }
            
        }

        public ListResultDto<AttributeGroupDetailList> GetAttributeGroupDetail(GetAttributeGroupDetailInput input)
        {

            var query = _AttributeGroupDetailRepository.GetAll().Where(p => p.AttributeGroupId == input.AttributeGroupId);

            var AttributeGroupDetail = (from r in query
                                        select new AttributeGroupDetailList
                                        {
                                            Id = r.Id,
                                            AttributeGroupId = r.AttributeGroupId,
                                            AttributeGroupName = r.AttributeGroups.AttributeGroupName,
                                            AttributeId = r.AttributeId,
                                            AttributeName = r.Attributes.AttributeName

                                        }).ToList();


            return new ListResultDto<AttributeGroupDetailList>(AttributeGroupDetail.MapTo<List<AttributeGroupDetailList>>());
        }

        public async Task CreateOrUpdateAttributeGroupDetail(AttributeGroupDetailInput input)
        {
            if (input.Id != 0)
            {
                await UpdateAttributeGroupDetail(input);
            }
            else
            {
                await CreateAttributeGroupDetail(input);
            }
        }

        public async Task CreateAttributeGroupDetail(AttributeGroupDetailInput input)
        {
            var AttributeGroupDetail = input.MapTo<AttributeGroupDetail>();

            var val = _AttributeGroupDetailRepository
             .GetAll().Where(p => p.AttributeGroupId == input.AttributeGroupId && p.AttributeId == input.AttributeId).FirstOrDefault();

            if (val == null)
            {
                await _AttributeGroupDetailRepository.InsertAsync(AttributeGroupDetail);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "This data already exixts '");
            }
        }

        public async Task UpdateAttributeGroupDetail(AttributeGroupDetailInput input)
        {
            var AttributeGroupDetail = input.MapTo<AttributeGroupDetail>();
            var val = _AttributeGroupDetailRepository
             .GetAll().Where(p => p.AttributeGroupId == input.AttributeGroupId && p.AttributeId == input.AttributeId).FirstOrDefault();

            if (val == null)
            {
                AttributeGroupDetail.LastModificationTime = DateTime.Now;
                await _AttributeGroupDetailRepository.UpdateAsync(AttributeGroupDetail);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "This data already exixts '");
            }
        }

        public async Task GetDeleteAttributeGroupDetail(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 25);
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
                    throw new UserFriendlyException("Ooops!", "AttributeGroupDetail cannot be deleted '");
                }
                else
                {
                    await _AttributeGroupDetailRepository.DeleteAsync(input.Id);
                }
            }
        }

        public async Task GetDeleteAttributeGroup(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 22);
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
                    throw new UserFriendlyException("Ooops!", "AttributeGroup cannot be deleted '");
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
                    {
                        SqlCommand sqlComm = new SqlCommand("Sp_DeleteAllDetails", conn);
                        sqlComm.Parameters.AddWithValue("@TableId", 5);
                        sqlComm.Parameters.AddWithValue("@Id", input.Id);
                        sqlComm.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        sqlComm.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }

        public async Task<FileDto> GetAttributeGroupToExcel()
        {

            var query = _attributeGroupRepository.GetAll();
            var reg = from r in query
                      select new AttributeGroupListDto
                      {
                          Id = r.Id,
                          AttributeGroupName = r.AttributeGroupName,
                          AttributeGroupCode = r.AttributeGroupCode

                      };
            var order = await reg.OrderBy("AttributeGroupName").ToListAsync();

            var AttributeGroupListDtos = order.MapTo<List<AttributeGroupListDto>>();

            return _attributeGroupListExcelExporter.ExportToFile(AttributeGroupListDtos);
        }

    }

}
