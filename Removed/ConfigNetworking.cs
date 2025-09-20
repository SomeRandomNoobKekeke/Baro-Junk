using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Barotrauma;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace BaroJunk
{
  public static partial class ConfigNetworking
  {

    public static string NetHeader => IConfig.Current is null ? IConfig.HookId : IConfig.Current.ID;

    public static void OnCurrentConfigChanged(IConfig newConfig)
    {
      if (!GameMain.IsMultiplayer) return;

#if CLIENT
      GameMain.LuaCs.Networking.Receive(NetHeader + "_sync", Receive);
#elif SERVER
      GameMain.LuaCs.Networking.Receive(NetHeader + "_ask", Give);
      GameMain.LuaCs.Networking.Receive(NetHeader + "_sync", Receive);
#endif

      if (ConfigManager.AutoSetup) NetSync();
    }

    public static void Init()
    {

    }

    public static void NetSync()
    {
#if CLIENT
      Ask();
#elif SERVER
      Broadcast();
#endif
    }
  }
}