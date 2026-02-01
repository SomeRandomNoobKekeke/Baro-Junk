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
    public IEnumerable<IModule> Modules(IModuleContainer container)
    {
      IEnumerable<IModule> getSubModules(IModuleContainer container)
      {
        PropExplorer.For<IModuleContainer>(container, (nested) =>
        {
          foreach (IModule subModule in getSubModules(nested))
          {
            yield return subModule;
          }
        });

        PropExplorer.For<IModule>(container, (module) =>
        {
          yield return module;
        });
      }

      return getSubModules(container);
    }
  }

}
