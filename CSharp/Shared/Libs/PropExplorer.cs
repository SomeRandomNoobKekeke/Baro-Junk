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
    public const BindingFlags Pls = BindingFlags.Public | BindingFlags.Instance;
    public const BindingFlags All = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
    public static void ForValues<T>(object target, Action<T> action, BindingFlags flags = BindingFlags.Public | BindingFlags.Instance)
    {
      foreach (PropertyInfo pi in target.GetType().GetProperties(flags))
      {
        if (pi.PropertyType.IsAssignableTo(typeof(T)))
        {
          action((T)pi.GetValue(target));
        }
      }
    }

    public static void ForProps<T>(object target, Action<PropertyInfo> action, BindingFlags flags = Pls)
      => ForProps<T>(target.GetType(), action, flags);

    public static void ForProps<T>(Type host, Action<PropertyInfo> action, BindingFlags flags = Pls)
    {
      foreach (PropertyInfo pi in host.GetProperties(flags))
      {
        if (pi.PropertyType.IsAssignableTo(typeof(T)))
        {
          action(pi);
        }
      }
    }

    public static void ForPropsWith<AttributeT>(Type host, Action<PropertyInfo, AttributeT> action, BindingFlags flags = Pls) where AttributeT : Attribute
    {
      foreach (PropertyInfo pi in host.GetProperties(flags))
      {
        AttributeT attribute = pi.GetCustomAttribute<AttributeT>();

        if (attribute is not null)
        {
          action(pi, attribute);
        }
      }
    }

    public static void ForMethodsWith<AttributeT>(Type host, Action<MethodInfo, AttributeT> action, BindingFlags flags = Pls) where AttributeT : Attribute
    {
      foreach (MethodInfo method in host.GetMethods(flags))
      {
        AttributeT attribute = method.GetCustomAttribute<AttributeT>();

        if (attribute is not null)
        {
          action(method, attribute);
        }
      }
    }


  }
}
