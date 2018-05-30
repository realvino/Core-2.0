using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Dimensions.Dto;

namespace tibs.stem.Dimensions
{
    public class DimensionAppService : stemAppServiceBase, IDimensionAppService
    {
        private readonly IRepository<Dimension> _DimensionRepository;
        public DimensionAppService(IRepository<Dimension> DimensionRepository)
        {
            _DimensionRepository = DimensionRepository;
           
        }

        public ListResultDto<DimensionListDto> GetDimension(GetDimensionInput input)
        {
            var dimension = _DimensionRepository
                .GetAll();
            var query = dimension
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.DimensionCode.Contains(input.Filter) ||
                        u.DimensionName.Contains(input.Filter) )
                .ToList();

            return new ListResultDto<DimensionListDto>(query.MapTo<List<DimensionListDto>>());
        }

        public async Task<GetDimension> GetDimensionForEdit(EntityDto input)
        {
            var output = new GetDimension
            {
            };

            var dimension = _DimensionRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            output.Dimension = dimension.MapTo<DimensionListDto>();
            return output;
        }


        public async Task CreateOrUpdateDimension(DimensionInputDto input)
        {
            if (input.Id != 0)
            {
                await UpdateDimension(input);
            }
            else
            {
                await CreateDimension(input);
            }
        }

        public async Task CreateDimension(DimensionInputDto input)
        {
            var dimension = input.MapTo<Dimension>();
            var val = _DimensionRepository
             .GetAll().Where(p => p.DimensionCode == input.DimensionCode || p.DimensionName == input.DimensionName).FirstOrDefault();

            if (val == null)
            {
                await _DimensionRepository.InsertAsync(dimension);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Dimension Name '" + input.DimensionName + "' or Dimension Code '" + input.DimensionCode + "'...");
            }
        }

        
        public async Task UpdateDimension(DimensionInputDto input)
        {
            var dimension = await _DimensionRepository.GetAsync(input.Id);
            dimension.LastModificationTime = DateTime.Now;
            ObjectMapper.Map(input, dimension);

            var val = _DimensionRepository
              .GetAll().Where(p => (p.DimensionCode == input.DimensionCode || p.DimensionName == input.DimensionName) && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _DimensionRepository.UpdateAsync(dimension);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Dimension Name '" + input.DimensionName + "' or Dimension Code '" + input.DimensionCode + "'...");
            }

        }

                
        public async Task DeleteDimension(EntityDto input)
        {
            await _DimensionRepository.DeleteAsync(input.Id);
        }
   

}
}
