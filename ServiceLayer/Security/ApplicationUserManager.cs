using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Camps.CommonLib.Security;
using Camps.Contract;
using Comps.DomainLayer.Security;
 
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;


namespace Comps.ServiceLayer.Security
{
    public class ApplicationUserManager
        : UserManager<ApplicationUser, int>, IApplicationUserManager
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IApplicationRoleManager _roleManager;
        private readonly IUserStore<ApplicationUser, int> _store;
        public IDictionary<string,string> Errors { get; set; }
        public ApplicationUserManager(IUserStore<ApplicationUser, int> store,
            IApplicationRoleManager roleManager,
            IDataProtectionProvider dataProtectionProvider,
            IIdentityMessageService smsService,
            IIdentityMessageService emailService)
            : base(store)
        {
            _store = store;
            _roleManager = roleManager;
            _dataProtectionProvider = dataProtectionProvider;
            this.SmsService = smsService;
            this.EmailService = emailService;
            Errors = new Dictionary<string, string>();
            CreateApplicationUserManager();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUser applicationUser)
        {

            var userIdentity = await GenerateUserIdentityAsync(applicationUser, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }

        public async Task<bool> HasPassword(int userId)
        {
            var user = await FindByIdAsync(userId);
            return user != null && user.PasswordHash != null;
        }

        public async Task<bool> HasPhoneNumber(int userId)
        {
            var user = await FindByIdAsync(userId);
            return user != null && user.PhoneNumber != null;
        }

        public Task<ApplicationUser> FindByPhoneNumberAsync(string phoneNumber)
        {
            return this.Users.Where(x => x.PhoneNumber.Equals(phoneNumber)).FirstOrDefaultAsync();
        }

        public Func<CookieValidateIdentityContext, Task> OnValidateIdentity()
        {
            return SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser, int>(TimeSpan.FromMinutes(30), generateUserIdentityAsync, id => id.GetUserId<int>());
        }

        public void SeedDatabase()
        {
            //const string nameAdmin = "admin@example.com";
            //const string passwordAdmin = "Admin@123456";
      

            //const string nameUser = "User@example.com";
            //const string passwordUser = "User@123456";
       


            //const string nameCharity = "Charity@example.com";
            //const string passwordCharity = "Charity@123456";
     

            ////Create Role Admin if it does not exist
            //var roleAdmin = _roleManager.FindRoleByName(AccountTypeEnum.Admin.ToString());
            //roleAdmin.TextFa = "مدیر سیستم";
            //var roleUser = _roleManager.FindRoleByName(AccountTypeEnum.Agent.ToString());
            //roleUser.TextFa = "کاربر";
            //var roleCharity = _roleManager.FindRoleByName(AccountTypeEnum.Passenger.ToString());
            //roleCharity.TextFa = "مسافر";
            //roleAdmin = new CustomRole() { TextFa = AccountTypeEnum.Admin.GetAttributeDescription(), Name = AccountTypeEnum.Admin.ToString() };
            //roleUser = new CustomRole() { TextFa = AccountTypeEnum.Agent.GetAttributeDescription(), Name = AccountTypeEnum.Agent.ToString() };
            //roleCharity = new CustomRole() { TextFa = AccountTypeEnum.Passenger.GetAttributeDescription(), Name = AccountTypeEnum.Passenger.ToString() };
           
            //if (string.IsNullOrEmpty(roleAdmin.Name ))
            //{
            //    var roleresult0 = _roleManager.CreateRole(roleAdmin);
            //    var roleresult1 = _roleManager.CreateRole(roleUser);
            //    var roleresult2 = _roleManager.CreateRole(roleCharity);
            //}

            //var userAdmin = this.FindByName(nameAdmin);
            //var user = this.FindByName(nameUser);
            //var userCharity = this.FindByName(nameCharity);

            //if (userAdmin == null)
            //{
            //    userAdmin = new ApplicationUser { UserName = nameAdmin, Email = nameAdmin };
            //    var result = this.Create(userAdmin, passwordAdmin);
            //    result = this.SetLockoutEnabled(userAdmin.Id, false);

            //    user = new ApplicationUser { UserName = nameUser, Email = nameUser };
            //    var result1 = this.Create(user, passwordUser);
            //    result1 = this.SetLockoutEnabled(user.Id, false);

            //    userCharity = new ApplicationUser { UserName = nameCharity, Email = nameCharity };
            //    var result2 = this.Create(userCharity, passwordCharity);
            //    result2 = this.SetLockoutEnabled(userCharity.Id, false);
            //}

            //// Add user admin to Role Admin if not already added
            //var rolesForUser = this.GetRoles(userAdmin.Id);
            //if (!rolesForUser.Contains(roleAdmin.Name))
            //{
            //    var resultAdminRole = this.AddToRole(userAdmin.Id, roleAdmin.Name);
            //    var resultUserRole = this.AddToRole(user.Id, roleUser.Name);
            //    var resultCharityRole = this.AddToRole(userCharity.Id, roleCharity.Name);


            //}
        }

        private void CreateApplicationUserManager()
        {
            // Configure validation logic for usernames
            this.UserValidator = new UserValidator<ApplicationUser, int>(this)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            this.UserLockoutEnabledByDefault = true;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            this.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            this.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<ApplicationUser, int>
            {
                MessageFormat = "Your security code is: {0}"
            });
            this.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<ApplicationUser, int>
            {
                Subject = "SecurityCode",
                BodyFormat = "Your security code is {0}"
            });

