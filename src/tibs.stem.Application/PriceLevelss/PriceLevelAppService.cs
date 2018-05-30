using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.PriceLevels;
using tibs.stem.PriceLevelss.Dto;

namespace tibs.stem.PriceLevelss
{
    public class PriceLevelAppService : stemAppServiceBase, IPriceLevelAppService
    {

        private readonly IRepository<PriceLevel> _priceLevelRepository;
        public PriceLevelAppService(IRepository<PriceLevel> milestoneRepository)
        {
            _priceLevelRepository = milestoneRepository;
        }

        public ListResultDto<PriceLevelListDto> GetPriceLevel(GetPriceLevelInput input)
        {
            var priceLevel = _priceLevelRepository.GetAll()
                .WhereIf(
                    !input.Filter.IsNullOrEmpty(),
                    p => p.Id.ToString().Contains(input.Filter) ||
                        p.PriceLevelCode.Contains(input.Filter) ||
                         p.PriceLevelName.Contains(input.Filter)
                )
                .OrderBy(p => p.PriceLevelName)
                .ThenBy(p => p.PriceLevelCode)
                .ToList();

            return new ListResultDto<PriceLevelListDto>(priceLevel.MapTo<List<PriceLevelListDto>>());
        }

        public async Task<GetPriceLevel> GetPriceLevelForEdit(NullableIdDto input)
        {
            var output = new GetPriceLevel { };
            var query = _priceLevelRepository.GetAll().Where(p => p.Id == input.Id);
            if (query.Count() > 0)
            {
                var priceLevel = (from a in query
                                  select new PriceLevelListDto
                                  {
                                      Id = a.Id,
                                      PriceLevelCode = a.PriceLevelCode,
                                      PriceLevelName = a.PriceLevelName,
                                      DiscountAllowed = a.DiscountAllowed
                                  }).FirstOrDefault();

                output = new GetPriceLevel
                {
                    PriceLevels = priceLevel
                };
            }

            return output;
        }


        public async Task CreateOrUpdatePriceLevel(CreatePriceLevelInput input)
        {
            if (input.Id != 0)
            {
                await UpdatePriceLevelAsync(input);
            }
            else
            {
                await CreatePriceLevelAsync(input);
            }
        }

        public virtual async Task CreatePriceLevelAsync(CreatePriceLevelInput input)
        {
            var priceLevel = input.MapTo<PriceLevel>();
            var val = _priceLevelRepository
              .GetAll().Where(p => p.PriceLevelCode == input.PriceLevelCode || p.PriceLevelName == input.PriceLevelName).FirstOrDefault();

            if (val == null)
            {
                await _priceLevelRepository.InsertAsync(priceLevel);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in PriceLevel Code '" + input.PriceLevelCode + "' orPriceLevel Name '" + input.PriceLevelName + "'...");
            }
        }

        public virtual async Task UpdatePriceLevelAsync(CreatePriceLevelInput input)
        {
            var priceLevel = await _priceLevelRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, priceLevel);

            var val = _priceLevelRepository
             .GetAll().Where(p => (p.PriceLevelCode == input.PriceLevelCode || p.PriceLevelName == input.PriceLevelName) && p.Id != input.Id).FirstOrDefault();
            if (val == null)
            {
                await _priceLevelRepository.UpdateAsync(priceLevel);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in PriceLevel Code '" + input.PriceLevelCode + "' orPriceLevel Name '" + input.PriceLevelName + "'...");
            }
        }

        public async Task GetDeletePriceLevel(EntityDto input)
        {
            await _priceLevelRepository.DeleteAsync(input.Id);

        }

    }
}
