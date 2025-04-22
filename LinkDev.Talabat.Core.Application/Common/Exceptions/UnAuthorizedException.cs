namespace LinkDev.Talabat.Core.Application.Common.Exceptions
{
    public class UnAuthorizedException : ApplicationException
    {
        public UnAuthorizedException(string message) : base(message) { }
    }
}
