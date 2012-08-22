using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using UIT.iDeal.Common.Helpers;
using UIT.iDeal.Infrastructure.Configuration.Extensions;
using UIT.iDeal.Infrastructure.Configuration.Flavours;
using UIT.iDeal.TestLibrary.ProcessLifecycleManagement;

namespace UIT.iDeal.TestLibrary.Configuration.WebServers
{
    public class IISExpressWebServer : IConfigureWebServer
    {
        private readonly string _webProjectFolderName;
        public const int PortNumber = 30001;

        readonly ProjectFlavour _projectFlavour;

        private Process _iisProcess;
        public string BaseUrl { get { return string.Format("http://localhost:{0}", PortNumber); } }

        public IISExpressWebServer(string webProjectFolderName, ProjectFlavour projectFlavour)
        {
            _webProjectFolderName = webProjectFolderName;
            _projectFlavour = projectFlavour;
        }

        public void Start()
        {
            StartServer();
        }

        public void Stop()
        {
            StopServer();
        }

        private void StartServer()
        {
            var path = FileHelpers.GetCsharpProjectFolderPath(_webProjectFolderName);

            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Normal,
                ErrorDialog = false,
                CreateNoWindow = false,
                UseShellExecute = false,
                Arguments = string.Format("/path:\"{0}\" /port:{1} /systray:false", path, PortNumber)
            };

            startInfo.SetProjectFlavour(_projectFlavour);
            
            string iisExpress = GetPortableIISExpress();
            
            if (string.IsNullOrEmpty(iisExpress))
            {
                var programfiles = !string.IsNullOrEmpty(startInfo.EnvironmentVariables["programfiles(x86)"])
                                       ? startInfo.EnvironmentVariables["programfiles(x86)"]
                                       : startInfo.EnvironmentVariables["programfiles"];

                iisExpress = programfiles + "\\IIS Express\\iisexpress.exe";
            }

            if (!File.Exists(iisExpress))
            {
                throw new FileNotFoundException(string.Format("Did not find iisexpress.exe at {0}. Ensure that IIS Express is installed to the default location.", iisExpress));
            }

            startInfo.FileName = iisExpress;

            _iisProcess = new Process { StartInfo = startInfo };
            _iisProcess.Start();

            MortalChildProcessList.AddProcess(_iisProcess.Id);
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        private string GetPortableIISExpress()
        {
            string directory = FileHelpers.GetProjectFolderPath("thirdparty");
            string iisExpress = string.Format("{0}\\BinDeployableIISExpress\\iisexpress.exe", directory);
            if (File.Exists(iisExpress))
            {
                return iisExpress;
            }

            return string.Empty;
        }

        private void StopServer()
        {
            if (_iisProcess != null && !_iisProcess.HasExited)
            {
                _iisProcess.CloseMainWindow();
                _iisProcess.Dispose();
            }
        }

    }
}
