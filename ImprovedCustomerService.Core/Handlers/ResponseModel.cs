using System.Collections.Generic;

namespace ImprovedCustomerService.Core.Handlers
{
    public class ResponseModel
    {
        public IList<string> Errors { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }

        /// <summary>
        ///     empty constructor for property initialization
        /// </summary>
        public ResponseModel()
        {

        }

        /// <summary>
        ///     constructor that injects a concrete implementation of IList into Errors property
        /// </summary>
        /// <param name="errors">Implementation of IList containing errors as strings (if any)</param>
        public ResponseModel(IList<string> errors)
        {
            Errors = errors;
        }
    }
}
