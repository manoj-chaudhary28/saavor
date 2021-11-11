using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.IO;


namespace saavor.Web.Services
{
    public class UtilitieService
    {
        public static string Encrypt(string strText)
        {
            try
            {
                Exception exObj;
                string strEncryptedText;
                if (DevelopTech.security.Encrypt(strText, out strEncryptedText, out exObj) == -1)
                {
                    // ExceptionUtility.LogException(exObj, "converter class");
                   // ApplicationLogger.LogError(exObj, "clsUtilities", "Encrypt");
                }
                return strEncryptedText;
            }
            catch
            {
               // ApplicationLogger.LogError(ex, "clsUtilities", "Encrypt");
                // ExceptionUtility.LogException(ex, "converter class");
                return "";
            }
        }

        public static string Decrypt(string strText)
        {
            try
            {
                Exception exObj;
                string strDecryptedText;

                if (DevelopTech.security.Decrypt(strText, out strDecryptedText, out exObj) == -1)
                {
                    //ExceptionUtility.LogException(exObj, "converter class");
                    //ApplicationLogger.LogError(exObj, "clsUtilities", "Decrypt");
                }
                return strDecryptedText;
            }
            catch
            {
                //ApplicationLogger.LogError(ex, "clsUtilities", "Decrypt");
                //ExceptionUtility.LogException(ex, "converter class");
                return "";
            }
        }
    }
}
