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

        public static string GetRandomString(int length)
        {
            Random random = new Random();
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
