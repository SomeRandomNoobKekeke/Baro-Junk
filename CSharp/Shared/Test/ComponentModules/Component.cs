using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{

  public partial class ComponentTest : UTestPack
  {


    public class ModuleA : IModule
    {
      public Component Host { get; set; }

      [ModuleDependency] public ModuleB ModuleB { get; set; }


      [ForwardedProp] public string Value => $"bruh -> {ModuleB.Value}";

    }
    public class ModuleB : IModule
    {
      public string Value { get; set; } = "bruh";
    }
    public class ModuleC : IModule
    {
      public string Value { get; set; } = "bruh";
    }

    [GeneratedComponent]
    public partial class Component : IComponent
    {
      public class PropsContainer : IModuleContainer
      {
        public ModuleA ModuleA { get; } = new();
      }
      public PropsContainer Props { get; } = new();

      [Module] public ModuleB ModuleB { get; } = new();
    }

    public override void CreateTests()
    {
      IComponent component = new Component();


      ModuleCodeGenerator.GenerateFor<Component>();

    }
  }
}