using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace Comercio.Extensions
{
    public static class ModelStateExtension
    {
        public static List<string> GetErros(this ModelStateDictionary modelState)
        {
            return modelState.Where(a => a.Value.ValidationState == ModelValidationState.Invalid)
                            .Select(a => a.Key + " - " + a.Value.Errors[0].ErrorMessage)
                            .ToList();
        }
    }
}
