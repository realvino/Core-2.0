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
using System.Threading.Tasks;
using tibs.stem;
using System.Linq.Dynamic.Core;
using tibs.stem.Dto;
using tibs.stem.ColorCodes;
using tibs.stem.ColorCodess.Dto;
using System.Linq;

namespace tibs.stem.ColorCodess
{
    public class ColorCodeAppService : stemAppServiceBase, IColorCodeAppService
    {
        private readonly IRepository<ColorCode> _colorCodeRepository;
        public ColorCodeAppService(IRepository<ColorCode> colorCodeRepository)
        {
            _colorCodeRepository = colorCodeRepository;

        }
        public ListResultDto<ColorCodeList> GetColorcode(GetColorCodeListInput input)
        {
            var act = _colorCodeRepository
                .GetAll();
            var query = act
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Component.Contains(input.Filter) ||
                         u.Code.Contains(input.Filter) ||
                          u.Color.Contains(input.Filter) ||
                        u.Id.ToString().Contains(input.Filter))
                .ToList();

            return new ListResultDto<ColorCodeList>(query.MapTo<List<ColorCodeList>>());
        }

        public async Task<GetColorCode> GetColorCodeForEdit(NullableIdDto input)
        {
            var output = new GetColorCode
            {
            };

            var color = _colorCodeRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            output.Colors = color.MapTo<ColorCodeList>();
            return output;
        }

        public async Task CreateOrUpdateColorcode(CreateColorCodeInput input)
        {
            if (input.Id != 0)
            {
                await UpdateColorcode(input);
            }
            else
            {
                await CreateColorcode(input);
            }
        }

        public async Task CreateColorcode(CreateColorCodeInput input)
        {
            var color = input.MapTo<ColorCode>();
            var val = _colorCodeRepository
             .GetAll().Where(p => p.Component == input.Component || p.Code == input.Code|| p.Color == input.Color).FirstOrDefault();

            if (val == null)
            {
                await _colorCodeRepository.InsertAsync(color);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Component '" + input.Component + "' or Code '" + input.Code+ "' or Color '"+input.Color + "'...");
            }
        }

        public async Task UpdateColorcode(CreateColorCodeInput input)
        {
            var color = await _colorCodeRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, color);

            var val = _colorCodeRepository
              .GetAll().Where(p => (p.Component == input.Component || p.Code == input.Code || p.Color == input.Color) && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _colorCodeRepository.UpdateAsync(color);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Component '" + input.Component + "' or Code '" + input.Code+ "' or Color '" + input.Color + "'...");
            }

        }

        public async Task DeleteColorcode(EntityDto input)
        {
            await _colorCodeRepository.DeleteAsync(input.Id);
        }

    }
}
