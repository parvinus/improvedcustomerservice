using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace ImprovedCustomerService.Core.Handlers
{
    public class ResponseHandler
    {
        #region fields
        private readonly HttpRequestMessage _request;
        #endregion

        #region properties

        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public IList<string> Errors { get; set; }
        public object Payload { get; set; }

        public HttpResponseMessage CreateResponse => _request.CreateResponse(StatusCode, new ResponseModel
        {
            Errors = Errors,
            Message = Message,
            Result = Payload
        });

        #endregion

        #region constructor(s)

        public ResponseHandler(HttpRequestMessage request, HttpStatusCode statusCode = HttpStatusCode.OK, string message = "", IList<string> errors = null, object payload = null)
        {
            _request = request;
            StatusCode = statusCode;
            Message = message;
            Errors = errors;
            Payload = payload;
        }

        #endregion

        #region static methods

        public static ResponseModel GetResponse(HttpRequestMessage request, HttpStatusCode statusCode,
            string message = "", IList<string> errors = null, object payload = null)
        {
            var responseModel = new ResponseModel
            {
                Errors = errors,
                Message = statusCode == HttpStatusCode.OK ? "success" : message,
                Result = payload
            };

            return responseModel;
        }

        #endregion
    }
}
