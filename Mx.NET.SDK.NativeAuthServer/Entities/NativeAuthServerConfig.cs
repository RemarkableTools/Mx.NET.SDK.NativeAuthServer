namespace Mx.NET.SDK.NativeAuthServer.Entities
{
    public class NativeAuthServerConfig
    {
        public string[] AcceptedOrigins { get; set; } = Array.Empty<string>();
        public int MaxExpirySeconds { get; set; } = 60 * 60 * 24;
    }
}
