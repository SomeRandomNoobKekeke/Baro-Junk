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
    public static Logger Logger = new Logger()
    {
      PrintFilePath = false
    };

    public void Initialize()
    {
      UTestLogger.CollapseTestPackIfSucceed = false;

      UTestCommands.AddCommands();
      UTestExplorer.ScanCategory("internal");

      // UTestRunner.RunRecursive<RealNetParserTest>();

      Experiment();
      if (ModInfo.ModDir<Mod>().Contains("LocalMods"))
      {
        Logger.Log($"{ModInfo.AssemblyName} compiled");
      }

      ProjectInfo.CheckIncompatibleLibs();

      PluginCommands.Add("generatecode", (args) => ModuleCodeGenerator.GenarateAll());
      PluginCommands.Add("printcommands", (args) => PluginCommands.PrintCommands());
      PluginCommands.Add("printhooks", (args) => PluginCommands.PrintHooks());



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