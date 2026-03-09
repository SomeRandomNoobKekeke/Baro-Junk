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
  public class GetPartsTest : ComponentModulesTest
  {

    public class Component : IComponent
    {
      public class Component_Part : IPart { public Component Self { get; set; } }

      public class GraphicProps_Part : Component_Part { }
      public class LayoutProps_Part : Component_Part { }
      public class Secret_Part : Component_Part { }

      public class DeeplyNested_Part : Component_Part { }
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
    }

    public override void CreateTests()
    {
      Tests.Add(new UListTest(
        CodeAnalyzer.GetParts(typeof(Component)).Select(part => part.ToString()),
        new List<string>()
        {
          // "Component",
          "Component.Graphics",
          "Component.Layout",
          "Component.Secret",
          "Component.Wrapper",
          "Component.Wrapper.Nested",
          "Component.Wrapper.Nested.DeeplyNested",
        }
      ));
    }
  }
}