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
  public class GetModulesTest : ComponentModulesTest
  {
    public class ModuleA : IModule { }
    public class ModuleB : IModule { }
    public class ModuleC : IModule { }

    public class Component : IComponent
    {
      public class Component_Part : IPart { public Component Self { get; set; } }

      public class GraphicProps_Part : Component_Part { }
      public class LayoutProps_Part : Component_Part
      {
        public ModuleB ModuleB { get; } = new();
      }
      public class Secret_Part : Component_Part { }

      public class DeeplyNested_Part : Component_Part
      {
        public ModuleC ModuleC { get; } = new();
      }
      public class Nested_Part : Component_Part
      {
        public DeeplyNested_Part DeeplyNested { get; } = new();
      }
      public class Wrapper_Part : Component_Part
      {
        public Nested_Part Nested { get; } = new();
      }

      public GraphicProps_Part Graphics { get; } = new();
      public LayoutProps_Part Layout { get; } = new();
      private Secret_Part Secret { get; } = new();
      public Wrapper_Part Wrapper { get; } = new();

      public ModuleA ModuleA { get; } = new();
    }

    public override void CreateTests()
    {
      Tests.Add(new UListTest(
        CodeAnalyzer.GetModules(typeof(Component)).Select(part => part.ToString()),
        new List<string>()
        {
          "Component.ModuleA",
          "Component.Layout.ModuleB",
          "Component.Wrapper.Nested.DeeplyNested.ModuleC",
        }
      ));
    }
  }
}