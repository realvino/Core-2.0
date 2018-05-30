using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using Abp.UI;
using tibs.stem.Citys;
using tibs.stem.Locations.Dto;
using tibs.stem.Dto;
using tibs.stem.Locations.Exporting;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace tibs.stem.Locations
{
    public class LocationAppService : stemAppServiceBase, ILocationAppService
    {
        private readonly IRepository<Location> _locationRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly ILocationListExcelExporter _locationListExcelExporter;

        public LocationAppService(IRepository<Location> locationRepository, IRepository<City> cityRepository, ILocationListExcelExporter locationListExcelExporter)
        {
            _locationRepository = locationRepository;
            _cityRepository = cityRepository;
            _locationListExcelExporter = locationListExcelExporter;

        }
       
       
        public async Task<PagedResultDto<LocationListDto>> GetLocation(GetLocationInput input)
        {

            var query = _locationRepository.GetAll()
                .WhereIf( !input.Filter.IsNullOrEmpty(),
                          p => p.LocationCode.Contains(input.Filter) ||
                               p.LocationName.Contains(input.Filter));

            var location = (from a in query select new LocationListDto { Id = a.Id, LocationName = a.LocationName, LocationCode = a.LocationCode, CityId = a.citys.Id,CityName = a.citys.CityName });

            var locationCount = await location.CountAsync();
            var locationlist = await location
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            var output = locationlist.MapTo<List<LocationListDto>>();
            return new PagedResultDto<LocationListDto>(locationCount, output);  
        }

        public async Task<GetLocation> GetlocationForEdit(NullableIdDto<long> input)
        {
            var output = new GetLocation();
            var query = _locationRepository
               .GetAll().Where(p => p.Id == input.Id);

            if (query.Count() >0 )
            {
                var location = (from a in query select new LocationListDto { Id = a.Id, LocationName = a.LocationName, LocationCode = a.LocationCode, CityId = a.citys.Id, CityName = a.citys.CityName }).FirstOrDefault();
                output = new GetLocation
                {
                    Locations = location
                };
            }  

            return output;

        }

        public async Task CreateOrUpdateLocation(LocationInputDto input)
        {
            if (input.Id != 0)
            {
                await UpdateLocationAsync(input);
            }
            else
            {
                await CreateLocationAsync(input);
            }
        }

        public virtual async Task CreateLocationAsync(LocationInputDto input)
        {
            var location = input.MapTo<Location>();
            var val = _locationRepository
              .GetAll().Where(p => p.LocationCode == input.LocationCode || p.LocationName == input.LocationName).FirstOrDefault();

            if (val == null)
            {
                await _locationRepository.InsertAsync(location);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in location Name '" + input.LocationName + "' or location Code '" + input.LocationCode + "'...");
            }
        }

        public virtual async Task UpdateLocationAsync(LocationInputDto input)
        {
            var location = await _locationRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, location);

            var val = _locationRepository
              .GetAll().Where(p => (p.LocationCode == input.LocationCode || p.LocationName == input.LocationName) && p.Id != input.Id).FirstOrDefault();
            if (val == null)
            {
                await _locationRepository.UpdateAsync(location);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in location Name '" + input.LocationName + "' or location Code '" + input.LocationCode + "'...");
            }
        }
        public async Task GetDeleteLocation(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 3);
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
                    throw new UserFriendlyException("Ooops!", "Location cannot be deleted '");
                }
                else
                {
                    await _locationRepository.DeleteAsync(input.Id);
                }
            }
        }
        public async Task<FileDto> GetLocationToExcel()
        {
            var query = _locationRepository.GetAll();
            var select = (from a in query select new LocationListDto { Id = a.Id, LocationName = a.LocationName, LocationCode = a.LocationCode, CityName = a.citys.CityName });
            var order = await select.OrderBy("LocationName").ToListAsync();
            var list = order.MapTo<List<LocationListDto>>();
            return _locationListExcelExporter.ExportToFile(list);
        }
      
    }
}
