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
using tibs.stem.Authorization.Users.Dto;
using tibs.stem.UserDesignations;
using tibs.stem.UserDesignationss.Dto;

namespace tibs.stem.UserDesignationss
{
    public class UserDesignationAppService : stemAppServiceBase, IUserDesignationAppService
    {
        private readonly IRepository<UserDesignation> _userDesignationRepository;

        public UserDesignationAppService(IRepository<UserDesignation> userDesignationRepository)
        {
            _userDesignationRepository = userDesignationRepository;
        }

        public ListResultDto<UserDesignationListDto> GetUserDesignation(GetUserDesignationInput input)
        {

            var query = _userDesignationRepository.GetAll()
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Name.Contains(input.Filter) ||
                        u.Id.ToString().Contains(input.Filter))
                .ToList();

            return new ListResultDto<UserDesignationListDto>(query.MapTo<List<UserDesignationListDto>>());
        }
        public async Task<GetUserDesignation> GetUserDesignationForEdit(NullableIdDto input)
        {
            var output = new GetUserDesignation
            {
            };

            var user = _userDesignationRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            output.users = user.MapTo<UserDesignationListDto>();

            return output;

        }
        public async Task CreateOrUpdateUserDesignation(UserDesignationInputDto input)
        {
            if (input.Id != 0)
            {
                await UpdateUserDesignation(input);
            }
            else
            {
                await CreateUserDesignation(input);
            }
        }
        public async Task CreateUserDesignation(UserDesignationInputDto input)
        {
            var user = input.MapTo<UserDesignation>();
            var val = _userDesignationRepository
             .GetAll().Where(p => p.Name == input.Name).FirstOrDefault();

            if (val == null)
            {
                await _userDesignationRepository.InsertAsync(user);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Name '" + input.Name + "' ...");
            }
        }

        public async Task UpdateUserDesignation(UserDesignationInputDto input)
        {
            var user = input.MapTo<UserDesignation>();

            var val = _userDesignationRepository
            .GetAll().Where(p => p.Name == input.Name && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _userDesignationRepository.UpdateAsync(user);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Name '" + input.Name + "' ...");
            }

        }
        public async Task DeleteUserDesignation(EntityDto input)
        {
            await _userDesignationRepository.DeleteAsync(input.Id);
        }
    }
}
