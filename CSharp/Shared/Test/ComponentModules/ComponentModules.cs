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
      public string PropA { get; set; } = "123";
    }

    public class ModuleB : IModule
    {
      [ModuleDependency] public ModuleA ModuleA { get; set; }

      public string GetPropA() => ModuleA.PropA;
    }

    public class ModuleC : IModule { }

    public class Component : IComponent
    {
      public class NestedWrapper : IModuleContainer
      {

        public ModuleB ModuleB { get; set; } = new();
        public ModuleC ModuleC { get; set; } = new();
      }

      public NestedWrapper Modules { get; } = new();

      [Module]
      public ModuleA ModuleA { get; set; } = new();
    }

    public override void CreateTests()
    {
      Component component = new();
      Logger.Default.Log(ModuleMap.GetFor<Component>());

      component.InjectModules();

      Tests.Add(new UTest(component.Modules.ModuleB.GetPropA(), "123"));
    }
  }
}