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


    public void Initialize()
    {
      UTestCommands.AddCommands();

      UTestExplorer.ScanCategory("internal");
      // UTestPack.RunRecursive<DebugTest>();
    }


    public void OnLoadCompleted() { }
    public void PreInitPatching() { }
    public void Dispose()
    {
      UTestCommands.RemoveCommands();
    }
  }
}