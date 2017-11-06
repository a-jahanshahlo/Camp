using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Optimization;
using AutoMapper;
using Camps.CommonLib.ExtentionMethods;
using Camps.CommonLib.Security;
using Camps.WebUI.ViewModels.Accounts;
using Camps.WebUI.ViewModels.AppSettings;
using Camps.WebUI.ViewModels.Camps;
using Camps.WebUI.ViewModels.Department;
using Camps.WebUI.ViewModels.DepartmentBoss;
using Camps.WebUI.ViewModels.DepartmentDeptRole;
using Camps.WebUI.ViewModels.DeptRoles;
using Camps.WebUI.ViewModels.Festival;
using Camps.WebUI.ViewModels.Passengers;
using Camps.WebUI.ViewModels.Position;
using Camps.WebUI.ViewModels.Quota;
using Camps.WebUI.ViewModels.SuiteGrade;
using Camps.WebUI.ViewModels.Suites;
using Camps.WebUI.ViewModels.User;
using Camps.WebUI.ViewModels.UserInDeptRole;
using Comps.DomainLayer;
using Comps.DomainLayer.Security;

namespace Camps.WebUI.AutoMapper
{



    public static class AutoMapperWebConfiguration
    {
        //static IProjectCategoryService _projectCategoryService;
        //public static void Configure(IProjectCategoryService service)
        //{

        //    ConfigureUserMapping();
        //    _projectCategoryService = service;
        //}


