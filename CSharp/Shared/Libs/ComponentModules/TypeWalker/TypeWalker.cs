using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace BaroJunk.ComponentModules
{
  public static class TypeWalker
  {
    public static BindingFlags All = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

    public static void WalkProps(Type T, Action<IEnumerable<PropertyInfo>, PropertyInfo> onProp)
    {
      ArgumentNullException.ThrowIfNull(T);
      ArgumentNullException.ThrowIfNull(onProp);

      bool ShouldGoDeeper(Type container, Type PropType)
      {
        return (
          container.IsAssignableTo(typeof(IComponent)) ||
          container.IsAssignableTo(typeof(IModuleContainer))
        ) && (
          PropType.IsAssignableTo(typeof(IModuleContainer)) ||
          PropType.IsAssignableTo(typeof(IModule))
        );
      }

      void WalkRec(Type container, IEnumerable<PropertyInfo> path)
      {
        foreach (PropertyInfo pi in T.GetProperties(All))
        {
          onProp(path, pi);

          if (ShouldGoDeeper(container, pi.PropertyType))
          {
            WalkRec(pi.PropertyType, path.Append(pi));
          }
        }
      }

      WalkRec(T, new List<PropertyInfo>());
    }
  }
}
