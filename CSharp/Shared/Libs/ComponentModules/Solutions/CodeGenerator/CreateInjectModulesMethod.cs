using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;
using BaroJunk;
using CsCodeGenerator;

namespace BaroJunk.ComponentModules
{
  public partial class CodeGenerator
  {
    public static Method CreateInjectModulesMethod(ComponentInfo component)
    {
      List<string> bodyLines = new List<string>();

      foreach (var request in component.ModuleRequests)
      {
        if (!component.ModulesByType.ContainsKey(request.Type)) continue;

        bodyLines.Add($"{request.Module.StringPath}.{request.Target.Name} = {component.ModulesByType[request.Type].StringPath};");
      }

      return new Method("void", "InjectModules")
      {
        BodyLines = bodyLines,
        AccessModifier = CsCodeGenerator.Enums.AccessModifier.Private
      };
    }
  }
}