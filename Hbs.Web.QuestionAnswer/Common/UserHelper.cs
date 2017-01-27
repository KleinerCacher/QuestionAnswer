using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;

namespace Hbs.Web.QuestionAnswer.Common
{
    public static class UserHelper
    {
        public static string GetUserDisplayname(string account)
        {
            if (!string.IsNullOrEmpty(account))
            {
                using (var context = new PrincipalContext(ContextType.Machine))
                {
                    var principal = UserPrincipal.FindByIdentity(context, account);

                    if (principal != null)
                    {
                        var fullName = string.Format("{0} {1}", principal.GivenName, principal.Surname);
                        if (string.IsNullOrWhiteSpace(fullName))
                        {
                            fullName = principal.Name;
                        }

                        return fullName;
                    }
                } 
            }

            return string.Empty;
        }
    }
}