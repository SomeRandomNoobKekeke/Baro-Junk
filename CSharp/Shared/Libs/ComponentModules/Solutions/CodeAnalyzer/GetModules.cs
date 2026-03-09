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
  public partial class CodeAnalyzer
  {
    public static IEnumerable<ModuleInfo> GetModules(Type T) => GetModules(new ComponentInfo(T));
    public static IEnumerable<ModuleInfo> GetModules(ComponentInfo component)
    {
      IEnumerable<ModuleInfo> GetModulesFromPart(PartInfo part)
      {
        foreach (PropertyInfo pi in part.Type.GetProperties(Pls))
        {
          if (pi.PropertyType.IsAssignableTo(typeof(IModule)))
          {
            yield return new ModuleInfo(component.Type, part.Path, pi);
          }
        }
      }

      foreach (PropertyInfo pi in component.Type.GetProperties(Pls))
      {
        if (pi.PropertyType.IsAssignableTo(typeof(IModule)))
        {
          yield return new ModuleInfo(component.Type, new List<PropertyInfo>(), pi);
        }
      }

      foreach (PartInfo part in component.Parts)
      {
        foreach (ModuleInfo module in GetModulesFromPart(part))
        {
          yield return module;
        }
      }
    }


  }
}