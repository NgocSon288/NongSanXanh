using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NongSan.Utilities.Constants
{
    public static class SystemConstants
    {
        public static string NongSanConnectionString = "NongSanConnectionString";
        public static string PathUploadFile = "Uploads";

        public static class Callback
        {
            public static string CallbackUrl = "CallbackUrl";
            public static string CreateAccountVerify = "CreateAccountVerify";
        }

        public static class AppSettings
        {
            public static string TokensIssuer = "Tokens:Issuer";
            public static string TokensKey = "Tokens:Key";

            public static string MailSettings = "MailSettings";
        }
    }
}
