using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Barotrauma;
using Microsoft.Xna.Framework;
using System.IO;
using System.Text;

namespace BaroJunk
{

  public partial class ModuleInjector
  {
    public object GetNested(object target, IEnumerable<PropertyInfo> path)
    {
      foreach (PropertyInfo pi in path)
      {
        target = pi.GetValue(target);
        if (target is null) return null;
      }

      return target;
    }

    public void InjectHost(IComponent host, InjectHostInstruction instruction)
    {
      object module = GetNested(host, instruction.FullPath);

      if (module is null)
      {
        ModuleManager.Logger.Warning($"Module is not initialized: {host}.{String.Join('.', instruction.FullPath.Select(pi => pi.Name))}");
        return;
      }

      instruction.HostProp.SetValue(module, host);
    }

    public void InjectDependency(IComponent host, InjectDependencyInstruction instruction)
    {
      Logger.Default.Point();
      object dependency = GetNested(host, instruction.DependencyPath);
      object target = GetNested(host, instruction.TargetPath);

      Logger.Default.Point();

      if (dependency is null)
      {
        ModuleManager.Logger.Warning($"Dependency module is not initialized: {host}.{String.Join('.', instruction.DependencyPath.Select(pi => pi.Name))}");
        return;
      }

      if (target is null)
      {
        ModuleManager.Logger.Warning($"Module {host}.{String.Join('.', instruction.TargetPath.Select(pi => pi.Name))} is not initialized");
        return;
      }

      instruction.Property.SetValue(target, dependency);
    }


    public void InjectHosts(IComponent host, IEnumerable<InjectHostInstruction> instructions)
    {
      foreach (var instruction in instructions) { InjectHost(host, instruction); }
    }

    public void InjectDependencies(IComponent host, IEnumerable<InjectDependencyInstruction> instructions)
    {
      foreach (var instruction in instructions) { InjectDependency(host, instruction); }
    }
  }


}
