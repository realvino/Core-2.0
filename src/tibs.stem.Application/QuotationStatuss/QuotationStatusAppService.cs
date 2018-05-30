using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.QuotationStatuss;
using tibs.stem.QuotationStatuss.Dto;

namespace tibs.stem.QuotationStatuss
{
    public class QuotationStatusAppService : stemAppServiceBase, IQuotationStatusAppService
    {
        private readonly IRepository<QuotationStatus> _quotationStatusRepository;

        public QuotationStatusAppService(IRepository<QuotationStatus> quotationStatusRepository)
        {
            _quotationStatusRepository = quotationStatusRepository;
        }

        public ListResultDto<QuotationStatusList> GetQuotationStatus(GetQuotationStatusInput input)
        {

            var query = _quotationStatusRepository.GetAll()
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Code.Contains(input.Filter) ||
                        u.Name.Contains(input.Filter) ||
                        u.Id.ToString().Contains(input.Filter))
                .ToList();

            return new ListResultDto<QuotationStatusList>(query.MapTo<List<QuotationStatusList>>());
        }

        public async Task<GetQuotationStatus> GetQuotationStatusForEdit(NullableIdDto input)
        {
            var output = new GetQuotationStatus
            {
            };

            var status = _quotationStatusRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();
            output.status = status.MapTo<QuotationStatusInput>();

            return output;

        }

        public async Task CreateOrUpdateQuotationStatus(QuotationStatusInput input)
        {
            if (input.Id != 0)
            {
                await UpdateQuotationStatus(input);
            }
            else
            {
                await CreateQuotationStatus(input);
            }
        }
        public async Task CreateQuotationStatus(QuotationStatusInput input)
        {
            var status = input.MapTo<QuotationStatus>();
            var val = _quotationStatusRepository
             .GetAll().Where(p => p.Code == input.Code || p.Name == input.Name).FirstOrDefault();

            if (val == null)
            {
                await _quotationStatusRepository.InsertAsync(status);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Code '" + input.Code + "' orName '" + input.Name + "'...");
            }
        }

        public async Task UpdateQuotationStatus(QuotationStatusInput input)
        {
            var status = input.MapTo<QuotationStatus>();

            var val = _quotationStatusRepository
            .GetAll().Where(p => (p.Code == input.Code || p.Name == input.Name) && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _quotationStatusRepository.UpdateAsync(status);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Code '" + input.Code + "' or Name  '" + input.Name + "'...");
            }

        }
        public async Task DeleteQuotationStatus(EntityDto input)
        {
            await _quotationStatusRepository.DeleteAsync(input.Id);
        }

    }
}
