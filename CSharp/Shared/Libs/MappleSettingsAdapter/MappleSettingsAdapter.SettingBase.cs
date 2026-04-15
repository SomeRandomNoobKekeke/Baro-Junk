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
    public ConfigInfo CreateConfigInfo(string name)
    {
      return new ConfigInfo()
      {
        InternalName = name,
        OwnerPackage = Package,
        DataType = "string",
        Element = null,
        EditableStates = RunState.Running,
        NetSync = NetSync.None,

#if CLIENT
        DisplayName = name,
        Description = "who cares",
        DisplayCategory = "CanICreateASettingAlreadyPls",
        ShowInMenus = true,
        Tooltip = "who cares",
        ImageIconPath = new ContentPath(Package, "./Balls.png"),
#endif
      };
    }

    public IEnumerable<SettingBase> Settings
      => ConfigService._settingsInstancesByPackage[Package].Cast<SettingBase>();

    public SettingBase GetSetting(string internalName)
      => ConfigService._settingsInstances[(Package, internalName)] as SettingBase;

    public SettingBase CreateSetting(string name)
    {
      SettingBase setting = new SettingEntry<string>(
        CreateConfigInfo(name),
        (bruh) => true
      );

      ConfigService._settingsInstances[(Package, name)] = setting;
      ConfigService._settingsInstancesByPackage[Package].Add(setting);

      setting.OnValueChanged += (self) => OnPropChanged?.Invoke((self as SettingBase).InternalName, (self as SettingBase).GetStringValue());

      return setting;
    }

    public bool RemoveSetting(string internalName)
    {
      if (!ContainsKey(internalName)) return false;

      SettingBase setting = GetSetting(internalName);

      ConfigService._settingsInstances.TryRemove((Package, internalName), out _);

      // Guess what? you can't remove elements from ConcurrentBag
      ConfigService._settingsInstancesByPackage[Package] = new ConcurrentBag<ISettingBase>(
        ConfigService._settingsInstancesByPackage[Package].Where(s => s != setting)
      );

      setting.Dispose();
      return true;
    }

    public void ClearSettings()
    {
      foreach (string key in Keys)
      {
        ConfigService._settingsInstances.TryRemove((Package, key), out _);
      }

      foreach (SettingBase setting in Settings)
      {
        setting.Dispose(); // i also dont get why pure data type is IDisposable
      }

      ConfigService._settingsInstancesByPackage[Package] = new ConcurrentBag<ISettingBase>();
    }




  }
}