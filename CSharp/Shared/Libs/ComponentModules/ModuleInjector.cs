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
    public IComponent Host { get; }

    public object GetNested(object target, IEnumerable<PropertyInfo> path)
    {
      foreach (PropertyInfo pi in path)
      {
        target = pi.GetValue(target);
      }

      return target;
    }

    public void CreateModule(CreateModuleInstruction instruction)
    {
      object container = GetNested(Host, instruction.FullPath.SkipLast(1));

      instruction.FullPath.Last().SetValue(
        container,
        Activator.CreateInstance(instruction.ModuleType)
      );
    }

    public void InjectHost(InjectHostInstruction instruction)
    {
      IModule module = (IModule)GetNested(Host, instruction.FullPath);
      module.Host = Host;
    }

    public void InjectDependency(InjectDependencyInstruction instruction)
    {
      object dependency = GetNested(Host, instruction.DependencyPath);
      object target = GetNested(Host, instruction.TargetPath);
      instruction.Property.SetValue(target, dependency);
    }

    public void CreateModules(IEnumerable<CreateModuleInstruction> instructions)
    {
      foreach (var instruction in instructions) { CreateModule(instruction); }
    }

    public void InjectHosts(IEnumerable<InjectHostInstruction> instructions)
    {
      foreach (var instruction in instructions) { InjectHost(instruction); }
    }

    public void InjectDependencies(IEnumerable<InjectDependencyInstruction> instructions)
    {
      foreach (var instruction in instructions) { InjectDependency(instruction); }
    }
  }


}
