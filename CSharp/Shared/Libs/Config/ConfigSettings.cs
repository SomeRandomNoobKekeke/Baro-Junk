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

  public enum ConfigBehaviour
  {
    None,
  }
  public class ConfigSettings
  {
    public IConfig Config;
    public ConfigSettings(IConfig config) => Config = config;

    public string CommandName
    {
      get => Config.Manager.CommandsManager.CommandName;
      set => Config.Manager.CommandsManager.CommandName = value;
    }

    public string SavePath { get; set; } = null;
    public bool PrintAsXML { get; set; } = false;
  }
}