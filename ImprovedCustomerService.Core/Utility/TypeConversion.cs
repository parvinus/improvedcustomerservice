using System.Collections.Generic;
using System.Linq;
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
    }
}
