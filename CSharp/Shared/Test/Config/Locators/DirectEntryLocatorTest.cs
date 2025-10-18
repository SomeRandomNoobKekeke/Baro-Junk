using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

using Barotrauma;


namespace BaroJunk
{
  public class DirectEntryLocatorTest : ConfigTest
  {
    public List<UTest> GetEntryTest()
    {
      ExampleConfigs.ConfigA config = new();
      DirectEntryLocator locator = new DirectEntryLocator(new ConfiglikeObject(config));

      return new List<UTest>()
      {
        new UTest(
          locator.GetEntry("IntProp"),
          new ConfigEntry(new ConfiglikeObject(config),"IntProp")
        ),
        new UTest(
          locator.GetEntry("NestedConfigB.IntProp"),
          new ConfigEntry(new ConfiglikeObject(config.NestedConfigB),"IntProp")
        ),
        new UTest(
          locator.GetEntry("NestedConfigB.NestedConfigC.IntProp"),
          new ConfigEntry(new ConfiglikeObject(config.NestedConfigB.NestedConfigC),"IntProp")
        ),
        new UTest(
          locator.GetEntry(""),
          new ConfigEntry(new ConfiglikeObject(config),"")
        ),
        new UTest(
          locator.GetEntry(null),
          new ConfigEntry(new ConfiglikeObject(config), null)
        ),
        new UTest(
          locator.GetEntry("Bruh"),
          new ConfigEntry(new ConfiglikeObject(config), "Bruh")
        ),
        new UTest(
          locator.GetEntry("NestedConfigB.Bruh"),
          new ConfigEntry(new ConfiglikeObject(config.NestedConfigB), "Bruh")
        ),
        new UTest(
          locator.GetEntry("NestedConfigB..Bruh"),
          ConfigEntry.Empty
        ),
        new UTest(
          locator.GetEntry("...Bruh"),
          ConfigEntry.Empty
        ),
        new UTest(
          locator.GetEntry("NestedConfigB.    .Bruh"),
          ConfigEntry.Empty
        ),
        new UTest(
          locator.GetEntry("    .     .     .       "),
          ConfigEntry.Empty
        ),
      };
    }


    public List<UTest> GetValueTest()
    {
      ExampleConfigs.ConfigA config = new();
      DirectEntryLocator locator = new DirectEntryLocator(new ConfiglikeObject(config));

      return new List<UTest>()
      {
        new UTest(locator.GetValue("IntProp"), 2),
        new UTest(locator.GetValue("NestedConfigB"), config.NestedConfigB),
        new UTest(locator.GetValue("Bruh"), null),
        new UTest(locator.GetValue(null), null),
        new UTest(locator.GetValue(""), null),
      };
    }

    public List<UTest> SetValueTest()
    {
      ExampleConfigs.ConfigA config = new();
      DirectEntryLocator locator = new DirectEntryLocator(new ConfiglikeObject(config));

      // SetValue also returns success status
      return new List<UTest>()
      {
        new UTest(locator.SetValue("IntProp", 3), true),
        new UTest(locator.SetValue("NestedNullConfigB", new ExampleConfigs.ConfigB()), true),
        new UTest(locator.SetValue("NestedConfigB.IntProp", 5), true),

        new UTest(config.IntProp, 3),
        new UTest(config.NestedNullConfigB is not null, true),
        new UTest(config.NestedConfigB.IntProp, 5),

        new UTest(locator.SetValue("Bruh", 123), false),
        new UTest(locator.SetValue("", 123), false),
        new UTest(locator.SetValue(null, 123), false),
        new UTest(locator.SetValue("FloatProp", "100px"), false),
      };
    }

    // public void SetValue(string propPath, object value);
    // public IEnumerable<ConfigEntry> GetEntries();
    // public IEnumerable<ConfigEntry> GetAllEntries();
    // public IEnumerable<ConfigEntry> GetEntriesRec();
    // public IEnumerable<ConfigEntry> GetAllEntriesRec();
    // public Dictionary<string, ConfigEntry> GetFlat();
    // public Dictionary<string, ConfigEntry> GetAllFlat();
    // public Dictionary<string, object> GetFlatValues();
    // public Dictionary<string, object> GetAllFlatValues();
  }
}