namespace DotMessenger.Shared.Exceptions
{
    public class AppException : Exception
    {
        public int Code { get; protected set; }
        public string Detail { get; protected set; }

        public AppException(string? message) : base(message)
        {
            //...
        }
    }
}
