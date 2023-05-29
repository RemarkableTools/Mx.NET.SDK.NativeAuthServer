namespace Mx.NET.SDK.NativeAuthServer.Data
{
    public class Body
    {
        public string Origin { get; set; }
        public string BlockHash { get; set; }
        public int TTL { get; set; }
        public ExtraInfo ExtraInfo { get; set; }

        public Body(string origin, 
                    string blockHash, 
                    int ttl, 
                    ExtraInfo extraInfo)
        {
            Origin = origin;
            BlockHash = blockHash;
            TTL = ttl;
            ExtraInfo = extraInfo;
        }
    }
}
