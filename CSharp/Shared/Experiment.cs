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

    public void Experiment()
    {
      ExampleConfigs.ConfigA config = new ExampleConfigs.ConfigA();
      ConfigMixin mixin = new ConfigMixin(config);

      Mod.Logger.Log(mixin.Locator.GetEntry("NestedConfigB.IntProp"));

    }
  }
}