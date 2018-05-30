﻿using System.Threading.Tasks;
using Abp.Application.Services;
using tibs.stem.MultiTenancy.Dto;
using tibs.stem.MultiTenancy.Payments.Dto;
using Abp.Application.Services.Dto;

namespace tibs.stem.MultiTenancy.Payments
{
    public interface IPaymentAppService : IApplicationService
    {
        Task<PaymentInfoDto> GetPaymentInfo(PaymentInfoInput input);

        Task<CreatePaymentResponse> CreatePayment(CreatePaymentDto input);

        Task<ExecutePaymentResponse> ExecutePayment(ExecutePaymentDto input);

        Task<PagedResultDto<SubscriptionPaymentListDto>> GetPaymentHistory(GetPaymentHistoryInput input);
    }
}
