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
  public class ModuleInfo
  {
    public List<PropertyInfo> Path { get; }
    public List<PropertyInfo> FullPath { get; }
    public PropertyInfo Property { get; }
    public string Category { get; }
    public ModuleTypeAnalysis TypeAnalysis { get; }

    public Type Type => Property.PropertyType;
    public string Name => Property.Name;

    public string StringPath => String.Join('.', Path.Append(Property).Select(pi => pi.Name));


    public T GetValue<T>(object host) => (T)GetValue(host);
    public object GetValue(object host)
    {
      foreach (PropertyInfo pi in FullPath)
      {
        if (host is null) return null;
        host = pi.GetValue(host);
      }

      return host;
    }

    public ModuleInfo(List<PropertyInfo> path, PropertyInfo property, string category)
    {
      Path = path;
      Property = property;
      FullPath = Path.Append(Property).ToList();
      Category = category ?? ModuleCategoryAttribute.None;
      TypeAnalysis = ModuleTypeAnalysis.For(Type);
    }

    public override string ToString() => $"{Type.Name} {StringPath}";
  }

}
