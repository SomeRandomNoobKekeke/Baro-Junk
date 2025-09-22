using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Barotrauma;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Text;
using Barotrauma.Networking;

namespace BaroJunk
{
  public class ConfigServerNetController
  {
    public IConfig Config;
    public ConfigServerNetController(IConfig config) => Config = config;

    public bool Enabled { get; set; }

    private void Init()
    {
      if (!GameMain.IsMultiplayer) return;
      Config.NetFacade.ListenForClients(Config.NetHeader + "_ask", Give);
      Config.NetFacade.ListenForClients(Config.NetHeader + "_sync", Receive);
      Config.NetFacade.ServerEncondeAndBroadcast(Config.NetHeader + "_sync", Config);
    }

    //TODO how to not fail silently here?
    public void Give(IReadMessage msg, Client client)
    {
      if (!Enabled) return;
      if (Config is null) return;
      Config.NetFacade.ServerEncondeAndSend(Config.NetHeader + "_sync", Config, client);
    }

    public void Receive(IReadMessage msg, Client client)
    {
      if (!Enabled) return;
      if (Config is null) return;
      if (!Config.NetFacade.DoesClientHasPermissions(client)) return;

      Config.NetDecode(msg);
      Config.NetFacade.ServerEncondeAndBroadcast(Config.NetHeader + "_sync", Config);
    }
  }


}