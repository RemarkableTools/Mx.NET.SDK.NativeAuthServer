using Mx.NET.SDK.Core.Domain.Values;
using Mx.NET.SDK.Core.Domain;
using Mx.NET.SDK.Wallet.Wallet;
using static Mx.NET.SDK.NativeAuthServer.Entities.NativeAuthToken;

namespace Mx.NET.SDK.NativeAuthServer.Services
{
    public static class SignatureVerifier
    {
        public static bool Verify(string accessToken)
        {
            var parts = accessToken.Split('.');
            var address = DecodeValue(parts[0]);
            var token = DecodeValue(parts[1]);
            var signature = parts[2];

            var verifier = WalletVerifier.FromAddress(Address.FromBech32(address));

            var message = new SignableMessage()
            {
                Message = $"{address}{token}",
                Signature = signature
            };
            var valid = verifier.Verify(message);

            if (!valid) // check legacy validation
            {
                message = new SignableMessage()
                {
                    Message = $"{address}{token}{{}}",
                    Signature = signature
                };
                valid = verifier.Verify(message);
            }

            return valid;
        }
    }
}
