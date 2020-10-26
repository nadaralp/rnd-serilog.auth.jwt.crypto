using System;

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
    }
}
