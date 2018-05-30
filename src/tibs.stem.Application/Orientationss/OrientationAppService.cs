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
using tibs.stem.Orientations.Dto;

namespace tibs.stem.Orientations
{
    public class OrientationAppService : stemAppServiceBase, IOrientationAppService
    {
        private readonly IRepository<Orientation> _OrientationRepository;
        public OrientationAppService(IRepository<Orientation> OrientationRepository)
        {
            _OrientationRepository = OrientationRepository;

        }

        public ListResultDto<OrientationListDto> GetOrientation(GetOrientationInput input)
        {
            var orientation = _OrientationRepository
                .GetAll();
            var query = orientation
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.OrientationCode.Contains(input.Filter) ||
                        u.OrientationName.Contains(input.Filter))
                .ToList();

            return new ListResultDto<OrientationListDto>(query.MapTo<List<OrientationListDto>>());
        }

        public async Task<GetOrientation> GetOrientationForEdit(EntityDto input)
        {
            var output = new GetOrientation
            {
            };

            var orientation = _OrientationRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            output.Orientation = orientation.MapTo<OrientationListDto>();
            return output;
        }


        public async Task CreateOrUpdateOrientation(OrientationInputDto input)
        {
            if (input.Id != 0)
            {
                await UpdateOrientation(input);
            }
            else
            {
                await CreateOrientation(input);
            }
        }

        public async Task CreateOrientation(OrientationInputDto input)
        {
            var orientation = input.MapTo<Orientation>();
            var val = _OrientationRepository
             .GetAll().Where(p => p.OrientationCode == input.OrientationCode || p.OrientationName == input.OrientationName).FirstOrDefault();

            if (val == null)
            {
                await _OrientationRepository.InsertAsync(orientation);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Orientation Name '" + input.OrientationName + "' or Orientation Code '" + input.OrientationCode + "'...");
            }
        }


        public async Task UpdateOrientation(OrientationInputDto input)
        {
            var orientation = await _OrientationRepository.GetAsync(input.Id);
            orientation.LastModificationTime = DateTime.Now;
            ObjectMapper.Map(input, orientation);

            var val = _OrientationRepository
              .GetAll().Where(p => (p.OrientationCode == input.OrientationCode || p.OrientationName == input.OrientationName) && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _OrientationRepository.UpdateAsync(orientation);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Orientation Name '" + input.OrientationName + "' or Orientation Code '" + input.OrientationCode + "'...");
            }

        }


        public async Task DeleteOrientation(EntityDto input)
        {
            await _OrientationRepository.DeleteAsync(input.Id);
        }


    }
    
}
