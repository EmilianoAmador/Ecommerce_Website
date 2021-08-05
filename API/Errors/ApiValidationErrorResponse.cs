using System.Collections.Generic;

namespace API.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse() : base(400)   // sending status code that we know will send from this which is 400
        {
        }

        public IEnumerable<string> Errors { get; set; }
    }
}