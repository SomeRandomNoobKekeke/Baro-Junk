using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{

  public class MappleSettingsAdapterTest : UTestPack
  {

    public class NormalConfig : IConfig
    {
      public string SomeProp { get; set; } = "bruh";


      public void SyncForward(MappleSettingsAdapter settings)
      {
        foreach (var (path, entry) in this.GetFlat())
        {
          settings[path] = entry.Value.ToString();
        }
      }

      public void SyncBack(MappleSettingsAdapter settings)
      {
        foreach (var (path, entry) in this.GetFlat())
        {
          entry.SetValue(SimpleParser.Default.Parse(settings[path], entry.Type));
        }
      }
    }


    public override void CreateTests()
    {
      NormalConfig normalConfig = new();
      MappleSettingsAdapter settings = new MappleSettingsAdapter();

      settings.OnPropChanged += (key, value) =>
      {
        ConfigEntry entry = normalConfig.GetEntry(key);
        entry.SetValue(SimpleParser.Default.Parse(value, entry.Type));
        Logger.Default.Log($"setting set {key} {value}");
      };

      normalConfig.SyncForward(settings);
    }
  }
}