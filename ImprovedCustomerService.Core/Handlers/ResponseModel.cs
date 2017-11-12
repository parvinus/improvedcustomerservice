using System.Collections.Generic;

namespace ImprovedCustomerService.Core.Handlers
{
    public class ResponseModel
    {
        public IList<string> Errors { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }
    }
}
