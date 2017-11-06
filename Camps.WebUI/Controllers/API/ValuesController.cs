using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Camps.CommonLib.Validation;
using Camps.WebUI.Helpers;
 
using Camps.WebUI.ViewModels.Accounts;
using Camps.WebUI.ViewModels.Profile;
using Comps.DomainLayer.Security;
using Comps.ServiceLayer.Security;
 
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Camps.WebUI.Controllers.API
{
 
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
         //   ProfileValidator v=new ProfileValidator();
         //   ValidationResult validationResult = v.Validate(new ProfileCreateViewModel());
         //   IList<ValidationFailure> validationFailures = validationResult.Errors;
         //   ModelStateDictionary modelStateDictionary = ModelState;
         //// validationResult.Errors.AddToModelState(ref ModelState);
         //   ModelState.AddModelError(validationResult.Errors);
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}