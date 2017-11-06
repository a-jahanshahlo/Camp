using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;
using AutoMapper;
using Camps.CommonLib.Security;
using Camps.WebUI.ViewModels.Accounts;
using Comps.DomainLayer.Security;
using Comps.ServiceLayer.Security;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Camps.WebUI.Controllers.API
{
    /// <summary>
    /// سرویس ثبت نام کاربر
    /// </summary>
    //[Authorize]
    // [RoutePrefix("api/Account")]

    public class AccountApiController : ApiController
    {


        int t;
        string ss;

        private readonly IAuthenticationManager _authenticationManager;
        private readonly IApplicationSignInManager _signInManager;
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationRoleManager _applicationRoleManager;

        private readonly ITestCodeManager _testCodeManager;
        /// <summary>
        /// تزریق سرویس های مورد نیاز
        /// </summary>
        /// <param name="testCodeManager"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="authenticationManager"></param>

        public AccountApiController(
            ITestCodeManager testCodeManager,
            IApplicationUserManager userManager,
            IApplicationSignInManager signInManager,
            IAuthenticationManager authenticationManager, IApplicationRoleManager applicationRoleManager)
        {
            _testCodeManager = testCodeManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationManager = authenticationManager;
            _applicationRoleManager = applicationRoleManager;

        }
        [HttpPost]
        public async Task<IHttpActionResult> RemoveFromRole(UserInRoleCrateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var role = await _applicationRoleManager.FindByIdAsync(model.RoleId);

            var roles = await _userManager.RemoveFromRoleAsync(model.UserId, role.Name);

            return Ok();
        }
        [HttpPost]
        public async Task<IHttpActionResult> AddToRole(UserInRoleCrateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var role = await _applicationRoleManager.FindByIdAsync(model.RoleId);

            var roles = await _userManager.AddToRoleAsync(model.UserId, role.Name);

            return Ok();
        }
        [HttpGet]
        public async Task<IEnumerable<RoleIndexViewModel>> GetRoles()
        {
            var roles = await _applicationRoleManager.GetAllCustomRolesAsync();


            var models = Mapper.Map<IList<CustomRole>, IList<RoleIndexViewModel>>(roles);
            return models;
        }
        [HttpGet]
        public async Task<IEnumerable<RoleIndexViewModel>> GetRolesForUser(int userId)
        {
            var roles = await _applicationRoleManager.FindUserRolesAsync(userId);


            var models = Mapper.Map<IList<CustomRole>, IList<RoleIndexViewModel>>(roles);
            return models;
        }
        [HttpGet]
        public async Task<IEnumerable<RoleViewModel>> GetCurrentUserRoles()
        {
            var userId = int.Parse(User.Identity.GetUserId());
            var roles = await _applicationRoleManager.FindUserRolesAsync(userId);


            var models = Mapper.Map<IList<CustomRole>, IList<RoleViewModel>>(roles);
            return models;
        }
        [HttpGet]
        public async Task<IEnumerable<UserFindViewModel>> GetFindByName(string q)
        {

            List<ApplicationUser> items = await _userManager.GetAllUsersAsync(q);

            var models = Mapper.Map<IList<ApplicationUser>, IList<UserFindViewModel>>(items);
            return models;
        }
        [HttpGet]
        public async Task<IEnumerable<UserIndexViewModel>> Get(int skip, int pageSize)
        {
            List<ApplicationUser> items = await _userManager.GetUsers()
                .OrderByDescending(x => x.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();


            var models = Mapper.Map<IList<ApplicationUser>, IList<UserIndexViewModel>>(items);
            return models;
        }

        /// <summary>
        /// خروج کاربر از سیستم
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage PostLogOff()
        {

            //SMS_Sender sms=new SMS_Sender();
            //sms.Send("09307185332", "77677");

            //_smsService.username = "sinafh";
            //_smsService.password = "sx29%BR";
            //_smsService.from = "3444434";
            //_smsService.to = "09307185332";
            //_smsService.message = "77677";

            _authenticationManager.SignOut();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<TestPhoneCode>> GetTestCode()
        {
            return await _testCodeManager.Get();
        }

        //
        // POST: /Account/Register
        /// <summary>
        /// درخواست ورود کاربر به سیستم به کمک شماره موبایل با مکانیزم دو عاملی
        /// </summary>
        /// <param name="mobileNumber">شماره موبایل کاربر</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> PostLogin(string mobileNumber)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "The current user already logged in");

            }
            if (!string.IsNullOrEmpty(mobileNumber))
            {
                // var user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.MobileNumber };
                var user = await _userManager.FindByPhoneNumberAsync(mobileNumber);
                if (user == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }


                var s = _userManager.TwoFactorProviders.First(x => x.Key.Contains("PhoneCode"));
                var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user.Id, user.PhoneNumber);

                //            mihansmscenterWSDLPortTypeClient portType =
                //new mihansmscenterWSDLPortTypeClient();

                //            portType.send("sinafh", "sx29%BR", mobileNumber, "30007650007216", code, 0, false, string.Empty, out t, out ss);


                //_smsService.username = "sinafh";
                //_smsService.password = "sx29%BR";
                //_smsService.from = "سامانه صبا";
                //_smsService.to = mobileNumber;
                //_smsService.message = code;

                //  Saba.SMS.Gateway.SmsService.message message = new message();
                //   Saba.SMS.Gateway.SmsService.verifyReceiveRequest request = new verifyReceiveRequest();

                //   Saba.SMS.Gateway.SmsService.checkSendStatusRequest request2=new checkSendStatusRequest()
                //    Saba.SMS.Gateway.SmsService.getNewMessagesRequest newMessagesRequest=new getNewMessagesRequest();
                //  mihansmscenterWSDLPortType portType = new mihansmscenterWSDLPortTypeClient();
                // portType.send(_smsService);
                // _smsService.message = code;

                // var code = await _userManager.GenerateTwoFactorTokenAsync(user.Id, s.Key);
                _testCodeManager.Add(new TestPhoneCode() { Code = code, UserName = user.UserName, AddedDate = DateTime.UtcNow });
                //   await _userManager.SendSmsAsync(user.Id, "Confirm your account", "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                //   ViewBag.Link = callbackUrl;
                //   return View("DisplayEmail");
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            //  addErrors(result);

            return Request.CreateResponse(HttpStatusCode.NotFound);

        }
        [HttpPut]

        public async Task<IHttpActionResult> Put(int skip, RegisterFullViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok("Register with successfully .");
        }

        /// <summary>
        /// سرویس ثبت نام کاربر
        /// </summary>
        /// <param name="model">ساختار داده ارسال شده جهت ثبت نام در سیستم</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]

        public async Task<IHttpActionResult> PostAdminRegister(RegisterFullViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ApplicationUser user = Mapper.Map<RegisterFullViewModel, ApplicationUser>(model);

            if (!await _userManager.IsValid(user))
            {
                var resul = new IdentityResult(_userManager.Errors.Select(x => x.Value).AsEnumerable());
                return GetErrorResult(resul);
            }

            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }
            }

            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }

            catch (Exception ex)
            {
                throw;
            }


            return Ok();

        }

        /// <summary>
        /// سرویس ثبت نام کاربر
        /// </summary>
        /// <param name="model">ساختار داده ارسال شده جهت ثبت نام در سیستم</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]

        public async Task<IHttpActionResult> PostRegister(RegisterMobileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = Mapper.Map<RegisterMobileViewModel, ApplicationUser>(model);
            // var user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.MobileNumber };
            if (!await _userManager.IsValid(user))
            {
                var resul = new IdentityResult(_userManager.Errors.Select(x => x.Value).AsEnumerable());
                return GetErrorResult(resul);
            }

            var result = await _userManager.CreateAsync(user, model.Email);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            try
            {

            }
            catch (Exception ex)
            {

            }

            await EnableTfa(user);
            var s = _userManager.TwoFactorProviders.First(x => x.Key.Contains("PhoneCode"));
            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user.Id, user.PhoneNumber);

            //   await _userManager.AddToRoleAsync(user.Id, Common.Security.SabaUser);

            //            mihansmscenterWSDLPortTypeClient portType =
            //new mihansmscenterWSDLPortTypeClient();
            //            portType.send("sinafh", "sx29%BR", model.MobileNumber, "30007650007216", code, 0, false, string.Empty, out t, out ss);


            // var code = await _userManager.GenerateTwoFactorTokenAsync(user.Id, s.Key);
            _testCodeManager.Add(new TestPhoneCode() { Code = code, UserName = user.UserName, AddedDate = DateTime.UtcNow });

            //   await _userManager.SendSmsAsync(user.Id, "Confirm your account", "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
            //   ViewBag.Link = callbackUrl;
            //   return View("DisplayEmail");
            return Ok("Register with successfully .");


        }

        /// <summary>
        /// سرویس تائید کد ارسال شده با مکانیزم دو  عاملی
        /// </summary>
        /// <param name="model">ساختار داده ارسال شده به سرویس جهت تائید کد</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]

        public async Task<HttpResponseMessage> PostVerifyCode(VerifyMobileCodeViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "The current user already logged in");

            }

            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Your sended data are not acceptable");

            }

            var user = await _userManager.FindByPhoneNumberAsync(model.MobileNumber);
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);

            }

            var result = await _userManager.ChangePhoneNumberAsync(user.Id, model.MobileNumber, model.Code);
            if (result.Succeeded)
            {
                // 
                await SignInAsync(user, model.RememberBrowser);
                return Request.CreateResponse(HttpStatusCode.OK, "Wellcome ");
                //  return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }

            return Request.CreateResponse(HttpStatusCode.NotFound);

        }


        //
        // POST: /Account/ForgotPassword
        [HttpPost]
 

        public async Task<IHttpActionResult> ResetPassword(ResetPasswordByAdminViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)//|| !(await _userManager.IsEmailConfirmedAsync(user.Id)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return BadRequest("کاربری با مشخصات مورد نظر در سیستم موجود نیست");
            }

            var code = await _userManager.RemovePasswordAsync(user.Id);

            var result = await _userManager.AddPasswordAsync(user.Id, model.Password);
            // If we got this far, something failed, redisplay form
            return Ok();
        }















        //
        // POST: /Account/Login
        //[HttpPost]
        //[AllowAnonymous]

        //public async Task<IHttpActionResult> Login( string mobile)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //      //  return View(model);
        //    }
        //    //_userManager.
        //   // _signInManager.



        //    // This doen't count login failures towards lockout only two factor authentication
        //    // To enable password failures to trigger lockout, change to shouldLockout: true
        //    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //        //    return RedirectToLocal(returnUrl);
        //        case SignInStatus.LockedOut:
        //      //      return View("Lockout");
        //        case SignInStatus.RequiresVerification:
        //       //     return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
        //        case SignInStatus.Failure:
        //        default:
        //            ModelState.AddModelError("", "Invalid login attempt.");
        //       //     return View(model);
        //            break;
        //    }

        //    return null;
        //}
        //

        private async Task<bool> EnableTfa(ApplicationUser user)
        {
            await _userManager.SetTwoFactorEnabledAsync(user.Id, true);
            var thisUser = await _userManager.FindByIdAsync(user.Id);
            if (thisUser != null)
            {
                //  await SignInAsync(thisUser, isPersistent: false);
            }
            return true;
        }
        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent },
                await _userManager.GenerateUserIdentityAsync(user));
        }
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

    }
}