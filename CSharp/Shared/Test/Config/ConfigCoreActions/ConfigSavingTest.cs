using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;



namespace BaroJunk
{
  public class ConfigSavingTest : ConfigTest
  {
    public override void CreateTests()
    {
      ExampleConfigs.ConfigA config = new();

      FakeIOFacade IOFacade = new FakeIOFacade();

      config.Self().Facades.IOFacade = IOFacade;
      config.Save($"ModSettings\\Configs\\{config.Self().ID}.xml");

      Tests.Add(new UDictTest(IOFacade.Storage, new Dictionary<string, string>()
      {
        [$"ModSettings\\Configs"] = "dir",
        [$"ModSettings\\Configs\\{config.Self().ID}.xml"] = config.ToXML().ToString(),
      }, "config is properly saved"));


      ExampleConfigs.ConfigA loaded = new();
      loaded.Self().Facades.IOFacade = IOFacade;
      loaded.Clear();

      var result = loaded.Load($"ModSettings\\Configs\\{config.Self().ID}.xml");
      Tests.Add(new UTest(config.EqualsTo(loaded), true, "loaded config equals to original"));

      if (!Tests.Last().Passed)
      {
        UTestLogger.Log($"Load result: {result}");
        UTestLogger.Log($"Storage: {Logger.Wrap.IDictionary(IOFacade.Storage)}");
        UTestLogger.Log(config.CompareTo(loaded));
      }
    }

  }
}