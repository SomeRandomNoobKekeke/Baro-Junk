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

  public static class ModuleCodeGenerator
  {
    public static IEnumerable<string> GenerateInjectCode(IComponent host)
      => GenerateInjectCode(ModuleMap.GetFor(host.GetType()));
    public static IEnumerable<string> GenerateInjectCode(ModuleMap map)
    {
      foreach (string s in ModuleCodeGenerator.InjectHosts(
        ModuleMapAnalizer.CreateInjectHostInstructions(map))
      ) yield return s;

      foreach (string s in ModuleCodeGenerator.InjectDependencies(
        ModuleMapAnalizer.CreateInjectDependencyInstructions(map))
      ) yield return s;
    }

    public static IEnumerable<string> InjectHosts(IEnumerable<InjectHostInstruction> instructions)
    {
      foreach (var instruction in instructions)
      {
        yield return $"{String.Join('.', instruction.FullPath.Select(pi => pi.Name))}.{instruction.HostProp.Name} = this;";
      }
    }

    public static IEnumerable<string> InjectDependencies(IEnumerable<InjectDependencyInstruction> instructions)
    {
      foreach (var instruction in instructions)
      {
        yield return $"{String.Join('.', instruction.TargetPath.Select(pi => pi.Name))} = {String.Join('.', instruction.DependencyPath.Select(pi => pi.Name))};";
      }
    }
  }


}
