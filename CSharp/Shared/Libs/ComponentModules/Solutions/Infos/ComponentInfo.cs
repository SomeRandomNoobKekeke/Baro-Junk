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
  public class ComponentInfo
  {
    public List<string> Errors { get; } = new();

    public Type Type { get; }

    public List<PartInfo> Parts { get; }
    public List<ModuleInfo> Modules { get; }
    public Dictionary<Type, ModuleInfo> ModulesByType { get; }
    public List<ModuleInfo.ModuleRequest> ModuleRequests { get; }

    private void Report(string error)
    {
      Errors.Add(error);
    }

    public ComponentInfo(Type T)
    {
      Type = T;
      Parts = CodeAnalyzer.GetParts(T).ToList();
      Modules = CodeAnalyzer.GetModules(this).ToList();
      ModulesByType = new();

      foreach (ModuleInfo module in Modules)
      {
        foreach (Type t in module.CanBeUsedAs)
        {
          if (ModulesByType.ContainsKey(t))
          {
            Report($"Ambiguous match: [{ModulesByType[t]}] [{module}] both can be used as [{t.Name}]");
          }
          ModulesByType[t] = module;
        }
      }

      ModuleRequests = Modules.SelectMany(m => m.RequiredModules).ToList();
      foreach (var request in ModuleRequests)
      {
        if (!ModulesByType.ContainsKey(request.Type))
        {
          Report($"Unsatisfied request: [{T.Name}] can't provide [{request.Type.Name}] for [{request.Module}]");
        }
      }
    }

  }
}