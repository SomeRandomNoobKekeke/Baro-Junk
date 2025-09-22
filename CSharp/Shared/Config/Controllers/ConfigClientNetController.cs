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
  public class ConfigClientNetController
  {
    public IConfig Config;
    public ConfigClientNetController(IConfig config) => Config = config;

    public bool Enabled { get; set; }

    private void Initialize()
    {
      if (!GameMain.IsMultiplayer) return;
      Config.NetFacade.ListenForServer(Config.NetHeader + "_sync", Receive);
      Config.NetFacade.ClientSend(Config.NetHeader + "_ask");
    }


    public void Receive(IReadMessage msg)
    {
      if (!Enabled) return;
      Config?.NetDecode(msg);
    }

    //TODO this should be in IConfig
    public SimpleResult Sync()
    {
      if (GameMain.IsSingleplayer) return SimpleResult.Failure("It's not multiplayer");

      if (Config is null) return SimpleResult.Failure("nothing to sync");

      if (!Config.NetFacade.DoIHavePermissions()) return SimpleResult.Failure(
        "You need to be host or have ConsoleCommands permission to use it"
      );

      Config.NetFacade.ClientEncondeAndSend(Config.NetHeader + "_sync", Config);

      return SimpleResult.Success();
    }

    //TODO this should be in IConfig
    public SimpleResult Ask()
    {
      if (GameMain.IsSingleplayer) return SimpleResult.Failure("It's not multiplayer");
      if (Config is null) return SimpleResult.Failure("nothing to sync");


      Config.NetFacade.ClientSend(Config.NetHeader + "_ask");

      return SimpleResult.Success();
    }
  }
}