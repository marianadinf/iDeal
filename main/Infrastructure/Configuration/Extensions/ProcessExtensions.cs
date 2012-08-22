using System;
using System.Diagnostics;
using UIT.iDeal.Infrastructure.Configuration.Flavours;

namespace UIT.iDeal.Infrastructure.Configuration.Extensions
{
    public static class ProcessExtensions
    {
        const string SqLiteFileName = "SqLiteFileName";
        
        public static void SetProjectFlavour(this ProcessStartInfo startInfo, ProjectFlavour projectFlavour)
        {
            startInfo.EnvironmentVariables.Add(typeof(ProjectFlavour).Name, projectFlavour.ToString());
        }

        public static void SetSqLiteFileName(this ProcessStartInfo startInfo, string fileName)
        {
            startInfo.EnvironmentVariables.Add(SqLiteFileName, fileName);
        }

        public static ProjectFlavour? GetProjectFlavour(this Process process)
        {
            var projectFlavourValue = process.StartInfo.EnvironmentVariables[typeof(ProjectFlavour).Name];

            ProjectFlavour possibleProjectFlavour;

            var enumParsed = Enum.TryParse(projectFlavourValue, out possibleProjectFlavour);

            return enumParsed ? (ProjectFlavour?)possibleProjectFlavour : null;
        }

        public static string GetSqLiteFileName(this Process process)
        {
            var sqLiteFileName = process.StartInfo.EnvironmentVariables[SqLiteFileName];

            return sqLiteFileName;
        }
    }
}
