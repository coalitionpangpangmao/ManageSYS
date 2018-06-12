using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
namespace MSYS.Security
{
    public class EncryptandDecrypt
    {
        public static void DecryptFile(string sInputFilename, string sOutputFilename, string sKey)
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider {
                Key = Encoding.ASCII.GetBytes(sKey),
                IV = Encoding.ASCII.GetBytes(sKey)
            };
            FileStream stream = new FileStream(HttpContext.Current.Server.MapPath(sInputFilename), FileMode.Open, FileAccess.Read);
            ICryptoTransform transform = provider.CreateDecryptor();
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
            StreamWriter writer = new StreamWriter(HttpContext.Current.Server.MapPath(sOutputFilename));
            writer.Write(new StreamReader(stream2).ReadToEnd());
            writer.Flush();
            writer.Close();
        }

        public static void EncryptFile(string sInputFilename, string sOutputFilename, string sKey)
        {
            FileStream stream = new FileStream(HttpContext.Current.Server.MapPath(sInputFilename), FileMode.Open, FileAccess.Read);
            FileStream stream2 = new FileStream(HttpContext.Current.Server.MapPath(sOutputFilename), FileMode.Create, FileAccess.Write);
            ICryptoTransform transform = new DESCryptoServiceProvider { 
                Key = Encoding.ASCII.GetBytes(sKey),
                IV = Encoding.ASCII.GetBytes(sKey)
            }.CreateEncryptor();
            CryptoStream stream3 = new CryptoStream(stream2, transform, CryptoStreamMode.Write);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            stream3.Write(buffer, 0, buffer.Length);
            stream3.Close();
            stream.Close();
            stream2.Close();
        }

        public static string GenerateKey()
        {
            DESCryptoServiceProvider provider = (DESCryptoServiceProvider) DES.Create();
            return Encoding.ASCII.GetString(provider.Key);
        }
    }
}



