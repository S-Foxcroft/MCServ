using NATUPNPLib;

namespace uk.co.ytfox.MCWrap
{
    class PortMap
    {
        private static bool portMapError = false;
        private static UPnPNAT NatMgr = null;
        public static void Run()
        {
            //some prep work before task runs
            if (NatMgr == null) NatMgr = new UPnPNAT();
            ITC.msgToUi.Add("Initiating MCPortMap...");
            Task();
        }
        private static void Task()
        {
            int portIn = Preferences.AsInt("PortMap.server_port", 25565);
            int portEx = Preferences.AsBool("PortMap.match_external") ? portIn : 25565;
            string desc = Preferences.AsString("Server.name", "Minecraft Server");
            
            //check for existing mappings
            IStaticPortMappingCollection result = GetMaps();
            if (result != null)
                foreach (IStaticPortMapping entry in result)
                {
                    bool found = false;
                    if (entry.Description == desc) found = true;
                    else if (entry.Description == "Minecraft Server") found = true;
                    if (found && entry.ExternalPort == portEx) RemoveMap(entry);
                }
            else ITC.msgToUi.Add("Port mapping failed.");
            //map the port here
            AddMap(portIn, portEx, desc);
        }
        private static IStaticPortMappingCollection GetMaps()
        {
            if (NatMgr == null)
            {
                ITC.msgToUi.Add("Failed to create UPnP-NAT interface.");
                portMapError = true;
                return null;
            }
            IStaticPortMappingCollection maps = NatMgr.StaticPortMappingCollection;
            if (maps == null)
            {
                ITC.msgToUi.Add("Failed to obtain NAT map. Is your gatewar UpPnP-enabled? This utility may not be required.");
                portMapError = true;
                return null;
            }
            if (maps.Count == 1) ITC.msgToUi.Add("Mapset found with 1 entry.");
            else ITC.msgToUi.Add("Mapset found with " + maps.Count + " entries.");
            return maps;
        }
        private static int AddMap(int portHere, int portThere, string desc)
        {
            if (portMapError) return -1;
            NatMgr.StaticPortMappingCollection.Add(portThere, "UDP", portHere, ITC.IPAddr, true, desc);
            NatMgr.StaticPortMappingCollection.Add(portThere, "TCP", portHere, ITC.IPAddr, true, desc);
            return 0;
        }
        private static int RemoveMap(IStaticPortMapping map)
        {
            if (portMapError) return -1;
            NatMgr.StaticPortMappingCollection.Remove(map.ExternalPort, map.Protocol);
            return 0;
        }
    }
}
