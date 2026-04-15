using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Barotrauma;
using Barotrauma.LuaCs;
using Barotrauma.LuaCs.Data;
using System.Runtime.CompilerServices;
using System.IO;
using System.Collections.Concurrent;

namespace BaroJunk
{
  public partial class MappleSettingsAdapter
  {

    public static PluginManagementService PluginManagementService
      => (LuaCsSetup.Instance.PluginManagementService as PluginManagementService);
    public static ConfigService ConfigService
      => (LuaCsSetup.Instance.ConfigService as ConfigService);



    public static ConcurrentDictionary<Type, ContentPackage> PluginPackageLookup
      => PluginManagementService._pluginPackageLookup;

    public static ContentPackage ThisPackage
    {
      get
      {
        foreach (var (pluginType, package) in PluginPackageLookup)
        {
          if (pluginType.Assembly == Assembly.GetExecutingAssembly())
          {
            return package;
          }
        }

        return null;
      }
    }
  }
}