namespace Mx.NET.SDK.NativeAuthServer.Exceptions
{
    public class TokenExpiredException : Exception
    {
        public TokenExpiredException() : base("Token expired") { }
    }
}
