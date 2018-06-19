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
using System.Data.SqlClient;
using tibs.stem.Countrys;
using tibs.stem.Citys.Dto;
using tibs.stem.Tenants.Dashboard;
using tibs.stem.Cityss.Exporting;
using tibs.stem.Dto;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace tibs.stem.Citys
{
    public class CityAppService : stemAppServiceBase, ICityAppService
    {
        private readonly IRepository<City> _cityRepository;

        private readonly IRepository<Country> _countryRepository;

        private readonly ICityListExcelExporter _cityListExcelExporter;
        public CityAppService(IRepository<City> cityRepository, IRepository<Country> countryRepository, ICityListExcelExporter cityListExcelExporter)
        {
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
            _cityListExcelExporter = cityListExcelExporter;
        }


        public async Task<PagedResultDto<CityList>> GetCity(GetCItyListInput input)
        {
            var query = _cityRepository.GetAll()
                .WhereIf(
                !input.Filter.IsNullOrEmpty(),
                p => p.CityCode.Contains(input.Filter) ||
                    p.CityName.Contains(input.Filter)
                );
            var city = (from a in query select new CityList { Id=a.Id, CityCode = a.CityCode, CityName = a.CityName, CountryName = a.Country.CountryName,CountryId = a.CountryId});
            var cityCount = await city.CountAsync();
            var citylist = await city
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var citylistoutput = citylist.MapTo<List<CityList>>();
            return new PagedResultDto<CityList>(
                cityCount,citylistoutput);
        }

        public async Task<GetCity> GetCityForEdit(NullableIdDto input)
        {
            var output = new GetCity();
            var query = _cityRepository
               .GetAll().Where(p => p.Id == input.Id);

            if (query.Count() > 0)
            {
                var city = (from a in query select new CityList { Id = a.Id, CityCode = a.CityCode, CountryId = a.Country.Id,CountryName = a.Country.CountryName,CityName = a.CityName }).FirstOrDefault();
                output = new GetCity
                {
                    MyCity = city
                };
            }
            return output;
        }


        public async Task CreateOrUpdateCity(CreateCityInput input)
        {
            if (input.Id != 0)
            {
                await UpdateCityAsync(input);
            }
            else
            {
                await CreateCityAsync(input);
            }
        }

        public virtual async Task CreateCityAsync(CreateCityInput input)
        {
            var City = input.MapTo<City>();
            var val = _cityRepository
              .GetAll().Where(p => p.CityName == input.CityName || p.CityCode == input.CityCode).FirstOrDefault();

            if (val == null)
            {
                await _cityRepository.InsertAsync(City);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in City Name '" + input.CityName + "' or City Code '" + input.CityCode + "'...");
            }
        }

        public virtual async Task UpdateCityAsync(CreateCityInput input)
        {
            var city = await _cityRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, city);

            var val = _cityRepository
             .GetAll().Where(p => (p.CityName == input.CityName || p.CityCode == input.CityCode) && p.Id != input.Id).FirstOrDefault();
            if (val == null)
            {
                await _cityRepository.UpdateAsync(city);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in City Name '" + input.CityName + "' or City Code '" + input.CityCode + "'...");
            }
        }
      
        public async Task<FileDto> GetCityToExcel()
        {

            var city = _cityRepository.GetAll();

            var cityListDtos = city.MapTo<List<CityList>>();

            await FillCountryNames(cityListDtos);

            return _cityListExcelExporter.ExportToFile(cityListDtos);
        }

        private async Task FillCountryNames(List<CityList> cityListDtos)
        {

            var distinctcountryIds = (
                from cityListDto in cityListDtos
                select cityListDto.CountryId
                ).Distinct();

            var countryNames = new Dictionary<int, string>();
            foreach (var countryId in distinctcountryIds)
            {
                var coun = (_countryRepository.GetAll().Where(p => p.Id == countryId)).FirstOrDefault();
                countryNames[countryId] = coun.CountryName.ToString();
            }

            foreach (var countryListDto in cityListDtos)
            {
                countryListDto.CountryName = countryNames[countryListDto.CountryId];
            }
        }

        public bool GetMappedCity(EntityDto input)
        {
            bool ok = false;
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 2);
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
                    ok = true;
                }
            }
            return ok;
        }

        public async Task GetDeleteCity(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 2);
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
                    throw new UserFriendlyException("Ooops!", "City cannot be deleted '");
                }
                else
                {
                    await _cityRepository.DeleteAsync(input.Id);
                }
            }

        }
        public object Convert(object value)
        {
            if (!(value is Color))
                return value;
            Color color = (Color)value;
            double Y = 0.2126 * color.R + 0.7152 * color.G + 0.0722 * color.B;
            return Y > 0.4 ? Brushes.Black : Brushes.White;
        }

        public void Convertexcel()
        {
            string pathSource = @"D:\Excel\Quote.xlsx";

            IWorkbook templateWorkbook;
            using (FileStream fs = new FileStream(pathSource, FileMode.Open, FileAccess.Read))
            {
                templateWorkbook = new XSSFWorkbook(fs);
            }

            string sheetName = "Quote";
            ISheet sheet = templateWorkbook.GetSheet(sheetName) ?? templateWorkbook.CreateSheet(sheetName);
            IRow dataRow = sheet.GetRow(18) ?? sheet.CreateRow(18);
            ICell cell2 = dataRow.GetCell(2) ?? dataRow.CreateCell(2);
            ICell cell3 = dataRow.GetCell(3) ?? dataRow.CreateCell(3);
            ICell cell4 = dataRow.GetCell(4) ?? dataRow.CreateCell(4);
            ICell cell5 = dataRow.GetCell(5) ?? dataRow.CreateCell(5);
            ICell cell6 = dataRow.GetCell(6) ?? dataRow.CreateCell(6);
            ICell cell7 = dataRow.GetCell(7) ?? dataRow.CreateCell(7);
            ICell cell8 = dataRow.GetCell(8) ?? dataRow.CreateCell(8);

            cell2.SetCellValue("foo");
            cell3.SetCellValue("foo");
            cell4.SetCellValue("foo");
            cell5.SetCellValue("foo");
            cell6.SetCellValue("foo");
            cell7.SetCellValue("foo");
            cell8.SetCellValue("foo");



            using (FileStream fs = new FileStream(pathSource, FileMode.Create, FileAccess.Write))
            {
                templateWorkbook.Write(fs);
            }
        }

    }
}
