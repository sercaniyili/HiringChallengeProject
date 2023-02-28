namespace HiringChallange.Domain.Common.Response
{
    public class ServiceResponse<T> : BaseResponse
    {
        public T Value { get; set; }

        public ServiceResponse(T value, string message, bool ısSuccess) : base(message, ısSuccess)
        {
            Value = value;
        }
    }
}
