using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Text;

using Barotrauma;

namespace BaroJunk
{
  public static class PropAccess
  {
    public static object GetProp(object target, string propPath)
    {
      ConfigEntry entry = GetEntry(target, propPath);
      return entry.Value;
    }

    public static void SetProp(object target, string propPath, object value)
    {
      ConfigEntry entry = GetEntry(target, propPath);
      entry.Value = value;
    }

    public static ConfigEntry GetEntry(object target, string propPath)
    {
      if (target is null || propPath is null) return ConfigEntry.Empty;

      string[] names = propPath.Split('.');
      if (names.Length == 0) return ConfigEntry.Empty;

      object o = target;

      foreach (string name in names.SkipLast(1))
      {
        if (o is null) return ConfigEntry.Empty;
        o = o.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance)?.GetValue(o);
      }

      if (o is null) return ConfigEntry.Empty;

      return new ConfigEntry(o, names.Last());
    }

    public static IEnumerable<ConfigEntry> GetEntries(object target)
      => target?.GetType()
          .GetProperties(BindingFlags.Public | BindingFlags.Instance)
          .Select(pi => new ConfigEntry(target, pi));
  }


}