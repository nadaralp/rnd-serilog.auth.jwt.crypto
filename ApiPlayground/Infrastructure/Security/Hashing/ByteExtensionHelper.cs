using System;
using System.Linq;

namespace ApiPlayground.Infrastructure.Security.Hashing
{
    public static class ByteExtensionHelper
    {
        public static string AsBase64String(this byte[] buffer)
        {
            return Convert.ToBase64String(buffer);
        }

        public static string AsHexString(this byte[] buffer)
        {
            return BitConverter.ToString(buffer).Replace("-", "");
        }

        public static byte[] StringToByteArray(this string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
