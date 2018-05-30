using Abp.Application.Services.Dto;
using Abp.Authorization;
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
using System.Threading.Tasks;
using tibs.stem.Authorization;
using tibs.stem.Citys;
using tibs.stem.Countrys;
using tibs.stem.Dto;
using tibs.stem.Region.Dto;
using tibs.stem.Region.Exporting;
using tibs.stem.Tenants.Dashboard;

namespace tibs.stem.Region
{
    public class RegionAppService : stemAppServiceBase, IRegionAppService
    {
        private readonly IRepository<Regions> _regionRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<RegionCity> _regionCityRepository;
        private readonly IRegionListExcelExporter _regionListExcelExporter;
        public RegionAppService(IRepository<Regions> regionRepository, IRepository<City> cityRepository, IRepository<RegionCity> regionCityRepository , IRegionListExcelExporter regionListExcelExporter)
        {
            _regionRepository = regionRepository;
            _cityRepository = cityRepository;
            _regionCityRepository = regionCityRepository;
            _regionListExcelExporter = regionListExcelExporter;
        }
       // [AbpAuthorize(AppPermissions.Pages_Tenant_Geography_Region)]
        public ListResultDto<RegionList> GetRegion (RegionListInput input)
        {
            var region = _regionRepository.GetAll();
            var query = region
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.RegionCode.Contains(input.Filter) ||
                        u.RegionName.Contains(input.Filter)).ToList();
            return new ListResultDto<RegionList>(query.MapTo<List<RegionList>>());
        }

        public async Task<GetRegion> GetRegionForEdit(EntityDto input)
        {
            var output = new GetRegion
            {
            };

            var persion = _regionRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            output.Regions = persion.MapTo<RegionList>();
            return output;
        }
        public async Task CreateOrUpdateRegion(RegionInput input)
        {
            if (input.Id != 0)
            {
                await UpdateRegion(input);
            }
            else
            {
                await CreateRegion(input);
            }
        }
       // [AbpAuthorize(AppPermissions.Pages_Tenant_Geography_Region_Create)]
        public async Task CreateRegion(RegionInput input)
        {
            var region = input.MapTo<Regions>();
            var query = _regionRepository.GetAll().Where(a => a.RegionCode == input.RegionCode || a.RegionName == input.RegionName).FirstOrDefault();
            if(query ==null)
            {
                await _regionRepository.InsertAsync(region);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Region Name '" + input.RegionName + "' or Region Code '" + input.RegionCode + "'...");
            }

            
        }
       // [AbpAuthorize(AppPermissions.Pages_Tenant_Geography_Region_Edit)]
        public async Task UpdateRegion(RegionInput input)
        {
            var region = await _regionRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, region);
            var query = _regionRepository.GetAll().Where(a => (a.RegionCode == input.RegionCode || a.RegionName == input.RegionName) && a.Id !=input.Id).FirstOrDefault();
            if (query == null)
            {
                await _regionRepository.UpdateAsync(region);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Region Name '" + input.RegionName + "' or Region Code '" + input.RegionCode + "'...");
            }
        }
        public ListResultDto<RegionCityList> GetRegionCity(EntityDto input)
        {
            var cities = _regionCityRepository
                 .GetAll().Where(p => p.RegionId == input.Id).ToList();
                 //.OrderBy(p => p.Citys.CityName)
                 //.ToList();

            var regioncitylist = cities.MapTo<List<RegionCityList>>();
            foreach (var one in regioncitylist)
            {
                var coun = (_cityRepository.GetAll().Where(p => p.Id == one.CityId)).FirstOrDefault();
                if (coun != null)
                {
                    one.CityName = coun.CityName;
                }
            }
            regioncitylist = regioncitylist.OrderBy(p => p.CityName).ToList();
            return new ListResultDto<RegionCityList>(regioncitylist.MapTo<List<RegionCityList>>());

        }
        public  async Task AddRegionCity(RegionCityInput input)
        {
            var reci = input.MapTo<RegionCity>();
            await _regionCityRepository.InsertAsync(reci);
        }

        public async Task GetDeleteRegionCity(EntityDto input)
        {
            await _regionCityRepository.DeleteAsync(input.Id);
        }
        public async Task GetDeleteRegion(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 5);
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
                    throw new UserFriendlyException("Ooops!", "Region cannot be deleted '");
                }
                else
                {
                    await _regionRepository.DeleteAsync(input.Id);
                }
            }

        }

        public async Task<FileDto> GetRegionToExcel()

        {
            var region = _regionRepository.GetAll();
            var regionListDtos = region.MapTo<List<RegionList>>();
            return _regionListExcelExporter.ExportToFile(regionListDtos);
        }
    }
}
