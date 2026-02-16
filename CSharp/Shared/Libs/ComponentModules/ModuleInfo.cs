using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace BaroJunk
{
  public class ModuleInfo
  {
    public static string HostPropName => IModule.HostPropName;

    public List<PropertyInfo> Path { get; }
    public PropertyInfo Property { get; }

    public Type RealType => Property.PropertyType;

    public Type Type { get; }
    public string Name { get; }

    public string StringPath => String.Join('.', Path.Select(pi => pi.Name));

    public PropertyInfo HostProp { get; private set; }

    public List<ModuleDependencyInfo> Dependencies { get; } = new();
    public List<ForwardedPropInfo> ForwardedProps { get; } = new();

    private void ScanDependencies()
    {
      PropExplorer.ForPropsWith<ModuleDependencyAttribute>(RealType, (pi, attribute) =>
      {
        Dependencies.Add(new ModuleDependencyInfo(pi,
          attribute.Type ?? pi.PropertyType,
          attribute.Name ?? pi.Name
        ));
      });
    }

    private void ScanForwarded()
    {
      PropExplorer.ForPropsWith<ForwardedPropAttribute>(RealType, (pi, attribute) =>
      {
        PropAccess CanRead = pi.CanRead ? PropAccess.CanRead : PropAccess.None;
        PropAccess CanWrite = pi.CanWrite ? PropAccess.CanWrite : PropAccess.None;
        PropAccess propAccess = CanRead | CanWrite;

        PropAccess access = attribute.Access == PropAccess.None ? propAccess : attribute.Access;

        ForwardedProps.Add(new ForwardedPropInfo(pi,
          access,
          attribute.Name ?? pi.Name,
          attribute.Type ?? pi.PropertyType
        ));
      });
    }

    private void ScanHostProp()
    {
      HostProp = RealType.GetProperty(HostPropName, PropExplorer.Pls);
    }


    public ModuleInfo(List<PropertyInfo> path, PropertyInfo property, Type typeOverride = null, string nameOverride = null)
    {
      Path = path.Append(property).ToList();
      Property = property;
      Type = typeOverride ?? Property.PropertyType;
      Name = nameOverride ?? Property.Name;

      ScanDependencies();
      ScanHostProp();
      ScanForwarded();
    }

    public override string ToString() => $"{Type.Name} {StringPath}";
  }

}
