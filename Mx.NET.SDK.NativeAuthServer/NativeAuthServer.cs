using Mx.NET.SDK.NativeAuthServer.Entities;
using Mx.NET.SDK.NativeAuthServer.Exceptions;
using Mx.NET.SDK.NativeAuthServer.Services;
using System;
using System.Linq;

namespace Mx.NET.SDK.NativeAuthServer
{
    public class NativeAuthServer
    {
        private readonly NativeAuthServerConfig _config;
        public NativeAuthServer(NativeAuthServerConfig config)
        {
            _config = config;
        }

        public NativeAuthToken Validate(string accessToken)
        {
            var nativeToken = new NativeAuthToken(accessToken);

            if (_config.AcceptedOrigins.Any()
                && !_config.AcceptedOrigins.Contains(nativeToken.Body.Origin)
                && !_config.AcceptedOrigins.Contains($"https://{nativeToken.Body.Origin}"))
            {
                throw new InvalidOriginException();
            }

            if (string.IsNullOrEmpty(nativeToken.Body.BlockHash)
                || nativeToken.Body.BlockHash.Length != 64)
            {
                throw new InvalidBlockHashException();
            }

            if (nativeToken.Body.TTL > _config.MaxExpirySeconds)
            {
                throw new InvalidTtlException(nativeToken.Body.TTL, _config.MaxExpirySeconds);
            }

            var currentTimestamp = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
            if (nativeToken.Body.ExtraInfo.Timestamp <= 0)
            {
                throw new InvalidExtraInfoException();
            }

            if (nativeToken.Body.TTL + nativeToken.Body.ExtraInfo.Timestamp < currentTimestamp)
            {
                throw new TokenExpiredException();
            }

            if (!SignatureVerifier.Verify(accessToken))
            {
                throw new InvalidSignatureException();
            }

            return nativeToken;
        }
    }
}