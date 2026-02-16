using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace BaroJunk
{
  public class ComponentInfo
  {
    public static TypeInfoCache<ComponentInfo> Cache = new();
    public static ComponentInfo GetFor<T>() => Cache.Get(typeof(T));
    public static ComponentInfo GetFor(Type T) => Cache.Get(T);

    public Type RootType { get; }
    public string TypeDefPath { get; }

    public Dictionary<Type, Dictionary<string, ModuleInfo>> Modules = new();
    public Dictionary<string, ModuleInfo> ModulesByPath = new();
    public Dictionary<Type, List<ModuleInfo>> ModulesByType = new();

    public IEnumerable<ModuleInfo> AllModules => ModulesByPath.Values;

    private record ScanContext(
      Type ContainerType,
      List<PropertyInfo> Path,
      Type ModuleType = null,
      bool TopLevel = false
    );

    private void AddModule(ModuleInfo moduleInfo)
    {
      if (!Modules.ContainsKey(moduleInfo.Type))
      {
        Modules[moduleInfo.Type] = new Dictionary<string, ModuleInfo>();
      }
      Modules[moduleInfo.Type][moduleInfo.Name] = moduleInfo;

      ModulesByPath[moduleInfo.StringPath] = moduleInfo;

      if (!ModulesByType.ContainsKey(moduleInfo.Type))
      {
        ModulesByType[moduleInfo.Type] = new List<ModuleInfo>();
      }
      ModulesByType[moduleInfo.Type].Add(moduleInfo);
    }

    private void ScanModules()
    {
      void ScanContainer(ScanContext context)
      {
        PropExplorer.ForProps<IModuleContainer>(context.ContainerType, (pi) =>
        {
          ModuleContainerAttribute attribute = pi.GetCustomAttribute<ModuleContainerAttribute>();

          ScanContainer(new ScanContext(
            pi.PropertyType,
            context.Path.Append(pi).ToList(),
            attribute?.Type
          ));
        }, PropExplorer.All);


        PropExplorer.ForProps<IModule>(context.ContainerType, (pi) =>
        {
          ModuleAttribute attribute = pi.GetCustomAttribute<ModuleAttribute>();

          if (context.TopLevel && attribute is null) return;

          AddModule(new ModuleInfo(
            context.Path,
            pi,
            typeOverride: attribute?.Type ?? context.ModuleType,
            nameOverride: attribute?.Name
          ));
        }, PropExplorer.All);
      }

      ScanContainer(new ScanContext(RootType, new List<PropertyInfo>(), TopLevel: true));
    }

    public ComponentInfo(Type T)
    {
      if (!T.IsAssignableTo(typeof(IComponent)))
      {
        throw new ArgumentException($"can't create ComponentInfo for [{T}], it must be an [{typeof(IComponent)}] type");
      }

      RootType = T;
      TypeDefPath = IComponent.GetFilePath(T);
      ScanModules();
    }




    public override string ToString() => Logger.Wrap.IDictionary(ModulesByPath);

    public string DependenciesToText() => Logger.Wrap.IEnumerable(
      ModulesByPath.SelectMany(
        kvp => kvp.Value.Dependencies,
        (kvp, info) => $"{kvp.Key} => {info}"
      ),
      true
    );
  }

}
