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
      public class NestedWrapper : IModuleContainer
      {

        public ModuleB moduleB { get; set; }
        public ModuleC moduleC { get; set; }
      }

      public NestedWrapper Modules { get; } = new();

      public ModuleA moduleA { get; set; }
    }

    public override void CreateTests()
    {
      ComponentStaticAnalysis analysis = ComponentStaticAnalysis.For<Component>();

      Component component = new();
      Tests.Add(new UTest(
        analysis.GetModule<ModuleB>(component, "moduleB"),
        component.Modules.moduleB
      ));
    }
  }
}