using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.EntityFrameworkCore;
using tibs.stem.Authorization.Roles;
using tibs.stem.Chat.SignalR;
using tibs.stem.Editions;
using tibs.stem.Sessions.Dto;

namespace tibs.stem.Sessions
{
    public class SessionAppService : stemAppServiceBase, ISessionAppService
    {
        private readonly RoleManager _roleManager;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        public SessionAppService(
            RoleManager roleManager,
            IRepository<UserRole, long> userRoleRepository)
        {
            _roleManager = roleManager;
            _userRoleRepository = userRoleRepository;
        }
        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput
            {
                Application = new ApplicationInfoDto
                {
                    Version = AppVersionHelper.Version,
                    ReleaseDate = AppVersionHelper.ReleaseDate,
                    Features = new Dictionary<string, bool>
                    {
                        { "SignalR", SignalRFeature.IsAvailable }
                    }
                }
            };

            if (AbpSession.TenantId.HasValue)
            {
                output.Tenant = ObjectMapper
                                    .Map<TenantLoginInfoDto>(await TenantManager
                                        .Tenants
                                        .Include(t => t.Edition)
                                        .FirstAsync(t => t.Id == AbpSession.GetTenantId()));
            }

            if (AbpSession.UserId.HasValue)
            {
                output.User = ObjectMapper.Map<UserLoginInfoDto>(await GetCurrentUserAsync());
                var userrole = (from a in UserManager.Users
                                join urole in _userRoleRepository.GetAll() on a.Id equals urole.UserId
                                join role in _roleManager.Roles on urole.RoleId equals role.Id
                                where urole.UserId == AbpSession.UserId
                                select role).FirstOrDefault();
                output.User.Role = userrole.DisplayName;
            }

            if (output.Tenant == null)
            {
                return output;
            }

            output.Tenant.Edition?.SetEditionIsHighest(GetTopEditionOrNullByMonthlyPrice());
            output.Tenant.SubscriptionDateString = GetTenantSubscriptionDateString(output);
            output.Tenant.CreationTimeString = output.Tenant.CreationTime.ToString("d");

            return output;
        }

        private SubscribableEdition GetTopEditionOrNullByMonthlyPrice()
        {
            var editions = TenantManager.EditionManager.Editions;
            if (editions == null || !editions.Any())
            {
                return null;
            }

            return ObjectMapper
                  .Map<IEnumerable<SubscribableEdition>>(editions)
                  .OrderByDescending(e => e.MonthlyPrice)
                  .FirstOrDefault();
        }

        private string GetTenantSubscriptionDateString(GetCurrentLoginInformationsOutput output)
        {
            return output.Tenant.SubscriptionEndDateUtc == null
                ? L("Unlimited")
                : output.Tenant.SubscriptionEndDateUtc?.ToString("d");
        }

        public async Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken()
        {
            if (AbpSession.UserId <= 0)
            {
                throw new Exception(L("ThereIsNoLoggedInUser"));
            }

            var user = await UserManager.GetUserAsync(AbpSession.ToUserIdentifier());
            user.SetSignInToken();
            return new UpdateUserSignInTokenOutput
            {
                SignInToken = user.SignInToken,
                EncodedUserId = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Id.ToString())),
                EncodedTenantId = user.TenantId.HasValue ? Convert.ToBase64String(Encoding.UTF8.GetBytes(user.TenantId.Value.ToString())) : ""
            };
        }
    }
}