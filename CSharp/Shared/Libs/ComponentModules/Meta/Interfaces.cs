using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace BaroJunk
{
  public interface IModule
  {
    public static string HostPropName = "Host";
  }
  public interface IModuleAccessor
  {

  }

  public interface IModuleContainer { }
  public interface IComponent : IModuleContainer
  {
    public static string GetFilePath(Type T) => T.GetCustomAttribute<GeneratedComponent>()?.FilePath;
    public static ComponentInfo GetInfo(Type T) => ComponentInfo.GetFor(T);

    public string GetFilePath() => GetFilePath(GetType());
    public ComponentInfo GetInfo() => ComponentInfo.GetFor(GetType());
  }
}
