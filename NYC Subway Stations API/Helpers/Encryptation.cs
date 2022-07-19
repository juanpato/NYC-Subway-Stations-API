using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace NYC_Subway_Stations_API.Helpers
{
    public abstract class Encryptation
    {
        public static string PasswordHash(string inputString)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            byte[] data = Encoding.GetEncoding(1252).GetBytes(inputString);
            data = new SHA256Managed().ComputeHash(data);
            return Encoding.GetEncoding(1252).GetString(data);
        }
    }
}
