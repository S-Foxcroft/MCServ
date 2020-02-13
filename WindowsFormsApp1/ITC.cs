using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Reflection;

namespace uk.co.ytfox.MCWrap
{
    class ITC
    {
        public static List<String> msgToServer, msgToUi;
        public static string ServerStatus = "stopped";
        public static readonly string Path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static bool PrintToWindow = true;
        private static string IP;
        public static string IPAddr{
            get { return IP; }
        }
        public static void Setup()
        {
            msgToServer = new List<string>();
            msgToUi = new List<string>();
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                IP = endPoint.Address.ToString();
                socket.Close();
            }
        }
    }
}
