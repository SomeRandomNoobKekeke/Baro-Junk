using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Barotrauma;
using Microsoft.Xna.Framework;
using System.IO;
using System.Text;

namespace BaroJunk
{

  public class ModuleManager
  {
    public static Logger Logger { get; } = new Logger()
    {

    };

    public static void InjectModules(IComponent host)
    {
      ModuleManager manager = new ModuleManager(host);
      manager.InjectAll();
    }

    public IComponent Host { get; }
    public ModuleInjector Injector { get; }
    public ModuleMap Map { get; }
    public ModuleMapAnalizer MapAnalizer { get; }

    public void InjectAll()
    {
      Injector.InjectHosts(Host, MapAnalizer.CreateInjectHostInstructions(Map));
      Injector.InjectDependencies(Host, MapAnalizer.CreateInjectDependencyInstructions(Map));
    }

    public ModuleManager(IComponent host)
    {
      Host = host;
      Injector = new ModuleInjector();
      MapAnalizer = new ModuleMapAnalizer();
      Map = ModuleMap.GetFor(host.GetType());
    }
  }


}
