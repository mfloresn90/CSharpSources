﻿#region Disclaimer/License Info

/* *********************************************** */

// sBlog.Net

// sBlog.Net is a minimalistic blog engine software.

// Homepage: http://sblogproject.net
// Github: http://github.com/karthik25/sBlog.Net

// This project is licensed under the BSD license.  
// See the License.txt file for more information.

/* *********************************************** */

#endregion

namespace sBlog.Net.Infrastructure
{
    public static class PasswordHelper
    {
        public static string GenerateHashedPassword(string userPassword, string randomCode)
        {
            var hasher = Hasher.Instance;
            var hashedPassword = hasher.HashString(string.Format("{0}{1}", userPassword, randomCode));
            return hashedPassword;
        }
    }
}