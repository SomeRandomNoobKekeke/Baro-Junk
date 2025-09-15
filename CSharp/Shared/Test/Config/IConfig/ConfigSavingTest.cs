using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{
  public class ConfigSavingTest : UTestPack
  {
    public override void CreateTests()
    {
      ExampleConfigs.ConfigA config = new();

      FakeIOAccess ioAccess = new FakeIOAccess();

      config.Self().IOAccess = ioAccess;
      config.Save($"ModSettings\\Configs\\{config.Self().ID}.xml");



      Tests.Add(new UDictTest(ioAccess.Storage, new Dictionary<string, string>()
      {
        [$"ModSettings\\Configs"] = "dir",
        [$"ModSettings\\Configs\\{config.Self().ID}.xml"] = config.ToXML().ToString(),
      }));


      ExampleConfigs.ConfigA loaded = new();
      loaded.Self().IOAccess = ioAccess;
      // loaded.Clear();

      loaded.Load($"ModSettings\\Configs\\{config.Self().ID}.xml");

      if (!AddTest(new UTest(loaded.Equals(config), true)).Passed)
      {
        IConfig.Logger.Log(config.CompareTo(loaded));
      }


    }

  }
}