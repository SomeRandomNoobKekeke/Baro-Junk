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
  public class ConfigSaverSettings
  {
    public bool ShouldSaveInMultiplayer { get; set; } = false;
    public bool LoadOnInit { get; set; } = false;
    public bool SaveOnQuit { get; set; } = true;
    public bool SaveEveryRound { get; set; } = true;
    public string SavePath { get; set; } = null;
  }

  public class ConfigSaverResult
  {
    public bool Success;
    public string Details;
    public Exception Exception;

    public ConfigSaverResult(bool success) => Success = success;
    public override string ToString()
      => $"{(Success ? "Ok" : "Failed")}{(Details != null ? $": {Details}" : "")}{(Exception != null ? $" [{Exception.Message}]" : "")}";
  }

  public class ConfigSaver
  {
    static ConfigSaver() => InstallHooks();

    public static void InstallHooks()
    {
      GameMain.LuaCs.Hook.Add("stop", $"save {IConfig.HookId} config on quit", (object[] args) =>
      {
        if (IConfig.ConfigSaver.Settings.SaveOnQuit) IConfig.ConfigSaver.Save();
        return null;
      });

      GameMain.LuaCs.Hook.Add("roundEnd", $"save {IConfig.HookId} config on round end", (object[] args) =>
      {
        if (IConfig.ConfigSaver.Settings.SaveEveryRound) IConfig.ConfigSaver.Save();
        return null;
      });
    }

    public static string DefaultSavePathFor(IConfig config)
      => Path.Combine("ModSettings", "Configs", $"{config.ID}.xml");

    public IIOAdapter Adapter;



    public bool ShouldSave =>
      GameMain.IsSingleplayer ||
      LuaCsSetup.IsServer ||
      LuaCsSetup.IsClient && Settings.ShouldSaveInMultiplayer;



    private IConfig _config; public IConfig Config
    {
      get => _config;
      set
      {
        _config = value;
        Settings.SavePath ??= DefaultSavePathFor(_config);
        if (Settings.LoadOnInit) LoadSave();
      }
    }
    public ConfigSaverSettings Settings = new();


    public ConfigSaverResult LoadSave(string path = null)
    {
      ConfigSaverResult result = Load(path);
      Save(path);
      return result;
    }

    public ConfigSaverResult Save(string path = null)
    {
      if (!ShouldSave) return new ConfigSaverResult(false)
      {
        Details = "Not supposed to save here",
      };

      if (Config is null) return new ConfigSaverResult(false)
      {
        Details = "Current config is null",
      };

      path ??= Settings.SavePath;

      try
      {
        Adapter.EnsureDirectory(Path.GetDirectoryName(path));

        XDocument xdoc = new XDocument();
        xdoc.Add(Config.ToXML());
        Adapter.SaveXDoc(xdoc, path);
      }
      catch (Exception e)
      {
        return new ConfigSaverResult(false)
        {
          Details = "Error",
          Exception = e,
        };
      }

      return new ConfigSaverResult(true);
    }


    public ConfigSaverResult Load(string path = null)
    {
      if (Config is null) return new ConfigSaverResult(false)
      {
        Details = "Current config is null",
      };

      path ??= Settings.SavePath;

      if (!Adapter.FileExists(path)) return new ConfigSaverResult(false)
      {
        Details = $"[{path}] not found",
      };

      try
      {
        XDocument xdoc = Adapter.LoadXDoc(path);
        Config.FromXML(xdoc.Root);
      }
      catch (Exception e)
      {
        return new ConfigSaverResult(false)
        {
          Details = "Error",
          Exception = e,
        };
      }

      return new ConfigSaverResult(true);
    }


    public ConfigSaver(IIOAdapter adapter) => Adapter = adapter;
  }
}