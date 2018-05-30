using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using tibs.stem.EntityFrameworkCore;
using Abp.Authorization;
using Abp.BackgroundJobs;
using Abp.Notifications;
using tibs.stem.Chat;
using tibs.stem.Friendships;
using tibs.stem.MultiTenancy.Payments;

namespace tibs.stem.Migrations
{
    [DbContext(typeof(stemDbContext))]
    [Migration("20180220044738_Alter-Milestone-Table-add-reset")]
    partial class AlterMilestoneTableaddreset
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Abp.Application.Editions.Edition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("AbpEditions");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Edition");
                });

            modelBuilder.Entity("Abp.Application.Features.FeatureSetting", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(2000);

                    b.HasKey("Id");

                    b.ToTable("AbpFeatures");

                    b.HasDiscriminator<string>("Discriminator").HasValue("FeatureSetting");
                });

            modelBuilder.Entity("Abp.Auditing.AuditLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BrowserInfo")
                        .HasMaxLength(256);

                    b.Property<string>("ClientIpAddress")
                        .HasMaxLength(64);

                    b.Property<string>("ClientName")
                        .HasMaxLength(128);

                    b.Property<string>("CustomData")
                        .HasMaxLength(2000);

                    b.Property<string>("Exception")
                        .HasMaxLength(2000);

                    b.Property<int>("ExecutionDuration");

                    b.Property<DateTime>("ExecutionTime");

                    b.Property<int?>("ImpersonatorTenantId");

                    b.Property<long?>("ImpersonatorUserId");

                    b.Property<string>("MethodName")
                        .HasMaxLength(256);

                    b.Property<string>("Parameters")
                        .HasMaxLength(1024);

                    b.Property<string>("ServiceName")
                        .HasMaxLength(256);

                    b.Property<int?>("TenantId");

                    b.Property<long?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId", "ExecutionDuration");

                    b.HasIndex("TenantId", "ExecutionTime");

                    b.HasIndex("TenantId", "UserId");

                    b.ToTable("AbpAuditLogs");
                });

            modelBuilder.Entity("Abp.Authorization.PermissionSetting", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<bool>("IsGranted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId", "Name");

                    b.ToTable("AbpPermissions");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PermissionSetting");
                });

            modelBuilder.Entity("Abp.Authorization.Roles.RoleClaim", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<int>("RoleId");

                    b.Property<int?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("TenantId", "ClaimType");

                    b.ToTable("AbpRoleClaims");
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserAccount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("EmailAddress");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastLoginTime");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int?>("TenantId");

                    b.Property<long>("UserId");

                    b.Property<long?>("UserLinkId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("EmailAddress");

                    b.HasIndex("UserName");

                    b.HasIndex("TenantId", "EmailAddress");

                    b.HasIndex("TenantId", "UserId");

                    b.HasIndex("TenantId", "UserName");

                    b.ToTable("AbpUserAccounts");
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserClaim", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<int?>("TenantId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("TenantId", "ClaimType");

                    b.ToTable("AbpUserClaims");
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserLogin", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LoginProvider")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int?>("TenantId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("TenantId", "UserId");

                    b.HasIndex("TenantId", "LoginProvider", "ProviderKey");

                    b.ToTable("AbpUserLogins");
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserLoginAttempt", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BrowserInfo")
                        .HasMaxLength(256);

                    b.Property<string>("ClientIpAddress")
                        .HasMaxLength(64);

                    b.Property<string>("ClientName")
                        .HasMaxLength(128);

                    b.Property<DateTime>("CreationTime");

                    b.Property<byte>("Result");

                    b.Property<string>("TenancyName")
                        .HasMaxLength(64);

                    b.Property<int?>("TenantId");

                    b.Property<long?>("UserId");

                    b.Property<string>("UserNameOrEmailAddress")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("UserId", "TenantId");

                    b.HasIndex("TenancyName", "UserNameOrEmailAddress", "Result");

                    b.ToTable("AbpUserLoginAttempts");
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserOrganizationUnit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<bool>("IsDeleted");

                    b.Property<long>("OrganizationUnitId");

                    b.Property<int?>("TenantId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId", "OrganizationUnitId");

                    b.HasIndex("TenantId", "UserId");

                    b.ToTable("AbpUserOrganizationUnits");
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<int>("RoleId");

                    b.Property<int?>("TenantId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("TenantId", "RoleId");

                    b.HasIndex("TenantId", "UserId");

                    b.ToTable("AbpUserRoles");
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<int?>("TenantId");

                    b.Property<long>("UserId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("TenantId", "UserId");

                    b.ToTable("AbpUserTokens");
                });

            modelBuilder.Entity("Abp.BackgroundJobs.BackgroundJobInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<bool>("IsAbandoned");

                    b.Property<string>("JobArgs")
                        .IsRequired()
                        .HasMaxLength(1048576);

                    b.Property<string>("JobType")
                        .IsRequired()
                        .HasMaxLength(512);

                    b.Property<DateTime?>("LastTryTime");

                    b.Property<DateTime>("NextTryTime");

                    b.Property<byte>("Priority");

                    b.Property<short>("TryCount");

                    b.HasKey("Id");

                    b.HasIndex("IsAbandoned", "NextTryTime");

                    b.ToTable("AbpBackgroundJobs");
                });

            modelBuilder.Entity("Abp.Configuration.Setting", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int?>("TenantId");

                    b.Property<long?>("UserId");

                    b.Property<string>("Value")
                        .HasMaxLength(2000);

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("TenantId", "Name");

                    b.ToTable("AbpSettings");
                });

            modelBuilder.Entity("Abp.IdentityServer4.PersistedGrantEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(200);

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000);

                    b.Property<DateTime?>("Expiration");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("SubjectId", "ClientId", "Type");

                    b.ToTable("AbpPersistedGrants");
                });

            modelBuilder.Entity("Abp.Localization.ApplicationLanguage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("Icon")
                        .HasMaxLength(128);

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsDisabled");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<int?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId", "Name");

                    b.ToTable("AbpLanguages");
                });

            modelBuilder.Entity("Abp.Localization.ApplicationLanguageText", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("LanguageName")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int?>("TenantId");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(67108864);

                    b.HasKey("Id");

                    b.HasIndex("TenantId", "Source", "LanguageName", "Key");

                    b.ToTable("AbpLanguageTexts");
                });

            modelBuilder.Entity("Abp.Notifications.NotificationInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("Data")
                        .HasMaxLength(1048576);

                    b.Property<string>("DataTypeName")
                        .HasMaxLength(512);

                    b.Property<string>("EntityId")
                        .HasMaxLength(96);

                    b.Property<string>("EntityTypeAssemblyQualifiedName")
                        .HasMaxLength(512);

                    b.Property<string>("EntityTypeName")
                        .HasMaxLength(250);

                    b.Property<string>("ExcludedUserIds")
                        .HasMaxLength(131072);

                    b.Property<string>("NotificationName")
                        .IsRequired()
                        .HasMaxLength(96);

                    b.Property<byte>("Severity");

                    b.Property<string>("TenantIds")
                        .HasMaxLength(131072);

                    b.Property<string>("UserIds")
                        .HasMaxLength(131072);

                    b.HasKey("Id");

                    b.ToTable("AbpNotifications");
                });

            modelBuilder.Entity("Abp.Notifications.NotificationSubscriptionInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("EntityId")
                        .HasMaxLength(96);

                    b.Property<string>("EntityTypeAssemblyQualifiedName")
                        .HasMaxLength(512);

                    b.Property<string>("EntityTypeName")
                        .HasMaxLength(250);

                    b.Property<string>("NotificationName")
                        .HasMaxLength(96);

                    b.Property<int?>("TenantId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("NotificationName", "EntityTypeName", "EntityId", "UserId");

                    b.HasIndex("TenantId", "NotificationName", "EntityTypeName", "EntityId", "UserId");

                    b.ToTable("AbpNotificationSubscriptions");
                });

            modelBuilder.Entity("Abp.Notifications.TenantNotificationInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("Data")
                        .HasMaxLength(1048576);

                    b.Property<string>("DataTypeName")
                        .HasMaxLength(512);

                    b.Property<string>("EntityId")
                        .HasMaxLength(96);

                    b.Property<string>("EntityTypeAssemblyQualifiedName")
                        .HasMaxLength(512);

                    b.Property<string>("EntityTypeName")
                        .HasMaxLength(250);

                    b.Property<string>("NotificationName")
                        .IsRequired()
                        .HasMaxLength(96);

                    b.Property<byte>("Severity");

                    b.Property<int?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("AbpTenantNotifications");
                });

            modelBuilder.Entity("Abp.Notifications.UserNotificationInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<int>("State");

                    b.Property<int?>("TenantId");

                    b.Property<Guid>("TenantNotificationId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "State", "CreationTime");

                    b.ToTable("AbpUserNotifications");
                });

            modelBuilder.Entity("Abp.Organizations.OrganizationUnit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(95);

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<long?>("ParentId");

                    b.Property<int?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("TenantId", "Code");

                    b.ToTable("AbpOrganizationUnits");
                });

            modelBuilder.Entity("tibs.stem.AbpSalesCoOrinators.AbpSalesCoOrinator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CoordinatorId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<long?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CoordinatorId");

                    b.HasIndex("UserId");

                    b.ToTable("AbpSalesCoOrinator");
                });

            modelBuilder.Entity("tibs.stem.AcitivityTracks.AcitivityTrack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActivityId");

                    b.Property<int?>("ContactId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("CurrentStatus");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<int>("EnquiryId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Message");

                    b.Property<string>("PreviousStatus");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("ContactId");

                    b.HasIndex("EnquiryId");

                    b.ToTable("AcitivityTrack");
                });

            modelBuilder.Entity("tibs.stem.Activities.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActivityCode")
                        .IsRequired();

                    b.Property<string>("ActivityName")
                        .IsRequired();

                    b.Property<string>("ColorCode");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.HasKey("Id");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("tibs.stem.ActivityTrackComments.ActivityTrackComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActivityTrackId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Message");

                    b.HasKey("Id");

                    b.HasIndex("ActivityTrackId");

                    b.ToTable("ActivityTrackComment");
                });

            modelBuilder.Entity("tibs.stem.AttributeGroupDetails.AttributeGroupDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AttributeGroupId");

                    b.Property<int>("AttributeId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.HasKey("Id");

                    b.HasIndex("AttributeGroupId");

                    b.HasIndex("AttributeId");

                    b.ToTable("AttributeGroupDetail");
                });

            modelBuilder.Entity("tibs.stem.Authorization.Roles.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<bool>("IsDefault");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsStatic");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<int?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("CreatorUserId");

                    b.HasIndex("DeleterUserId");

                    b.HasIndex("LastModifierUserId");

                    b.HasIndex("TenantId", "NormalizedName");

                    b.ToTable("AbpRoles");
                });

            modelBuilder.Entity("tibs.stem.Authorization.Users.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("AuthenticationSource")
                        .HasMaxLength(64);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<int?>("DepartmentId");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("EmailConfirmationCode")
                        .HasMaxLength(328);

                    b.Property<string>("GoogleAuthenticatorKey");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsEmailConfirmed");

                    b.Property<bool>("IsLockoutEnabled");

                    b.Property<bool>("IsPhoneNumberConfirmed");

                    b.Property<bool>("IsTwoFactorEnabled");

                    b.Property<DateTime?>("LastLoginTime");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<DateTime?>("LockoutEndDateUtc");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("NormalizedEmailAddress")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("PasswordResetCode")
                        .HasMaxLength(328);

                    b.Property<string>("PhoneNumber");

                    b.Property<Guid?>("ProfilePictureId");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("ShouldChangePasswordOnNextLogin");

                    b.Property<string>("SignInToken");

                    b.Property<DateTime?>("SignInTokenExpireTimeUtc");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<int?>("TenantId");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.HasIndex("CreatorUserId");

                    b.HasIndex("DeleterUserId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("LastModifierUserId");

                    b.HasIndex("TenantId", "NormalizedEmailAddress");

                    b.HasIndex("TenantId", "NormalizedUserName");

                    b.ToTable("AbpUsers");
                });

            modelBuilder.Entity("tibs.stem.Chat.ChatMessage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(4096);

                    b.Property<int>("ReadState");

                    b.Property<int>("Side");

                    b.Property<int?>("TargetTenantId");

                    b.Property<long>("TargetUserId");

                    b.Property<int?>("TenantId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TargetTenantId", "TargetUserId", "ReadState");

                    b.HasIndex("TargetTenantId", "UserId", "ReadState");

                    b.HasIndex("TenantId", "TargetUserId", "ReadState");

                    b.HasIndex("TenantId", "UserId", "ReadState");

                    b.ToTable("AppChatMessages");
                });

            modelBuilder.Entity("tibs.stem.Citys.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CityCode")
                        .IsRequired();

                    b.Property<string>("CityName")
                        .IsRequired();

                    b.Property<int>("CountryId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("tibs.stem.Collections.Collection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CollectionCode");

                    b.Property<string>("CollectionName");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.HasKey("Id");

                    b.ToTable("Collection");
                });

            modelBuilder.Entity("tibs.stem.ColorCodes.ColorCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Color");

                    b.Property<string>("Component");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.HasKey("Id");

                    b.ToTable("ColorCode");
                });

            modelBuilder.Entity("tibs.stem.Companies.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("AccountManagerId");

                    b.Property<string>("Address");

                    b.Property<int>("CityId");

                    b.Property<string>("CompanyCode");

                    b.Property<string>("CompanyName");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<int>("CustomerTypeId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("Email");

                    b.Property<string>("Fax");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Mob_No");

                    b.Property<string>("PhoneNo");

                    b.Property<string>("TelNo");

                    b.HasKey("Id");

                    b.HasIndex("AccountManagerId");

                    b.HasIndex("CityId");

                    b.HasIndex("CustomerTypeId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("tibs.stem.CompanyContacts.CompanyContact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<int>("CompanyId");

                    b.Property<string>("ContactPersonName");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("Description");

                    b.Property<int?>("DesiginationId");

                    b.Property<string>("Email");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Mobile_No");

                    b.Property<int>("TitleId");

                    b.Property<string>("Work_No");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("DesiginationId");

                    b.HasIndex("TitleId");

                    b.ToTable("CompanyContacts");
                });

            modelBuilder.Entity("tibs.stem.Countrys.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasMaxLength(3);

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("ISDCode")
                        .HasMaxLength(5);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.HasKey("Id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("tibs.stem.CustomerTypes.CustomerType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("CustomerTypeName");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.HasKey("Id");

                    b.ToTable("CustomerType");
                });

            modelBuilder.Entity("tibs.stem.Departments.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("DepartmentCode");

                    b.Property<string>("DepatmentName");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.HasKey("Id");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("tibs.stem.Designations.Designation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("DesiginationName");

                    b.Property<string>("DesignationCode");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.HasKey("Id");

                    b.ToTable("Designations");
                });

            modelBuilder.Entity("tibs.stem.Dimensions.Dimension", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("DimensionCode");

                    b.Property<string>("DimensionName");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.HasKey("Id");

                    b.ToTable("Dimension");
                });

            modelBuilder.Entity("tibs.stem.Discounts.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<decimal>("Discountable");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("QuotationDescription");

                    b.Property<decimal>("UnDiscountable");

                    b.Property<int>("Vat");

                    b.HasKey("Id");

                    b.ToTable("Discount");
                });

            modelBuilder.Entity("tibs.stem.Emaildomains.Emaildomain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("EmaildomainName");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.HasKey("Id");

                    b.ToTable("Emaildomain");
                });

            modelBuilder.Entity("tibs.stem.EnquiryContacts.EnquiryContact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ContactId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<int?>("InquiryId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.HasIndex("InquiryId");

                    b.ToTable("EnquiryContacts");
                });

            modelBuilder.Entity("tibs.stem.EnquiryDetails.EnquiryDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("AssignedbyDate");

                    b.Property<long?>("AssignedbyId");

                    b.Property<DateTime?>("ClosureDate");

                    b.Property<int?>("CompanyId");

                    b.Property<int?>("CompatitorsId");

                    b.Property<int?>("ContactId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<int?>("DepartmentId");

                    b.Property<int?>("DesignationId");

                    b.Property<decimal>("EstimationValue");

                    b.Property<int?>("InquiryId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastActivity");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int?>("LeadTypeId");

                    b.Property<string>("Size");

                    b.Property<string>("Summary");

                    b.Property<int?>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("AssignedbyId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("CompatitorsId");

                    b.HasIndex("ContactId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("DesignationId");

                    b.HasIndex("InquiryId");

                    b.HasIndex("LeadTypeId");

                    b.HasIndex("TeamId");

                    b.ToTable("EnquiryDetail");
                });

            modelBuilder.Entity("tibs.stem.EnquirySources.EnquirySource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<int?>("InquiryId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int?>("SourceId");

                    b.HasKey("Id");

                    b.HasIndex("InquiryId");

                    b.HasIndex("SourceId");

                    b.ToTable("EnquirySource");
                });

            modelBuilder.Entity("tibs.stem.EnquiryStatuss.EnquiryStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("EnqStatusCode")
                        .IsRequired();

                    b.Property<string>("EnqStatusColor");

                    b.Property<string>("EnqStatusName")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<decimal?>("Percentage");

                    b.Property<int?>("StagestateId");

                    b.HasKey("Id");

                    b.HasIndex("StagestateId");

                    b.ToTable("EnquiryStatus");
                });

            modelBuilder.Entity("tibs.stem.Friendships.Friendship", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<Guid?>("FriendProfilePictureId");

                    b.Property<string>("FriendTenancyName");

                    b.Property<int?>("FriendTenantId");

                    b.Property<long>("FriendUserId");

                    b.Property<string>("FriendUserName")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<int>("State");

                    b.Property<int?>("TenantId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("FriendTenantId", "FriendUserId");

                    b.HasIndex("FriendTenantId", "UserId");

                    b.HasIndex("TenantId", "FriendUserId");

                    b.HasIndex("TenantId", "UserId");

                    b.ToTable("AppFriendships");
                });

            modelBuilder.Entity("tibs.stem.ImportHistorys.ImportHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("FileName");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("ProductCode");

                    b.Property<string>("Quantity");

                    b.Property<int?>("QuotationId");

                    b.Property<string>("SectionName");

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.HasIndex("QuotationId");

                    b.ToTable("ImportHistory");
                });

            modelBuilder.Entity("tibs.stem.Industrys.Industry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("IndustryCode")
                        .IsRequired();

                    b.Property<string>("IndustryName")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.HasKey("Id");

                    b.ToTable("Industry");
                });

            modelBuilder.Entity("tibs.stem.Inquirys.Inquiry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<bool?>("Archieved");

                    b.Property<string>("Browcerinfo");

                    b.Property<int?>("CompanyId");

                    b.Property<string>("CompanyName");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<int?>("DepartmentId");

                    b.Property<int?>("DesignationId");

                    b.Property<string>("DesignationName");

                    b.Property<string>("Email");

                    b.Property<string>("IpAddress");

                    b.Property<bool?>("IsClosed");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool?>("Junk");

                    b.Property<DateTime?>("JunkDate");

                    b.Property<string>("LandlineNumber");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int?>("LocationId");

                    b.Property<string>("MbNo");

                    b.Property<int?>("MileStoneId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("OpportunitySourceId");

                    b.Property<string>("Remarks");

                    b.Property<int?>("StatusId");

                    b.Property<string>("SubMmissionId");

                    b.Property<string>("WebSite");

                    b.Property<int?>("WhyBafcoId");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("DesignationId");

                    b.HasIndex("LocationId");

                    b.HasIndex("MileStoneId");

                    b.HasIndex("OpportunitySourceId");

                    b.HasIndex("StatusId");

                    b.HasIndex("WhyBafcoId");

                    b.ToTable("Inquiry");
                });

            modelBuilder.Entity("tibs.stem.Inquirys.JobActivity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("AllottedDate");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<long?>("DesignerId");

                    b.Property<DateTime?>("EndDate");

                    b.Property<int?>("InquiryId");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("Isopen");

                    b.Property<string>("JobNumber");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Remark");

                    b.Property<DateTime?>("StartDate");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("DesignerId");

                    b.HasIndex("InquiryId");

                    b.ToTable("JobActivity");
                });

            modelBuilder.Entity("tibs.stem.LeadDetails.LeadDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CoordinatorId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<long?>("DesignerId");

                    b.Property<float?>("EstimationValue");

                    b.Property<int?>("InquiryId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int?>("LeadSourceId");

                    b.Property<int?>("LeadTypeId");

                    b.Property<long?>("SalesManagerId");

                    b.Property<string>("Size");

                    b.HasKey("Id");

                    b.HasIndex("CoordinatorId");

                    b.HasIndex("DesignerId");

                    b.HasIndex("InquiryId");

                    b.HasIndex("LeadSourceId");

                    b.HasIndex("LeadTypeId");

                    b.HasIndex("SalesManagerId");

                    b.ToTable("LeadDetails");
                });

            modelBuilder.Entity("tibs.stem.LeadReasons.LeadReason", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("LeadReasonCode");

                    b.Property<string>("LeadReasonName");

                    b.HasKey("Id");

                    b.ToTable("LeadReason");
                });

            modelBuilder.Entity("tibs.stem.LeadSources.LeadSource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("LeadSourceCode");

                    b.Property<string>("LeadSourceName");

                    b.HasKey("Id");

                    b.ToTable("LeadSources");
                });

            modelBuilder.Entity("tibs.stem.LeadTypes.LeadType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("LeadTypeCode");

                    b.Property<string>("LeadTypeName");

                    b.HasKey("Id");

                    b.ToTable("LeadType");
                });

            modelBuilder.Entity("tibs.stem.LineTypes.LineType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("LineTypeCode")
                        .IsRequired();

                    b.Property<string>("LineTypeName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("LineType");
                });

            modelBuilder.Entity("tibs.stem.Locations.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CityId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("LocationCode")
                        .IsRequired();

                    b.Property<string>("LocationName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("tibs.stem.Milestones.MileStone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsQuotation");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("MileStoneCode");

                    b.Property<string>("MileStoneName");

                    b.Property<bool>("ResetActivity");

                    b.Property<int?>("RottingPeriod");

                    b.Property<int>("SourceTypeId");

                    b.HasKey("Id");

                    b.HasIndex("SourceTypeId");

                    b.ToTable("MileStone");
                });

            modelBuilder.Entity("tibs.stem.Milestones.StageDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int>("MileStoneId");

                    b.Property<int>("StageId");

                    b.HasKey("Id");

                    b.HasIndex("MileStoneId");

                    b.HasIndex("StageId");

                    b.ToTable("MileStoneDetails");
                });

            modelBuilder.Entity("tibs.stem.MultiTenancy.Payments.SubscriptionPayment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<int>("DayCount");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<int>("EditionId");

                    b.Property<int>("Gateway");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("PaymentId");

                    b.Property<int?>("PaymentPeriodType");

                    b.Property<int>("Status");

                    b.Property<int>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("EditionId");

                    b.HasIndex("PaymentId", "Gateway");

                    b.HasIndex("Status", "CreationTime");

                    b.ToTable("AppSubscriptionPayments");
                });

            modelBuilder.Entity("tibs.stem.MultiTenancy.Tenant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConnectionString")
                        .HasMaxLength(1024);

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<Guid?>("CustomCssId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<int?>("EditionId");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsInTrialPeriod");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("LogoFileType")
                        .HasMaxLength(64);

                    b.Property<Guid?>("LogoId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<DateTime?>("SubscriptionEndDateUtc");

                    b.Property<string>("TenancyName")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.HasIndex("CreationTime");

                    b.HasIndex("CreatorUserId");

                    b.HasIndex("DeleterUserId");

                    b.HasIndex("EditionId");

                    b.HasIndex("LastModifierUserId");

                    b.HasIndex("SubscriptionEndDateUtc");

                    b.HasIndex("TenancyName");

                    b.ToTable("AbpTenants");
                });

            modelBuilder.Entity("tibs.stem.NewCustomerCompanys.NewAddressInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address1");

                    b.Property<string>("Address2");

                    b.Property<int?>("CityId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int?>("NewCompanyId");

                    b.Property<int?>("NewContacId");

                    b.Property<int?>("NewInfoTypeId");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("NewCompanyId");

                    b.HasIndex("NewContacId");

                    b.HasIndex("NewInfoTypeId");

                    b.ToTable("NewAddressInfo");
                });

            modelBuilder.Entity("tibs.stem.NewCustomerCompanys.NewCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("AccountManagerId");

                    b.Property<long?>("ApprovedById");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("CustomerId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsApproved");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name");

                    b.Property<int?>("NewCustomerTypeId");

                    b.Property<string>("TRNnumber");

                    b.Property<string>("TradeLicense");

                    b.HasKey("Id");

                    b.HasIndex("AccountManagerId");

                    b.HasIndex("ApprovedById");

                    b.HasIndex("NewCustomerTypeId");

                    b.ToTable("NewCompany");
                });

            modelBuilder.Entity("tibs.stem.NewCustomerCompanys.NewContact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<int?>("DesignationId");

                    b.Property<int?>("IndustryId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("LastName");

                    b.Property<string>("Name");

                    b.Property<int?>("NewCompanyId");

                    b.Property<int?>("NewCustomerTypeId");

                    b.Property<int?>("TitleId");

                    b.HasKey("Id");

                    b.HasIndex("DesignationId");

                    b.HasIndex("IndustryId");

                    b.HasIndex("NewCompanyId");

                    b.HasIndex("NewCustomerTypeId");

                    b.HasIndex("TitleId");

                    b.ToTable("NewContact");
                });

            modelBuilder.Entity("tibs.stem.NewCustomerCompanys.NewContactInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("InfoData");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int?>("NewCompanyId");

                    b.Property<int?>("NewContacId");

                    b.Property<int?>("NewInfoTypeId");

                    b.HasKey("Id");

                    b.HasIndex("NewCompanyId");

                    b.HasIndex("NewContacId");

                    b.HasIndex("NewInfoTypeId");

                    b.ToTable("NewContactInfo");
                });

            modelBuilder.Entity("tibs.stem.NewCustomerCompanys.NewCustomerType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("Company");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("NewCustomerType");
                });

            modelBuilder.Entity("tibs.stem.NewCustomerCompanys.NewInfoType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContactName");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool?>("Info");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.HasKey("Id");

                    b.ToTable("NewInfoType");
                });

            modelBuilder.Entity("tibs.stem.OpportunitySources.OpportunitySource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("OpportunitySource");
                });

            modelBuilder.Entity("tibs.stem.Orientations.Orientation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("OrientationCode");

                    b.Property<string>("OrientationName");

                    b.HasKey("Id");

                    b.ToTable("Orientation");
                });

            modelBuilder.Entity("tibs.stem.PriceLevels.PriceLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("DiscountAllowed");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("PriceLevelCode");

                    b.Property<string>("PriceLevelName");

                    b.HasKey("Id");

                    b.ToTable("PriceLevel");
                });

            modelBuilder.Entity("tibs.stem.ProductAttributeGroups.ProductAttributeGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AttributeGroupCode");

                    b.Property<string>("AttributeGroupName");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.HasKey("Id");

                    b.ToTable("AttributeGroup");
                });

            modelBuilder.Entity("tibs.stem.ProductAttributes.ProductAttribute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AttributeCode");

                    b.Property<string>("AttributeName");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("Description");

                    b.Property<string>("Imageurl");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.HasKey("Id");

                    b.ToTable("ProductAttributes");
                });

            modelBuilder.Entity("tibs.stem.ProductCategorys.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ProductCategory");
                });

            modelBuilder.Entity("tibs.stem.ProductFamilys.ProductFamily", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CollectionId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("Description");

                    b.Property<bool?>("Discount");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("ProductFamilyCode");

                    b.Property<string>("ProductFamilyName");

                    b.Property<int>("Warranty");

                    b.HasKey("Id");

                    b.HasIndex("CollectionId");

                    b.ToTable("ProductFamily");
                });

            modelBuilder.Entity("tibs.stem.ProductGroups.ProductGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AttributeData");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<int?>("FamilyId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int?>("ProductCategoryId");

                    b.Property<string>("ProductGroupName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("FamilyId");

                    b.HasIndex("ProductCategoryId");

                    b.ToTable("ProductGroup");
                });

            modelBuilder.Entity("tibs.stem.ProductGroups.ProductGroupDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AttributeGroupId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Metedata");

                    b.Property<int>("OrderBy");

                    b.Property<int>("ProductGroupId");

                    b.Property<int>("ReturnBy");

                    b.HasKey("Id");

                    b.HasIndex("AttributeGroupId");

                    b.HasIndex("ProductGroupId");

                    b.ToTable("ProductGroupDetail");
                });

            modelBuilder.Entity("tibs.stem.ProductImageUrls.ProductImageUrl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("ImageUrl");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImageUrl");
                });

            modelBuilder.Entity("tibs.stem.Products.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<int>("Depth");

                    b.Property<string>("Description");

                    b.Property<string>("Gpcode");

                    b.Property<int>("Height");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<decimal?>("Price");

                    b.Property<string>("ProductCode");

                    b.Property<string>("ProductName");

                    b.Property<int?>("ProductSpecificationId");

                    b.Property<string>("SuspectCode");

                    b.Property<int>("Width");

                    b.HasKey("Id");

                    b.HasIndex("ProductSpecificationId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("tibs.stem.Products.ProductPricelevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<decimal>("Price");

                    b.Property<int>("PriceLevelId");

                    b.Property<int>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("PriceLevelId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductPricelevel");
                });

            modelBuilder.Entity("tibs.stem.ProductSpecificationDetails.ProductSpecificationDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AttributeGroupId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int>("ProductSpecificationId");

                    b.HasKey("Id");

                    b.HasIndex("AttributeGroupId");

                    b.HasIndex("ProductSpecificationId");

                    b.ToTable("ProductSpecificationDetail");
                });

            modelBuilder.Entity("tibs.stem.ProductSpecifications.ProductSpecification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("BafcoMade");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("Description");

                    b.Property<string>("ImageUrl");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name");

                    b.Property<int?>("ProductGroupId");

                    b.Property<bool>("Reset");

                    b.HasKey("Id");

                    b.HasIndex("ProductGroupId");

                    b.ToTable("ProductSpecification");
                });

            modelBuilder.Entity("tibs.stem.ProductSubGroups.ProductSubGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<int>("GroupId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("ProductSubGroupCode")
                        .IsRequired();

                    b.Property<string>("ProductSubGroupName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("ProductSubGroup");
                });

            modelBuilder.Entity("tibs.stem.ProductTypes.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("ProductTypeCode");

                    b.Property<string>("ProductTypeName");

                    b.HasKey("Id");

                    b.ToTable("ProductType");
                });

            modelBuilder.Entity("tibs.stem.ProdutSpecLinks.ProdutSpecLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AttributeGroupId");

                    b.Property<int>("AttributeId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int>("ProductGroupId");

                    b.Property<int?>("ProductSpecificationId");

                    b.HasKey("Id");

                    b.HasIndex("AttributeGroupId");

                    b.HasIndex("AttributeId");

                    b.HasIndex("ProductGroupId");

                    b.HasIndex("ProductSpecificationId");

                    b.ToTable("ProdutSpecLink");
                });

            modelBuilder.Entity("tibs.stem.QuotationProducts.QuotationProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Approval");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<decimal>("Discount");

                    b.Property<bool?>("Discountable");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<bool>("Locked");

                    b.Property<decimal>("OverAllDiscount");

                    b.Property<decimal>("OverAllPrice");

                    b.Property<string>("ProductCode");

                    b.Property<int?>("ProductId");

                    b.Property<decimal>("Quantity");

                    b.Property<int>("QuotationId");

                    b.Property<int?>("SectionId");

                    b.Property<string>("TemporaryCode");

                    b.Property<int?>("TemporaryProductId");

                    b.Property<decimal>("TotalAmount");

                    b.Property<decimal>("UnitOfMeasurement");

                    b.Property<decimal>("UnitOfPrice");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("QuotationId");

                    b.HasIndex("SectionId");

                    b.HasIndex("TemporaryProductId");

                    b.ToTable("QuotationProduct");
                });

            modelBuilder.Entity("tibs.stem.Quotations.Quotation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("Archieved");

                    b.Property<int?>("AttentionContactId");

                    b.Property<int?>("CompatitorId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("CustomerId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("Email");

                    b.Property<int?>("InquiryId");

                    b.Property<bool?>("IsClosed");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsVat");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<bool>("Lost");

                    b.Property<DateTime?>("LostDate");

                    b.Property<int?>("MileStoneId");

                    b.Property<string>("MobileNumber");

                    b.Property<string>("Name");

                    b.Property<int?>("NewCompanyId");

                    b.Property<bool>("Optional");

                    b.Property<DateTime>("OrgDate");

                    b.Property<string>("PONumber");

                    b.Property<int>("QuotationStatusId");

                    b.Property<int?>("ReasonId");

                    b.Property<string>("ReasonRemark");

                    b.Property<string>("RefNo");

                    b.Property<bool>("Revised");

                    b.Property<int?>("RevisionId");

                    b.Property<long?>("SalesPersonId");

                    b.Property<int?>("StageId");

                    b.Property<bool>("Submitted");

                    b.Property<DateTime?>("SubmittedDate");

                    b.Property<string>("TermsandCondition");

                    b.Property<int>("Total");

                    b.Property<int>("Vat");

                    b.Property<int>("VatAmount");

                    b.Property<bool>("Void");

                    b.Property<bool>("Won");

                    b.Property<DateTime?>("WonDate");

                    b.HasKey("Id");

                    b.HasIndex("AttentionContactId");

                    b.HasIndex("CompatitorId");

                    b.HasIndex("InquiryId");

                    b.HasIndex("MileStoneId");

                    b.HasIndex("NewCompanyId");

                    b.HasIndex("QuotationStatusId");

                    b.HasIndex("ReasonId");

                    b.HasIndex("SalesPersonId");

                    b.HasIndex("StageId");

                    b.ToTable("Quotation");
                });

            modelBuilder.Entity("tibs.stem.QuotationStatuss.QuotationStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("QuotationStatus");
                });

            modelBuilder.Entity("tibs.stem.Region.RegionCity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CityId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int>("RegionId");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("RegionId");

                    b.ToTable("RegionCity");
                });

            modelBuilder.Entity("tibs.stem.Region.Regions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("RegionCode")
                        .IsRequired();

                    b.Property<string>("RegionName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Region");
                });

            modelBuilder.Entity("tibs.stem.Sections.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name");

                    b.Property<int>("QuotationId");

                    b.HasKey("Id");

                    b.HasIndex("QuotationId");

                    b.ToTable("Section");
                });

            modelBuilder.Entity("tibs.stem.Sources.Source", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ColorCode");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("SourceCode")
                        .IsRequired();

                    b.Property<string>("SourceName")
                        .IsRequired();

                    b.Property<int>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Source");
                });

            modelBuilder.Entity("tibs.stem.SourceTypes.SourceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("SourceTypeCode")
                        .IsRequired();

                    b.Property<string>("SourceTypeName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("SourceType");
                });

            modelBuilder.Entity("tibs.stem.Stagestates.Stagestate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Stagestate");
                });

            modelBuilder.Entity("tibs.stem.Storage.BinaryObject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Bytes")
                        .IsRequired();

                    b.Property<int?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("AppBinaryObjects");
                });

            modelBuilder.Entity("tibs.stem.Team.Teams", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<int>("DepartmentId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name");

                    b.Property<long>("SalesManagerId");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("SalesManagerId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("tibs.stem.TeamDetails.TeamDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<long>("SalesmanId");

                    b.Property<int?>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("SalesmanId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamDetail");
                });

            modelBuilder.Entity("tibs.stem.TemporaryProducts.TemporaryProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<int?>("Depth");

                    b.Property<string>("Description");

                    b.Property<string>("Gpcode");

                    b.Property<int?>("Height");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<decimal?>("Price");

                    b.Property<string>("ProductCode");

                    b.Property<string>("ProductName");

                    b.Property<string>("SuspectCode");

                    b.Property<bool>("Updated");

                    b.Property<int?>("Width");

                    b.HasKey("Id");

                    b.ToTable("TemporaryProduct");
                });

            modelBuilder.Entity("tibs.stem.TemporaryProducts.TemporaryProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("ImageUrl");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int>("TemporaryProductId");

                    b.HasKey("Id");

                    b.HasIndex("TemporaryProductId");

                    b.ToTable("TemporaryProductImages");
                });

            modelBuilder.Entity("tibs.stem.TitleOfCourtes.TitleOfCourtesy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("TitleOfCourtesy");
                });

            modelBuilder.Entity("tibs.stem.Ybafcos.Ybafco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("YbafcoCode");

                    b.Property<string>("YbafcoName");

                    b.HasKey("Id");

                    b.ToTable("Ybafco");
                });

            modelBuilder.Entity("tibs.stem.Editions.SubscribableEdition", b =>
                {
                    b.HasBaseType("Abp.Application.Editions.Edition");

                    b.Property<decimal?>("AnnualPrice");

                    b.Property<int?>("ExpiringEditionId");

                    b.Property<decimal?>("MonthlyPrice");

                    b.Property<int?>("TrialDayCount");

                    b.Property<int?>("WaitingDayAfterExpire");

                    b.ToTable("AbpEditions");

                    b.HasDiscriminator().HasValue("SubscribableEdition");
                });

            modelBuilder.Entity("Abp.Application.Features.EditionFeatureSetting", b =>
                {
                    b.HasBaseType("Abp.Application.Features.FeatureSetting");

                    b.Property<int>("EditionId");

                    b.HasIndex("EditionId", "Name");

                    b.ToTable("AbpFeatures");

                    b.HasDiscriminator().HasValue("EditionFeatureSetting");
                });

            modelBuilder.Entity("Abp.MultiTenancy.TenantFeatureSetting", b =>
                {
                    b.HasBaseType("Abp.Application.Features.FeatureSetting");

                    b.Property<int>("TenantId");

                    b.HasIndex("TenantId", "Name");

                    b.ToTable("AbpFeatures");

                    b.HasDiscriminator().HasValue("TenantFeatureSetting");
                });

            modelBuilder.Entity("Abp.Authorization.Roles.RolePermissionSetting", b =>
                {
                    b.HasBaseType("Abp.Authorization.PermissionSetting");

                    b.Property<int>("RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AbpPermissions");

                    b.HasDiscriminator().HasValue("RolePermissionSetting");
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserPermissionSetting", b =>
                {
                    b.HasBaseType("Abp.Authorization.PermissionSetting");

                    b.Property<long>("UserId");

                    b.HasIndex("UserId");

                    b.ToTable("AbpPermissions");

                    b.HasDiscriminator().HasValue("UserPermissionSetting");
                });

            modelBuilder.Entity("Abp.Authorization.Roles.RoleClaim", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Roles.Role")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserClaim", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserLogin", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserRole", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserToken", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Abp.Configuration.Setting", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User")
                        .WithMany("Settings")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Abp.Organizations.OrganizationUnit", b =>
                {
                    b.HasOne("Abp.Organizations.OrganizationUnit", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("tibs.stem.AbpSalesCoOrinators.AbpSalesCoOrinator", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User", "Coordinator")
                        .WithMany()
                        .HasForeignKey("CoordinatorId");

                    b.HasOne("tibs.stem.Authorization.Users.User", "AbpUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("tibs.stem.AcitivityTracks.AcitivityTrack", b =>
                {
                    b.HasOne("tibs.stem.Activities.Activity", "Activity")
                        .WithMany()
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("tibs.stem.NewCustomerCompanys.NewContact", "NewContacts")
                        .WithMany()
                        .HasForeignKey("ContactId");

                    b.HasOne("tibs.stem.Inquirys.Inquiry", "Enquiry")
                        .WithMany()
                        .HasForeignKey("EnquiryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.ActivityTrackComments.ActivityTrackComment", b =>
                {
                    b.HasOne("tibs.stem.AcitivityTracks.AcitivityTrack", "ActivityTrack")
                        .WithMany()
                        .HasForeignKey("ActivityTrackId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.AttributeGroupDetails.AttributeGroupDetail", b =>
                {
                    b.HasOne("tibs.stem.ProductAttributeGroups.ProductAttributeGroup", "AttributeGroups")
                        .WithMany()
                        .HasForeignKey("AttributeGroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("tibs.stem.ProductAttributes.ProductAttribute", "Attributes")
                        .WithMany()
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.Authorization.Roles.Role", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User", "CreatorUser")
                        .WithMany()
                        .HasForeignKey("CreatorUserId");

                    b.HasOne("tibs.stem.Authorization.Users.User", "DeleterUser")
                        .WithMany()
                        .HasForeignKey("DeleterUserId");

                    b.HasOne("tibs.stem.Authorization.Users.User", "LastModifierUser")
                        .WithMany()
                        .HasForeignKey("LastModifierUserId");
                });

            modelBuilder.Entity("tibs.stem.Authorization.Users.User", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User", "CreatorUser")
                        .WithMany()
                        .HasForeignKey("CreatorUserId");

                    b.HasOne("tibs.stem.Authorization.Users.User", "DeleterUser")
                        .WithMany()
                        .HasForeignKey("DeleterUserId");

                    b.HasOne("tibs.stem.Departments.Department", "Departments")
                        .WithMany()
                        .HasForeignKey("DepartmentId");

                    b.HasOne("tibs.stem.Authorization.Users.User", "LastModifierUser")
                        .WithMany()
                        .HasForeignKey("LastModifierUserId");
                });

            modelBuilder.Entity("tibs.stem.Citys.City", b =>
                {
                    b.HasOne("tibs.stem.Countrys.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.Companies.Company", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User", "AbpAccountManager")
                        .WithMany()
                        .HasForeignKey("AccountManagerId");

                    b.HasOne("tibs.stem.Citys.City", "Cities")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("tibs.stem.CustomerTypes.CustomerType", "CustomerTypes")
                        .WithMany()
                        .HasForeignKey("CustomerTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.CompanyContacts.CompanyContact", b =>
                {
                    b.HasOne("tibs.stem.Companies.Company", "Companies")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("tibs.stem.Designations.Designation", "Desiginations")
                        .WithMany()
                        .HasForeignKey("DesiginationId");

                    b.HasOne("tibs.stem.TitleOfCourtes.TitleOfCourtesy", "TitleOfCourtesies")
                        .WithMany()
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.EnquiryContacts.EnquiryContact", b =>
                {
                    b.HasOne("tibs.stem.NewCustomerCompanys.NewContact", "Contacts")
                        .WithMany()
                        .HasForeignKey("ContactId");

                    b.HasOne("tibs.stem.Inquirys.Inquiry", "Inquiry")
                        .WithMany()
                        .HasForeignKey("InquiryId");
                });

            modelBuilder.Entity("tibs.stem.EnquiryDetails.EnquiryDetail", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User", "AbpAccountManager")
                        .WithMany()
                        .HasForeignKey("AssignedbyId");

                    b.HasOne("tibs.stem.NewCustomerCompanys.NewCompany", "Companys")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("tibs.stem.NewCustomerCompanys.NewCompany", "Compatitor")
                        .WithMany()
                        .HasForeignKey("CompatitorsId");

                    b.HasOne("tibs.stem.NewCustomerCompanys.NewContact", "Contacts")
                        .WithMany()
                        .HasForeignKey("ContactId");

                    b.HasOne("tibs.stem.Departments.Department", "Departments")
                        .WithMany()
                        .HasForeignKey("DepartmentId");

                    b.HasOne("tibs.stem.Designations.Designation", "Designations")
                        .WithMany()
                        .HasForeignKey("DesignationId");

                    b.HasOne("tibs.stem.Inquirys.Inquiry", "Inquirys")
                        .WithMany()
                        .HasForeignKey("InquiryId");

                    b.HasOne("tibs.stem.LeadTypes.LeadType", "LeadTypes")
                        .WithMany()
                        .HasForeignKey("LeadTypeId");

                    b.HasOne("tibs.stem.Team.Teams", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("tibs.stem.EnquirySources.EnquirySource", b =>
                {
                    b.HasOne("tibs.stem.Inquirys.Inquiry", "Inquiry")
                        .WithMany()
                        .HasForeignKey("InquiryId");

                    b.HasOne("tibs.stem.Sources.Source", "Source")
                        .WithMany()
                        .HasForeignKey("SourceId");
                });

            modelBuilder.Entity("tibs.stem.EnquiryStatuss.EnquiryStatus", b =>
                {
                    b.HasOne("tibs.stem.Stagestates.Stagestate", "Stagestatess")
                        .WithMany()
                        .HasForeignKey("StagestateId");
                });

            modelBuilder.Entity("tibs.stem.ImportHistorys.ImportHistory", b =>
                {
                    b.HasOne("tibs.stem.Quotations.Quotation", "Quotations")
                        .WithMany()
                        .HasForeignKey("QuotationId");
                });

            modelBuilder.Entity("tibs.stem.Inquirys.Inquiry", b =>
                {
                    b.HasOne("tibs.stem.NewCustomerCompanys.NewCompany", "Companys")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("tibs.stem.Departments.Department", "Departments")
                        .WithMany()
                        .HasForeignKey("DepartmentId");

                    b.HasOne("tibs.stem.Designations.Designation", "Designations")
                        .WithMany()
                        .HasForeignKey("DesignationId");

                    b.HasOne("tibs.stem.Locations.Location", "Locations")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("tibs.stem.Milestones.MileStone", "MileStones")
                        .WithMany()
                        .HasForeignKey("MileStoneId");

                    b.HasOne("tibs.stem.OpportunitySources.OpportunitySource", "opportunitySource")
                        .WithMany()
                        .HasForeignKey("OpportunitySourceId");

                    b.HasOne("tibs.stem.EnquiryStatuss.EnquiryStatus", "EnqStatus")
                        .WithMany()
                        .HasForeignKey("StatusId");

                    b.HasOne("tibs.stem.Ybafcos.Ybafco", "whyBafco")
                        .WithMany()
                        .HasForeignKey("WhyBafcoId");
                });

            modelBuilder.Entity("tibs.stem.Inquirys.JobActivity", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User", "Designer")
                        .WithMany()
                        .HasForeignKey("DesignerId");

                    b.HasOne("tibs.stem.Inquirys.Inquiry", "Inquiry")
                        .WithMany()
                        .HasForeignKey("InquiryId");
                });

            modelBuilder.Entity("tibs.stem.LeadDetails.LeadDetail", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User", "Coordinators")
                        .WithMany()
                        .HasForeignKey("CoordinatorId");

                    b.HasOne("tibs.stem.Authorization.Users.User", "Designers")
                        .WithMany()
                        .HasForeignKey("DesignerId");

                    b.HasOne("tibs.stem.Inquirys.Inquiry", "Inquirys")
                        .WithMany()
                        .HasForeignKey("InquiryId");

                    b.HasOne("tibs.stem.LeadSources.LeadSource", "LeadSources")
                        .WithMany()
                        .HasForeignKey("LeadSourceId");

                    b.HasOne("tibs.stem.LeadTypes.LeadType", "LeadTypes")
                        .WithMany()
                        .HasForeignKey("LeadTypeId");

                    b.HasOne("tibs.stem.Authorization.Users.User", "SalesManagers")
                        .WithMany()
                        .HasForeignKey("SalesManagerId");
                });

            modelBuilder.Entity("tibs.stem.Locations.Location", b =>
                {
                    b.HasOne("tibs.stem.Citys.City", "citys")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.Milestones.MileStone", b =>
                {
                    b.HasOne("tibs.stem.SourceTypes.SourceType", "SourceTypes")
                        .WithMany()
                        .HasForeignKey("SourceTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.Milestones.StageDetails", b =>
                {
                    b.HasOne("tibs.stem.Milestones.MileStone", "MileStones")
                        .WithMany()
                        .HasForeignKey("MileStoneId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("tibs.stem.EnquiryStatuss.EnquiryStatus", "EnquiryStatuss")
                        .WithMany()
                        .HasForeignKey("StageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.MultiTenancy.Payments.SubscriptionPayment", b =>
                {
                    b.HasOne("Abp.Application.Editions.Edition", "Edition")
                        .WithMany()
                        .HasForeignKey("EditionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.MultiTenancy.Tenant", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User", "CreatorUser")
                        .WithMany()
                        .HasForeignKey("CreatorUserId");

                    b.HasOne("tibs.stem.Authorization.Users.User", "DeleterUser")
                        .WithMany()
                        .HasForeignKey("DeleterUserId");

                    b.HasOne("Abp.Application.Editions.Edition", "Edition")
                        .WithMany()
                        .HasForeignKey("EditionId");

                    b.HasOne("tibs.stem.Authorization.Users.User", "LastModifierUser")
                        .WithMany()
                        .HasForeignKey("LastModifierUserId");
                });

            modelBuilder.Entity("tibs.stem.NewCustomerCompanys.NewAddressInfo", b =>
                {
                    b.HasOne("tibs.stem.Citys.City", "Citys")
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.HasOne("tibs.stem.NewCustomerCompanys.NewCompany", "NewCompanys")
                        .WithMany()
                        .HasForeignKey("NewCompanyId");

                    b.HasOne("tibs.stem.NewCustomerCompanys.NewContact", "NewContacts")
                        .WithMany()
                        .HasForeignKey("NewContacId");

                    b.HasOne("tibs.stem.NewCustomerCompanys.NewInfoType", "NewInfoTypes")
                        .WithMany()
                        .HasForeignKey("NewInfoTypeId");
                });

            modelBuilder.Entity("tibs.stem.NewCustomerCompanys.NewCompany", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User", "AbpAccountManager")
                        .WithMany()
                        .HasForeignKey("AccountManagerId");

                    b.HasOne("tibs.stem.Authorization.Users.User", "AbpApprovedBy")
                        .WithMany()
                        .HasForeignKey("ApprovedById");

                    b.HasOne("tibs.stem.NewCustomerCompanys.NewCustomerType", "NewCustomerTypes")
                        .WithMany()
                        .HasForeignKey("NewCustomerTypeId");
                });

            modelBuilder.Entity("tibs.stem.NewCustomerCompanys.NewContact", b =>
                {
                    b.HasOne("tibs.stem.Designations.Designation", "Designations")
                        .WithMany()
                        .HasForeignKey("DesignationId");

                    b.HasOne("tibs.stem.Industrys.Industry", "Industries")
                        .WithMany()
                        .HasForeignKey("IndustryId");

                    b.HasOne("tibs.stem.NewCustomerCompanys.NewCompany", "NewCompanys")
                        .WithMany()
                        .HasForeignKey("NewCompanyId");

                    b.HasOne("tibs.stem.NewCustomerCompanys.NewCustomerType", "NewCustomerTypes")
                        .WithMany()
                        .HasForeignKey("NewCustomerTypeId");

                    b.HasOne("tibs.stem.TitleOfCourtes.TitleOfCourtesy", "TitleOfCourtesies")
                        .WithMany()
                        .HasForeignKey("TitleId");
                });

            modelBuilder.Entity("tibs.stem.NewCustomerCompanys.NewContactInfo", b =>
                {
                    b.HasOne("tibs.stem.NewCustomerCompanys.NewCompany", "NewCompanys")
                        .WithMany()
                        .HasForeignKey("NewCompanyId");

                    b.HasOne("tibs.stem.NewCustomerCompanys.NewContact", "NewContacts")
                        .WithMany()
                        .HasForeignKey("NewContacId");

                    b.HasOne("tibs.stem.NewCustomerCompanys.NewInfoType", "NewInfoTypes")
                        .WithMany()
                        .HasForeignKey("NewInfoTypeId");
                });

            modelBuilder.Entity("tibs.stem.ProductFamilys.ProductFamily", b =>
                {
                    b.HasOne("tibs.stem.Collections.Collection", "Collections")
                        .WithMany()
                        .HasForeignKey("CollectionId");
                });

            modelBuilder.Entity("tibs.stem.ProductGroups.ProductGroup", b =>
                {
                    b.HasOne("tibs.stem.ProductFamilys.ProductFamily", "prodFamily")
                        .WithMany()
                        .HasForeignKey("FamilyId");

                    b.HasOne("tibs.stem.ProductCategorys.ProductCategory", "ProductCategorys")
                        .WithMany()
                        .HasForeignKey("ProductCategoryId");
                });

            modelBuilder.Entity("tibs.stem.ProductGroups.ProductGroupDetail", b =>
                {
                    b.HasOne("tibs.stem.ProductAttributeGroups.ProductAttributeGroup", "ProductAttributeGroups")
                        .WithMany()
                        .HasForeignKey("AttributeGroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("tibs.stem.ProductGroups.ProductGroup", "ProductGroups")
                        .WithMany()
                        .HasForeignKey("ProductGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.ProductImageUrls.ProductImageUrl", b =>
                {
                    b.HasOne("tibs.stem.Products.Product", "Products")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.Products.Product", b =>
                {
                    b.HasOne("tibs.stem.ProductSpecifications.ProductSpecification", "ProductSpecifications")
                        .WithMany()
                        .HasForeignKey("ProductSpecificationId");
                });

            modelBuilder.Entity("tibs.stem.Products.ProductPricelevel", b =>
                {
                    b.HasOne("tibs.stem.PriceLevels.PriceLevel", "PriceLevels")
                        .WithMany()
                        .HasForeignKey("PriceLevelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("tibs.stem.Products.Product", "Products")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.ProductSpecificationDetails.ProductSpecificationDetail", b =>
                {
                    b.HasOne("tibs.stem.ProductAttributeGroups.ProductAttributeGroup", "ProductAttributeGroups")
                        .WithMany()
                        .HasForeignKey("AttributeGroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("tibs.stem.ProductSpecifications.ProductSpecification", "ProductSpecifications")
                        .WithMany()
                        .HasForeignKey("ProductSpecificationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.ProductSpecifications.ProductSpecification", b =>
                {
                    b.HasOne("tibs.stem.ProductGroups.ProductGroup", "ProductGroups")
                        .WithMany()
                        .HasForeignKey("ProductGroupId");
                });

            modelBuilder.Entity("tibs.stem.ProductSubGroups.ProductSubGroup", b =>
                {
                    b.HasOne("tibs.stem.ProductGroups.ProductGroup", "productGroups")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.ProdutSpecLinks.ProdutSpecLink", b =>
                {
                    b.HasOne("tibs.stem.ProductAttributeGroups.ProductAttributeGroup", "AttributeGroups")
                        .WithMany()
                        .HasForeignKey("AttributeGroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("tibs.stem.ProductAttributes.ProductAttribute", "Attributes")
                        .WithMany()
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("tibs.stem.ProductGroups.ProductGroup", "ProductGroups")
                        .WithMany()
                        .HasForeignKey("ProductGroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("tibs.stem.ProductSpecifications.ProductSpecification", "ProductSpecifications")
                        .WithMany()
                        .HasForeignKey("ProductSpecificationId");
                });

            modelBuilder.Entity("tibs.stem.QuotationProducts.QuotationProduct", b =>
                {
                    b.HasOne("tibs.stem.Products.Product", "product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.HasOne("tibs.stem.Quotations.Quotation", "quotation")
                        .WithMany()
                        .HasForeignKey("QuotationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("tibs.stem.Sections.Section", "section")
                        .WithMany()
                        .HasForeignKey("SectionId");

                    b.HasOne("tibs.stem.TemporaryProducts.TemporaryProduct", "TemporaryProducts")
                        .WithMany()
                        .HasForeignKey("TemporaryProductId");
                });

            modelBuilder.Entity("tibs.stem.Quotations.Quotation", b =>
                {
                    b.HasOne("tibs.stem.NewCustomerCompanys.NewContact", "AttentionContact")
                        .WithMany()
                        .HasForeignKey("AttentionContactId");

                    b.HasOne("tibs.stem.NewCustomerCompanys.NewCompany", "Compatitors")
                        .WithMany()
                        .HasForeignKey("CompatitorId");

                    b.HasOne("tibs.stem.Inquirys.Inquiry", "Inquiry")
                        .WithMany()
                        .HasForeignKey("InquiryId");

                    b.HasOne("tibs.stem.Milestones.MileStone", "MileStones")
                        .WithMany()
                        .HasForeignKey("MileStoneId");

                    b.HasOne("tibs.stem.NewCustomerCompanys.NewCompany", "NewCompanys")
                        .WithMany()
                        .HasForeignKey("NewCompanyId");

                    b.HasOne("tibs.stem.QuotationStatuss.QuotationStatus", "Quotationstatus")
                        .WithMany()
                        .HasForeignKey("QuotationStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("tibs.stem.LeadReasons.LeadReason", "LostReason")
                        .WithMany()
                        .HasForeignKey("ReasonId");

                    b.HasOne("tibs.stem.Authorization.Users.User", "SalesPerson")
                        .WithMany()
                        .HasForeignKey("SalesPersonId");

                    b.HasOne("tibs.stem.EnquiryStatuss.EnquiryStatus", "EnqStatus")
                        .WithMany()
                        .HasForeignKey("StageId");
                });

            modelBuilder.Entity("tibs.stem.Region.RegionCity", b =>
                {
                    b.HasOne("tibs.stem.Citys.City", "Citys")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("tibs.stem.Region.Regions", "Regions")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.Sections.Section", b =>
                {
                    b.HasOne("tibs.stem.Quotations.Quotation", "quotation")
                        .WithMany()
                        .HasForeignKey("QuotationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.Sources.Source", b =>
                {
                    b.HasOne("tibs.stem.SourceTypes.SourceType", "SourceTypes")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.Team.Teams", b =>
                {
                    b.HasOne("tibs.stem.Departments.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("tibs.stem.Authorization.Users.User", "SalesManager")
                        .WithMany()
                        .HasForeignKey("SalesManagerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tibs.stem.TeamDetails.TeamDetail", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User", "Salesman")
                        .WithMany()
                        .HasForeignKey("SalesmanId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("tibs.stem.Team.Teams", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("tibs.stem.TemporaryProducts.TemporaryProductImage", b =>
                {
                    b.HasOne("tibs.stem.TemporaryProducts.TemporaryProduct", "TemporaryProducts")
                        .WithMany()
                        .HasForeignKey("TemporaryProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Abp.Application.Features.EditionFeatureSetting", b =>
                {
                    b.HasOne("Abp.Application.Editions.Edition", "Edition")
                        .WithMany()
                        .HasForeignKey("EditionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Abp.Authorization.Roles.RolePermissionSetting", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Roles.Role")
                        .WithMany("Permissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Abp.Authorization.Users.UserPermissionSetting", b =>
                {
                    b.HasOne("tibs.stem.Authorization.Users.User")
                        .WithMany("Permissions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
