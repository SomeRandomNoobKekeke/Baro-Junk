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
  public class PartInfo
  {
    public Type Component { get; }
    public List<PropertyInfo> Path { get; }
    public Type Type { get; }
    public bool IsRoot => Type == Component;
    public string StringPath => string.Join('.', Path.Select(p => p.Name));

    public PartInfo(Type componentType, Type partType, IEnumerable<PropertyInfo> path)
    {
      Component = componentType;
      Type = partType;
      Path = path?.ToList() ?? new List<PropertyInfo>();
    }

    public override bool Equals(object obj)
    {
      if (obj is not PartInfo other) return false;
      return Component == other.Component && Path.SequenceEqual(other.Path);
    }

    public override string ToString()
      => Path.Count == 0 ? Component.Name : $"{Component.Name}.{StringPath}";
  }
}