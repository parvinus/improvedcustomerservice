using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web.Http.ModelBinding;
using FluentValidation.Results;

namespace ImprovedCustomerService.Core.Utility
{
    public static class TypeConversion
    {
        public static List<string> ValidationFailureToString(IList<ValidationFailure> targetList)
        {
            /* return the converted list */
            return (from error in targetList where error != null select error.ErrorMessage).ToList();
        }

        //public static IList<string> ModelStateErrorsToStringList(ModelStateDictionary targetModelState)
        //{
        //    var stringList = new List<string>();
        //    foreach (var entry in targetModelState)
        //    {
        //        stringList.AddRange(entry.Value.Errors.To);
        //    }

        //    return targetModelState.Errors.Select(error => error.ErrorMessage).ToList();
        //}
     }
}
