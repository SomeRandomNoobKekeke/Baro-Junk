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
    public static Logger Logger = new Logger();

    public void Initialize()
    {
      UTestCommands.AddCommands();

      UTestExplorer.ScanCategory("internal");

      Experiment();
      if (ModInfo.ModDir<Mod>().Contains("LocalMods"))
      {
        Logger.Log($"{ModInfo.AssemblyName} compiled");
      }
    }


    public void OnLoadCompleted() { }
    public void PreInitPatching() { }
    public void Dispose()
    {
      UTestCommands.RemoveCommands();
    }
  }
}