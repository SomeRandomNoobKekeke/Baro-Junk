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

  /// <summary>
  /// Class responsible for auto loading and saving of current config
  /// There should be only one so it's static
  /// </summary>
  public static class ConfigAutoSaver
  {
    static ConfigAutoSaver() => InstallHooks();

    public static void InstallHooks()
    {
      GameMain.LuaCs.Hook.Add("stop", $"save {IConfig.HookId} config on quit", (object[] args) =>
      {
        if (IConfig.SaveOnQuit) Save();
        return null;
      });

      GameMain.LuaCs.Hook.Add("roundEnd", $"save {IConfig.HookId} config on round end", (object[] args) =>
      {
        if (IConfig.SaveEveryRound) Save();
        return null;
      });
    }

    public static bool ShouldSave =>
      GameMain.IsSingleplayer ||
      LuaCsSetup.IsServer ||
      LuaCsSetup.IsClient && IConfig.ShouldSaveInMultiplayer;

    public static void Save()
    {
      if (ShouldSave) IConfig.Current?.Save(IConfig.SavePath);
    }

    public static string DefaultSavePathFor(IConfig config)
      => Path.Combine("ModSettings", "Configs", $"{config.ID}.xml");


    public static void OnCurrentConfigChanged(IConfig newConfig)
    {
      IConfig.SavePath ??= DefaultSavePathFor(newConfig);
      if (IConfig.LoadOnInit) newConfig.LoadSave(IConfig.SavePath);
    }
  }
}