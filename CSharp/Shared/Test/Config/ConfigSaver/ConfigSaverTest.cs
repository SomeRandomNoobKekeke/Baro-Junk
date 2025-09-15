using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{
  public class ConfigSaverTest : UTestPack
  {
    public override void CreateTests()
    {
      ExampleConfigs.ConfigA config = new();

      FakeIOAdapter adapter = new FakeIOAdapter();
      ConfigSaver saver = new ConfigSaver(adapter);
      saver.Config = config;

      saver.Save();
      Tests.Add(new UDictTest(adapter.Storage, new Dictionary<string, string>()
      {
        [$"ModSettings\\Configs"] = "dir",
        [$"ModSettings\\Configs\\{config.Self().ID}.xml"] = config.ToXML().ToString(),
      }));


      ExampleConfigs.ConfigA loaded = new();
      loaded.Clear();
      saver.Config = loaded;
      saver.Load();

      IConfig.Logger.Log(loaded.CompareTo(config));

      Tests.Add(new UTest(loaded.Equals(config), true));
    }

  }
}