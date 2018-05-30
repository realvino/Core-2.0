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
using tibs.stem.Dto;
using tibs.stem.Emaildomains;
using tibs.stem.Emaildomainss.Dto;
using tibs.stem.Emaildomainss.Exporting;

using Abp.Linq.Extensions;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace tibs.stem.Emaildomainss
{
     
    public class EmaildomainAppService : stemAppServiceBase, IEmaildomainAppService
    {
        private readonly IRepository<Emaildomain> _emaildomainRepository;
        private readonly IEmaildomainListExcelExporter _emaildomainListExcelExporter;
        public EmaildomainAppService(IRepository<Emaildomain> emaildomainRepository, IEmaildomainListExcelExporter emaildomainListExcelExporter)
        {
            _emaildomainRepository = emaildomainRepository;
            _emaildomainListExcelExporter = emaildomainListExcelExporter;
        }

        public ListResultDto<EmaildomainList> GetEmaildomain(GetEmaildomainListInput input)
        {
            var query = _emaildomainRepository.GetAll()
                .WhereIf(
                !input.Filter.IsNullOrEmpty(),
                   p => p.EmaildomainName.Contains(input.Filter)
                );
            var Emaildomain = (from a in query
                               select new EmaildomainList
                               {
                                   Id = a.Id,
                                   EmaildomainName = a.EmaildomainName
                               });
            return new ListResultDto<EmaildomainList>(Emaildomain.MapTo<List<EmaildomainList>>());
        }

        public async Task<GetEmaildomain> GetEmaildomainForEdit(NullableIdDto input)
        {
            var output = new GetEmaildomain { };
            var query = _emaildomainRepository
               .GetAll().Where(p => p.Id == input.Id);


            if (query.Count() > 0)
            {
                var Emaildomain = (from a in query
                              select new EmaildomainList
                              {
                                  Id = a.Id,
                                  EmaildomainName = a.EmaildomainName
                              }).FirstOrDefault();

                output = new GetEmaildomain
                {
                    emaildomainList = Emaildomain
                };
            }
            return output;
        }

        public async Task CreateOrUpdateEmaildomain(CreateEmaildomainInput input)
        {
            if (input.Id != 0)
            {
                await UpdateEmaildomainAsync(input);
            }
            else
            {
                await CreateEmaildomainAsync(input);
            }
        }

        public virtual async Task CreateEmaildomainAsync(CreateEmaildomainInput input)
        {
            var emaildomain = input.MapTo<Emaildomain>();
            var val = _emaildomainRepository
              .GetAll().Where(p => p.EmaildomainName == input.EmaildomainName ).FirstOrDefault();

            if (val == null)
            {
                await _emaildomainRepository.InsertAsync(emaildomain);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Emaildomain Name '" + input.EmaildomainName  + "'...");
            }
        }

        public virtual async Task UpdateEmaildomainAsync(CreateEmaildomainInput input)
        {
            var emaildomain = await _emaildomainRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, emaildomain);

            var val = _emaildomainRepository
             .GetAll().Where(p => (p.EmaildomainName == input.EmaildomainName ) && p.Id != input.Id).FirstOrDefault();
            if (val == null)
            {
                await _emaildomainRepository.UpdateAsync(emaildomain);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Emaildomain Name '" + input.EmaildomainName + "'...");
            }
        }

        public async Task GetDeleteEmaildomain(EntityDto input)
        {
            await _emaildomainRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetEmaildomainToExcel()
        {

            var emaildomain = _emaildomainRepository.GetAll();

            var emaildomainlist = (from a in emaildomain
                              select new EmaildomainList
                              {
                                  Id = a.Id,
                                  EmaildomainName = a.EmaildomainName
                              });
            var order = await emaildomainlist.OrderBy("EmaildomainName").ToListAsync();

            var Emaildomainlistoutput = order.MapTo<List<EmaildomainList>>();

            return _emaildomainListExcelExporter.ExportToFile(Emaildomainlistoutput);
        }
    }
}
