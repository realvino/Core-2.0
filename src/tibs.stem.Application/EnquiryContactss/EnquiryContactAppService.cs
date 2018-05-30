using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.EnquiryContacts;
using tibs.stem.EnquiryContactss.Dto;
using tibs.stem.Inquirys;
using tibs.stem.NewCustomerCompanys;
using System.Linq.Dynamic.Core;
using tibs.stem.Tenants.Dashboard;
using tibs.stem.Countrys;
using System.Data.SqlClient;
using System.Data;
using tibs.stem.EnquiryDetails;
using tibs.stem.AcitivityTracks;

namespace tibs.stem.EnquiryContactss
{
    public class EnquiryContactAppService : stemAppServiceBase, IEnquiryContactAppService
    {
        private readonly IRepository<EnquiryContact> _enquiryContactRepository;
        private readonly IRepository<Inquiry> _inquiryRepository;
        private readonly IRepository<NewContact> _newContactRepository;
        private readonly IRepository<AcitivityTrack> _acitivityTrackRepository;
        private readonly IRepository<EnquiryDetail> _enquiryDetailRepository;
        public EnquiryContactAppService(
            IRepository<EnquiryContact> enquiryContactRepository, 
            IRepository<Inquiry> inquiryRepository, 
            IRepository<NewContact> newContactRepository,
            IRepository<EnquiryDetail> enquiryDetailRepository,
            IRepository<AcitivityTrack> acitivityTrackRepository)
        {
            _enquiryContactRepository = enquiryContactRepository;
            _inquiryRepository = inquiryRepository;
            _newContactRepository = newContactRepository;
            _enquiryDetailRepository = enquiryDetailRepository;
            _enquiryDetailRepository = enquiryDetailRepository;

        }



        public ListResultDto<EnquiryContactListDto> GetEnquiryWiseEnquiryContact(NullableIdDto<long> input)
        {
            var query = _enquiryContactRepository.GetAll().Where(p => p.InquiryId == input.Id && p.IsDeleted == false);

            try
            {
                var select = (from a in query
                              select new EnquiryContactListDto
                              {
                                  Id = a.Id,
                                  EnquiryName = a.Inquiry.Name,
                                  NewContactName = a.Contacts.Name,
                                  ContactId = (int)a.ContactId,
                                  NewCompanyName = a.Contacts.NewCompanys.Name,
                                  NewCustomerTypeTitle = a.Contacts.NewCompanys.NewCustomerTypes.Title,
                                  Default = false
                              }).ToList();

                foreach (var data in select)
                {
                    var inqcontact = (from r in _enquiryDetailRepository.GetAll() where r.ContactId == data.ContactId select r).FirstOrDefault();
                    if (inqcontact != null)
                    {
                        data.Default = true;
                    }
                }
                return new ListResultDto<EnquiryContactListDto>(select.MapTo<List<EnquiryContactListDto>>());
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task CreateOrUpdateEnquiryContact(EnquiryContactInputDto input)
        {
            if (input.Id != 0)
            {
                await UpdateEnquiryContactAsync(input);
            }
            else
            {
                await CreateEnquiryContactAsync(input);
            }
        }

        public virtual async Task CreateEnquiryContactAsync(EnquiryContactInputDto input)
        {
            var EnqContact = input.MapTo<EnquiryContact>();
            var val = _enquiryContactRepository
              .GetAll().Where(p => p.InquiryId == input.InquiryId && p.ContactId == input.ContactId).FirstOrDefault();

            if (val == null)
            {
                await _enquiryContactRepository.InsertAsync(EnqContact);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Inquiry name '" + val.Inquiry.Name + "' or Contact name '" + val.Contacts.Name + "'...");
            }
        }

        public virtual async Task UpdateEnquiryContactAsync(EnquiryContactInputDto input)
        {


            var EnqContact = await _enquiryContactRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, EnqContact);

            var val = _enquiryContactRepository
              .GetAll().Where(p => p.InquiryId == input.InquiryId && p.ContactId == input.ContactId && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _enquiryContactRepository.UpdateAsync(EnqContact);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Inquiry name '" + val.Inquiry.Name + "' or Contact name '" + val.Contacts.Name + "'...");
            }
        }

        public async Task GetDeleteEnquiryContact(EntityDto input)
        {
            var query = _enquiryContactRepository.GetAll().Where(p => p.Id == input.Id).FirstOrDefault();
            var inquiryquery = _enquiryDetailRepository.GetAll().Where(p => p.ContactId == query.ContactId).FirstOrDefault();

            if (inquiryquery == null)
            {
                var acitivityquery = _acitivityTrackRepository.GetAll().Where(p => p.ContactId == query.ContactId);
                if (acitivityquery == null)
                {
                    await _enquiryContactRepository.DeleteAsync(input.Id);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Enquiry Contact cannot be deleted '");
                }
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Enquiry Contact cannot be deleted '");
            }
        }

    }
}
