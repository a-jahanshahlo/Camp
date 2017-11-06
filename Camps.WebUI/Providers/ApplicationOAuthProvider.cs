using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Comps.ServiceLayer.Security;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Comps.DomainLayer.Security;

namespace Camps.WebUI.Providers
{

    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        //private readonly IAuthenticationManager _authenticationManager;
        //private readonly IApplicationSignInManager _signInManager;
        private readonly IApplicationUserManager _userManager;




        //,IApplicationSignInManager signInManager,
        //IAuthenticationManager authenticationManager




        //_signInManager = signInManager;
        //_authenticationManager = authenticationManager;

        public ApplicationOAuthProvider(string publicClientId, IApplicationUserManager userManager)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }
            _userManager = userManager;
            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            var data = await context.Request.ReadFormAsync();
            string code = string.Empty;
            string mobilenumber = string.Empty;

            foreach (KeyValuePair<string, string[]> item in data)
            {
                if (item.Key.Equals("mobileNumber", StringComparison.InvariantCultureIgnoreCase))
                {
                    mobilenumber = item.Value[0];
                }
                if (item.Key.Equals("code", StringComparison.InvariantCultureIgnoreCase))
                {
                    code = item.Value[0];
                }

            }
            ApplicationUser user = null;
            if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(mobilenumber))
            {
                user = await _userManager.FindByPhoneNumberAsync(mobilenumber);
                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
                var result = await _userManager.ChangePhoneNumberAsync(user.Id, mobilenumber, code);
                if (!result.Succeeded)
                {
                    context.SetError("invalid_grant", "The phone number or confirmation code is not valid");
                    return;
                }


            }
            else
            {
                user = await _userManager.FindAsync(context.UserName, context.Password);
            }


            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            ClaimsIdentity oAuthIdentity =
                await _userManager.GenerateUserIdentityAsync(user, OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookiesIdentity = await _userManager.GenerateUserIdentityAsync(user,
                CookieAuthenticationDefaults.AuthenticationType);

            AuthenticationProperties properties = CreateProperties(user.UserName);
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            var data = context.Request.ReadFormAsync();

            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}