using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem;
using tibs.stem.LineTypes.Dto;

namespace tibs.stem.LineTypes
{
    public class LineTypeAppService : stemAppServiceBase, ILineTypeAppService
    {
        private readonly IRepository<LineType> _lineTypeRepository;

        public LineTypeAppService(IRepository<LineType> lineTypeRepository)
        {
            _lineTypeRepository = lineTypeRepository;
        }

        public ListResultDto<LineTypeListDto> GetLineTypes(GetLineTypeInput input)
        {
            var linetype = _lineTypeRepository
                .GetAll();
            var query = linetype
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.LineTypeCode.Contains(input.Filter) ||
                        u.LineTypeName.Contains(input.Filter)).ToList();

            return new ListResultDto<LineTypeListDto>(query.MapTo<List<LineTypeListDto>>());

        }
        public async Task<GetLineType> GetLineTypeForEdit(EntityDto input)
        {
            var output = new GetLineType
            {
            };

            var lines = _lineTypeRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            output.lineTypes = lines.MapTo<LineTypeListDto>();
            return output;
        }


        public async Task CreateOrUpdateLineType(LineTypeInputDto input)
        {
            if (input.Id != 0)
            {
                await UpdateLineType(input);
            }
            else
            {
                await CreateLineType(input);
            }
        }


        public async Task CreateLineType(LineTypeInputDto input)
        {
            var line = input.MapTo<LineType>();
            var val = _lineTypeRepository
             .GetAll().Where(p => p.LineTypeCode == input.LineTypeCode || p.LineTypeName == input.LineTypeName).FirstOrDefault();

            if (val == null)
            {
                await _lineTypeRepository.InsertAsync(line);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Line Name '" + input.LineTypeName + "' or Line Code '" + input.LineTypeCode + "'...");
            }
        }

        public async Task UpdateLineType(LineTypeInputDto input)
        {
            var line = await _lineTypeRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, line);
            var val = _lineTypeRepository
              .GetAll().Where(p => (p.LineTypeCode == input.LineTypeCode || p.LineTypeName == input.LineTypeName) && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _lineTypeRepository.UpdateAsync(line);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Line Name '" + input.LineTypeName + "' or Line Code '" + input.LineTypeCode + "'...");
            }

        }
        public async Task DeleteLineType(EntityDto input)
        {         
            await _lineTypeRepository.DeleteAsync(input.Id);
        }

    }
}
