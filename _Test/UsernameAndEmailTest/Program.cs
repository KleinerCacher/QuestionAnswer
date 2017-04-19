using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace UsernameAndEmailTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var loginname = WindowsIdentity.GetCurrent().Name;
                Console.WriteLine("Loginname of current user is " + loginname);

                Console.WriteLine("Trying to get name of current user.");
                var username = GetUserDisplayname(loginname);
                Console.WriteLine("Success..Username is " + username);

                Console.WriteLine("Trying to get mail of current user from AD");
                var mailadress = GetMailAdress(loginname);
                Console.WriteLine("Success..Mailaddress is " + mailadress);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failure..Error is");
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("Press any key");
            Console.ReadLine();
        }

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

        private static string GetMailAdress(string loginame)
        {
            if (loginame.Contains(@"\"))
            {
                loginame = loginame.Substring(loginame.IndexOf(@"\", StringComparison.Ordinal) + 1);
            }

            PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, loginame);

            if (user == null)
            {
                throw new Exception("Account not found");
            }

            if (string.IsNullOrEmpty(user.EmailAddress))
            {
                throw new ArgumentException("users mailadress is empty");
            }

            return user.EmailAddress;
        }
    }
}
