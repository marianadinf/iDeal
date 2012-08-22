using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace UIT.iDeal.Common.Helpers
{
    public static class FileHelpers
    {
        static FileHelpers()
        {
            SetReleaseConfigurationPathFragment();
            SetDebugConfigurationPathFragment();
        }

        public static string CurrentConfigurationPathFragment;

        const string CsharpProjectsFolderPathFragment = @"main";
        const string CsharpTestFolderPathFragment = @"test";



        public static string TempFolderPath
        {
            get
            {
                return Path.Combine(SolutionFolderPath, "Temp");
            }
        }

		[Conditional("DEBUG")]
		static void SetDebugConfigurationPathFragment()
		{
			CurrentConfigurationPathFragment = "debug";
		}

		static void SetReleaseConfigurationPathFragment()
		{
			CurrentConfigurationPathFragment = "release";
		}
		
        static string SolutionFolderPath
        {
            get
            {
                DirectoryInfo directory = GetAssemblyDirectory();

                while (directory.GetFiles("*.sln").Length == 0)
                {
                    directory = directory.Parent;
                }
                return directory.FullName;
            }
        }

        public static DirectoryInfo GetAssemblyDirectory()
        {
            var assemblyPath = (new Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;

            return new DirectoryInfo(Path.GetDirectoryName(assemblyPath));
        }

        static string csharpProjectsFolderPath
        {
            get
            {
                return Path.Combine(SolutionFolderPath, CsharpProjectsFolderPathFragment);
            }
        }

        static string testProjectFolderPath
        {
            get { return Path.Combine(SolutionFolderPath, CsharpTestFolderPathFragment); }
        }

        public static void DeletePreviousSQliteDbFiles()
        {
            string[] files = Directory.GetFiles(".", "*.Test.db*");
            foreach (string file in files)
            {
                File.Delete(file);
            }
        }

        public static string GetCsharpProjectFolderPath(string projectFolderName)
        {
            return FindSubFolderPath(csharpProjectsFolderPath, projectFolderName);
        }
        
        public static string FullTestProjectFolderPath(this string testProjectFolderName)
        {
            return FindSubFolderPath(testProjectFolderPath, testProjectFolderName);
        }

        public static string GetProjectFolderPath(string projectFolderName)
        {
            return FindSubFolderPath(SolutionFolderPath, projectFolderName);
        }

        static string FindSubFolderPath(string rootFolderPath, string folderName)
        {
            var directory = new DirectoryInfo(rootFolderPath);

            directory = (directory.GetDirectories("*", SearchOption.AllDirectories)
                .Where(folder => folder.Name.ToLower() == folderName.ToLower()))
                .FirstOrDefault();

            if (directory == null)
            {
                throw new DirectoryNotFoundException();
            }

            return directory.FullName;
        }
    }
}