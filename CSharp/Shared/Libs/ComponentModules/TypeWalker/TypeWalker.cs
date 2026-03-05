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
    public static BindingFlags All = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

    public static IEnumerable<PropInfo> WalkProps<T>() => WalkProps(typeof(T));
    public static IEnumerable<PropInfo> WalkProps(Type T)
    {
      ArgumentNullException.ThrowIfNull(T);

      foreach (PropertyInfo pi in T.GetProperties(All))
      {
        yield return new PropInfo(new List<PropertyInfo>() { }, pi);
      }
    }


    public static IEnumerable<PropInfo> WalkPropsRec<T>() => WalkPropsRec(typeof(T));
    public static IEnumerable<PropInfo> WalkPropsRec(Type T)
    {
      ArgumentNullException.ThrowIfNull(T);

      bool ShouldGoDeeper(Type container, Type prop)
      {
        return (
          container.IsAssignableTo(typeof(IComponent)) ||
          container.IsAssignableTo(typeof(IContainer))
        ) && (
          prop.IsAssignableTo(typeof(IContainer))
        );
      }

      IEnumerable<PropInfo> WalkRec(Type container, IEnumerable<PropertyInfo> path)
      {
        // BreakTheLoop.After(100);

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
