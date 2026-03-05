using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace BaroJunk.ComponentModules
{
  public class PropInfo
  {
    public List<PropertyInfo> Path { get; set; }
    public PropertyInfo Property { get; set; }
    public string StringPath => string.Join(".", Path.Select(p => p.Name));
    public Type Type => Property.PropertyType;
    public string Name => Property.Name;

    public PropInfo(IEnumerable<PropertyInfo> path, PropertyInfo property)
    {
      Path = path.Append(property).ToList();
      Property = property;
    }

    public override bool Equals(object obj)
    {
      if (obj is not PropInfo other) return false;
      return Property.Equals(other.Property);
    }
  }
}