        public static void ConfigureUserMapping()
        {
            Mapper.CreateMap<AppSetting, AppSettingsViewModel>();
            Mapper.CreateMap<Province, ProvinceViewModel>();
            Mapper.CreateMap<Binary, FileViewModel>();
            Mapper.CreateMap<Binary, PhotoViewModel>();
            Mapper.CreateMap<Binary, VideoViewModel>();
            Mapper.CreateMap<Binary, AudioViewModel>();

            Mapper.CreateMap<DepartmentDeptRole, DepartmentDeptRoleShortViewModel>()
                 .ForMember(src => src.Id, des => des.MapFrom(x => x.Id))
                .ForMember(src => src.Text, des => des.MapFrom(x => x.Department.DepTitle + " " + x.DeptRole.RoleTitle));
            //-----------------------Reverse Binding----------------
            Mapper.CreateMap<DeptRoleIndexViewModel, DeptRole>();

            Mapper.CreateMap<CustomRole, RoleIndexViewModel>()
                 .ForMember(src => src.Id, des => des.MapFrom(x => x.Id))
                .ForMember(src => src.Text, des => des.MapFrom(x => x.TextFa));
            Mapper.CreateMap<CustomRole, RoleViewModel>()
                .ForMember(src => src.Id, des => des.MapFrom(x => x.Id))
                .ForMember(src => src.Role, des => des.MapFrom(x => x.Name))
                .ForMember(src => src.Text, des => des.MapFrom(x => x.TextFa));
            //-----------------------Reverse Binding----------------
            Mapper.CreateMap<DeptRoleIndexViewModel, DeptRole>();


            Mapper.CreateMap<DeptRole, DeptRoleIndexViewModel>();
            //-----------------------Reverse Binding----------------
            Mapper.CreateMap<DeptRoleIndexViewModel, DeptRole>();
            Mapper.CreateMap<DeptRoleEditViewModel, DeptRole>();
            Mapper.CreateMap<DeptRoleCreateViewModel, DeptRole>();

            Mapper.CreateMap<Festival, FestivalIndexViewModel>();
            Mapper.CreateMap<Festival, FestivalShortIndexViewModel>()
                .ForMember(src => src.Id, des => des.MapFrom(x => x.Id))
                .ForMember(src => src.Text, des => des.MapFrom(x => x.FestivalTitle));
            //-----------------------Reverse Binding----------------
            Mapper.CreateMap<FestivalIndexViewModel, Festival>();
            Mapper.CreateMap<FestivalCreateViewModel, Festival>()
                  .ForMember(src => src.FromDate, des => des.MapFrom(x => x.FromDate))
                  .ForMember(src => src.ToDate, des => des.MapFrom(x => x.ToDate))
                  ;
            Mapper.CreateMap<FestivalEditViewModel, Festival>();

            Mapper.CreateMap<DepartmentDeptRole, UserPostViewModel>()
                  .ForMember(src => src.Text, des => des.MapFrom(x => x.Department.DepTitle + " " + x.DeptRole.RoleTitle))
                  .ForMember(src => src.Id, des => des.MapFrom(x => x.Id));
            Mapper.CreateMap<UserInDeptRole, UserInDeptRoleIndexViewModel>();
            //-----------------------Reverse Binding----------------
            Mapper.CreateMap<UserInDeptRoleIndexViewModel, UserInDeptRole>();

            Mapper.CreateMap<Department, DepartmentIndexViewModel>();
            Mapper.CreateMap<Department, DepartmentShortIndexViewModel>()
                .ForMember(src => src.Id, des => des.MapFrom(x => x.Id))
                .ForMember(src => src.Text, des => des.MapFrom(x => x.DepTitle));
            //-----------------------Reverse Binding----------------
            Mapper.CreateMap<DepartmentIndexViewModel, Department>();
            Mapper.CreateMap<DepartmentCreateViewModel, Department>();

            Mapper.CreateMap<DepartmentEditViewModel, Department>();

            Mapper.CreateMap<Period, PeriodShortIndexViewModel>()
                .ForMember(src => src.Id, des => des.MapFrom(x => x.Id))
                .ForMember(src => src.Text, des => des.MapFrom(x => x.PeriodTitle));
            Mapper.CreateMap<Period, PeriodIndexViewModel>()
                .ForMember(src => src.FestivalIndexViewModel, des => des.MapFrom(x => x.Festival))
                .ForMember(src => src.CampsIndexViewModel, des => des.MapFrom(x => x.Camp));
            //-----------------------Reverse Binding----------------
            Mapper.CreateMap<PeriodCreateViewModel, Period>();
            Mapper.CreateMap<PeriodEditViewModel, Period>();



            Mapper.CreateMap<Gender, GenderIndexViewModel>();

            Mapper.CreateMap<ApplicationUser, RegisterMobileViewModel>();
            Mapper.CreateMap<ApplicationUser, UserFindViewModel>()
                .ForMember(src => src.Email, des => des.MapFrom(x => x.Email))
                .ForMember(src => src.UserName, des => des.MapFrom(x => x.UserName))
                .ForMember(src => src.FirstName, des => des.MapFrom(x => x.UserInfo.FirstName))
                .ForMember(src => src.LastName, des => des.MapFrom(x => x.UserInfo.LastName))
                .ForMember(src => src.Mobile, des => des.MapFrom(x => x.PhoneNumber))
                .ForMember(src => src.Text, des => des.MapFrom(x => x.UserInfo.FirstName + " " + x.UserInfo.LastName))
                .ForMember(src => src.Id, des => des.MapFrom(x => x.Id));
            Mapper.CreateMap<ApplicationUser, ViewModels.Accounts.UserIndexViewModel>()
            .ForMember(src => src.UserName, des => des.MapFrom(x => x.UserName))
            .ForMember(src => src.Email, des => des.MapFrom(x => x.Email))
            .ForMember(src => src.Id, des => des.MapFrom(x => x.Id))
            .ForMember(src => src.Mobile, des => des.MapFrom(x => x.PhoneNumber));
            //-----------------------Reverse Binding----------------
            Mapper.CreateMap<RegisterMobileViewModel, ApplicationUser>()
            .ForMember(src => src.UserName, des => des.MapFrom(x => x.Email));
            Mapper.CreateMap<RegisterFullViewModel, ApplicationUser>()
                .ForMember(src => src.UserName, des => des.MapFrom(x => x.UserName))
                .ForMember(src => src.Email, des => des.MapFrom(x => x.UserName + "@behzisti.net"))
                .ForMember(src => src.PhoneNumber, des => des.MapFrom(x => x.Mobile))
                .ForMember(src => src.PhoneNumberConfirmed, des => des.MapFrom(x => x.IsActive))

                .ForMember(src => src.UserInfo, des => des.MapFrom(x => new UserInfo
                {
                    Address = x.Profile.Address,
                    FirstName = x.Profile.FirstName,
                    LastName = x.Profile.LastName,
                    Nid = x.Profile.Nid,
                    Phone = x.Profile.Phone,
                    Age = x.Profile.Age,
                    GenderId = x.Profile.GenderId,
                    IsDeleted = x.Profile.IsDeleted,
                    Birthday = x.Profile.Birthday
                }));

            Mapper.CreateMap<DepartmentDeptRole, DepartmentDeptRoleIndexViewModel>()
                .ForMember(src => src.DepartmentIndexViewModel, des => des.MapFrom(x => x.Department))
                .ForMember(src => src.DeptRoleIndexViewModel, des => des.MapFrom(x => x.DeptRole));
            //-----------------------Reverse Binding----------------
            Mapper.CreateMap<DepartmentDeptRoleCreateViewModel, DepartmentDeptRole>();
            Mapper.CreateMap<DepartmentDeptRoleEditViewModel, DepartmentDeptRole>();



            Mapper.CreateMap<UserInDeptRole, PositionIndexViewModel>();
            //-----------------------Reverse Binding----------------
            Mapper.CreateMap<PositionCreateViewModel, UserInDeptRole>();
            Mapper.CreateMap<PositionEditViewModel, UserInDeptRole>();
            Mapper.CreateMap<UserInDeptRoleShortViewModel, UserInDeptRole>()
                .ForMember(src => src.FromDate, des => des.MapFrom(x => DateTime.UtcNow))
                .ForMember(src => src.ToDate, des => des.MapFrom(x => DateTime.UtcNow.AddYears(100)))
                .ForMember(src => src.UserId, des => des.MapFrom(x => x.UserId))
                .ForMember(src => src.DepartmentDeptRoleId, des => des.MapFrom(x => x.DeptId))
                .ForMember(src => src.IsActive, des => des.MapFrom(x => true));

            Mapper.CreateMap<SuiteGrade, SuiteGradeIndexViewModel>();
            //-----------------------Reverse Binding----------------
            Mapper.CreateMap<SuiteGradeIndexViewModel, SuiteGrade>();

            Mapper.CreateMap<City, CityViewModel>()
                .ForMember(src => src.ProvinceName, des => des.MapFrom(x => x.Province.Name))
                .ForMember(src => src.ProvinceId, des => des.MapFrom(x => x.Province.Id))
                .ForMember(src => src.Name, des => des.MapFrom(x => x.Name))
                .ForMember(src => src.Id, des => des.MapFrom(x => x.Id));

            Mapper.CreateMap<Gallery, GalleryShortViewModel>();
            //-----------------------Reverse Binding----------------
            Mapper.CreateMap<GalleryShortViewModel, Gallery>();

            Mapper.CreateMap<Address, AddressViewModel>();
            //-----------------------Reverse Binding----------------
            Mapper.CreateMap<AddressViewModel, Address>();

            Mapper.CreateMap<Phone, PhoneViewModel>();
            //-----------------------Reverse Binding----------------
            Mapper.CreateMap<PhoneViewModel, Phone>();

            Mapper.CreateMap<FreePassenger, PassengerIndexViewModel>();
            //-----------------------Reverse Binding----------------
            Mapper.CreateMap<PassengerIndexViewModel, FreePassenger>();

            Mapper.CreateMap<Gallery, GalleryViewModel>()
                  .ForMember(src => src.Photos, des => des.MapFrom(x => x.Files.Where(c => c.Extention.IsImage())))
                  .ForMember(src => src.Videos, des => des.MapFrom(x => x.Files.Where(c => c.Extention.IsVideo())))
                  .ForMember(src => src.Audios, des => des.MapFrom(x => x.Files.Where(c => c.Extention.IsAudio())));
            //-----------------------Reverse Binding----------------
            Mapper.CreateMap<GalleryViewModel, Gallery>();



            Mapper.CreateMap<CampFacilitie, CampFacilitieViewModel>();

            Mapper.CreateMap<Suite, SuiteCreateViewModel>();
            //-----------------------Reverse Binding----------------
            Mapper.CreateMap<SuiteCreateViewModel, Suite>();
            Mapper.CreateMap<SuiteEditViewModel, Suite>();

            Mapper.CreateMap<ApplicationUser, UserViewModel>()
                  .ForMember(src => src.UserName, dest => dest.MapFrom(x => x.UserName))
                  .ForMember(src => src.UserInfo, dest => dest.MapFrom(x => x.UserInfo));
            Mapper.CreateMap<ApplicationUser, ViewModels.User.UserIndexViewModel>()
                 .ForMember(src => src.UserName, dest => dest.MapFrom(x => x.UserName))
                 .ForMember(src => src.Address, dest => dest.MapFrom(x => x.UserInfo.Address))
                 .ForMember(src => src.Age, dest => dest.MapFrom(x => x.UserInfo.Age))
                 .ForMember(src => src.Birthday, dest => dest.MapFrom(x => x.UserInfo.Birthday))
                 .ForMember(src => src.Email, dest => dest.MapFrom(x => x.Email))
                 .ForMember(src => src.FirstName, dest => dest.MapFrom(x => x.UserInfo.FirstName))
                 .ForMember(src => src.LastName, dest => dest.MapFrom(x => x.UserInfo.LastName))
                 .ForMember(src => src.Gender, dest => dest.MapFrom(x => x.UserInfo.Gender))
                 .ForMember(src => src.Nid, dest => dest.MapFrom(x => x.UserInfo.Nid))
                 .ForMember(src => src.Phone, dest => dest.MapFrom(x => x.UserInfo.Phone))
                 .ForMember(src => src.UserName, dest => dest.MapFrom(x => x.UserInfo.User.UserName))
                 .ForMember(src => src.Id, dest => dest.MapFrom(x => x.Id))
                 ;

            Mapper.CreateMap<Quota, QuotaIndexViewModel>()
                 .ForMember(src => src.PeriodIndexViewModel, des => des.MapFrom(x => x.Period))
                 .ForMember(src => src.DepartmentIndexViewModel, des => des.MapFrom(x => x.Department))
                 .ForMember(src => src.BossUserViewModel, des => des.MapFrom(x => x.BossUser))
                 .ForMember(src => src.OperatorUserViewModel, des => des.MapFrom(x => x.OperatorUser))
                 .ForMember(src => src.PassengerUserViewModel, des => des.MapFrom(x => x.PassengerUser));
            //-----------------------Reverse Binding----------------
            Mapper.CreateMap<QuotaEditViewModel, Quota>();
            Mapper.CreateMap<QuotaCreateViewModel, Quota>()
                .ForMember(src => src.AddDate, des => des.MapFrom(x => DateTime.UtcNow));
            Mapper.CreateMap<ConfirmQuotaEditViewModel, Quota>()
                .ForMember(src => src.BossUserId, des => des.MapFrom(x => x.BossUserId))
                .ForMember(src => src.PassengerUserId, des => des.MapFrom(x => x.PassengerUserId));

            Mapper.CreateMap<ConfirmQuotaViewModel, Quota>()
                .ForMember(src => src.BossUserId, des => des.MapFrom(x => x.BossUserId))
                .ForMember(src => src.PassengerUserId, des => des.MapFrom(x => x.PassengerUserId))
                .ForMember(src => src.WhoRefuseId, des => des.MapFrom(x => x.WhoRefuseId))
                .ForMember(src => src.Id, des => des.MapFrom(x => x.Id))
                .ForMember(src => src.IsRefuse, des => des.MapFrom(x => x.IsRefuse));

            Mapper.CreateMap<SuiteOwner, SuiteOwnerIndexViewModel>();

            Mapper.CreateMap<SuiteType, SuiteTypeIndexViewModel>();


            Mapper.CreateMap<Camp, CampsExistViewModel>();

            Mapper.CreateMap<Camp, CampsViewModel>()
                .ForMember(src => src.Name, des => des.MapFrom(x => x.Name))

                .ForMember(src => src.AddressViewModel, des => des.MapFrom(x => x.Address))
                .ForMember(src => src.PhoneViewModels, des => des.MapFrom(x => x.Phones))
                .ForMember(src => src.GallaryViewModels, des => des.MapFrom(x => x.Galleries))
                .ForMember(src => src.SuitesViewModels, des => des.MapFrom(x => x.Suites))
                .ForMember(src => src.CampFacilitiesViewModels, des => des.MapFrom(x => x.CampFacilities));
            //------------------------Reverse Binding---------------
            Mapper.CreateMap<CampsViewModel, Camp>()
                .ForMember(src => src.Name, des => des.MapFrom(x => x.Name))

                .ForMember(src => src.Address, des => des.MapFrom(x => x.AddressViewModel))
                .ForMember(src => src.Phones, des => des.MapFrom(x => x.PhoneViewModels))
                .ForMember(src => src.Galleries, des => des.MapFrom(x => x.GallaryViewModels))
                .ForMember(src => src.Suites, des => des.MapFrom(x => x.SuitesViewModels))
                .ForMember(src => src.CampFacilities, des => des.MapFrom(x => x.CampFacilitiesViewModels));

            Mapper.CreateMap<Camp, CampsCreateViewModel>();

            Mapper.CreateMap<Camp, CampsIndexViewModel>();

            Mapper.CreateMap<Suite, SuiteViewModel>();
            Mapper.CreateMap<Suite, SuiteIndexViewModel>();
            //Mapper.CreateMap<ProjectModel, ProjectViewModel>();

            //Mapper.CreateMap<ProjectModel, ProjectMainViewModel>()
            //    .ForMember(src => src.ProjectCategorys, dest => dest.MapFrom(x => new SelectList(_projectCategoryService.GetAll(), "Id", "Name")))
            //    .ForMember(src => src.ProjectCategory, des => des.MapFrom(x => x.Id));

            //Mapper.CreateMap<CustomerModel, CustomerViewModel>();
            //Mapper.CreateMap<ContactModel, ContactViewModel>();

            //Mapper.CreateMap<OurInfoModel, OurInfoViewModel>();
            //Mapper.CreateMap<OurSocialModel, OurSocialViewModel>();


            //Mapper.CreateMap<BlogNews, BlogNewsViewModel>()
            //    .ForMember(src => src.CommentCount, des => des.MapFrom(x => x.Comments.Count(y => y.IsVisible)))
            //    .ForMember(src => src.Tags, des => des.MapFrom(x => x.TagsRefrences.Select(y => new Tag() { Id = y.Id, TagTitle = y.TagTitle })));

            //Mapper.CreateMap<Comment, CommensViewModel>();
            //Mapper.CreateMap<TagsRefrence, TagsViewModel>();
            //Mapper.CreateMap<Archive, ArchiveViewModel>().ForMember(src => src.PersianDate, des => des.MapFrom(x => x.DateTime.ToShamsi())).ForMember(src => src.DateTime, des => des.MapFrom(x => x.DateTime));
            //Mapper.CreateMap<Tag, TagViewModel>();
            //Mapper.CreateMap<ProjectCategory, ProjectCategoryViewModel>();
            //Mapper.CreateMap<BlogNews, BlogViewModel>()
            //    .ForMember(src => src.ContentTypes, des => des.MapFrom(x => Enum.GetNames(typeof(ContentType)).Select(e => new SelectListItem { Text = e })))
            //    .ForMember(src => src.SectionNumbers, des => des.MapFrom(x => Enum.GetNames(typeof(SectionNumber)).Select(e => new SelectListItem { Text = e })));


            ////==========================vise verca binding===============

            Mapper.CreateMap<CityViewModel, City>()
                .ForMember(src => src.Id, des => des.MapFrom(x => x.Id))
                .ForMember(src => src.Name, des => des.MapFrom(x => x.Name));
            Mapper.CreateMap<CampsCreateViewModel, Camp>();
            //Mapper.CreateMap<ContactViewModel, ContactModel>();
            //Mapper.CreateMap<BlogViewModel, BlogNews>();
            //Mapper.CreateMap<ProjectMainViewModel, ProjectModel>()
            //    .ForMember(src => src.ProjectCategory, des => des.MapFrom(x => _projectCategoryService.Find(x.ProjectCategory)));

        }


        // ... etc
    }
}