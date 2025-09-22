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

namespace BaroJunk
{
  public class ConfigManager
  {
    public IConfig Config;

    public IIOFacade IOFacade { get; set; } = new IOFacade();
    public INetFacade NetFacade { get; set; } = new NetFacade();


    public ConfigAutoSaver AutoSaver;
    public ConfigClientNetManager ClientNetController;
    public ConfigServerNetManager ServerNetController;
    public ConfigCommandsManager CommandsManager;

    //HACK bruh
    public bool NetSync
    {
      get
      {
#if CLIENT
        return ClientNetController.Enabled;
#elif SERVER
        return ServerNetController.Enabled;
#endif
      }

      set
      {
#if CLIENT
        ClientNetController.Enabled = value;
#elif SERVER
        ServerNetController.Enabled = value;
#endif
      }
    }

    public ConfigManager(IConfig config)
    {
      Config = config;
      AutoSaver = new ConfigAutoSaver(config);
      ClientNetController = new ConfigClientNetManager(config);
      ServerNetController = new ConfigServerNetManager(config);
      CommandsManager = new ConfigCommandsManager(config);
    }
  }
}