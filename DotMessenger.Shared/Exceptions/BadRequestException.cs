namespace DotMessenger.Shared.Exceptions
{
    public class BadRequestException : AppException
    {
        public BadRequestException(string? message) : base(message)
        {
            Code = 400;
            Detail = "Bad request";
        }
    }
}
