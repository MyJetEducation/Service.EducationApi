using Service.Verification.Api.Controllers;

namespace Service.EducationApi.Controllers.Models
{
    public class Response<T> 
    {
        public T Result { get; set; }
        
        public ErrorCodes ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public static Response<T> Ok(T data)
        {
            return new Response<T>()
            {
                Result = data,
                ErrorCode = ErrorCodes.Ok
            };
        }
        
        public static Response<T> Error(ErrorCodes code, string message)
        {
            return new Response<T>()
            {
                Result = default,
                ErrorCode = code,
                ErrorMessage = message
            };
        }
    }
}