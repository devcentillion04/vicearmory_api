using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ViceArmory.Utility
{
    public class EncryptionHandler
    {
        //public static string Decrypt(string encrString)
        //{
        //    byte[] b;
        //    string decrypted;
        //    try
        //    {
        //        b = Convert.FromBase64String(encrString);
        //        decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
        //    }
        //    catch (FormatException fe)
        //    {
        //        decrypted = "";
        //    }
        //    return decrypted;
        //}

        //public static string Encrypt(string strEncrypted)
        //{
        //    byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
        //    string encrypted = Convert.ToBase64String(b);
        //    return encrypted;
        //}

        public static string Encrypt(string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Startup.encryptKey);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }
                        array = memoryStream.ToArray();
                    }
                }
            }
            var data = Convert.ToBase64String(array);
            return data.Replace('+', '-').Replace('/', '_').PadRight(4 * ((data.Length + 3) / 4), '=');
            ;
        }

        public static string Decrypt(string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = DecodeUrlBase64(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Startup.encryptKey);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        public static byte[] DecodeUrlBase64(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/').PadRight(4 * ((s.Length + 3) / 4), '=');
            return Convert.FromBase64String(s);
        }
    }
}