            if (_dataProtectionProvider != null)
            {
                var dataProtector = _dataProtectionProvider.Create("ASP.NET Identity");
                this.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser, int>(dataProtector);
            }
        }

        private async Task<ClaimsIdentity> generateUserIdentityAsync(ApplicationUserManager manager, ApplicationUser applicationUser)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(applicationUser, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return this.Users.ToListAsync();
        }

        public Task<List<ApplicationUser>> GetAllUsersAsync(int skip, int pageSize)
        {
            return this
                          .Users
                          .OrderByDescending(x => x.Id)
                          .Skip(skip)
                          .Take(pageSize)
                          .ToListAsync();
        }

        public Task<List<ApplicationUser>> GetAllUsersAsync(string name)
        {
            return this
                        .Users
                        .Include(x=>x.UserInfo)
                        .Where(x=>x.UserInfo.LastName.Contains(name)||x.UserInfo.FirstName.Contains(name)||x.PhoneNumber.Contains(name))
                        .OrderByDescending(x => x.Id)
                        .ToListAsync();
        }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUser applicationUser, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await CreateIdentityAsync(applicationUser, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }


        public IQueryable<ApplicationUser> GetUsers()
        {
            return this.Users.AsQueryable();
        }

 

        public async Task<bool> IsValid(ApplicationUser user)
        {
            if (user == null)
            {
                this.Errors.Add("Entity", "The is null");
                return false;

            }
            if (await this.FindByPhoneNumberAsync(user.PhoneNumber) != null)
            {
                this.Errors.Add(new KeyValuePair<string, string>("UserEntity", "این شماره همراه قبلا ثبت شده است"));

                return false;
            }
            if (await this.FindByEmailAsync(user.Email) != null)
            {
                this.Errors.Add(new KeyValuePair<string, string>("Email", "این ایمیل  قبلا ثبت شده است"));
                return false;

            }
            if (await this.FindByNameAsync(user.UserName) != null)
            {
                this.Errors.Add(new KeyValuePair<string, string>("UserName", "این نام کاربری قبلا ثبت شده است "));
                return false;

            }
            //if (HttpContext.Current.User.Identity.IsAuthenticated)
            //{
            //    this.Errors.Add(new KeyValuePair<string, string>("logged_in", "The current user already logged in"));
            //    return false;

            //}
            return true;
        }

       
    }
}