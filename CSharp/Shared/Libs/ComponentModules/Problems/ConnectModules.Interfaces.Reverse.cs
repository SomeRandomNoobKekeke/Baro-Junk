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
  /// now ModuleA defines interfaces he wants
  /// </summary>
  public partial class ConnectModules_Interfaces_Reverse_Problem
  {
    public class ModuleA : IModule
    {
      public interface IPropContainer
      {
        public string Prop { get; set; }
      }

      [In] public IPropContainer ModuleB { get; set; }
      [Out] public string Prop => ModuleB.Prop;
    }

    /// <summary>
    /// ModuleB is guessing that it will be used by ModuleA
    /// </summary>
    public class ModuleB : IModule, ModuleA.IPropContainer
    {
      public string Prop { get; set; } = "bruh";
    }

    public class Component : IComponent
    {
      private ModuleA ModuleA { get; } = new();
      private ModuleB ModuleB { get; } = new();

      public string Prop => ModuleA.Prop;

      /// <summary>
      /// this should be generated
      /// </summary>
      private void Connect()
      {
        ModuleA.ModuleB = ModuleB;
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