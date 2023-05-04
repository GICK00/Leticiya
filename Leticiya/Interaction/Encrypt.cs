using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Leticiya.Interaction
{
    internal class Encrypt
    {
        public static byte[] EncryptData(byte[] data, string password)
        {
            SymmetricAlgorithm sa = Aes.Create();
            Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(password, sa.IV, 1000, HashAlgorithmName.SHA256);
            sa.Key = hasher.GetBytes(32);
            MemoryStream ms = new MemoryStream();
            ms.Write(sa.IV, 0, sa.IV.Length);
            using (CryptoStream cs = new CryptoStream(ms, sa.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(data, 0, data.Length);
            }
            return ms.ToArray();
        }

        public static string EncryptText(string text, string password) => Convert.ToBase64String(EncryptData(Encoding.UTF8.GetBytes(text), password));

        public static byte[] DecryptData(byte[] data, string password)
        {
            MemoryStream ms = new MemoryStream(data);
            byte[] iv = new byte[16];
            ms.Read(iv, 0, iv.Length);
            SymmetricAlgorithm sa = Aes.Create();
            Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(password, iv, 1000, HashAlgorithmName.SHA256);
            sa.Key = hasher.GetBytes(32);
            sa.IV = iv;
            CryptoStream cs = new CryptoStream(ms, sa.CreateDecryptor(), CryptoStreamMode.Read, true);
            MemoryStream output = new MemoryStream();
            cs.CopyTo(output);
            return output.ToArray();
        }

        public static string DecryptText(string text, string password) => Encoding.UTF8.GetString(DecryptData(Convert.FromBase64String(text), password));
    }
}
