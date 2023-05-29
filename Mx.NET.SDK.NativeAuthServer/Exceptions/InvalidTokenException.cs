namespace Mx.NET.SDK.NativeAuthServer.Exceptions
{
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException() : base("The provided token is not a NativeAuth token") { }
    }
}
