using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace uk.co.ytfox.MCWrap
{
    public partial class Form1 : Form
    {
        Rotator<String> lineBuffer;
        int ticks = 0;
        bool Locked = false;
        public Form1()
        {
            lineBuffer = new Rotator<string>(70);
            for (int i = 0; i < 70; i++) lineBuffer.append("");//init the slots
            ITC.Setup();
            //check we have any of the required files...
            if (!Directory.Exists(ITC.Path + "\\Server")) Directory.CreateDirectory(ITC.Path + "\\Server");
            if (!File.Exists(ITC.Path + "\\Server\\eula.txt")) using (Stream target = File.OpenWrite(ITC.Path + "\\Server\\eula.txt"))
                    using (StreamWriter sw = new StreamWriter(target))
                    {
                        sw.Write(Properties.Resources.eula);
                    }
            if (!File.Exists(ITC.Path + "\\Server\\server.properties")) using (Stream target = File.OpenWrite(ITC.Path + "\\Server\\server.properties"))
                    using (StreamWriter sw = new StreamWriter(target))
                    {
                        sw.Write(Properties.Resources.server);
                    }
            InitializeComponent();
            Preferences.Load();
            if (Preferences.AsString("Server.autostart") == "true") Server.Run();
            this.Text = Preferences.AsString("Server.name") + " - MCServ";
        }
        

        private void Tick(object sender, EventArgs e)
        {
            if (Backup.Interval == 0) return;
            ticks = (ticks%Backup.Interval)+1;
            if (!Locked)
            {
                Locked = true;
                for (int i = 0; i < ITC.msgToUi.Count; i++)
                {
                    string item = ITC.msgToUi.First();
                    if (ITC.PrintToWindow)
                        if(!item.Contains("[Server thread/WARN]") || Preferences.AsBool("Server.warn")) lineBuffer.append(item.Replace("[Server thread/INFO]", "[INFO]").Replace("[Server thread/WARN]", "[WARN]"));
                    Automation.OnMessage(item);
                }
                string screen = "";
                for (int i = 0; i < lineBuffer.Length; i++) if(lineBuffer[i] != "") screen += lineBuffer[i] + "\r\n";
                ServerOutput.Text = screen;
                ServerOutput.Select(screen.Length-1, 0);
                ServerOutput.ScrollToCaret();
                Locked = false;
            }
            Server.DoTick(ticks);
            Backup.DoTick(ticks);
            Automation.DoTick(ticks);
        }
        private void ServerInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Automation.OnCommand(ServerInput.Text);
                ServerInput.Text = "";
            }
        }
    }
}