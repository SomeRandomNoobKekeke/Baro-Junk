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

    public class A : IModule
    {

    }

    public class B : IModule
    {

    }

    public class C : IModule
    {

    }



    public class Component : IComponent
    {
      public class PropWrapper : IModuleContainer
      {
        public A A { get; set; } = new();
        public B B { get; set; } = new();
        public C C { get; set; } = new();
      }

      public PropWrapper Props { get; set; } = new();

      public A A { get; set; } = new();
      public B B { get; set; } = new();
      public C C { get; set; } = new();
    }

    public UListTest ModuleDiscovery()
    {
      Component component = new();
      ModuleManager manager = new();
      return new UListTest(manager.Modules(component), new List<IModule>{
        component.A,
        component.B,
        component.C,
        component.Props.A,
        component.Props.B,
        component.Props.C,
      });
    }


    // public override void CreateTests()
    // {

    // }
  }
}