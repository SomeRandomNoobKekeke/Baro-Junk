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
    public class ConfigC : IConfig
    {
      public int IntProp { get; set; } = 6;
      public float FloatProp { get; set; } = 7.0f;
      public string StringProp { get; set; } = "bruh";
      public string NullStringProp { get; set; }
    }

    public void Experiment()
    {



    }
  }
}