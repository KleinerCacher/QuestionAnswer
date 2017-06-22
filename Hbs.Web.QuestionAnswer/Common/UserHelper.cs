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
            string fullName = string.Empty;
            if (!string.IsNullOrEmpty(account))
            {              
                PrincipalContext context = null;
                try
                {
                    context = new PrincipalContext(ContextType.Domain);
                }
                catch (Exception)
                {
                    context = new PrincipalContext(ContextType.Machine);
                }
                finally
                {
                    if (context != null)
                    {
                        var principal = UserPrincipal.FindByIdentity(context, account);

                        if (principal != null)
                        {
                            fullName = string.Format("{0} {1}", principal.GivenName, principal.Surname);
                            if (string.IsNullOrWhiteSpace(fullName))
                            {
                                fullName = principal.Name;
                            }
                        }

                        context.Dispose();
                    }
                }
            }

            return fullName;
        }
    }
}