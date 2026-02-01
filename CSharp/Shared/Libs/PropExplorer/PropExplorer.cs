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
    public static void For<T>(object target, Action<T> action)
    {
      foreach (PropertyInfo pi in target.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
      {
        if (pi.PropertyType.IsAssignableTo(typeof(T)))
        {
          action((T)pi.GetValue(target));
        }
      }
    }


  }
}
