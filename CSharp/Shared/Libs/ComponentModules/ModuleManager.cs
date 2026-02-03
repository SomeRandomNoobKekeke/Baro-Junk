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
  public record ModuleEntry(object Host, PropertyInfo Property)
  {
    public IModule Value
    {
      get => (IModule)Property.GetValue(Host);
      set => Property.SetValue(Host, value);
    }
  }

  public class ModuleManager
  {


    public List<IModule> Modules(IModuleContainer container)
    {
      List<IModule> modules = new();

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

      getSubModules(container);
      return modules;
    }

    private void CreateModule(object container, PropertyInfo pi)
    {
      if (pi.GetValue(container) is not null) return;

      pi.SetValue(container, Activator.CreateInstance(pi.PropertyType));
    }

    public void CreateModules(object container)
    {
      PropExplorer.ForProps<IModule>(container, (pi) => CreateModule(container, pi));
    }
  }

}
