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
  public class ConfigSettings
  {
    public IConfig Config;
    public ConfigSettings(IConfig config) => Config = config;

    public bool AutoSave
    {
      get => Config.Mixin.ConfigManager.AutoSaver.Enabled;
      set => Config.Mixin.ConfigManager.AutoSaver.Enabled = value;
    }

    public bool NetSync
    {
      get => Config.Mixin.ConfigManager.NetSync;
      set => Config.Mixin.ConfigManager.NetSync = value;
    }

    public bool ShouldSaveInMultiplayer { get; set; } = false;
    public bool LoadOnInit { get; set; } = false;
    public bool SyncOnInit { get; set; } = false;
    public bool SaveOnQuit { get; set; } = true;
    public bool SaveEveryRound { get; set; } = true;
    public string SavePath { get; set; } = null;
  }
}