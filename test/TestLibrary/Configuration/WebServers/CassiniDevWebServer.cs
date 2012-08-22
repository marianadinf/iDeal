//using CassiniDev;
//using TestLibrary.Parliament.Transcripts.SelectCommittees.Browsers.Helpers.Utilities;

//namespace TestLibrary.Parliament.Transcripts.SelectCommittees.Configuration.WebServers
//{
//    public class CassiniDevWebServer : CassiniDevServer, IConfigureWebServer
//    {
//        public string BaseUrl { get; set; }
//        private readonly string _webProjectFolderName;

//        public CassiniDevWebServer(string webProjectFolderName)
//        {
//            _webProjectFolderName = webProjectFolderName;
//        }

//        public void Start()
//        {
//            string projectPath = FileHelpers.GetProjectPath(_webProjectFolderName);
//            StartServer(projectPath);
//            BaseUrl = RootUrl;
//            //WebClient wc = new WebClient();
//            //wc.DownloadString(Urls.HomePage);
//        }

//        public void Stop()
//        {
//            StopServer();
//        }
//    }
//}