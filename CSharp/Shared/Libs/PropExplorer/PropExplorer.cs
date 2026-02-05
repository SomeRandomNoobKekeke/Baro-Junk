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
  public class PropExplorer
  {
    public static BindingFlags Pls { get; } = BindingFlags.Public | BindingFlags.Instance;

    public static void For<T>(object target, Action<T> action)
    {
      foreach (PropertyInfo pi in target.GetType().GetProperties(Pls))
      {
        if (pi.PropertyType.IsAssignableTo(typeof(T)))
        {
          action((T)pi.GetValue(target));
        }
      }
    }

    public static void ForProps<T>(object target, Action<PropertyInfo> action)
    {
      foreach (PropertyInfo pi in target.GetType().GetProperties(Pls))
      {
        if (pi.PropertyType.IsAssignableTo(typeof(T)))
        {
          action(pi);
        }
      }
    }

    public static void ForProps<T>(Type host, Action<PropertyInfo> action)
    {
      foreach (PropertyInfo pi in host.GetProperties(Pls))
      {
        if (pi.PropertyType.IsAssignableTo(typeof(T)))
        {
          action(pi);
        }
      }
    }


  }
}
