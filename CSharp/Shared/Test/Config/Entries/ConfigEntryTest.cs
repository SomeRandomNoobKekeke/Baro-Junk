using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{
  public class ConfigEntryTest : ConfigTest
  {

    public class ConfigEntryLocatorTest : ConfigEntryTest
    {
      public override void CreateTests()
      {
        ExampleConfigs.ConfigA config = new();

        ConfigEntry configb = new ConfigEntry(new ConfiglikeObject(config), "NestedConfigB");
        ConfigEntry configc = configb.Locator.GetEntry("NestedConfigC");
        ConfigEntry deepIntProp = configc.Locator.GetEntry("IntProp");
        Tests.Add(new UTest(configb.Locator.Host.Target, config.NestedConfigB));
        Tests.Add(new UTest(configc.Locator.Host.Target, config.NestedConfigB.NestedConfigC));
      }
    }

    public class ConfigEntryGarbageInputTest : ConfigEntryTest
    {
      public override void CreateTests()
      {
        ExampleConfigs.ConfigA config = new();
        IConfiglike configlike = new ConfiglikeObject(config);

        Tests.Add(new UTest(new ConfigEntry(null, null), ConfigEntry.Empty));
        Tests.Add(new UTest(new ConfigEntry(configlike, null).IsValid, false));
        Tests.Add(new UTest(new ConfigEntry(configlike, "Bruh").IsValid, false));
        Tests.Add(new UTest(new ConfigEntry(null, "Bruh"), ConfigEntry.Empty));
      }
    }

    public override void CreateTests()
    {
      ExampleConfigs.ConfigA config = new();

      ConfigEntry entry1 = new ConfigEntry(new ConfiglikeObject(config), "IntProp");
      ConfigEntry entry2 = new ConfigEntry(new ConfiglikeObject(config.NestedConfigB), "IntProp");
      ConfigEntry entry3 = new ConfigEntry(new ConfiglikeObject(config.NestedConfigB.NestedConfigC), "IntProp");
      ConfigEntry entry4 = new ConfigEntry(new ConfiglikeObject(config.NestedConfigB), "NestedNullConfigC");
      ConfigEntry entry5 = new ConfigEntry(new ConfiglikeObject(config), "NestedConfigB").Locator.GetEntry("FloatProp");

      Tests.Add(new UTest(entry1.Value, 2, "Direct prop"));
      Tests.Add(new UTest(entry2.Value, 4, "Nested prop"));
      Tests.Add(new UTest(entry3.Value, 6, "Deeply nested prop"));
      Tests.Add(new UTest(entry4.Value, null, "nested object prop"));
      Tests.Add(new UTest(entry5.Value, 5.0f, "entry made from another entry"));
      entry1.Value = 3;
      entry2.Value = 5;
      entry3.Value = 7;
      entry4.Value = new ExampleConfigs.ConfigC();
      entry5.Value = 6.0f;
      Tests.Add(new UTest(entry1.Value, 3));
      Tests.Add(new UTest(entry2.Value, 5));
      Tests.Add(new UTest(entry3.Value, 7));
      Tests.Add(new UTest(entry4.Value is not null, true));
      Tests.Add(new UTest(entry5.Value, 6.0f));

      Tests.Add(new UTest(config.IntProp, 3));
      Tests.Add(new UTest(config.NestedConfigB.IntProp, 5));
      Tests.Add(new UTest(config.NestedConfigB.NestedConfigC.IntProp, 7));
      Tests.Add(new UTest(config.NestedConfigB.NestedNullConfigC is not null, true));
      Tests.Add(new UTest(config.NestedConfigB.FloatProp, 6.0f));
    }



  }
}