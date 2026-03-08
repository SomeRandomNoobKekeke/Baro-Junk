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
  public class MatchModulesTest : ComponentModulesTest
  {

    public interface ILol { }
    public interface IKek { }


    public class ModuleA : IModule { }
    public class ModuleB : IModule, ILol { }
    public class ModuleC : IModule, IKek { }
    public class ModuleD : IModule, IKek { }
    public class ModuleE : IModule { }

    public class Component : IComponent
    {
      public class Component_Part : IPart { public Component Self { get; set; } }
      public class GraphicProps_Part : Component_Part
      {
        public ModuleD ModuleD { get; } = new();
        public ModuleE ModuleE { get; } = new();
      }

      public GraphicProps_Part Graphics { get; } = new();

      public ModuleA ModuleA { get; } = new();
      public ModuleB ModuleB { get; } = new();
      public ModuleC ModuleC { get; } = new();
    }

    public USetTest FindAllAvailableModules()
    {
      ComponentInfo componentInfo = new ComponentInfo(typeof(Component));

      Logger.Default.Log(Logger.Wrap.IEnumerable(componentInfo.Errors));

      return new USetTest(
        componentInfo.ModulesByType.Select(kvp => $"{kvp.Key.Name} - {kvp.Value}"),
        new HashSet<string>()
        {
          "ModuleA - Component.ModuleA",
          "ModuleB - Component.ModuleB",
          "ModuleC - Component.ModuleC",
          "ModuleD - Component.Graphics.ModuleD",
          "ModuleE - Component.Graphics.ModuleE",
          "ILol - Component.ModuleB",
          "IKek - Component.Graphics.ModuleD",
        }
      );
    }
  }
}