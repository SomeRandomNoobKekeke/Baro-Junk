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
        foreach (PropertyInfo pi in container.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
          if (pi.PropertyType.IsAssignableTo(typeof(IModuleContainer)))
          {
            foreach (IModule subModule in getSubModules((IModuleContainer)pi.GetValue(container)))
            {
              yield return subModule;
            }
          }

          if (pi.PropertyType.IsAssignableTo(typeof(IModule)))
          {
            yield return (IModule)pi.GetValue(container);
          }
        }
      }

      return getSubModules(container);
    }
  }

}
