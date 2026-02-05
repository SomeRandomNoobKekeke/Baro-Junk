using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{

  public class ComponentModulesTest : UTestPack
  {
    public class ModuleA : IModule
    {

    }

    public class ModuleB : IModule
    {

    }

    public class ModuleC : IModule
    {

    }

    public class Component : IComponent
    {
      [ModuleCategory("Prop")]
      public class NestedWrapper : IModuleContainer
      {
        [ModuleCategory("1231")]
        public ModuleB moduleB { get; set; }

        public ModuleC moduleC { get; set; }
      }

      public NestedWrapper Modules { get; } = new();

      [ModuleCategory("Prop")]
      public ModuleA moduleA { get; set; }

      [ModuleCategory("1231")]
      public ModuleC moduleC { get; set; }

    }

    public override void CreateTests()
    {
      ComponentAnalysis analysis = ComponentAnalysis.For<Component>();

      Component component = new();
      Tests.Add(new UTest(
        analysis.GetModule<ModuleB>(component, "moduleB", "1231"),
        component.Modules.moduleB
      ));
    }
  }
}