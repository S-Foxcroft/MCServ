﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace uk.co.ytfox.MCWrap.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("uk.co.ytfox.MCWrap.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to eula=true.
        /// </summary>
        internal static string eula {
            get {
                return ResourceManager.GetString("eula", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [Server]
        ///warn=true
        ///autostart=true
        ///name=Minecraft Server
        ///mp_message=A Minecraft Server
        ///port=25565
        ///players=20
        ///jarfile=server.jar
        ///#difficulty: (peaceful|easy|normal|hard|hardcore)
        ///difficulty=easy
        ///# game mode: (survival|creative)
        ///game_mode=survival
        ///# Allow pvp, nether portals and command blocks?
        ///pvp=true
        ///nether_portals=true
        ///command_blocks=false
        ///# RAM allocation for JVM heap (ends G or M, recommend no lower than 512M if necessary)
        ///ram_minimum=512M
        ///ram_maximum=2G
        ///
        ///[World]
        ///#Set the seed for yo [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string mcserver {
            get {
                return ResourceManager.GetString("mcserver", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to allow-nether=true
        ///server-port=25565
        ///hardcore=false
        ///level-type=default
        ///pvp=true
        ///difficulty=2
        ///enable-command-block=false
        ///gamemode=0
        ///broadcast-console-to-ops=false
        ///max-players=20
        ///motd=A Minecraft Server
        ///level-seed=.
        /// </summary>
        internal static string server {
            get {
                return ResourceManager.GetString("server", resourceCulture);
            }
        }
    }
}
