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
    public record Error();

    public record AmbiguousMatchError(ModuleInfo A, ModuleInfo B, Type Target) : Error
    {
      public override string ToString()
        => $"Ambiguous match: [{A}] [{B}] both can be used as [{Target.Name}]";
    }

    public record UnsatisfiedRequestError(Type componentType, ModuleInfo.ModuleRequest Request) : Error
    {
      public override string ToString()
        => $"Unsatisfied request: [{componentType.Name}] can't provide [{Request.Type.Name}] for [{Request.Module}]";
    }


    public List<Error> Errors { get; } = new();

    public Type Type { get; }

    public List<PartInfo> Parts { get; }
    public List<ModuleInfo> Modules { get; }
    public Dictionary<Type, ModuleInfo> ModulesByType { get; }
    public List<ModuleInfo.ModuleRequest> ModuleRequests { get; }

    private void Report(Error error)
    {
      Errors.Add(error);
    }

    public ComponentInfo(Type componentType)
    {
      Type = componentType;
      Parts = CodeAnalyzer.GetParts(componentType).ToList();
      Modules = CodeAnalyzer.GetModules(this).ToList();
      ModulesByType = new();

      foreach (ModuleInfo module in Modules)
      {
        foreach (Type t in module.CanBeUsedAs)
        {
          if (ModulesByType.ContainsKey(t))
          {
            Report(new AmbiguousMatchError(ModulesByType[t], module, t));
          }
          ModulesByType[t] = module;
        }
      }

      ModuleRequests = Modules.SelectMany(m => m.RequiredModules).ToList();
      foreach (var request in ModuleRequests)
      {
        if (!ModulesByType.ContainsKey(request.Type))
        {
          Report(new UnsatisfiedRequestError(componentType, request));
        }
      }
    }

  }
}