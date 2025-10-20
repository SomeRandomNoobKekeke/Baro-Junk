using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;

namespace BaroJunk
{
  public class ConfigAutoSaverTest : ConfigManagersTest
  {
    public UTest ShouldLoadOnInit()
    {
      ExampleConfigs.ConfigA config = new();
      FakeIOFacade IOFacade = new FakeIOFacade();
      config.Self().Facades.IOFacade = IOFacade;

      IOFacade.Storage[config.GetDefaultSavePath()] = "<ConfigA><NestedConfigB><IntProp>123</IntProp></NestedConfigB></ConfigA>";

      config.UseStrategy(ConfigStrategy.OnlyAutosave);

      return new UTest(config.NestedConfigB.IntProp, 123, "config should be autoloaded when you enable autosaving");
    }

    public UDictTest ShouldSaveOnStop()
    {
      ExampleConfigs.ConfigA config = new();
      FakeIOFacade IOFacade = new FakeIOFacade();
      FakeHooksFacade HooksFacade = new FakeHooksFacade();

      config.Self().Facades.IOFacade = IOFacade;
      config.Self().Facades.HooksFacade = HooksFacade;

      config.UseStrategy(ConfigStrategy.OnlyAutosave);
      IOFacade.Storage.Clear();
      HooksFacade.CallHook("stop");

      return new UDictTest(IOFacade.Storage, new Dictionary<string, string>()
      {
        ["ModSettings\\Configs"] = "dir",
        [config.GetDefaultSavePath()] = config.ToXML().ToString()
      });
    }

    public UTest ShouldTriggerConfigUpdatedOnLoad()
    {
      ExampleConfigs.ConfigA config = new();
      FakeIOFacade IOFacade = new FakeIOFacade();
      config.Self().Facades.IOFacade = IOFacade;

      bool wasTriggered = false;
      config.OnUpdated(() => wasTriggered = true);

      IOFacade.Storage[config.GetDefaultSavePath()] = "<ConfigA><NestedConfigB><IntProp>123</IntProp></NestedConfigB></ConfigA>";

      config.UseStrategy(ConfigStrategy.OnlyAutosave);

      return new UTest(wasTriggered, true, "ConfigUpdated event should be raised when config is loaded");
    }

  }
}