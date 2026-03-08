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
  /// ModuleA knows it wants ModuleB, but uses an interface just so you could mock it if you want
  /// </summary>
  public partial class ConnectModules_Interfaces_Problem
  {
    public class ModuleA : IModule
    {
      [In] public ModuleB.Like ModuleB { get; set; }
      [Out] public string Prop => ModuleB.Prop;
    }


    public class ModuleB : IModule, ModuleB.Like
    {
      public interface Like
      {
        public string Prop { get; set; }
      }

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
      Component component = new Component();
      Logger.Default.Log(component.Prop);
    }
  }
}