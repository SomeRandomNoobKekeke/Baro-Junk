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
    public ConfigClientNetController ClientNetController;
    public ConfigServerNetController ServerNetController;

    public ConfigManager(IConfig config)
    {
      Config = config;
      AutoSaver = new ConfigAutoSaver(config);
      ClientNetController = new ConfigClientNetController(config);
      ServerNetController = new ConfigServerNetController(config);
    }
  }
}