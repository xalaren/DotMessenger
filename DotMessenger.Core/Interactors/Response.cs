using System.Text;

namespace DotMessenger.Core.Interactors
{
    public class Response<T> : Response where T : class
    {
        public Response() { }

        public Response(bool error, int errorCode, string? errorMessage = null, T? value = null, string[]? detailedErrorInfo = null) :
            base(error, errorCode, errorMessage, detailedErrorInfo)
        {
            Value = value;
        }

        public T? Value { get; init; }
    }

    public class Response
    {
        public bool Error { get; init; }
        public string? ErrorMessage { get; init; }
        public int ErrorCode { get; init; }
        public string[]? DetailedErrorInfo { get; init; }

        public Response() { }

        public Response(bool error, int errorCode, string? errorMessage = null, string[]? detailedErrorInfo = null)
        {
            Error = error;
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
            DetailedErrorInfo = detailedErrorInfo;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append($"{(ErrorMessage == null ? $"Message: {ErrorMessage}\n" : "")}");
            builder.AppendLine($"Code: {ErrorCode}\n");

            if (DetailedErrorInfo != null)
            {
                builder.AppendLine("Details:");

                foreach(var info in DetailedErrorInfo)
                {
                    builder.AppendLine($"\t{info}");
                }
            }

            return builder.ToString();
        }
    }
}
