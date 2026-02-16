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
      [ForwardedMethod] public void SayBruh(string args = "123") => Logger.Default.Log($"{args} {Value}");

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

      public Component()
      {
        InjectModules();
      }
    }

    public override void CreateTests()
    {
      ModuleCodeGenerator.GenerateFor<Component>();

      Component component = new Component();


      Mod.Logger.Log(component.Value);


    }
  }
}