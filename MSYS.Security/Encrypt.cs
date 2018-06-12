
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace MSYS.Security
{
    public class Encrypt
    {
        private static byte[] func_AA1YNilqR20iU4oER62ESkJKEw26(string text1)
        {
            string[] strArray = text1.Split(new char[] { ':' });
            byte[] buffer = new byte[strArray.Length + 2];
            for (int i = 0; i < buffer.Length; i++)
            {
                int index = i;
                if (index > 5)
                {
                    index = i - 5;
                }
                buffer[i] = (byte) Convert.ToInt32("0x" + strArray[index], 0x10);
            }
            return buffer;
        }

        private static string func_ABIi(string text3, string text1, string text2)
        {
            SecCheck exp = new SecCheck(Convert.FromBase64String(text1));
            SecCheck n = new SecCheck(Convert.FromBase64String(text2));
            int length = text3.Length;
            int num2 = 0;
            int num3 = 0;
            if ((length % 0x100) == 0)
            {
                num2 = length / 0x100;
            }
            else
            {
                num2 = (length / 0x100) + 1;
            }
            string str2 = "";
            for (int i = 0; i < num2; i++)
            {
                num3 = length;
                if (length >= 0x100)
                {
                    num3 = 0x100;
                }
                SecCheck check4 = new SecCheck(text3.Substring(i * 0x100, num3), 0x10).modPow(exp, n);
                str2 = str2 + Encoding.Default.GetString(check4.getBytes());
                length -= num3;
            }
            return str2;
        }

        public static string DataUnLock(string sourceStr1, string sourceStr2, string sourceStr3)
        {
           return func_ABIi(sourceStr1, sourceStr2, sourceStr3);
        }

        public static string GetMD5String(string sourceStr)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.Default.GetBytes(sourceStr);
            byte[] buffer2 = provider.ComputeHash(bytes);
            string str = "";
            for (int i = 0; i < buffer2.Length; i++)
            {
                str = str + buffer2[i].ToString("x").PadLeft(2, '0');
            }
            return str;
        }

        public static string InfoLock(string sourceStr1, string sourceStr2)
        {
            byte[] rgbKey = new byte[0];
            byte[] rgbIV = func_AA1YNilqR20iU4oER62ESkJKEw26(sourceStr2);
            try
            {
                rgbKey = Encoding.UTF8.GetBytes(sourceStr2.Substring(0, 8));
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                byte[] bytes = Encoding.UTF8.GetBytes(sourceStr1);
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                stream2.Write(bytes, 0, bytes.Length);
                stream2.FlushFinalBlock();
                return Convert.ToBase64String(stream.ToArray());
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public static string InfoUnLock(string sourceStr1, string sourceStr2)
        {
            byte[] rgbKey = new byte[0];
            byte[] rgbIV = func_AA1YNilqR20iU4oER62ESkJKEw26(sourceStr2);
            byte[] buffer = new byte[sourceStr1.Length];
            try
            {
                rgbKey = Encoding.UTF8.GetBytes(sourceStr2.Substring(0, 8));
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                buffer = Convert.FromBase64String(sourceStr1);
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();
                return Encoding.UTF8.GetString(stream.ToArray());
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public static string LockData(string info)
        {
            byte[] buffer = StringToStream(info);
            long longLength = buffer.LongLength;
            byte[] stream = new byte[longLength];
            for (long i = 0L; i < longLength; i += 1L)
            {
                stream[(int) ((IntPtr) i)] = buffer[(int) ((IntPtr) ((longLength - i) - 1L))];
            }
            return StreamToString(stream);
        }

        public static string StreamToString(byte[] stream)
        {
            return
            Encoding.UTF8.GetString(stream);
        }

        public static byte[] StringToStream(string str)
        {
            return
            Encoding.UTF8.GetBytes(str);
        }

        public static string UnLockData(string info)
        {
            byte[] buffer = StringToStream(info);
            long longLength = buffer.LongLength;
            byte[] stream = new byte[longLength];
            for (long i = 0L; i < longLength; i += 1L)
            {
                stream[(int) ((IntPtr) i)] = buffer[(int) ((IntPtr) ((longLength - i) - 1L))];
            }
            return StreamToString(stream);
        }
    }
}
