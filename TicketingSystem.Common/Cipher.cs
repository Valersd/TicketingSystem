using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace TicketingSystem.Common
{
    public static class Cipher
    {
        private const string Salt = "T8JKMV3C";

        public static string Encrypt(string text)
        {
            byte[] key = { };
            byte[] IV = { 0x32, 0x41, 0x54, 0x67, 0x73, 0x21, 0x47, 0x19 };
            MemoryStream ms = null;

            try
            {
                key = Encoding.UTF8.GetBytes(Salt);
                byte[] bytes = Encoding.UTF8.GetBytes(text);
                DESCryptoServiceProvider dcp = new DESCryptoServiceProvider();
                ICryptoTransform ict = dcp.CreateEncryptor(key, IV);
                ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, ict, CryptoStreamMode.Write);
                cs.Write(bytes, 0, bytes.Length);
                cs.FlushFinalBlock();
            }
            catch (Exception)
            {
                return text;
            }
            return Convert.ToBase64String(ms.ToArray()).Replace('+','_');
        }

        public static string Decrypt(string text)
        {
            byte[] key = { };
            byte[] IV = { 0x32, 0x41, 0x54, 0x67, 0x73, 0x21, 0x47, 0x19 };
            MemoryStream ms = null;

            try
            {
                key = Encoding.UTF8.GetBytes(Salt);
                byte[] bytes = new byte[text.Length];
                bytes = Convert.FromBase64String(text.Replace('_', '+'));
                DESCryptoServiceProvider dcp = new DESCryptoServiceProvider();
                ICryptoTransform ict = dcp.CreateDecryptor(key, IV);
                ms = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(ms, ict, CryptoStreamMode.Write);
                cryptoStream.Write(bytes, 0, bytes.Length);
                cryptoStream.FlushFinalBlock();
            }
            catch (Exception)
            {
                return text;
            }
            Encoding en = Encoding.UTF8;
            return en.GetString(ms.ToArray());
        }

        //public static string Encrypt(string clearText)
        //{
        //    byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        //    using (Aes encryptor = Aes.Create())
        //    {
        //        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(Salt, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        //        encryptor.Key = pdb.GetBytes(32);
        //        encryptor.IV = pdb.GetBytes(16);
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(clearBytes, 0, clearBytes.Length);
        //                cs.Close();
        //            }
        //            clearText = Convert.ToBase64String(ms.ToArray());
        //        }
        //    }
        //    string result = clearText.Replace('+', '_');
        //    return result;
        //}

        //public static string Decrypt(string cipherText)
        //{
        //    cipherText = cipherText.Replace("_", "+");
        //    byte[] cipherBytes = Convert.FromBase64String(cipherText);
        //    using (Aes encryptor = Aes.Create())
        //    {
        //        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(Salt, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        //        encryptor.Key = pdb.GetBytes(32);
        //        encryptor.IV = pdb.GetBytes(16);
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(cipherBytes, 0, cipherBytes.Length);
        //                cs.Close();
        //            }
        //            cipherText = Encoding.Unicode.GetString(ms.ToArray());
        //        }
        //    }
        //    return cipherText;
        //}
    }
}
