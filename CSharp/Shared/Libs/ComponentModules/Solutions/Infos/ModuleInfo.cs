using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;
using BaroJunk;

namespace BaroJunk.ComponentModules
{
  public class ModuleInfo
  {
    public Type Component { get; }
    public List<PropertyInfo> Path { get; }
    public PropertyInfo Property { get; }
    public Type Type => Property.PropertyType;
    public string Name => Property.Name;
    public string StringPath => string.Join('.', Path.Select(p => p.Name));

    public ModuleInfo(Type componentType, IEnumerable<PropertyInfo> path, PropertyInfo property)
    {
      Component = componentType;
      Property = property;
      Path = path.Append(property).ToList();
    }

    public override string ToString() => $"{Component.Name}.{StringPath}";
  }
}