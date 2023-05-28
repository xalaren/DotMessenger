namespace DotMessenger.Shared.Exceptions
{
    public class NotAllowedException : AppException
    {
        public NotAllowedException(string? message) : base(message)
        {
            Code = 405;
            Detail = "Not allowed method";
        }
    }
}
