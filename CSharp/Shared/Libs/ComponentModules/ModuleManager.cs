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
    public static BindingFlags Pls { get; } = BindingFlags.Public | BindingFlags.Instance;

    public List<IModule> Modules(IModuleContainer container)
    {
      void getSubModules(IModuleContainer container)
      {
        PropExplorer.For<IModule>(container, (module) =>
        {
          modules.Add(module);
        });

        PropExplorer.For<IModuleContainer>(container, (nested) =>
        {
          getSubModules(nested);
        });
      }

      List<IModule> modules = new();
      getSubModules(container);
      return modules;
    }
  }

}
