using System;

namespace Mx.NET.SDK.NativeAuthServer.Exceptions
{
    public class InvalidOriginException : Exception
    {
        public InvalidOriginException():base("Origin not accepted") { }
    }
}
