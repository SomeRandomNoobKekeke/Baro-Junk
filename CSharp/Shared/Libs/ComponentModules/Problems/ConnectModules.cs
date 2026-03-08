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
  public partial class ConnectModules_Problem
  {
    public class ModuleA : IModule
    {
      [In] public ModuleB ModuleB { get; set; }
    }

    public class ModuleB : IModule
    {

    }

    public class Component : IComponent
    {
      private ModuleA ModuleA { get; } = new();
      private ModuleB ModuleB { get; } = new();

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