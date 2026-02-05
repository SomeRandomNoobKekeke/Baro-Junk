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
  public class ComponentStaticAnalysis
  {
    public static bool UseCache = true;

    public static Dictionary<Type, ComponentStaticAnalysis> Cache = new();
    public static ComponentStaticAnalysis For<T>() => For(typeof(T));
    public static ComponentStaticAnalysis For(Type T)
    {
      if (!UseCache) return new ComponentStaticAnalysis(T);

      if (!Cache.ContainsKey(T))
      {
        Cache[T] = new ComponentStaticAnalysis(T);
      }

      return Cache[T];
    }

    public static string GetModuleCategory(PropertyInfo pi)
         => pi.GetCustomAttribute<ModuleCategoryAttribute>()?.Category;

    public static string GetModuleCategory(Type T)
         => T.GetCustomAttribute<ModuleCategoryAttribute>()?.Category;

    public static List<ModuleInfo> GetModules(Type T)
    {
      List<ModuleInfo> modules = new();

      void AnalyzeContainer(Type containerType, List<PropertyInfo> path, string category)
      {
        PropExplorer.ForProps<IModuleContainer>(containerType, (pi) =>
        {
          AnalyzeContainer(
            pi.PropertyType,
            path.Append(pi).ToList(),
            GetModuleCategory(pi) ?? GetModuleCategory(pi.PropertyType) ?? category
          );
        });

        PropExplorer.ForProps<IModule>(containerType, (pi) =>
        {
          modules.Add(new ModuleInfo(path, pi, GetModuleCategory(pi) ?? GetModuleCategory(pi.PropertyType) ?? category));
        });
      }

      AnalyzeContainer(T, new List<PropertyInfo>(), null);

      return modules;
    }



    public List<ModuleInfo> AllModules { get; } = new();
    public Dictionary<string, Dictionary<string, ModuleInfo>> Modules = new();


    public T GetModule<T>(object host, string name, string category = ModuleCategoryAttribute.None)
      => (T)GetModule(host, name, category);
    public object GetModule(object host, string name, string category = ModuleCategoryAttribute.None)
    {
      if (!Modules.ContainsKey(category)) return null;
      if (!Modules[category].ContainsKey(name)) return null;
      return Modules[category][name].GetValue(host);
    }

    public ComponentAnalysis(Type T)
    {
      if (!T.IsAssignableTo(typeof(IComponent))) throw new ArgumentException("Its not a component");

      AllModules = GetModules(T);

      Modules = AllModules.GroupBy(m => m.Category).ToDictionary(
        cat => cat.Key,
        cat => cat.GroupBy(m => m.Name).ToDictionary(
          names => names.Key,
          names => names.First() //TODO detect collisions
        )
      );
    }

    public override string ToString() => Logger.Wrap.IEnumerable(Modules.Select(cat => $"{cat.Key}: [{String.Join(", ", cat.Value.Keys)}]"));
  }

}
