using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Utility
{
    public class EncodingLib
    {
        public static long CuTime;

        /// <summary>
        /// Aes Key 
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static Dictionary<string,string> AesEncryptKey(string Module, string Exponent)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            string gid = Guid.NewGuid().ToString("N");
            string aceKey = RsaEncrypt(gid, Module, Exponent);

            dict.Add("gid", gid);
            dict.Add("acekey", aceKey);

            return dict;
        }

        /// <summary>
        /// Aes 암호화
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string AesEncrypt(string Input, string gid)
        {
            try
            {             
                RijndaelManaged aes = new RijndaelManaged();
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = Encoding.UTF8.GetBytes(gid);
                aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
                byte[] xBuff = null;
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                    {
                        byte[] xXml = Encoding.UTF8.GetBytes(Input);
                        cs.Write(xXml, 0, xXml.Length);
                    }
                    xBuff = ms.ToArray();
                }

                string encStr = Convert.ToBase64String(xBuff).Replace("&", "^26").Replace("+", "^2B").Replace("/", "^47");
                return encStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string HextoString(string hexString)
        {
            try
            {
                if (hexString.Length % 2 != 0)
                {
                    throw new Exception("Error ==> HextoString");
                }

                byte[] HexAsBytes = new byte[hexString.Length / 2];
                for (int index = 0; index < HexAsBytes.Length; index++)
                {
                    string byteValue = hexString.Substring(index * 2, 2);
                    HexAsBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                }

                return Convert.ToBase64String(HexAsBytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string RsaEncrypt(string value ,string module, string exponent)
        {
            try
            {
                string Modulus = HextoString(module);
                string Exponent = Convert.ToUInt32(exponent, 16).ToString();

                // 암호화 개체 생성
                string publicKeyText = string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>", Modulus, "AQAB");

                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(publicKeyText);

                //암호화할 문자열을 UFT8인코딩
                byte[] inbuf = (new UTF8Encoding()).GetBytes(value);
                //암호화
                byte[] encbuf = rsa.Encrypt(inbuf, false);

                //16진수로 변경
                StringBuilder hex = new StringBuilder(encbuf.Length * 2);
                foreach (byte b in encbuf)
                {
                    hex.AppendFormat("{0:x2}", b);
                }
                return hex.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// HmacSha256 암호화
        /// </summary>
        /// <returns></returns>
        public static long HmacSha256Encrypt(long sSysTime)
        {
            try
            {
                string a = "BITKRX_SUCCESS_PROJECT_SEACRET_KEY";
                byte[] data = new byte[8];
                
                long value = sSysTime / 1000;
                /* 논리 오른쪽 시프트 연산자 >>>
                 * 오른쪽으로 2 비트 시프트한다.
                 * 오른쪽으로 밀면서 비게되는 앞쪽 비트를 무조건 0 으로 채워넣는 것이다.*/
                for (int i = 8; i-- > 0; value >>= 8)
                {
                    data[i] = (byte)value;
                }

                // Initialize the keyed hash object.
                using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(a)))
                {
                    byte[] hashValue = hmac.ComputeHash(data);
                    // Reset inStream to the beginning of the file.

                    int offset = hashValue[20 - 1] & 0xF;
                    long truncatedHash = 0;
                    for (int i = 0; i < 4; ++i)
                    {
                        truncatedHash <<= 8;
                        truncatedHash |= hashValue[(offset + i)] & (long)0xFF;
                    }

                    truncatedHash &= 0x7FFFFFFF;
                    truncatedHash %= 1000000;

                    return truncatedHash;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region Login ID 암호화

        public static byte[] Skey = ASCIIEncoding.ASCII.GetBytes("14789632");

        public static string Encrypt(string p_data)
        {
            if (p_data.Equals("")) return string.Empty;

            DESCryptoServiceProvider rc2 = new DESCryptoServiceProvider();
            rc2.Key = Skey;
            rc2.IV = Skey;

            MemoryStream ms = new MemoryStream();
            CryptoStream cryStream = new CryptoStream(ms, rc2.CreateEncryptor(), CryptoStreamMode.Write);
            byte[] data = Encoding.UTF8.GetBytes(p_data.ToCharArray());
            cryStream.Write(data, 0, data.Length);
            cryStream.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray());
        }



        public static string Decrypt(string p_data)
        {
            if (p_data.Equals("")) return string.Empty;

            DESCryptoServiceProvider rc2 = new DESCryptoServiceProvider();

            rc2.Key = Skey;
            rc2.IV = Skey;

            MemoryStream ms = new MemoryStream();
            CryptoStream cryStream = new CryptoStream(ms, rc2.CreateDecryptor(), CryptoStreamMode.Write);
            byte[] data = Convert.FromBase64String(p_data);
            cryStream.Write(data, 0, data.Length);
            cryStream.FlushFinalBlock();

            return Encoding.UTF8.GetString(ms.GetBuffer());
        }

        #endregion
    }
}