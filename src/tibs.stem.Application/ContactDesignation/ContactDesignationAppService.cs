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
using tibs.stem.ContactDesignation.Dto;
using tibs.stem.ContactDesignation.Exporting;
using tibs.stem.Designations;
using tibs.stem.Dto;

namespace tibs.stem.ContactDesignation
{
    public class ContactDesignationAppService : stemAppServiceBase, IContactDesignationAppService
    {
        private readonly IRepository<Designation> _DesignationRepository;
        private readonly IContactDesignationExcelExporter _ContactDesignationExcelExporter;

        public ContactDesignationAppService(IRepository<Designation> DesignationRepository, IContactDesignationExcelExporter ContactDesignationExcelExporter)
        {
            _DesignationRepository = DesignationRepository;
            _ContactDesignationExcelExporter = ContactDesignationExcelExporter;
        }
        public ListResultDto<ContactDesignationInput> GetContactDesignation(GetContactDesignationInput input)
        {
            var ContactDesignation = _DesignationRepository.GetAll()

                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                       u.DesiginationName.Contains(input.Filter) ||
                       u.DesignationCode.Contains(input.Filter) ||
                       u.Id.ToString().Contains(input.Filter))
                .ToList();

            return new ListResultDto<ContactDesignationInput>(ContactDesignation.MapTo<List<ContactDesignationInput>>());
        }

        public async Task<GetContactDesignation> GetContactDesignationForEdit(NullableIdDto input)
        {
            var output = new GetContactDesignation
            {
            };

            var ContactDesignation = _DesignationRepository.GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            output.ContactDesignation = ContactDesignation.MapTo<ContactDesignationInput>();
            return output;
        }

        public async Task ContactDesignationCreateOrUpdate(ContactDesignationInput input)
        {
            if (input.Id != 0)
            {
                await ContactDesignationUpdate(input);
            }
            else
            {
                await ContactDesignationCreate(input);
            }


        }

        public async Task ContactDesignationCreate(ContactDesignationInput input)
        {
            var contactDesignation = input.MapTo<Designation>();
            var val = _DesignationRepository
             .GetAll().Where(p => p.DesiginationName == input.DesiginationName || p.DesignationCode == input.DesignationCode).FirstOrDefault();

            if (val == null)
            {
                await _DesignationRepository.InsertAsync(contactDesignation);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in DesiginationName '" + input.DesiginationName + "' or DesignationCode '" + input.DesignationCode + "'...");
            }

        }

        public async Task ContactDesignationUpdate(ContactDesignationInput input)
        {
            var contactDesignation = input.MapTo<Designation>();
            var val = _DesignationRepository
             .GetAll().Where(p => (p.DesiginationName == input.DesiginationName || p.DesignationCode == input.DesignationCode) && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _DesignationRepository.UpdateAsync(contactDesignation);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in DesiginationName '" + input.DesiginationName + "' or DesignationCode '" + input.DesignationCode + "'...");
            }

        }
        public async Task GetDeleteContactDesignation(EntityDto input)
        {
            await _DesignationRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetContactDesignationToExcel()
        {

            var ContactDesignation = _DesignationRepository.GetAll();
            var ContactDesignations = (from a in ContactDesignation
                                       select new ContactDesignationInput
                                       {
                                          Id = a.Id,
                                          DesignationCode = a.DesignationCode,
                                          DesiginationName = a.DesiginationName

                                       }).ToList();


            var ContactDesignationList = ContactDesignations.MapTo<List<ContactDesignationInput>>();

            return _ContactDesignationExcelExporter.ExportToFile(ContactDesignationList);
        }

    }
}
