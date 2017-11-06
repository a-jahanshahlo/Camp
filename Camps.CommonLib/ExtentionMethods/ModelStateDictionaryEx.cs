using System.Collections.Generic;
using System.Web.Http.ModelBinding;


namespace Camps.CommonLib.ExtentionMethods
{
    public static   class  ModelStateDictionaryEx
    {

      public static void AddError(this ModelStateDictionary model, IDictionary<string, string> data)
      {
          foreach (var item in data)
          {
              model.AddModelError(item.Key,item.Value);
          }
      }
    }
}
