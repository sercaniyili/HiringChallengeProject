namespace HiringChallange.Domain.Common.Response
{
    public class BaseResponse
    {
        public BaseResponse(string message, bool ısSuccess)
        {
            Message = message;
            IsSuccess = ısSuccess;
        }
        public String Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
