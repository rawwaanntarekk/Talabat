namespace LinkDev.Talabat.Core.Application.Common.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string message)
            : base(message)
        {
        }
    }
}
