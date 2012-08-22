using System.Linq;
using System;
using System.IO;
using System.Xml.Linq;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Common.Helpers;
using UIT.iDeal.Common.Interfaces;

namespace UIT.iDeal.Infrastructure.Security.Helpers
{
    public class FileBasedTestSecurityHelper : HttpContextSecurityHelper
    {
        static string TestUserNameFilePath = Path.Combine(FileHelpers.TempFolderPath, "TestUserName.xml");
        static string TestUserRolesFilePath = Path.Combine(FileHelpers.TempFolderPath, "TestUserRoles.xml");

        public FileBasedTestSecurityHelper()
        {
            SetUsername("Joe Bloggs");
        }

        public override Func<string> UserName
        {
            get
            {
                return () =>
                    {
                        if (!File.Exists(TestUserNameFilePath))
                        {
                            var errorMessage = string.Format(
                                "No test Username file has been created at '{0}'.",
                                TestUserNameFilePath);

                            throw new FileNotFoundException(errorMessage);
                        }

                        var xml = XElement.Load(TestUserNameFilePath);
                        string username = xml.Value;

                        return username;
                    };
            }
        }

        public override Func<System.Collections.Generic.IEnumerable<object>> UserRoles
        {
            get
            {
                return
                    () =>
                        {
                            if (!File.Exists(TestUserRolesFilePath))
                            {
                                var errorMessage = string.Format(
                                    "No test UserRoles file has been created at '{0}'.",
                                    TestUserRolesFilePath);

                                throw new FileNotFoundException(errorMessage);
                            }

                            var xml = XElement.Load(TestUserRolesFilePath);
                            var roles = xml.Elements("Role").Select(x => x.Value.ToEnum<UserRole>());

                            return roles.Cast<object>();
                        };
            }
        }

        public static void SetUsername(string username)
        {
            var xml = new XElement("Username", username);
            xml.Save(TestUserNameFilePath);
        }

        public static void SetUserRoles(params UserRole[] roles)
        {
            var xml = new XElement("Roles",
                                   roles.Select(role => new XElement("Role", role)));

            xml.Save(TestUserRolesFilePath);
        }


    }
}
