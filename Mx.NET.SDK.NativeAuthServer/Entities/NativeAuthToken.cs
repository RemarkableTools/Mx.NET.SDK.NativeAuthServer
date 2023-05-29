using Mx.NET.SDK.NativeAuthServer.Data;
using Mx.NET.SDK.NativeAuthServer.Exceptions;
using Newtonsoft.Json;
using System.Text;

namespace Mx.NET.SDK.NativeAuthServer.Entities
{
    public class NativeAuthToken
    {
        public string Address { get; set; }
        public Body Body { get; set; }
        public string Signature { get; set; }

        public NativeAuthToken(string accessToken)
        {
            var tokenComponents = accessToken.Split('.');
            if (tokenComponents.Length != 3)
                throw new InvalidTokenException();

            Address = DecodeValue(tokenComponents[0]);
            Signature = tokenComponents[2];

            var bodyComponents = DecodeValue(tokenComponents[1]).Split('.');
            if (bodyComponents.Length != 4)
                throw new InvalidTokenException();

            ExtraInfo extraInfo;
            try
            {
                extraInfo = JsonConvert.DeserializeObject<ExtraInfo>(DecodeValue(bodyComponents[3])) ?? throw new InvalidTokenException();
            }
            catch
            {
                throw new InvalidTokenException();
            }

            Body = new Body(DecodeValue(bodyComponents[0]),
                            bodyComponents[1],
                            int.Parse(bodyComponents[2]),
                            extraInfo);
        }

        public static string DecodeValue(string value)
        {
            return Unescape(Encoding.UTF8.GetString(Convert.FromBase64String(Pad(value))));
        }

        private static string Unescape(string str)
            => str.Replace('-', '+').Replace('+', '/');

        private static string Pad(string str)
        {
            if (str.Length % 4 == 0) return str;
            else if (str.Length % 4 == 2) return str += "==";
            else if (str.Length % 4 == 3) return str += "=";
            return str;
        }
    }
}
