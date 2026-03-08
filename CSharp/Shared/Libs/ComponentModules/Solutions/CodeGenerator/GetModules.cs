using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;
using BaroJunk;

namespace BaroJunk.ComponentModules
{
  public partial class CodeGenerator
  {
    // public static string GetModules(PartsInfo parts)
    // {
    //   IEnumerable<ModuleInfo> GetModulesFromPart(PartInfo part)
    //   {
    //     foreach (PropertyInfo pi in part.Type.GetProperties(Pls))
    //     {
    //       if (pi.PropertyType.IsAssignableTo(typeof(IModule)))
    //       {
    //         yield return new ModuleInfo(parts.Component, part.Path, pi);
    //       }
    //     }
    //   }

    //   foreach (PartInfo part in parts.Parts)
    //   {
    //     foreach (ModuleInfo module in GetModulesFromPart(part))
    //     {
    //       yield return module;
    //     }
    //   }
    // }
  }
}