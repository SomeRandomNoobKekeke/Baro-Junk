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
    private IConfig config; public IConfig Config
    {
      get => config;
      set
      {
        config = value;
        Init();
      }
    }

    private void Init()
    {
      if (!GameMain.IsMultiplayer) return;
      Config.NetFacade.ListenForServer(Config.NetHeader + "_sync", Receive);
      Config.NetFacade.ClientSend(Config.NetHeader + "_ask");
    }

    public void Receive(IReadMessage msg)
    {
      Config?.NetDecode(msg);
    }

    public SimpleResult Sync()
    {
      if (GameMain.IsSingleplayer) return new SimpleResult(false)
      {
        Details = "It's not multiplayer",
      };
      if (Config is null) return new SimpleResult(false)
      {
        Details = "nothing to sync",
      };
      if (!Config.NetFacade.DoIHavePermissions()) return new SimpleResult(false)
      {
        Details = "You need to be host or have ConsoleCommands permission to use it",
      };

      Config.NetFacade.ClientEncondeAndSend(Config.NetHeader + "_sync", Config);

      return new SimpleResult(true);
    }

    public SimpleResult Ask()
    {
      if (GameMain.IsSingleplayer) return new SimpleResult(false)
      {
        Details = "It's not multiplayer",
      };
      if (Config is null) return new SimpleResult(false)
      {
        Details = "nothing to sync",
      };

      Config.NetFacade.ClientSend(Config.NetHeader + "_ask");

      return new SimpleResult(true);
    }
  }
}