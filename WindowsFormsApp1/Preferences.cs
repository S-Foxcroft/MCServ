using System;
using System.Collections.Generic;
using System.IO;

namespace uk.co.ytfox.MCWrap
{
    static class Preferences
    {
        private static Dictionary<String, Dictionary<String, String>> properties = null;
        //Base access method
        public static String AsString(string path)
        {
            string[] splitPath = path.Split('.');
            if (properties.TryGetValue(splitPath[0], out Dictionary<string, string> section))
                if (section.TryGetValue(splitPath[1], out string result)) return result;
                else if (properties.TryGetValue("default", out section))
                    if (section.TryGetValue(path, out result)) return result;
            return null;
        }
        public static String AsString(string path, string def)
        {
            string result = AsString(path);
            if (result != null) return result;
            return def;
        }
        public static int AsInt(string path, int def)
        {
            string input = AsString(path);
            if (input != null && Int32.TryParse(input, out int result)) return result;
            return def;
        }
        public static bool AsBool(string path, bool def)
        {
            string input = AsString(path);
            if (input != null) return (input == "true");
            return def;
        }
        public static bool AsBool(string path){ return AsBool(path, false); }
        public static void Load()
        {
            if (!File.Exists(ITC.Path + "\\mcserver.ini"))
            {
                using (Stream target = File.OpenWrite(ITC.Path+"\\mcserver.ini"))
                {
                    using (StreamWriter sw = new StreamWriter(target))
                    {
                        sw.Write(Properties.Resources.mcserver);
                    }
                }
            }

            if (properties == null) properties = new Dictionary<string, Dictionary<string, string>>();
            else properties.Clear();
            try
            {
                StreamReader sr = new StreamReader(File.OpenRead(ITC.Path + "\\mcserver.ini"));
                Dictionary<String, String> section = null;
                string sectionTitle = "default";
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if(line.Length >=2)
                        if (line.StartsWith("[") && line.EndsWith("]"))
                        {
                            if(section != null)
                            {
                                properties.Add(sectionTitle, section);
                            }
                            section = new Dictionary<String, String>();
                            sectionTitle = line.Substring(1, line.Length - 2);
                        }
                        else if(line.Substring(0,1) != "#")
                        {
                            string[] splitEntry = line.Split('=');
                            if (splitEntry.Length == 2) section.Add(splitEntry[0], splitEntry[1]);
                        }
                }
                properties.Add(sectionTitle, section);
                sr.Close();
                ITC.msgToUi.Add("Configuration loaded.");
            }
            catch(IOException e)
            {
                foreach(string line in e.StackTrace.Split('\n'))
                {
                    ITC.msgToUi.Add(line);
                }
            }
            string loaded = Preferences.AsString("Backup.delay");
            if (loaded == null || loaded.IndexOf(' ') <= 0) loaded = "30 minute";
            string[] lines = loaded.Split(' ');
            int.TryParse(lines[0], out int saveTicks);
            string unit = lines[1];
            switch (unit)
            {
                case "minute":
                    saveTicks *= 6000;
                    break;
                case "hour":
                    saveTicks *= 360000;
                    break;
                default: //if for some reason the unit is illegible, use minutes.
                    saveTicks *= 6000;
                    break;
            }
            Backup.SetInterval(saveTicks);
        }
    }
}
