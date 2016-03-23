namespace TwingHotLink.Tools
{
    internal class GlobleSetting
    {
        private static ParamManager reader;

        private static string serverMode = "Off";
        private static string dataMode = "Async";
        private static string syncMode = "On";

        public GlobleSetting()
        {
            reader = new ParamManager("setting.xml");
            PingTimeout = int.Parse(reader.GetNodeText("PingTimeout"));
            ProcessLimit = int.Parse(reader.GetNodeText("ProcessLimit"));
            RequestTimeout = int.Parse(reader.GetNodeText("RequestTimeout"));
            BufferSize = int.Parse(reader.GetNodeText("BufferSize"));
            TcpPort = int.Parse(reader.GetNodeText("TcpPort"));
            UdpPort = int.Parse(reader.GetNodeText("UdpPort"));
            ClientPort = int.Parse(reader.GetNodeText("ClientPort"));
            Adapter = reader.GetNodeText("Adapter");
            PlayerID = reader.GetNodeText("PlayerID");
            serverMode = reader.GetNodeText("ServerMode");
            dataMode = reader.GetNodeText("DataMode");
            LastAddress = reader.GetNodeText("LastAddress");
            syncMode = reader.GetNodeText("SyncMode");
            SendTimeout = int.Parse(reader.GetNodeText("SendTimeout"));
        }

        public static int PingTimeout { get; set; } = 3000;

        public static int ProcessLimit { get; private set; }

        public static int RequestTimeout { get; set; } = 50;

        public static int BufferSize { get; private set; } = 2046;

        public static int TcpPort { get; set; } = 9120;

        public static int UdpPort { get; set; } = 9121;

        public static int ClientPort { get; set; } = 9122;

        public static string Adapter { get; set; }

        public static string PlayerID { get; set; } = "无名氏";

        public static bool ServerMode
        {
            get { return serverMode.Equals("On"); }
        }

        public static bool IsAsync
        {
            get { return dataMode.Equals("Async"); }
            set { dataMode = value ? "Async" : "Sync"; }
        }

        public static string LastAddress { get; set; } = "";

        public static bool SyncMode
        {
            get { return syncMode.Equals("On"); }
            set { syncMode = value ? "On" : "Off"; }
        }

        public static int SendTimeout { get; private set; } = 300;

        public static void SaveSetting()
        {
            reader.SetNodeText("PingTimeout", PingTimeout.ToString());
            reader.SetNodeText("RequestTimeout", RequestTimeout.ToString());
            reader.SetNodeText("TcpPort", TcpPort.ToString());
            reader.SetNodeText("UdpPort", UdpPort.ToString());
            reader.SetNodeText("ClientPort", ClientPort.ToString());
            reader.SetNodeText("Adapter", Adapter);
            reader.SetNodeText("PlayerID", PlayerID);
            reader.SetNodeText("DataMode", dataMode);
            reader.SetNodeText("SyncMode", syncMode);
            reader.Save("setting.xml");
        }

        public static void SaveAddress()
        {
            reader.SetNodeText("LastAddress", LastAddress);
            reader.Save("setting.xml");
        }
    }
}