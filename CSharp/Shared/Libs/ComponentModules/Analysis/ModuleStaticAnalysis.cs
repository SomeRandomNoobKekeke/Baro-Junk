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
  public class ModuleStaticAnalysis
  {
    public static bool UseCache = true;
    public static Dictionary<Type, ModuleStaticAnalysis> Cache = new();
    public static ModuleStaticAnalysis For<T>() => For(typeof(T));
    public static ModuleStaticAnalysis For(Type T)
    {
      if (!UseCache) return new ModuleStaticAnalysis(T);

      if (!Cache.ContainsKey(T))
      {
        Cache[T] = new ModuleStaticAnalysis(T);
      }

      return Cache[T];
    }

    public List<ModuleDependencyInfo> Dependencies { get; } = new();




    public ModuleStaticAnalysis(Type T)
    {
      foreach (PropertyInfo pi in T.GetProperties(PropExplorer.Pls))
      {
        ModuleDependencyAttribute attribute = pi.GetCustomAttribute<ModuleDependencyAttribute>();
        if (attribute is null) continue;

        Dependencies.Add(new ModuleDependencyInfo(attribute.Category, attribute.Name, pi));
      }
    }


  }

}
