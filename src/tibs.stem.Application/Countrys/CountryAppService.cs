using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem;
using tibs.stem.Authorization;
using tibs.stem.Countrys;
using tibs.stem.Countrys.Dto;
using tibs.stem.Countrys.Exporting;
using tibs.stem.Dto;
using tibs.stem.IO;
using tibs.stem.Storage;
using tibs.stem.Tenants.Dashboard;

namespace tibs.stem.Countrys
{
    public class CountryAppService : stemAppServiceBase, ICountryAppService
    {
        private readonly IRepository<Country> _countryRepository;
        private readonly ICountryListExcelExporter _countryListExcelExporter;
        private readonly IAppFolders _appFolders;
        private readonly IBinaryObjectManager _binaryObjectManager;

        public CountryAppService(IRepository<Country> countryRepository, ICountryListExcelExporter countryListExcelExporter, IAppFolders appFolders, IBinaryObjectManager binaryObjectManager)
        {
            _countryRepository = countryRepository;
            _countryListExcelExporter = countryListExcelExporter;
            _appFolders = appFolders;
            _binaryObjectManager = binaryObjectManager;
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_Geography_Country)]
        public ListResultDto<CountryListDto> GetCountry(CountryInput input)
        {
            var country = _countryRepository
                .GetAll();
            var query = country
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.CountryCode.Contains(input.Filter) ||
                        u.CountryName.Contains(input.Filter) ||
                        u.ISDCode.Contains(input.Filter) )
                .ToList();

            return new ListResultDto<CountryListDto>(query.MapTo<List<CountryListDto>>());
         }

        public async Task<GetCountry> GetCountryForEdit(EntityDto input)
        {

            var datas = UserManager.Users.ToList();

            foreach (var data in datas)
            {
                if (data.ProfilePictureId != null)
                {
                    try
                    {
                        var files = await _binaryObjectManager.GetOrNullAsync((Guid)data.ProfilePictureId);
                        byte[] bytes = new byte[0];
                        bytes = files.Bytes;
                        var tempFileName = "userProfileImage_" + data.Id + ".jpg";

                        AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.ProfilePath, tempFileName);

                        var tempFilePath = Path.Combine(_appFolders.ProfilePath, tempFileName);

                        System.IO.File.WriteAllBytes(tempFilePath, bytes);
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
            var output = new GetCountry
            {
            };

            var persion = _countryRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            output.Countrys = persion.MapTo<CountryListDto>();
            return output;
        }


        public async Task CreateOrUpdateCountry(CountryInputDto input)
        {
            if (input.Id != 0)
            {
                await UpdateCountry(input);
            }
            else
            {
                await CreateCountry(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Geography_Country_Create)]
        public async Task CreateCountry(CountryInputDto input)
        {
            var country = input.MapTo<Country>();
            var val = _countryRepository
             .GetAll().Where(p => p.CountryCode == input.CountryCode || p.CountryName == input.CountryName).FirstOrDefault();

            if (val == null)
            {
                await _countryRepository.InsertAsync(country);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in country Name '" + input.CountryName + "' or country Code '" + input.CountryCode + "'...");
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Geography_Country_Edit)]
        public async Task UpdateCountry(CountryInputDto input)
        {
            var country = await _countryRepository.GetAsync(input.Id);         
            country.LastModificationTime = DateTime.Now;
            ObjectMapper.Map(input, country);

            var val = _countryRepository
              .GetAll().Where(p => (p.CountryCode == input.CountryCode || p.CountryName == input.CountryName) && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _countryRepository.UpdateAsync(country);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in country Name '" + input.CountryName + "' or country Code '" + input.CountryCode + "'...");
            }

        }

       // [AbpAuthorize(AppPermissions.Pages_Tenant_Geography_Country_Delete)]
        public bool GetMappedCountry(EntityDto input) 
        {
            bool ok = false;
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 1);
                sqlComm.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {

                    da.Fill(ds);
                }

            }

            if(input.Id > 0)
            {
                var results = ds.Rows.Cast<DataRow>().Where(myRow => (int)myRow["Id"] == input.Id);
                if (results.Count() > 0)
                {
                    ok = true;
                }
            }
            return ok;
        }

        public async Task GetDeleteCountry(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 1);
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
                    throw new UserFriendlyException("Ooops!", "Country cannot be deleted '");
                }
                else
                {
                    await _countryRepository.DeleteAsync(input.Id);
                }
            }

        }



        public async Task<FileDto> GetCountryToExcel()

        {
            var country = _countryRepository.GetAll();
            var countryListDtos = country.MapTo<List<CountryListDto>>();

            return _countryListExcelExporter.ExportToFile(countryListDtos);
        }

    }
    public class FindDelete
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class Output
    {
        public bool ok { get; set; }
    }
}