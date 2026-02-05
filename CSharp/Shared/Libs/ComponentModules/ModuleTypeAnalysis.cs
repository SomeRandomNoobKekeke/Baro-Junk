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
  public class ModuleTypeAnalysis
  {
    public static bool UseCache = true;
    public static Dictionary<Type, ModuleTypeAnalysis> Cache = new();
    public static ModuleTypeAnalysis For<T>() => For(typeof(T));
    public static ModuleTypeAnalysis For(Type T)
    {
      if (!UseCache) return new ModuleTypeAnalysis(T);

      if (!Cache.ContainsKey(T))
      {
        Cache[T] = new ModuleTypeAnalysis(T);
      }

      return Cache[T];
    }

    public List<ModuleDependency> Dependencies { get; } = new();




    public ModuleTypeAnalysis(Type T)
    {
      foreach (PropertyInfo pi in T.GetProperties(PropExplorer.Pls))
      {
        ModuleDependencyAttribute attribute = pi.GetCustomAttribute<ModuleDependencyAttribute>();
        if (attribute is null) continue;

        Dependencies.Add(new ModuleDependency(attribute.Category, attribute.Name, pi));
      }
    }


  }

}
