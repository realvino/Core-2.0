﻿using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace tibs.stem.MultiTenancy.Payments.Dto
{
    [AutoMap(typeof(SubscriptionPayment))]
    public class SubscriptionPaymentListDto: AuditedEntityDto
    {
        public string Gateway { get; set; }

        public decimal Amount { get; set; }

        public int EditionId { get; set; }

        public int DayCount { get; set; }

        public string PaymentPeriodType { get; set; }

        public string PaymentId { get; set; }

        public string PayerId { get; set; }

        public string Status { get; set; }

        public string EditionDisplayName { get; set; }
    }
}