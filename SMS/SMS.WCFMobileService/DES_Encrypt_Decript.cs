using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace INF
{
   public sealed class Encrypt_Decrypt
    {

        static byte[] bytes = ASCIIEncoding.ASCII.GetBytes("ZeroCool");
        /// <summary>
        /// Encrypt a string.
        /// </summary>
        /// <param name="originalString">The original string.</param>
        /// <returns>The encrypted string.</returns>
        /// <exception cref="ArgumentNullException">This exception will be 
        /// thrown when the original string is null or empty.</exception>
        public static string Encrypt(string originalString)
        {
            MemoryStream memoryStream = null;
            CryptoStream cryptoStream = null;
            StreamWriter writer = null; 
            try
            {
                if (String.IsNullOrEmpty(originalString))
                {
                    throw new ArgumentNullException
                           ("The string which needs to be encrypted can not be null.");
                }
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                memoryStream = new MemoryStream();
                cryptoStream = new CryptoStream(memoryStream,
                    cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
                writer = new StreamWriter(cryptoStream);
                writer.Write(originalString);
                writer.Flush();
                cryptoStream.FlushFinalBlock();
                writer.Flush();
                string retStr=Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
                writer.Close();
                cryptoStream.Close();
                memoryStream.Close();
                
                
                return retStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
                if (cryptoStream != null)
                    cryptoStream.Close();
                if(memoryStream!=null)
                    memoryStream.Close();
                      
            }
        }

        /// <summary>
        /// Decrypt a crypted string.
        /// </summary>
        /// <param name="cryptedString">The crypted string.</param>
        /// <returns>The decrypted string.</returns>
        /// <exception cref="ArgumentNullException">This exception will be thrown 
        /// when the crypted string is null or empty.</exception>
        public static string Decrypt(string cryptedString)
        {
            MemoryStream memoryStream = null;
            CryptoStream cryptoStream = null;
            StreamReader reader = null; 
            try
            {
                if (String.IsNullOrEmpty(cryptedString))
                {
                    throw new ArgumentNullException
                       ("The string which needs to be decrypted can not be null.");
                }
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                memoryStream = new MemoryStream
                        (Convert.FromBase64String(cryptedString));
                cryptoStream = new CryptoStream(memoryStream,
                    cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
                reader = new StreamReader(cryptoStream);
                string retStr=reader.ReadToEnd();;
                reader.Close();
                cryptoStream.Close();
                memoryStream.Close();
                return retStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (cryptoStream != null)
                    cryptoStream.Close();
                if (memoryStream != null)
                    memoryStream.Close();
            }
        }

    }

    
}
