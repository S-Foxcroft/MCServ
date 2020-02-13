using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace uk.co.ytfox.MCWrap
{
    class Server
    {
        private static StreamReader srServ;
        private static StreamWriter swServ;
        private static Thread serverThread = null;
        private static Process pServer;
        public static void Run()
        {
            serverThread = new Thread(Task);
            serverThread.Start();
        }
        private static void Task()
        {
            ITC.msgToUi.Add("Server Status on launch attempt: " + ITC.ServerStatus);
            if (ITC.ServerStatus != "stopped") return;
            if (Preferences.AsBool("PortMap.enabled")) PortMap.Run();
            ITC.ServerStatus = "starting";
            WriteProperties();
            pServer = new Process();
            ProcessStartInfo psi = new ProcessStartInfo("java")
            {
                Arguments = String.Format("-server -Xms{0} -Xmx{1} -jar {2} nogui",
                                          Preferences.AsString("Server.ram_minimum", "1G"),
                                          Preferences.AsString("Server.ram_minimum", "256M"),
                                          ITC.Path + "\\Server\\" + Preferences.AsString("Server.jarfile", "server.jar")),
                //Arguments = "-version",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                WorkingDirectory = ITC.Path + "\\Server",
                CreateNoWindow = true
            };
            pServer.StartInfo = psi;
            pServer.Start();
            swServ = pServer.StandardInput;
            srServ = pServer.StandardOutput;
            while (!pServer.HasExited)
            {
                if (!srServ.EndOfStream) ITC.msgToUi.Add(srServ.ReadLine().Substring(11));
            }
            ITC.ServerStatus = "stopping";
            Backup.Run("ServerClosed");
            ITC.ServerStatus = "stopped";
        }
        private static bool Locked = false;
        public static void DoTick() { DoTick(0); }
        public static void DoTick(int tickCount)
        {
            if (!Locked)
            {
                Locked = true;
                if (ITC.msgToServer.Count > 0)
                {
                    if (swServ != null) swServ.WriteLine(ITC.msgToServer[0]);
                    ITC.msgToServer.RemoveAt(0);
                }
                Locked = false;
            }
        }
        private static List<string> ServerProperties;
        private static void WriteProperties()
        {
            //provide defaults
            if(!File.Exists(ITC.Path+"\\Server\\server.properties"))
                using (Stream target = File.OpenWrite(ITC.Path + "\\Server\\server.properties"))
                    using (StreamWriter sw = new StreamWriter(target))
                       sw.Write(Properties.Resources.server);

            //overwrite supported settings
            ITC.msgToUi.Add("Injecting options...");
            List<string> lines = new List<string>();
            ServerProperties = new List<string>();
            StringBuilder sb = new StringBuilder();
            string d;
            using (StreamReader sr = new StreamReader(File.OpenRead(ITC.Path+"\\Server\\server.properties"))) while (!sr.EndOfStream) lines.Add(sr.ReadLine());
            foreach (string line in lines)
            {
                bool switched = false;
                if (!switched) switched = Sub(line, "allow-nether", Preferences.AsBool("Server.nether_portals"));
                if (!switched) switched = Sub(line, "server-port", Preferences.AsInt("Server.port", 25565));
                if (!switched) switched = Sub(line, "hardcore", Preferences.AsString("Server.difficulty", "easy") == "hardcore");
                if (!switched) switched = Sub(line, "pvp", Preferences.AsBool("Server.pvp"));
                d = Preferences.AsString("Server.difficulty", "normal");
                if (!switched) switched = Sub(line, "difficulty", d == "peaceful" ? 0 : (d == "easy" ? 1 : (d == "normal" ? 2 : (d == "hard" ? 3 : (d == "hardcore" ? 3 : 2)))));
                if (!switched) switched = Sub(line, "enable-command-block", Preferences.AsBool("Server.command_blocks"));
                if (!switched) switched = Sub(line, "max-players", Preferences.AsInt("Server.players", 20));
                if (!switched) switched = Sub(line, "motd", Preferences.AsString("Server.mp_message").Replace("&&", "\\u00A6").Replace("&", "\\u00A7").Replace("\\u00A6", "&"));
                d = Preferences.AsString("World.seed", "~");
                if (!switched) switched = Sub(line, "level-seed", d == "~" ? "" : d);
                if (!switched) switched = Sub(line, "level-type", Preferences.AsString("World.type", "default"));
                if (!switched) ServerProperties.Add(line);
            }
            foreach (string line in ServerProperties) sb.Append(line + "\n");
            File.Delete(ITC.Path + "\\Server\\server.properties");
            using (StreamWriter sw = new StreamWriter(File.OpenWrite(ITC.Path + "\\Server\\server.properties"))) sw.Write(sb.ToString());
        }
        private static bool Sub(string ln, string check, object value)
        {
            if (ln.StartsWith(check+"=")) { ServerProperties.Add(check + "=" + value); return true; }
            return false;
        }
    }
}
