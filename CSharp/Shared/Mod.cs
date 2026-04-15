using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;
using HarmonyLib;

namespace BaroJunk
{
  public partial class Mod : IAssemblyPlugin
  {
    public IPluginManagementService PluginService { get; set; }
    public static Logger Logger = new Logger()
    {
      PrintFilePath = false
    };

    public void Initialize()
    {
      PluginService.TryGetPackageForPlugin<Mod>(out ContentPackage package);


      UTestLogger.CollapseTestPackIfSucceed = false;

      UTestCommands.AddCommands();
      UTestExplorer.ScanCategory("internal");

      // UTestRunner.RunRecursive<RealNetParserTest>();

      Experiment();
      if (package.Dir.Contains("LocalMods"))
      {
        Logger.Log($"{package.Name} compiled");
      }

      ProjectInfo.CheckIncompatibleLibs();
    }


    public void OnLoadCompleted() { }
    public void PreInitPatching() { }
    public void Dispose()
    {
      UTestCommands.RemoveCommands();
      TypeInfoCache.ClearAll();
    }
  }
}