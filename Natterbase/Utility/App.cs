using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;

namespace Natterbase.Utility
{
    public class App
    {
        public static string CreateSalt()
        {
            //RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            //byte[] byteArr = new byte[32];
            //rng.GetBytes(byteArr);

           string byteArr = "XCAP05H6LoKvbRRa/QkqLNMI7cOHguaRyHzyg7n5qEkGjQmtBhz4SzYh4Fqwjyi3KJHlSXKPwVu2+bXr6CtpgQ==";

            return byteArr;
        }

        public static string CreatePasswordHash(string password, string salt)
        {
            string passwrodSalt = String.Concat(password, salt);
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(passwrodSalt, "sha1");
            return hashedPwd;
        }

    }
}