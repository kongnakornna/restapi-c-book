using Jtech.Common.MiddleWare;
using MassTransit;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.Helpers
{
    public static class Extension
    {
        #region String Extension

        public static string ToUnderscoreCase(this string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }

        public static byte[] AESEncrypt(this string str, byte[] keys)
        {
            byte[] encrypted;
            using (var aes = Aes.Create())
            {
                aes.Key = keys;

                aes.GenerateIV(); // The get method of the 'IV' property of the 'SymmetricAlgorithm' automatically generates an IV if it is has not been generate before. 

                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    msEncrypt.Write(aes.IV, 0, aes.IV.Length);
                    ICryptoTransform encoder = aes.CreateEncryptor();
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encoder, CryptoStreamMode.Write))
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(str);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            return encrypted;
        }

        public static string DecryptAES(this string str, byte[] cipherText, byte[] key)
        {
            string decrypted;
            using (var aes = Aes.Create())
            {
                // Setting a key size disposes the previously-set key. 
                // Setting a key size will generate a new key. 
                // Setting a key size is redundant if a key going to be set after this statement. 
                // aes.KeySize = 256; 

                aes.Key = key;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (MemoryStream msDecryptor = new MemoryStream(cipherText))
                {
                    byte[] readIV = new byte[16];
                    msDecryptor.Read(readIV, 0, 16);
                    aes.IV = readIV;
                    ICryptoTransform decoder = aes.CreateDecryptor();
                    using (CryptoStream csDecryptor = new CryptoStream(msDecryptor, decoder, CryptoStreamMode.Read))
                    using (StreamReader srReader = new StreamReader(csDecryptor))
                    {
                        decrypted = srReader.ReadToEnd();
                    }
                }
            }
            return decrypted;
        }

        public static string ToMD5(this string source)
        {
            return source.ToMD5(Encoding.ASCII.GetBytes(source));
        }

        public static string ToMD5(this string t, byte[] source)
        {
            byte[] tmpHash = MD5.Create().ComputeHash(source);
            return tmpHash.ByteArrayToString();
        }

        public static string ToHMAC(this string text, string key)
        {
            key = key ?? "";
            using (var hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                var hash = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                return Convert.ToBase64String(hash);
            }
        }

        #endregion

        #region DateTime Extension

        public static U Pipe<T, U>(this T input, Func<T, U> func)
        {
            return func(input);
        }

        public static T ChangeType<T>(this object value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static DateTime GetGMTNow(this DateTime t)
        {
            try
            {
                return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Asia/Bangkok");
            }
            catch
            {
                return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "SE Asia Standard Time");
            }
        }

        public static int GetUnixTimeSpamp(this DateTime t)
        {
            try
            {
                return (int)TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Asia/Bangkok")
                        .Subtract(new DateTime(1970, 1, 1))
                        .TotalSeconds;
            }
            catch
            {
                return (int)TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "SE Asia Standard Time")
                        .Subtract(new DateTime(1970, 1, 1))
                        .TotalSeconds;
            }
        }

        public static string ToDurationDisplay(this DateTime t, int seconds)
        {
            int hh = seconds / 3600;
            int diff = seconds - hh * 3600;
            int mm = diff / 60;
            int ss = diff - mm * 60;
            return string.Format("{0}:{1}:{2}",
                    hh.ToString().PadLeft(2, '0'),
                    mm.ToString().PadLeft(2, '0'),
                    ss.ToString().PadLeft(2, '0')
                    );
        }

        #endregion

        #region ByteArray Extension

        public static string ByteArrayToString(this byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length - 1; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }

        #endregion

        public static IHeaderDictionary AddPayload(this IHeaderDictionary header, Object payloadInfo, string Key)
        {
            header.Add("payload",
                   Helpers.Json.Serialize(new
                   {
                       Data = payloadInfo,
                       Sign = Helpers.Json.Serialize(payloadInfo).ToHMAC(Key)
                   }));
            return header;
        }
    }
}
