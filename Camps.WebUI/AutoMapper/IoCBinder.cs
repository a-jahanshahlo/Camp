using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Camps.DataLayer.Context;
using Camps.WebUI.Areas.HelpPage.Controllers;
using Camps.WebUI.Base;
using Comps.DomainLayer;
using Comps.DomainLayer.Security;
using Comps.ServiceLayer;
using Comps.ServiceLayer.AppServices;
using Comps.ServiceLayer.EFServices;
using Comps.ServiceLayer.Interfaces;
using Comps.ServiceLayer.IOServices;
using Comps.ServiceLayer.Security;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Web;
using StructureMap.Configuration.DSL;



namespace Camps.WebUI.AutoMapper
{



    /// <summary>
    /// 
    /// </summary>
    public class IoCBinder
    {
        /// <summary>
        /// 
        /// </summary>
        public static IContainer Container
        {
            get
            {
                return ObjectFactory.Container;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public static void InitStructureMap()
        {

            //ObjectFactory.Container.Configure(x => { x.AddRegistry(new Base.ValidationRegistry()); });
            ObjectFactory.Initialize(x =>
            {



                x.For<HelpController>().Use(() => new HelpController());

                x.For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Use(() => new MainContext());
                x.For<DbContext>().HybridHttpOrThreadLocalScoped().Use(() => new MainContext());

                x.For<IAppSettingService>().Use(() => new AppSettingService(new AppSetting()));
                x.For<IInOutBinaryService>().Use(() => new InOutBinaryService());


                x.For<UserInfo>().Use<UserInfo>();
                x.For<ICampService>().Use<EfCampService>();
                x.For<IProvinceService>().Use<EfProvinceService>();
                x.For<ICityService>().Use<EfCityService>();
                x.For<IAddressService>().Use<EfAddressService>();
                x.For<IPhoneService>().Use<EfPhoneService>();

                x.For<IGalleryService>().Use<EfGalleryService>();
                x.For<IFileService>().Use<EfFileService>();
                x.For<ISuiteGradeService>().Use<EfSuiteGradeService>();
                x.For<ISuiteService>().Use<EfSuiteService>();
                x.For<IPassengerService>().Use<EfPassengerService>();
                x.For<IReservationService>().Use<EfReservationService>();
                x.For<IFacilityPackageService>().Use<EfFacilityPackageService>();
                x.For<IFacilityService>().Use<EfFacilityService>();
                x.For<IFacilityUnitService>().Use<EfFacilityUnitService>();
                x.For<IItemsInFacilityPackageService>().Use<EfItemsInFacilityPackageService>();
                x.For<ISuiteFacilityPackageService>().Use<EfSuiteFacilityPackageService>();
                x.For<ISuiteOwnerService>().Use<EfSuiteOwnerService>();
                x.For<IPositionService>().Use<EfPositionService>();
                x.For<IDepartmentService>().Use<EfDepartmentService>();
                x.For<IUserInDeptRolesService>().Use<EfUserInDeptRoleService>();
                x.For<IFestivalService>().Use<EfFestivalService>();
                x.For<IDepartmentDeptRoleService>().Use<EfDepartmentDeptRoleService>();
                x.For<IDeptRoleService>().Use<EfDeptRoleService>();
                x.For<ISuiteTypeService>().Use<EfSuiteTypeService>();
                x.For<IGenderService>().Use<EfGenderService>();
                x.For<IPeriodService>().Use<EfPeriodService>();
                x.For<IQuotaService>().Use<EfQuotaService>();

                // x.For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Use<ApplicationDbContext>();
                // Remove these 2 lines if you want to use a connection string named connectionString1, defined in the web.config file.
                //              .Ctor<string>("connectionString")
                //              .Is("Data Source=(local);Initial Catalog=TestDbIdentity;Integrated Security = true");

                //x.For<ApplicationDbContext>().HybridHttpOrThreadLocalScoped()
                //   .Use(context => (ApplicationDbContext)context.GetInstance<IUnitOfWork>());
                //x.For<DbContext>().HybridHttpOrThreadLocalScoped()
                //   .Use(context => (ApplicationDbContext)context.GetInstance<IUnitOfWork>());

                x.For<IUserStore<ApplicationUser, int>>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<UserStore<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>>();

                x.For<IRoleStore<CustomRole, int>>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<RoleStore<CustomRole, int, CustomUserRole>>();

                x.For<IAuthenticationManager>()
                      .Use(() => HttpContext.Current.GetOwinContext().Authentication);

                x.For<IApplicationSignInManager>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<ApplicationSignInManager>();

                x.For<IApplicationRoleManager>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<ApplicationRoleManager>();

                // map same interface to different concrete classes
                x.For<IIdentityMessageService>().Use<SmsService>();
                x.For<IIdentityMessageService>().Use<EmailService>();

                x.For<IApplicationUserManager>().HybridHttpOrThreadLocalScoped()
                   .Use<ApplicationUserManager>()
                   .Ctor<IIdentityMessageService>("smsService").Is<SmsService>()
                   .Ctor<IIdentityMessageService>("emailService").Is<EmailService>()
                   .Setter<IIdentityMessageService>(userManager => userManager.SmsService).Is<SmsService>()
                   .Setter<IIdentityMessageService>(userManager => userManager.EmailService).Is<EmailService>();

                x.For<ApplicationUserManager>().HybridHttpOrThreadLocalScoped()
                   .Use(context => (ApplicationUserManager)context.GetInstance<IApplicationUserManager>());

                x.For<ICustomRoleStore>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<CustomRoleStore>();

                x.For<ICustomUserStore>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<CustomUserStore>();


                x.For<ITestCodeManager>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<TestCodeService>();




                /*************** This is bug in Structuremap 3.X for HelpPage***************/
                x.For<HelpController>().Use(ctx => new HelpController());

            });
            ////Set current Controller factory as StructureMapControllerFactory
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
        }
    }
}