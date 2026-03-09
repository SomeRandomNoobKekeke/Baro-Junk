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
  public class InjectModulesMethodTest : ComponentModulesTest
  {
    public interface ILol { }
    public interface IKek { }
    public interface ICheburek { }

    public class ModuleA : IModule
    {
      [In] public IKek KekDependency { get; set; }
    }
    public class ModuleB : IModule, ILol
    {
      [In] public ICheburek CheburekDependency { get; set; }
    }
    public class ModuleC : IModule, IKek
    {
      [In] public ModuleA ModuleADependency { get; set; }
    }
    public class ModuleD : IModule, IKek { }
    public class ModuleE : IModule
    {
      [In] public ILol ILolDependency { get; set; }
    }

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


    public override void CreateTests()
    {
      ComponentInfo component = new ComponentInfo(typeof(Component));

      foreach (ComponentInfo.Error error in component.Errors)
      {
        Logger.Default.Log(error);
      }

      Tests.Add(new USetTest(
        CodeGenerator.CreateInjectModulesMethod(component).BodyLines,
        new HashSet<string>()
        {
          "ModuleA.KekDependency = Graphics.ModuleD;",
          "ModuleC.ModuleADependency = ModuleA;",
          "Graphics.ModuleE.ILolDependency = ModuleB;",
        }
      ));
    }
  }
}