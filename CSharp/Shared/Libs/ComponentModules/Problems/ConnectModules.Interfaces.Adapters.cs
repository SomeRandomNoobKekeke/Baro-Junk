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
  /// <summary>
  /// now ModuleA defines interfaces he wants + ModuleB doesn't know that ModuleA even exists
  /// </summary>
  public partial class ConnectModules_Interfaces_Adapters_Problem
  {
    public class ModuleA : IModule
    {
      public interface IPropContainer
      {
        public string Prop { get; }
      }

      [In] public IPropContainer ModuleB { get; set; }
      [Out] public string Prop => ModuleB.Prop;
    }

    public class ModuleB : IModule
    {
      public string Prop { get; set; } = "bruh";
    }

    /// <summary>
    /// Should be generated
    /// </summary>
    public partial class Component
    {
      public class ModuleB_IPropContainer_Adapter : IModule, ModuleA.IPropContainer
      {
        [In] public ModuleB ModuleB { get; set; }
        [Out] public string Prop => ModuleB.Prop;
      }

      private ModuleB_IPropContainer_Adapter ModuleB_As_IPropContainer = new();
    }

    public partial class Component : IComponent
    {
      private ModuleA ModuleA { get; } = new();

      // this should create an adapter
      [ThisIsAlso<ModuleA.IPropContainer>]
      private ModuleB ModuleB { get; } = new();

      public string Prop => ModuleA.Prop;

      /// <summary>
      /// this should be generated
      /// </summary>
      private void Connect()
      {
        ModuleA.ModuleB = ModuleB_As_IPropContainer;
      }

      public Component()
      {
        Connect();
      }
    }

    public void Run()
    {

    }
  }
}