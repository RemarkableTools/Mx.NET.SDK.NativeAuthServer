using System;

namespace Mx.NET.SDK.NativeAuthServer.Exceptions
{
    public class InvalidTtlException : Exception
    {
        public InvalidTtlException(int currentTtl, int maxTtl) :
            base($"The provided TTL in the token ({currentTtl}) is larger than the maximum allowed TTL ({maxTtl})")
        { }
    }
}
