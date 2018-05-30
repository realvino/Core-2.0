﻿using System;
using Abp.Authorization.Users;
using Abp.Extensions;
using Abp.Timing;
using System.ComponentModel.DataAnnotations.Schema;
using tibs.stem.Departments;
using tibs.stem.UserDesignations;

namespace tibs.stem.Authorization.Users
{
    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    public class User : AbpUser<User>
    {
        public const int MaxPhoneNumberLength = 24;

        public virtual Guid? ProfilePictureId { get; set; }

        public virtual string ProfilePictureUrl { get; set; }

        public string EmailPassword { get; set; }

        public virtual bool ShouldChangePasswordOnNextLogin { get; set; }

        public DateTime? SignInTokenExpireTimeUtc { get; set; }

        public string SignInToken { get; set; }

        public string GoogleAuthenticatorKey { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Departments { get; set; }
        public virtual int? DepartmentId { get; set; }

        [ForeignKey("UserDesignationId")]
        public virtual UserDesignation UserDesignations { get; set; }
        public virtual int? UserDesignationId { get; set; }

        public string Url { get; set; }

        //Can add application specific user properties here

        public User()
        {
            IsLockoutEnabled = true;
            IsTwoFactorEnabled = true;
        }

        /// <summary>
        /// Creates admin <see cref="User"/> for a tenant.
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="emailAddress">Email address</param>
        /// <returns>Created <see cref="User"/> object</returns>
        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress
            };

            user.SetNormalizedNames();

            return user;
        }

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public override void SetNewPasswordResetCode()
        {
            /* This reset code is intentionally kept short.
             * It should be short and easy to enter in a mobile application, where user can not click a link.
             */
            PasswordResetCode = Guid.NewGuid().ToString("N").Truncate(10).ToUpperInvariant();
        }

        public void Unlock()
        {
            AccessFailedCount = 0;
            LockoutEndDateUtc = null;
        }

        public void SetSignInToken()
        {
            SignInToken = Guid.NewGuid().ToString();
            SignInTokenExpireTimeUtc = Clock.Now.AddMinutes(1).ToUniversalTime();
        }
    }
}