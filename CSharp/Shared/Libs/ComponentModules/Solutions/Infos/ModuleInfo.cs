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

    //TODO Target could be a path too
    public record ModuleRequest(Type Type, ModuleInfo Module, PropertyInfo Target);
    private void Analyze()
    {
      RequiredModules = new();
      foreach (PropertyInfo pi in Type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
      {
        if (pi.GetCustomAttribute<InAttribute>() != null)
        {
          RequiredModules.Add(new ModuleRequest(pi.PropertyType, this, pi));
        }
      }

      CanBeUsedAs = new List<Type>();
      CanBeUsedAs.Add(Type);
      CanBeUsedAs.AddRange(Type.GetInterfaces().Where(i => i != typeof(IModule)));
      //TODO add base classes?
    }

    //TODO propbably should be IEnumerable, they are also stored in ComponentInfo
    public List<ModuleRequest> RequiredModules { get; private set; }
    public List<Type> CanBeUsedAs { get; private set; }


    public ModuleInfo(Type componentType, IEnumerable<PropertyInfo> path, PropertyInfo property)
    {
      Component = componentType;
      Property = property;
      Path = path.Append(property).ToList();

      Analyze();
    }

    public override string ToString() => $"{Component.Name}.{StringPath}";
  }
}