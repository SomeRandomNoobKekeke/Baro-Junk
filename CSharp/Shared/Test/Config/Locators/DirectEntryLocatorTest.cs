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
    public List<UTest> GetEntry()
    {
      ExampleConfigs.ConfigA config = new();
      DirectEntryLocator locator = new DirectEntryLocator(new ConfiglikeObject(config));

      return new List<UTest>()
      {
        new UTest(locator.GetEntry("IntProp"), new ConfigEntry(new ConfiglikeObject(config),"IntProp")),
        new UTest(
          locator.GetEntry("NestedConfigB.IntProp"),
          new ConfigEntry(new ConfiglikeObject(config.NestedConfigB),"IntProp")
        ),
        new UTest(
          locator.GetEntry("NestedConfigB.NestedConfigC.IntProp"),
          new ConfigEntry(new ConfiglikeObject(config.NestedConfigB.NestedConfigC),"IntProp")
        ),
      };
    }


    // public object GetValue(string propPath);
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