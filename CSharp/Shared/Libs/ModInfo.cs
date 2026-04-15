// using System;
// using System.Reflection;
// using System.Diagnostics;
// using System.Collections.Generic;
// using System.Linq;
// using Barotrauma;
// using System.Xml;
// using System.Xml.Linq;
// using System.IO;
// using System.Runtime.CompilerServices;

// namespace BaroJunk
// {

//   /// <summary>
//   /// Static class with some info about package
//   /// Generally a wrapper around this magnificence
//   /// public bool TryGetPackageForPlugin<T>(out ContentPackage package) where T : IAssemblyPlugin
//   /// </summary>
//   public static class ModInfo
//   {
//     public static string AssemblyName => Assembly.GetExecutingAssembly().GetName().Name;
//     public static string HookId => Assembly.GetExecutingAssembly().GetName().Name;
//     public static string BarotraumaPath => Path.GetFullPath("./");


//     public static ContentPackage ModPackage<T>() => ModPackage(typeof(T));
//     public static ContentPackage ModPackage(Type T)
//     {
//       CsPackageManager _ = GameMain.LuaCs.PluginPackageManager;

//       foreach (var (guid, set) in _._pluginTypes)
//       {
//         if (set.Contains(T)) return _._reverseLookupGuidList[guid];
//       }

//       // throw new System.ExecutionEngineException("you died");
//       return null;
//     }

//     public static string ModDir<PluginType>() where PluginType : IAssemblyPlugin => ModDir(typeof(PluginType));
//     public static string ModDir(Type PluginType) => ModPackage(PluginType).Dir;

//     public static string ModVersion<PluginType>() where PluginType : IAssemblyPlugin => ModVersion(typeof(PluginType));
//     public static string ModVersion(Type PluginType) => ModPackage(PluginType).ModVersion;

//     public static string ModName<PluginType>() where PluginType : IAssemblyPlugin => ModName(typeof(PluginType));
//     public static string ModName(Type PluginType) => ModPackage(PluginType).Name;
//   }
// }