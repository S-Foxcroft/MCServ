using System;
using System.Collections.Generic;

namespace uk.co.ytfox.MCWrap
{
    class Automation
    {
        private static int Hour = 0;
        private static int Day = 0;
        private static DateTime now;
        private static List<string> Ops = null;
        private static string PlayerName;
        public static void Setup()
        {
            Ops = new List<string>();
            //load ops from file ops.yml if present;
        }
        public static void DoTick(int tickCount)
        {
            now = DateTime.Now;
            if (now.Hour != Hour) { FireEvent("hourly"); Hour = now.Hour; }
            if (now.Day != Day) { FireEvent("daily"); Day = now.Day; }
        }
        public static void OnMessage(string line)
        {
            PlayerName = "";
            //server-based events
            if (line.Contains("[Server thread/INFO]: Starting Minecraft server on")) ITC.PrintToWindow = false;
            else if (line.Contains("[Server thread/INFO]: Done"))
            {
                ITC.PrintToWindow = true;
                FireEvent("server_start");
            }
            else if (line.EndsWith("lost connection: Disconnected"))
            {
                int start = line.IndexOf("INFO]:") + 6, end = line.IndexOf(" lost connection");
                PlayerName = line.Substring(start, end - start);
                FireEvent("player_leave");
            }
            else if (line.Contains("[User Authenticator"))
            {
                int start = line.IndexOf("UUID of player") + 14, end = line.IndexOf(" is ");
                PlayerName = line.Substring(start, end - start);
                FireEvent("player_join");
            }


            ITC.msgToUi.RemoveAt(0);
        }
        public static void OnCommand(string s)
        {
            PlayerName = "";
            string command = s;
            string[] args = null;
            if(s.IndexOf(" ") >= 0)
            {
                command = s.Substring(0, s.IndexOf(" "));
                args = s.Substring(s.IndexOf(" ") + 1).Split(' ');
            }
            if (command == "start" && ITC.ServerStatus == "stopped") Server.Run();
            else if (command == "loadconfig" && ITC.ServerStatus == "stopped") Preferences.Load();
            else if (command == "lp" && Preferences.AsBool("Extensions.pluginmanager")) ITC.msgToServer.Add("plugman load " + args[0]);
            else if (command == "rp" && Preferences.AsBool("Extensions.pluginmanager")) ITC.msgToServer.Add("plugman reload " + args[0]);
            else if (command == "up" && Preferences.AsBool("Extensions.pluginmanager")) ITC.msgToServer.Add("plugman unload " + args[0]);
            else if (command == "dp" && Preferences.AsBool("Extensions.pluginmanager")) ITC.msgToServer.Add("plugman disable " + args[0]);
            else if (command == "ep" && Preferences.AsBool("Extensions.pluginmanager")) ITC.msgToServer.Add("plugman enable " + args[0]);
            else ITC.msgToServer.Add(command);
        }
        private static void FireEvent(string e)
        {
            string[] cmd = Preferences.AsString("Automation." + e, "~").Split(';');
            foreach (string s in cmd) if (s != "~") ITC.msgToServer.Add(Substitute(s));
        }
        private static string ZeroPad(int data){ return data < 10 ? "0" + data : ""+data; }
        private static string Substitute(string data) {
            return data.Replace("%date%", String.Format("{0}/{1}/{2}", ZeroPad(DateTime.Now.Day), ZeroPad(DateTime.Now.Month), DateTime.Now.Year))
                       .Replace("%time%", String.Format("{0}:{1}",ZeroPad(DateTime.Now.Hour),ZeroPad(DateTime.Now.Minute)))
                       .Replace("%player%",PlayerName);
        }
    }
}
