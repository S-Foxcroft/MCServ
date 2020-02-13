using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace uk.co.ytfox.MCWrap
{
    class Backup
    {
        private static Thread saveThread = null;
        private static Process pRar;
        private static bool Locked = false;
        private static int saveTicks;
        public static int Interval { get { return saveTicks; } }
        public static void Run(string savetype)
        {
            saveThread = new Thread(Task);
            saveThread.Start(savetype);
        }
        private static void Task(object s)
        {
            if (!Preferences.AsBool("Backup.enabled") || (ITC.ServerStatus == "stopped" && (string)s == "AutoSave")) return;
            pRar = new Process();
            DateTime now = DateTime.Now;
            string path = String.Format("{0}\\Backups\\{1}\\{2}\\{3}-{4}{5}", ITC.Path, now.Year, ZeroPad(now.Month), ZeroPad(now.Day), ZeroPad(now.Hour), ZeroPad(now.Minute));
            string folders = "";
            int worldsave = Preferences.AsInt("Backup.worlds", 7);
            if ((worldsave & 1) > 0) folders += " world\\*";
            if ((worldsave & 2) > 0) folders += " world_nether\\*";
            if ((worldsave & 4) > 0) folders += " world_the_end\\*";
            string rar = "C:\\Program Files\\WinRAR\\rar.exe";
            if (!File.Exists(rar)) ITC.msgToUi.Add("Unable to compress, aborting...");
            ProcessStartInfo psi = new ProcessStartInfo(rar)
            {
                Arguments = String.Format("a {0}-r -inul {1}-{2}.rar{3}{4}{5}",
                                          (Preferences.AsString("Backup.plugins") == "true" && Preferences.AsString("Backup.configs") == "true") ? "" : "-x*.jar ",
                                          path,
                                          (string) s,
                                          folders,
                                          Preferences.AsString("Backup.configs") == "true" ? " plugins\\*" : "",
                                          ((string)s == "ServerClose" && Preferences.AsBool("Backup.jarfile", false)) ? " " + Preferences.AsString("Server.jarfile", "") : ""),
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                WorkingDirectory = ITC.Path + "\\Server",
                CreateNoWindow = true
            };
            pRar.StartInfo = psi;
            //start rar process
            
            ITC.msgToUi.Add("Creating backup...");
            string dir = String.Format("{0}\\Backups\\{1}\\{2}\\", ITC.Path, now.Year, now.Month);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            pRar.Start();
            pRar.WaitForExit();
            ITC.msgToUi.Add("Backup complete.");
        }
        public static void DoTick(int tickCount)
        {
            if (!Locked && tickCount == saveTicks)
            {
                Locked = true;
                Run("AutoSave");
                Locked = false;
            }
        }
        public static void SetInterval(int i) { saveTicks = i; }
        private static string ZeroPad(int data) { return data < 10 ? "0" + data : "" + data; }
    }
}
