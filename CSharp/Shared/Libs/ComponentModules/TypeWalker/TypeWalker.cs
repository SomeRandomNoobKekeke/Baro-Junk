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

    public static IEnumerable<PropInfo> WalkProps<T>() => WalkProps(typeof(T));
    public static IEnumerable<PropInfo> WalkProps(Type T)
    {
      ArgumentNullException.ThrowIfNull(T);

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

      IEnumerable<PropInfo> WalkRec(Type container, IEnumerable<PropertyInfo> path)
      {
        Logger.Default.Log($"walking [{container}]");
        BreakTheLoop.After(100);

        foreach (PropertyInfo pi in container.GetProperties(All))
        {
          if (ShouldGoDeeper(container, pi.PropertyType))
          {
            foreach (PropInfo info in WalkRec(pi.PropertyType, path.Append(pi)))
            {
              yield return info;
            }
          }
          else
          {
            yield return new PropInfo(path, pi);
          }
        }
      }

      foreach (PropInfo info in WalkRec(T, new List<PropertyInfo>()))
      {
        yield return info;
      }
    }
  }
}
